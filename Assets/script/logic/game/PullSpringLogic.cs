using UnityEngine;

namespace script.logic.game
{
    public class PullSpringLogic : MonoBehaviour
    {
        [SerializeField] float distance = 1f;
        [SerializeField] float speed = 2.0f;
        [SerializeField] float power = 1200;
        GameObject ball;
        bool ready;
        bool fire;
        float moveCount;

        void Start()
        {
            ball = GameObject.Find("ball");
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.name == "ball")
            {
                ready = true;
            }
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (moveCount < distance)
                {
                    transform.Translate(0, -speed * Time.deltaTime, 0);
                    moveCount += speed * Time.deltaTime;
                    fire = true;
                }
            }
            else if (moveCount > 0)
            {
                if (fire && ready)
                {
                    ball.GetComponent<Rigidbody>().AddForce(0, moveCount * power, 0);
                    fire = false;
                    ready = false;
                }
                transform.Translate(0, 8 * Time.deltaTime, 0);
                moveCount -= 8 * Time.deltaTime;
            }

            if (!(moveCount <= 0)) return;
            ready = false;
            moveCount = 0;
        }
    }
}