using UnityEngine;

namespace script.core.character
{
    // TODO RididBodyのCollision Detectionは「Continous」に設定すること！！
    // 設定しないと、すり抜けてしまう
    // にわとりと、ゆうすけの両方に設定しなくてはいけないかも
    // その場合、にわとりのシーンだけはゆうすけのCollision Detectionを「Continous」に設定すること
    public class AutoCharacterController : CharacterBase
    {
        int currentRepeatNum;
        protected int type;
        [SerializeField] int repeatNum = 20;

        public int RepeatNum
        {
            set { repeatNum = value; }
        }

        public float SpeedFactor
        {
            set { speedFactor = value; }
        }

        [SerializeField] protected float speedFactor = 0.15f;


        void Start()
        {
            Anim = gameObject.GetComponent<Animator>();
        }

        void FixedUpdate()
        {
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
        }

        void Update()
        {
        }

        protected virtual void Walk()
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
    }
}
