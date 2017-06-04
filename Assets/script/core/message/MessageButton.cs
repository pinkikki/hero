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
            if (!MessageManager.Instance.AutoFlg) {
                EventManager.Instance.NextTask();
            }
        }
    }
}
