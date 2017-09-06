using script.core.character;
using UnityEngine;

namespace script.logic.game
{
    public class ChickenMainCharacterController : MainCharacterController
    {
        ChickenMainCharacterController()
        {
            FactorNum = 0.12f;
        }
        
        new void FixedUpdate()
        {
            base.FixedUpdate();
            Adjustment();
        }

        void Adjustment()
        {
            var pos = transform.position;

            pos.x = pos.x < -7.35f ? -7.35f : pos.x;
            pos.x = 7.34f < pos.x ? 7.34f : pos.x;
            pos.y = pos.y < -4.99f ? -4.99f : pos.y;
            pos.y = 4.715f < pos.y ? 4.715f : pos.y;

            transform.position = pos;
        }
    }
}