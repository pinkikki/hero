using script.core.@event;
using UnityEngine;

namespace script.core.character
{
    public class MainCharacterController : CharacterBase
    {
        void Start()
        {
            Anim = gameObject.GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            if (!FreezeFlg && !EventManager.Instance.IsMessageing())
            {
                if (!WarlkingFlg)
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
                    if (WarlkingFlg)
                    {
                        if (Input.GetKey(KeyCode.UpArrow) ||
                            Input.GetKey(KeyCode.DownArrow) ||
                            Input.GetKey(KeyCode.LeftArrow) ||
                            Input.GetKey(KeyCode.RightArrow))
                        {
                            Vector3 pos = gameObject.transform.position;
                            pos.x += hSpeed * 0.065f;
                            pos.y += vSpeed * 0.065f;
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
            // TODO にわとりのレイヤーはこのフラグはtrueにしないようにすること！！
            collisionFlg = true;
        }

        void OnCollisionExit2D(Collision2D other)
        {
            collisionFlg = false;
        }
    }
}
