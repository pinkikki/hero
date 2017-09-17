using UnityEngine;

namespace script.core.character
{
    public class VChaseCharacterController : ChaseCharacterController
    {
        [SerializeField] float defaultWalkSpeed = 0.065f;
        [SerializeField] float defaultCollisionWalkSpeed = 0.04f;
        [SerializeField] float defaultCatchUpWalkSpeed = 0.065f;
        [SerializeField] float maxDestNum;
        [SerializeField] float minDestNum;

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

//            if (isWaitF && targetController.CurrentDirection == Direction.F)
//            {
//                WalkFront();
//                if (targetY > selfY)
//                {
//                    return;
//                }
//            }
//            else if (isWaitB && targetController.CurrentDirection == Direction.B)
//            {
//                WalkBack();
//                if (targetY < selfY)
//                {
//                    return;
//                }
//            }
//            else if (isWaitL && targetController.CurrentDirection == Direction.L)
//            {
//                WalkLeft();
//                if (targetX > selfX)
//                {
//                    return;
//                }
//            }
//            else if (isWaitR && targetController.CurrentDirection == Direction.R)
//            {
//                WalkRight();
//                if (targetX < selfX)
//                {
//                    return;
//                }
//            }
//            ClearWaitStatus();

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

            float differenceX = absX - destX;
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
            float differenceY = absY - destY;
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
