using Assets.script.core.monoBehaviour;
using Assets.script.core.scene;
using UnityEngine.SceneManagement;

namespace Assets.script.logic.opening
{
    public class OpeningLogic : SingletonMonoBehaviour<OpeningLogic>
    {
        void Start()
        {
            SceneStatus.EntranceNo = 1;
            SceneStatus.Procedure = 1;
            SceneManager.LoadScene("classroom");
        }

        void Update()
        {
        }
    }
}
