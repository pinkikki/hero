using UnityEngine;
using UnityEngine.UI;

namespace script.core.sprite
{
    public class AlphaEffect : MonoBehaviour
    {
        [SerializeField] int effectWaitNum = 50;
        [SerializeField] int currentEffectWaitNum;
        float alphaValue = 1.0f;
        int direction = -1;

        void Start()
        {
        }

        void Update()
        {
        }

        void FixedUpdate()
        {
            if (currentEffectWaitNum < 1)
            {
                if (direction > 0 && 1.0f <= alphaValue)
                {
                    direction = -1;
                }
                else if (direction < 0 && 0.0f >= alphaValue)
                {
                    direction = 1;
                }
                alphaValue += Time.deltaTime * direction;
                GetComponent<Image>().color = new Color(1, 1, 1, alphaValue);

                if (1.0f <= alphaValue)
                {
                    currentEffectWaitNum = effectWaitNum;
                }
            }
            else
            {
                currentEffectWaitNum--;
            }
        }
    }
}
