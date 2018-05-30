using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class MasakiTrigger : MonoBehaviour
	{
		void Start()
		{
		}

		void Update()
		{
		}

		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.Procedure == 1)
			{
				var reQuizObj = GameObject.Find("ReQuiz");
				if (reQuizObj == null)
				{
					var yusuke = GameObject.Find("yusuke");
					if (yusuke != null)
					{
						var mainCharacterController = yusuke.GetComponent<MainCharacterController>();
						if (mainCharacterController != null)
						{
							if (!mainCharacterController.FreezeFlg)
							{
								if (SceneStatus.LastSearchedArtObject == SceneStatus.ArtObject.Smartball)
								{
									SceneStatus.ProcedureWithSceneId("classroom", 5);
									SceneStatus.CanSearchMarble = true;
									Register(other, 705);
								}
								else if (SceneStatus.LastSearchedArtObject == SceneStatus.ArtObject.None)
								{
									Register(other, 703);
								}
								else
								{
									Register(other, 704);
								}
							}
						}
					}
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
