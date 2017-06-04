using UnityEngine;

namespace Assets.script.core.character
{
    public class ChickenController : AutoCharacterController
    {
        bool onCollisionWithPlayer;

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.name == "yusuke")
            {
                onCollisionWithPlayer = true;
            }
            if (onCollisionWithPlayer && other.transform.name == "inside")
            {
                Debug.Log("GOAL");
            }

            // TODO にわとりが勝手にかごに入った時に、何かのボタンを押したらゴールする実装

        }

        void OnCollisionExit2D(Collision2D other)
        {
            if (other.transform.name == "yusuke")
            {
                onCollisionWithPlayer = false;
            }
        }
    }
}
