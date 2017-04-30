using UnityEngine;

namespace script.logic.smartball
{
    public class SmartBallLogic : MonoBehaviour
    {
        [SerializeField] GameObject ball;
        void Start()
        {
        }

        void Update()
        {
            // 左クリックがおされたら
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 ball_vec = new Vector3(
                    0.0f,
                    15.0f,
                    0.0f
                );

                // ボール生成時に速度を持たせる
                ball.GetComponent<Rigidbody>().velocity = ball_vec;
            }
        }
    }
}
