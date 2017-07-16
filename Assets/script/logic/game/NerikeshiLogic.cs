using script.core.@event;
using script.core.scene;
using UnityEngine;
using UnityEngine.UI;

namespace script.logic.game
{
    public class NerikeshiLogic : MonoBehaviour
    {
        private InputField matomariInputText;
        private InputField glueInputText;
        private InputField waterInputText;

        void Start()
        {
            matomariInputText = transform.FindChild("MatomariRow/Input").GetComponent<InputField>();
            glueInputText = transform.FindChild("GlueRow/Input").GetComponent<InputField>();
            waterInputText = transform.FindChild("WaterRow/Input").GetComponent<InputField>();
            matomariInputText.ActivateInputField();
        }

        void Update()
        {
        }

        public void Answer()
        {
            if (SceneStatus.IsFinishedWashingHands && SceneStatus.HasDuster)
            {
                if (IsValidRate())
                {
                    EventManager.Instance.Register(715);
                }
                else
                {
                    EventManager.Instance.Register(716);
                }
            }
            else if (SceneStatus.IsFinishedWashingHands || SceneStatus.HasDuster)
            {
                if (IsValidRate())
                {
                    EventManager.Instance.Register(713);
                }
                else
                {
                    EventManager.Instance.Register(714);
                }
            }
            else
            {
                if (IsValidRate())
                {
                    EventManager.Instance.Register(711);
                }
                else
                {
                    EventManager.Instance.Register(712);
                }
            }
            Destroy(gameObject);
        }

        bool IsValidRate()
        {
            var matomariText = matomariInputText.text;
            var glueText = glueInputText.text;
            var waterText = waterInputText.text;
            return matomariText == "83" && glueText == "12" && waterText == "5";
        }
    }
}