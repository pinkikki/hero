using script.core.operation;
using UnityEngine;

namespace script.trigger.corridor
{
    public class NoticeBoardBTrigger : MonoBehaviour
    {
        void Start()
        {
        }

        void Update()
        {
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name == "yusuke")
            {
                switch (gameObject.name)
                {
                    case "paper_a":
                        SearchButton.Instance.OnRegister(602);
                        break;
                    case "paper_b":
                        SearchButton.Instance.OnRegister(603);
                        break;
                    case "paper_c":
                        SearchButton.Instance.OnRegister(604);
                        break;
                }
            }
        }

        void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.name == "yusuke")
            {
                SearchButton.Instance.OnDialog();
            }
        }
    }
}