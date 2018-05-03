using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class AkoTrigger : MonoBehaviour
	{
		void Start()
		{
		}

		void Update()
		{
		}

		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke")
			{
				if (SceneStatus.Procedure == 1) {
					if (SceneStatus.LastSearchedArtObject == SceneStatus.ArtObject.Smartball)
					{
						SceneStatus.ProcedureWithSceneId("classroom", 5);
						Register(other, 705);
					}
					else if (SceneStatus.LastSearchedArtObject == SceneStatus.ArtObject.None)
					{
						Register(other, 702);
					}
					else
					{
						Register(other, 704);
					}
				}
				else if (SceneStatus.Procedure == 3 && SceneStatus.CanCreateNerikeshi)
				{
					EventManager.Instance.Register(710);
				}
			}
		}

		void Register(Collision2D other, int eventId)
		{
			if (!other.gameObject.GetComponent<MainCharacterController>().FreezeFlg)
			{
				EventManager.Instance.Register(eventId);
				gameObject.GetComponent<AutoCharacterController>().FreezeFlg = true;
			}
		}
	}
}
