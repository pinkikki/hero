using script.core.db;
using script.core.hint;
using script.core.monoBehaviour;
using script.core.scene;

namespace script.logic.opening
{
    public class OpeningLogic : SingletonMonoBehaviour<OpeningLogic>, DatabaseListener
    {
        void Start()
        {
            DbManager.Init(this, this);
        }

        void Update()
        {
        }

        public void OnDatabaseInit()
        {
            HintRepository.Instance.Load();
            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "starting", null);
        }
    }
}