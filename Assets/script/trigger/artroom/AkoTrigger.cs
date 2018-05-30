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
				if (SceneStatus.Procedure == 1)
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
										Register(other, 702);
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
				else if (SceneStatus.Procedure == 3 && SceneStatus.CanCreateNerikeshi)
				{
					var trigger = FindObjectOfType<NerikeshiGameTrigger>();
					if (trigger != null && !trigger.IsFirst)
					{
						EventManager.Instance.Register(723);
					}
					else
					{
						EventManager.Instance.Register(710);
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
