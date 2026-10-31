using System;
using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] goalList;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private Ball[] ballList;

    private bool _isKickProgress = false;
    private Coroutine returnCoroutine;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public bool Kick(Ball currentBall)
    {
        if(_isKickProgress || currentBall == null)
        {
            return false;
        }

        currentBall.BallReachedGoal += HandleBallReached;

        Vector3 nearestBallPosition = FindNearestGoal(currentBall);

        Physics.IgnoreCollision(
            currentBall.GetComponent<SphereCollider>(),
            player.GetComponent<BoxCollider>(),
            true
        );
        currentBall.Toward(nearestBallPosition);
        
        virtualCamera.Follow = currentBall.gameObject.transform;

        _isKickProgress = true;

        returnCoroutine = StartCoroutine(ReturnToPlayer(5, currentBall));

        return true;
    }

    private Vector3 FindNearestGoal(Ball currentBall)
    {
        float minDistance = float.MaxValue;
        GameObject nearestGoal = null;

        foreach(GameObject goal in goalList)
        {
            if(goal == null) continue;

            float distance = (goal.transform.position - currentBall.transform.position).sqrMagnitude;

            if(distance <= minDistance)
            {
                minDistance = distance;
                nearestGoal = goal;
            }
        }

        return nearestGoal.transform.position;
    }

    private IEnumerator ReturnToPlayer(float second, Ball currentBall)
    {
        yield return new WaitForSeconds(second);
        currentBall.BallReachedGoal -= HandleBallReached;
        virtualCamera.Follow = player.transform;
        _isKickProgress = false;
        Physics.IgnoreCollision(
            currentBall.GetComponent<SphereCollider>(),
            player.GetComponent<BoxCollider>(),
            false
        );
    }

    private void HandleBallReached(Ball currentBall)
    {
        currentBall.BallReachedGoal -= HandleBallReached;
        StopCoroutine(returnCoroutine);
        returnCoroutine = StartCoroutine(ReturnToPlayer(2, currentBall));
    }

    public void Restart()
    {
        SceneManager.LoadScene("Location soccer field");
    }

    public void AutoKick()
    {
        Ball farthestBall = FindFarthestBall();

        Kick(farthestBall);
    }

    private Ball FindFarthestBall()
    {
        float maxDistance = 0f;
        Ball farthestBall = null;

        foreach(Ball ball in ballList)
        {
            if(ball == null) continue;

            float distance = (ball.transform.position - player.transform.position).sqrMagnitude;

            if(distance >= maxDistance)
            {
                maxDistance = distance;
                farthestBall = ball;
            }
        }

        return farthestBall;
    }
}
