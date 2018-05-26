using script.core.@event;
using UnityEngine;

namespace script.trigger.schoolyard
{
    public class GrassyHelpTrigger : MonoBehaviour
    {
        void Start()
        {
        }

        void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "yusuke")
            {
                if (gameObject.name == "triggerA")
                {
                    EventManager.Instance.Register(1005);
                }
                else if (gameObject.name == "triggerB")
                {
                    EventManager.Instance.Register(1006);
                }
                else if (gameObject.name == "triggerC")
                {
                    EventManager.Instance.Register(1007);
                }
            }
        }
    }
}