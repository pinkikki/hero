using script.core.@event;
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
//            SceneStatus.CanFlowEndRoll = true;
//            SceneStatus.test("credit", 1);
//            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "credit", null);
            
            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "starting", null);
        }

        void Update()
        {
        }
    }
}
