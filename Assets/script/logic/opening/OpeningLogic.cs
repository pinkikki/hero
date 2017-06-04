using Assets.script.core.monoBehaviour;
using Assets.script.core.scene;

namespace Assets.script.logic.opening
{
    public class OpeningLogic : SingletonMonoBehaviour<OpeningLogic>
    {
        void Start()
        {
//            SceneStatus.EntranceNo = 1;
//            SceneStatus.Procedure = 1;
//            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "classroom", null);
            SceneStatus.EntranceNo = 1;
            SceneStatus.Procedure = 1;
            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "corridor", null);
//            SceneManager.LoadScene("classroom");
        }

        void Update()
        {
        }
    }
}
