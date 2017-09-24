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

        private Vector2 beginPos = Vector2.zero;
        private Vector2 endPos = Vector2.zero;
        private static readonly float startUp = 30.0f;
        

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
                        switch (t.phase)
                        {
                            case TouchPhase.Began:
                                beginPos = t.position;
                                break;
                            case TouchPhase.Canceled:
                                break;
                            case TouchPhase.Ended:
                                break;
                            case TouchPhase.Moved:
                                endPos = t.position;
                                var absX = Mathf.Abs(endPos.x - beginPos.x);
                                var absY = Mathf.Abs(endPos.y - beginPos.y);

                                if (startUp < absX || startUp < absY)
                                {
                                    if (absX > absY)
                                    {
                                        if (endPos.x > beginPos.x)
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
                                        if (endPos.y > beginPos.y)
                                        {
                                            WalkBack();
                                        }
                                        else
                                        {
                                            WalkFront();
                                        }
                                    }
                                }
                                break;
                            case TouchPhase.Stationary:
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
                                var currentEndPos = touch.position;
                                var absX = Mathf.Abs(currentEndPos.x - endPos.x);
                                var absY = Mathf.Abs(currentEndPos.y - endPos.y);
                                if (startUp < absX || startUp < absY)
                                {
                                    endPos = currentEndPos;
                                    WalkingFlg = false;
                                }
                                
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
                collisionFlg = true;
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            collisionFlg = false;
        }
    }
}