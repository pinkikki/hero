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

        private Vector2 endPos = Vector2.zero;
        private Vector2 currentEndPos = Vector2.zero;
        private static readonly float startUp = 15.0f;
        

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

                    if (0 < Input.touchCount)
                    {
                        var t = Input.touches[0];
                        Debug.Log(t.phase);
                        switch (t.phase)
                        {
                            case TouchPhase.Began:
                                WalkStop();
                                currentEndPos = t.position;
                                break;
                            case TouchPhase.Canceled:
                                WalkStop();
                                break;
                            case TouchPhase.Ended:
                                WalkStop();
                                break;
                            case TouchPhase.Moved:
                                endPos = t.position;
                                var absX = Mathf.Abs(endPos.x - currentEndPos.x);
                                var absY = Mathf.Abs(endPos.y - currentEndPos.y);


                                if (startUp < absX || startUp < absY)
                                {
                                    if (absX > absY)
                                    {
                                        if (endPos.x > currentEndPos.x)
                                        {
                                            WalkRight();
                                        }
                                        else
                                        {
                                            WalkLeft();
                                        }
                                    }
                                    else
                                    {
                                        if (endPos.y > currentEndPos.y)
                                        {
                                            WalkBack();
                                        }
                                        else
                                        {
                                            WalkFront();
                                        }
                                    }
                                }
                                else
                                {
                                    WalkingFlg = true;
                                }
                                break;
                            case TouchPhase.Stationary:
                                WalkingFlg = true;
                                break;
                        }
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
                            var pos = gameObject.transform.position;
                            pos.x += hSpeed * factorNum;
                            pos.y += vSpeed * factorNum;
                            gameObject.transform.position = pos;
                        }
                        else if (0 < Input.touchCount)
                        {
                            var touch = Input.touches[0];
                            if (touch.phase == TouchPhase.Moved ||
                                touch.phase == TouchPhase.Stationary)
                            {
                                currentEndPos = touch.position;
                                WalkingFlg = false;
                                var pos = gameObject.transform.position;
                                pos.x += hSpeed * factorNum;
                                pos.y += vSpeed * factorNum;
                                gameObject.transform.position = pos;
                            }
                            else
                            {
                                WalkStop();
                            }
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
                // 物を押す時に弊害が出るためコメントアウト
//                collisionFlg = true;
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            collisionFlg = false;
        }
    }
}