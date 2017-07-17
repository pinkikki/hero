using UnityEngine;

namespace script.logic.game
{
    public class SmartBallLogic : MonoBehaviour
    {
        GameObject ball;
        GameObject bar;
        Vector3 ballPos;
        Vector3 barPos;
        
        void Start()
        {
            ball = GameObject.Find("ball");
            ballPos = ball.transform.position;
            bar = GameObject.Find("bar");
            barPos = bar.transform.position;
        }

        void Update()
        {
        }

        public void Restart()
        {
            SetPosition(bar, barPos);
            SetPosition(ball, ballPos);
        }

        void SetPosition(GameObject obj, Vector3 vec)
        {
            var pos = obj.transform.position;
            pos = vec;
            ball.transform.position = pos;
        }
    }
}
