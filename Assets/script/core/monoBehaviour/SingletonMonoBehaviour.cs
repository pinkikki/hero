using UnityEngine;

namespace script.core.monoBehaviour
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {

        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        Debug.LogError(typeof(T) + " is none");
                    }
                }
                return instance;
            }
        }

        public static bool Exist()
        {
            if (instance == null)
            {
                return false;
            }
            return true;

        }
    }
}
