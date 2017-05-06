using UnityEngine;

namespace Assets.script.core.character
{
    // TODO RididBodyのCollision Detectionは「Continous」に設定すること！！
    // 設定しないと、すり抜けてしまう
    // にわとりと、ゆうすけの両方に設定しなくてはいけないかも
    // その場合、にわとりのシーンだけはゆうすけのCollision Detectionを「Continous」に設定すること
    public class AutoCharacterController : CharacterBase
    {
        int type;
        int repeatNum;

        void Start()
        {
            anim = gameObject.GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            repeatNum++;
            if (collisionFlg || repeatNum > 20)
            {
                type = Random.Range(0, 4);
                repeatNum = 0;
            }
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

            Vector3 pos = gameObject.transform.position;
            pos.x += hSpeed * 0.15f;
            pos.y += vSpeed * 0.15f;
            gameObject.transform.position = pos;
        }

        void Update()
        {
        }
    }
}
