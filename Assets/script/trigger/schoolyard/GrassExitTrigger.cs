using script.common.dao;
using script.core.audio;
using script.core.scene;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class GrassExitTrigger : MonoBehaviour {

		void Start () {
		
		}

		void Update () {
		
		}

		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke")
			{
				AudioManager.Instance.StopSe(MusicDao.SelectByPrimaryKey(5).MusicName);
				SceneStatus.EntranceNo = 4;
				SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "schoolyard", null);
			}
		}
	}
}
