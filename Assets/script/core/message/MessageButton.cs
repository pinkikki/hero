using Assets.script.core.@event;
using UnityEngine;

namespace Assets.script.core.message
{
    public class MessageButton : MonoBehaviour {

        void Start () {
	
        }
	
        void Update () {
	
        }

        public void OnClick() {
            if (!MessageManager.Instance.ManualFlg) {
                if (!MessageManager.Instance.AutoFlg) {
                    EventManager.Instance.NextTask();
                }
            } else {
                MessageManager.Instance.Hide();
                // TODO メニューができたらコメントアウト！！！
//                FieldMenuManager.getInstance().SetActive(true);
            }
        }
    }
}
