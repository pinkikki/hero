using script.core.operation;
using UnityEngine;

namespace script.trigger.corridor
{
    public class NoticeBoardCTrigger : MonoBehaviour
    {
        void Start()
        {
        }

        void Update()
        {
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "yusuke")
            {
                switch (gameObject.name)
                {
                    case "paper_a":
                        SearchButton.Instance.OnRegister(605);
                        break;
                    case "paper_b":
                        SearchButton.Instance.OnRegister(606);
                        break;
                    case "paper_c":
                        SearchButton.Instance.OnRegister(607);
                        break;
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == "yusuke")
            {
                SearchButton.Instance.OnDialog();
            }
        }
    }
}
