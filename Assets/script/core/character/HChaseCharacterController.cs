using UnityEngine;

namespace script.core.character
{
    public class HChaseCharacterController : CharacterBase
    {
        [SerializeField] GameObject target;
        [SerializeField] GameObject otherChaseTarget;
        [SerializeField] float defaultWalkSpeed = 0.065f;
        [SerializeField] float defaultCollisionWalkSpeed = 0.04f;
        [SerializeField] float defaultCatchUpWalkSpeed = 0.12f;
        [SerializeField] float maxDestNum = 0.771f;
        float minDestNum;
        float destX;
        float destY;
        float walkSpeed;
        MainCharacterController targetController;
        bool hFlg;
        bool vFlg;

        bool isSwitchingX;
        bool isSwitchingY;

        bool isPreSwitchingX;

        // TODO 一旦初期値設定
        bool isPreSwitchingY = true;

        bool isWaitF;
        bool isWaitB;
        bool isWaitL;
        bool isWaitR;


        bool selfStopFlg;
        Direction currentSwitchingDirection = Direction.W;

        void Start()
        {
            Anim = gameObject.GetComponent<Animator>();
            targetController = target.GetComponent<MainCharacterController>();
            minDestNum = maxDestNum / 1.5f;
            walkSpeed = defaultWalkSpeed;
        }

        void FixedUpdate()
        {
            SetDirectionInfo();

            var targetPos = target.transform.position;
            var otherChaseTargetPos = otherChaseTarget.transform.position;
            var selfPos = gameObject.transform.position;

            var targetX = targetPos.x;
            var targetY = targetPos.y;
            var otherChaseTargetX = otherChaseTargetPos.x;
            var otherChaseTargetY = otherChaseTargetPos.y;
            var selfX = selfPos.x;
            var selfY = selfPos.y;


            var absX = Mathf.Abs(targetX - selfX);
            var absY = Mathf.Abs(targetY - selfY);

            if (isSwitchingX && !Mathf.Approximately(absX, maxDestNum) && absX < maxDestNum)
            {
                ReverseDirectionXtoY(absX, selfY, otherChaseTargetY, selfPos);
                return;
            }
            else
            {
                isSwitchingX = false;
            }
            if (isSwitchingY && !Mathf.Approximately(absY, maxDestNum) && absY < maxDestNum)
            {
                ReverseDirectionYtoX(absY, selfX, otherChaseTargetX, selfPos);
                return;
            }
            else
            {
                isSwitchingY = false;
            }

            currentSwitchingDirection = Direction.W;

            if (isWaitF && targetController.CurrentDirection == Direction.F)
            {
                WalkFront();
                if (targetY > selfY)
                {
                    return;
                }
            }
            else if (isWaitB && targetController.CurrentDirection == Direction.B)
            {
                WalkBack();
                if (targetY < selfY)
                {
                    return;
                }
            }
            else if (isWaitL && targetController.CurrentDirection == Direction.L)
            {
                WalkLeft();
                if (targetX > selfX)
                {
                    return;
                }
            }
            else if (isWaitR && targetController.CurrentDirection == Direction.R)
            {
                WalkRight();
                if (targetX < selfX)
                {
                    return;
                }
            }
            ClearWaitStatus();

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
            if (targetController.CurrentDirection == Direction.F ||
                targetController.CurrentDirection == Direction.B)
            {
                destX = maxDestNum;
                destY = minDestNum;
                if (isPreSwitchingY)
                {
                    isSwitchingX = true;
                    isPreSwitchingX = true;
                    isPreSwitchingY = false;
                }
            }
            else
            {
                destX = minDestNum;
                destY = maxDestNum;
                if (isPreSwitchingX)
                {
                    isSwitchingY = true;
                    isPreSwitchingX = false;
                    isPreSwitchingY = true;
                }
            }
        }

