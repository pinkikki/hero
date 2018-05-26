using UnityEngine;
using UnityEngine.SceneManagement;

namespace script.core.character
{
    public class VChaseCharacterController : ChaseCharacterController
    {
        [SerializeField] float defaultWalkSpeed = 0.065f;
        [SerializeField] float defaultCollisionWalkSpeed = 0.04f;
        [SerializeField] float defaultCatchUpWalkSpeed = 0.12f;
        [SerializeField] float maxDestNum;
        [SerializeField] float minDestNum;

        public float MaxDestNum
        {
            get { return maxDestNum; }
            set { maxDestNum = value; }
        }
        
        public float MinDestNum
        {
            get { return minDestNum; }
            set { minDestNum = value; }
        }

        float destX;
        float destY;
        float walkSpeed;
        bool hFlg;
        bool vFlg;

        bool isPreSwitchingX;

        // TODO 一旦初期値設定
        bool isPreSwitchingY = true;

        bool isWaitF;
        bool isWaitB;
        bool isWaitL;
        bool isWaitR;


        bool selfStopFlg;

        void Start()
        {
            Anim = gameObject.GetComponent<Animator>();
            walkSpeed = defaultWalkSpeed;
        }

        private int specialCount;
        int currentRepeatNum;
        int repeatNum = 20;
        int type;
        float speedFactor = 0.05f;
        void Walk()
        {
            switch (type)
            {
                case 0:
                    WalkBack();
                    break;
                case 1:
                    WalkFront();
                    break;
                case 2:
                    WalkLeft();
                    break;
                case 3:
                    WalkRight();
                    break;
            }
        }
        void FixedUpdate()
        {
            SetDirectionInfo();

            var targetPos = Target.transform.position;
            var selfPos = gameObject.transform.position;

            var targetX = targetPos.x;
            var targetY = targetPos.y;
            var selfX = selfPos.x;
            var selfY = selfPos.y;


            var absX = Mathf.Abs(targetX - selfX);
            var absY = Mathf.Abs(targetY - selfY);

            if (SceneManager.GetActiveScene().name != "classroom" &&
                SceneManager.GetActiveScene().name != "grassy")
            {
                if (0 < specialCount)
                {
                    specialCount -= 1;
                    if (!FreezeFlg)
                    {
                        currentRepeatNum++;
                        if (collisionFlg || currentRepeatNum > repeatNum)
                        {
                            type = Random.Range(0, 4);
                            currentRepeatNum = 0;
                        }
    
                        Walk();
    
                        Vector3 pos = gameObject.transform.position;
                        pos.x += hSpeed * speedFactor;
                        pos.y += vSpeed * speedFactor;
                        gameObject.transform.position = pos;
                    }
                    return;
                }
                var ran = Random.Range(0, 3000);
                if (5 == ran)
                {
                    specialCount = 200;
                    return;
                }
            }

            if (!Mathf.Approximately(absX, destX) && absX > destX)
            {
                MoveX(absX, selfX, targetX, selfPos);
                return;
            }
            if (!Mathf.Approximately(absY, destY) && absY > destY)
            {
                MoveY(absY, selfY, targetY, selfPos);
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            collisionFlg = true;
            walkSpeed = defaultCollisionWalkSpeed;
        }

        void OnCollisionExit2D(Collision2D other)
        {
            collisionFlg = false;
            walkSpeed = defaultWalkSpeed;
        }

        void SetDirectionInfo()
        {
            if (TargetController.CurrentDirection == Direction.F ||
                TargetController.CurrentDirection == Direction.B)
            {
                destX = maxDestNum;
                destY = minDestNum;
            }
            else
            {
                destX = minDestNum;
                destY = maxDestNum;
            }
        }

        void MoveX(float absX, float selfX, float targetX, Vector2 selfPos)
        {
            float tmpWalkSpeed = walkSpeed;
            float inclementNumX;

            float differenceX = Mathf.Clamp(absX - destX, 0.01f, defaultCatchUpWalkSpeed);
            bool judge = differenceX < defaultCatchUpWalkSpeed;
            walkSpeed = differenceX < defaultCatchUpWalkSpeed ? differenceX : defaultCatchUpWalkSpeed;
            if (targetX < selfX)
            {
                inclementNumX = -walkSpeed;
                WalkLeft();
            }
            else
            {
                inclementNumX = walkSpeed;
                WalkRight();
            }
            selfPos.x += inclementNumX;
            walkSpeed = tmpWalkSpeed;
            gameObject.transform.position = selfPos;
        }

        void MoveY(float absY, float selfY, float targetY, Vector2 selfPos)
        {
            var tmpWalkSpeed = walkSpeed;
            float inclementNumY;
            float differenceY = Mathf.Clamp(absY - destY, 0.01f, defaultCatchUpWalkSpeed);
            walkSpeed = differenceY < defaultCatchUpWalkSpeed ? differenceY : defaultCatchUpWalkSpeed;
            if (targetY < selfY)
            {
                inclementNumY = -walkSpeed;
                WalkFront();
            }
            else
            {
                inclementNumY = walkSpeed;
                WalkBack();
            }
            selfPos.y += inclementNumY;
            walkSpeed = tmpWalkSpeed;
            gameObject.transform.position = selfPos;
        }

        void ClearWaitStatus()
        {
            isWaitF = false;
            isWaitB = false;
            isWaitL = false;
            isWaitR = false;
        }
    }
}
