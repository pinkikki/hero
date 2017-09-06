using script.core.@event;
using UnityEngine;

namespace script.core.character
{
    public class MainCharacterController : CharacterBase
    {
        private float factorNum = 0.065f;

        public float FactorNum
        {
            get { return factorNum; }
            protected set { factorNum = value; }
        }

        void Start()
        {
            Anim = gameObject.GetComponent<Animator>();
        }

        protected void FixedUpdate()
        {
            if (!FreezeFlg && !EventManager.Instance.IsMessageing())
            {
                if (!WalkingFlg)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
                    {
                        WalkBack();
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
                    {
                        WalkFront();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
                    {
                        WalkLeft();
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
                    {
                        WalkRight();
                    }
                }

                if (!collisionFlg)
                {
                    if (WalkingFlg)
                    {
                        if (Input.GetKey(KeyCode.UpArrow) ||
                            Input.GetKey(KeyCode.DownArrow) ||
                            Input.GetKey(KeyCode.LeftArrow) ||
                            Input.GetKey(KeyCode.RightArrow))
                        {
                            Vector3 pos = gameObject.transform.position;
                            pos.x += hSpeed * factorNum;
                            pos.y += vSpeed * factorNum;
                            gameObject.transform.position = pos;
                        }
                        else
                        {
                            WalkStop();
                        }
                    }
                }
                else
                {
                    WalkStop();
                }
            }
        }

        void Update()
        {
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer != 10)
            {
                collisionFlg = true;
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            collisionFlg = false;
        }
    }
}
