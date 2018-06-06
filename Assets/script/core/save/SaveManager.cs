using script.common.dao;
using script.core.scene;
using UnityEngine;

namespace script.core.save
{
    public class SaveManager : MonoBehaviour
    {
        void Save()
        {
            SaveDao.Update(
                    SceneStatus.SceneId,
                    SceneStatus.ProcedureBySceneId("classroom"),
                    SceneStatus.ProcedureBySceneId("corridor"),
                    SceneStatus.ProcedureBySceneId("artroom"),
                    SceneStatus.ProcedureBySceneId("schoolyard")
                );
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.name == "yusuke")
            {
                Save();
            }

        }
    }
}