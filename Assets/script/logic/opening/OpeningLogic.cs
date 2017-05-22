using script.core.monoBehaviour;
using script.core.scene;
using UnityEngine.SceneManagement;

namespace script.logic.opening
{
    public class OpeningLogic : SingletonMonoBehaviour<OpeningLogic>
    {
        void Start()
        {
            SceneStatus.EntranceNo = 1;
            SceneStatus.Procedure = 1;
            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "classroom", null);
//            SceneManager.LoadScene("classroom");
        }

        void Update()
        {
        }
    }
}
