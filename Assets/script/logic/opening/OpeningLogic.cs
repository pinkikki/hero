using script.core.monoBehaviour;
using script.core.scene;

namespace script.logic.opening
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
//            SceneStatus.HasQuizE = true;
            SceneStatus.CanFlowEndRoll = true;
            SceneStatus.test("credit", 1);
            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "credit", null);
//            SceneStatus.test("classroom_c", 1);
//            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "classroom_c", null);
//            SceneManager.LoadScene("classroom");
        }

        void Update()
        {
        }
    }
}
