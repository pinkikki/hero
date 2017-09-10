﻿using script.core.monoBehaviour;
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
            SceneStatus.EntranceNo = 4;
            SceneStatus.Procedure = 3;
            SceneStatus.HasQuizE = true;
            SceneStatus.test("schoolyard", 3);
            SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "schoolyard", null);
//            SceneManager.LoadScene("classroom");
        }

        void Update()
        {
        }
    }
}
