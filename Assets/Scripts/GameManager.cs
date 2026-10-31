using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] goalList;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private GameObject player;
    
    private Ball ball;

    public void Kick(Ball currentBall)
    {
        ball = currentBall;

        ball.BallReachedGoal += HandleBallReached;

        Vector3 nearestBallPosition = FindNearestGoal(currentBall);

        ball.Toward(nearestBallPosition);
        
        virtualCamera.Follow = currentBall.gameObject.transform;
    }

    private Vector3 FindNearestGoal(Ball currentBall)
    {
        Vector3 distance1 = goalList[0].transform.position - currentBall.transform.position;
        Vector3 distance2 = goalList[1].transform.position - currentBall.transform.position;

        if(distance1.sqrMagnitude <= distance2.sqrMagnitude)
        {
            return goalList[0].transform.position;
        } else
        {
            return goalList[1].transform.position;
        }
    }

    private IEnumerator ReturnToPlayer()
    {
        yield return new WaitForSeconds(2);
        virtualCamera.Follow = player.transform;
    }

    private void HandleBallReached()
    {
        ball.BallReachedGoal -= HandleBallReached;
        StartCoroutine(ReturnToPlayer());
    }
}
