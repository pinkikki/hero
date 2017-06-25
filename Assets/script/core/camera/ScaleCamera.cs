using UnityEngine;

namespace script.core.camera
{
    public class ScaleCamera : MonoBehaviour
    {
        [SerializeField] float targetWidth = 640;
        [SerializeField] float pixelsToUnits = 100;
        [SerializeField] Vector3 offset;
        [SerializeField] float lowerLimitX = float.MinValue;
        [SerializeField] float upperLimitX = float.MaxValue;
        [SerializeField] float lowerLimitY = float.MinValue;
        [SerializeField] float upperLimitY = float.MaxValue;
        [SerializeField] bool initialization = true;

        public bool Initialization
        {
            get { return initialization; }
            set { initialization = value; }
        }

        [SerializeField] GameObject target;

        public GameObject Target
        {
            get { return target; }
            set
            {
                target = value;
                initialize();
            }
        }

        [SerializeField] float leapBaseValue = 50.0f;

        public float LeapBaseValue
        {
            get { return leapBaseValue; }
            set { leapBaseValue = value; }
        }


        void Start()
        {
            if (target != null)
            {
                initialize();
            }
        }

        void Update()
        {
            float height = Mathf.RoundToInt(targetWidth / Screen.width * Screen.height);
            GetComponent<Camera>().orthographicSize = height / pixelsToUnits / 2;
        }

        void FixedUpdate()
        {
            if (target != null)
            {
                Vector3 targetCameraPos = target.transform.position + offset;
                targetCameraPos.z = transform.position.z;
                if (targetCameraPos.x < lowerLimitX)
                {
                    targetCameraPos.x = lowerLimitX;
                } else if (upperLimitX < targetCameraPos.x)
                {
                    targetCameraPos.x = upperLimitX;
                }
                if (targetCameraPos.y < lowerLimitY)
                {
                    targetCameraPos.y = lowerLimitY;
                } else if (upperLimitY < targetCameraPos.y)
                {
                    targetCameraPos.y = upperLimitY;
                }
                transform.position = Vector3.Lerp(transform.position, targetCameraPos, leapBaseValue * Time.deltaTime);
            }
        }

        void initialize()
        {
            if (initialization)
            {
                Vector3 pos = transform.position;
                pos.x = target.transform.position.x;
                pos.y = target.transform.position.y;
                transform.position = pos;
            }
        }
    }
}