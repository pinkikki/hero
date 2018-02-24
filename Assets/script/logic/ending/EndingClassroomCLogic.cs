using System.Collections;
using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.logic.ending
{
	public class EndingClassroomCLogic : MonoBehaviour {

		void Start () {
			EventManager.Instance.Register(2001);
		}
	
		void Update () {
		
		}
		
		public void Action001()
		{
			StartCoroutine(Action001Coroutine());
		}
		
		IEnumerator Action001Coroutine()
		{
			yield return new WaitForSeconds(3.0f);
			var shinobu = GameObject.Find("shinobu");
			var niccShinobu = shinobu.GetComponent<NoInputCharacterController>();

			niccShinobu.ConditionX = -0.4f;
			niccShinobu.WalkRight();

			yield return CheckWalking(niccShinobu);
			
			niccShinobu.WalkLeftNoSpeed();
			
			yield return new WaitForSeconds(0.5f);
			
			niccShinobu.WalkRightNoSpeed();
			
			yield return new WaitForSeconds(0.5f);
			
			niccShinobu.WalkLeftNoSpeed();
			
			yield return new WaitForSeconds(0.5f);
			
			niccShinobu.WalkRightNoSpeed();
			
			yield return new WaitForSeconds(0.5f);
			
			niccShinobu.WalkFrontNoSpeed();
			
			yield return new WaitForSeconds(2.5f);

			niccShinobu.SpeedFactor = 0.15f;
			niccShinobu.ConditionX = -10.0f;
			niccShinobu.WalkLeft();

			CheckWalking(niccShinobu);
			
			SceneLoadManager.Instance.FadeOut(10.0f);
			
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
