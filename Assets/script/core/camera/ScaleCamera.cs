using UnityEngine;

namespace script.core.camera
{
    public class ScaleCamera : MonoBehaviour
    {
        [SerializeField] float targetWidth = 1136;
        [SerializeField] float pixelsToUnits = 100;
        [SerializeField] GameObject target;
        [SerializeField] Vector3 offset;


        void Start()
        {
            if (target != null)
            {
                Vector3 pos = transform.position;
                pos.x = target.transform.position.x;
                pos.y = target.transform.position.y;
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
                transform.position = Vector3.Lerp(transform.position, targetCameraPos, 50f * Time.deltaTime);
            }
        }

        void SetTarget(GameObject obj)
        {
            target = obj;
            Vector3 pos = transform.position;
            pos.x = target.transform.position.x;
            pos.y = target.transform.position.y;
        }
    }
}