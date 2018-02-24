using System.Collections;
using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.logic.ending
{
	public class EndingChickenroomALogic : MonoBehaviour {

		void Start () {
			EventManager.Instance.Register(1901);
		}
	
		void Update () {
		
		}
		
		public void Action001()
		{
			StartCoroutine(Action001Coroutine());
		}
		
		IEnumerator Action001Coroutine()
		{
			var shinobu = GameObject.Find("shinobu");
			var niccShinobu = shinobu.GetComponent<NoInputCharacterController>();

			niccShinobu.ConditionY = 2;
			niccShinobu.WalkBack();

			yield return CheckWalking(niccShinobu);
			
			niccShinobu.ConditionX = -2.5f;
			niccShinobu.WalkLeft();
			
			yield return CheckWalking(niccShinobu);
		
			SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "classroom_c", null);
			EventManager.Instance.NextTask();
		}
		
		IEnumerator CheckWalking(CharacterBase nicc)
		{
			while (true)
			{
				if (!nicc.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
		}
	}
}
