using script.core.@event;
using script.core.monoBehaviour;
using script.core.scene;

namespace script.logic.opening
{
    public class OpeningLogic : SingletonMonoBehaviour<OpeningLogic>
    {
        void Start()
        {
            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "starting", null);
        }

        void Update()
        {
        }
    }
}
