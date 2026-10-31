using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] goalList;
    
    private Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kick(Ball currentBall)
    {
        ball = currentBall;

        Vector3 nearestBallPosition = FindNearestGoal(currentBall);

        ball.Toward(nearestBallPosition);
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
}
