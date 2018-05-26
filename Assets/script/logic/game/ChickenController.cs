using script.core.character;
using script.core.@event;
using script.core.hint;
using UnityEngine;

namespace script.logic.game
{
    public class ChickenController : AutoCharacterController
    {
        new void FixedUpdate()
        {
            base.FixedUpdate();
            Adjustment();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (gameObject.name == "chicken_target")
            {
                if (other.transform.name == "cover")
                {
                    HelpManager.Instance.Hide();
                    Destroy(this);
                    EventManager.Instance.Register(902);
                }
            }
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