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
            SceneStatus.HasQuizE = true;
            SceneStatus.test("ending_classroom_a", 1);
            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "ending_classroom_a", null);
//            SceneManager.LoadScene("classroom");
        }

        void Update()
        {
        }
    }
}