        void ReverseDirectionXtoY(float absX, float selfY, float otherChaseTargetY, Vector2 selfPos)
        {
            var tmpWalkSpeed = walkSpeed;
            float inclementNumX;
            // 精度の問題で必ずしもabsX == maxDestNumのならないためフラグ制御する
            bool lastFlg = false;

            if ((maxDestNum - absX) < walkSpeed)
            {
                walkSpeed = maxDestNum - absX;
                lastFlg = true;
            }
            else
            {
                walkSpeed = tmpWalkSpeed;
            }
            if (currentSwitchingDirection == Direction.R)
            {
                inclementNumX = walkSpeed;
                WalkRight();
            }
            else if (currentSwitchingDirection == Direction.L)
            {
                inclementNumX = -walkSpeed;
                WalkLeft();
            }
            else
            {
                if ((targetController.PreDirection == Direction.R &&
                     targetController.CurrentDirection == Direction.F) ||
                    (targetController.PreDirection == Direction.L &&
                     targetController.CurrentDirection == Direction.B))
                {
                    if (otherChaseTargetY < selfY)
                    {
                        inclementNumX = walkSpeed;
                        WalkRight();
                        currentSwitchingDirection = Direction.R;
                        if (targetController.CurrentDirection == Direction.B)
                        {
                            isWaitB = true;
                        }
                    }
                    else
                    {
                        inclementNumX = -walkSpeed;
                        WalkLeft();
                        currentSwitchingDirection = Direction.L;
                        if (targetController.CurrentDirection == Direction.F)
                        {
                            isWaitF = true;
                        }
                    }
                }
                else
                {
                    if (otherChaseTargetY < selfY)
                    {
                        inclementNumX = -walkSpeed;
                        WalkLeft();
                        currentSwitchingDirection = Direction.L;
                        if (targetController.CurrentDirection == Direction.B)
                        {
                            isWaitB = true;
                        }
                    }
                    else
                    {
                        inclementNumX = walkSpeed;
                        WalkRight();
                        currentSwitchingDirection = Direction.R;
                        if (targetController.CurrentDirection == Direction.F)
                        {
                            isWaitF = true;
                        }
                    }
                }
            }
            selfPos.x += inclementNumX;
            walkSpeed = tmpWalkSpeed;
            gameObject.transform.position = selfPos;
            if (lastFlg)
            {
                isSwitchingX = false;
                currentSwitchingDirection = Direction.W;
                minDestNum = Random.Range(0.0f, 0.6f);
            }
        }

        void ReverseDirectionYtoX(float absY, float selfX, float otherChaseTargetX, Vector2 selfPos)
        {
            var tmpWalkSpeed = walkSpeed;
            float inclementNumY;
            // 精度の問題で必ずしもabsX == maxDestNumのならないためフラグ制御する
            bool lastFlg = false;

            if ((maxDestNum - absY) < walkSpeed)
            {
                walkSpeed = maxDestNum - absY;
                lastFlg = true;
            }
            else
            {
                walkSpeed = tmpWalkSpeed;
            }
            if (currentSwitchingDirection == Direction.B)
            {
                inclementNumY = walkSpeed;
                WalkBack();
            }
            else if (currentSwitchingDirection == Direction.F)
            {
                inclementNumY = -walkSpeed;
                WalkFront();
            }
            else
            {
                if ((targetController.PreDirection == Direction.B &&
                     targetController.CurrentDirection == Direction.R) ||
                    (targetController.PreDirection == Direction.F &&
                     targetController.CurrentDirection == Direction.L))
                {
                    if (otherChaseTargetX < selfX)
                    {
                        inclementNumY = -walkSpeed;
                        WalkFront();
                        currentSwitchingDirection = Direction.F;
                        if (targetController.CurrentDirection == Direction.R)
                        {
                            isWaitR = true;
                        }
                    }
                    else
                    {
                        inclementNumY = walkSpeed;
                        WalkBack();
                        currentSwitchingDirection = Direction.B;
                        if (targetController.CurrentDirection == Direction.L)
                        {
                            isWaitL = true;
                        }
                    }
                }
                else
                {
                    if (otherChaseTargetX < selfX)
                    {
                        inclementNumY = walkSpeed;
                        WalkBack();
                        currentSwitchingDirection = Direction.B;
                        if (targetController.CurrentDirection == Direction.R)
                        {
                            isWaitR = true;
                        }
                    }
                    else
                    {
                        inclementNumY = -walkSpeed;
                        WalkFront();
                        currentSwitchingDirection = Direction.F;
                        if (targetController.CurrentDirection == Direction.L)
                        {
                            isWaitL = true;
                        }
                    }
                }
            }
            selfPos.y += inclementNumY;
            walkSpeed = tmpWalkSpeed;
            gameObject.transform.position = selfPos;
            if (lastFlg)
            {
                isSwitchingY = false;
                currentSwitchingDirection = Direction.W;
                minDestNum = Random.Range(0.0f, 0.3f);
            }
        }

        void MoveX(float absX, float selfX, float targetX, Vector2 selfPos)
        {
            float tmpWalkSpeed = walkSpeed;
            float inclementNumX;

            float differenceX = absX - destX;
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
