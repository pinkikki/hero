using System.Collections;
using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.logic.ending
{
	public class EndingSchoolyardCLogic : MonoBehaviour {

		void Start () {
			EventManager.Instance.Register(1401);
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
			niccShinobu.ConditionY = 26.5f;
			niccShinobu.WalkBack();

			yield return CheckWalking(niccShinobu);
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkRightNoSpeed();
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkLeftNoSpeed();
            
			yield return new WaitForSeconds(0.5f);

			niccShinobu.ConditionX = -13.7f;
			niccShinobu.WalkLeft();

			yield return CheckWalking(niccShinobu);
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkFrontNoSpeed();
            
			yield return new WaitForSeconds(0.5f);
			
			niccShinobu.ConditionY = 27.8f;
			niccShinobu.WalkBack();

			yield return CheckWalking(niccShinobu);
			
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkRightNoSpeed();
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkLeftNoSpeed();
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkRightNoSpeed();
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkBackNoSpeed();
			
			yield return new WaitForSeconds(1.5f);
			
			niccShinobu.ConditionY = 28.3f;
			niccShinobu.WalkBack();
			
			yield return CheckWalking(niccShinobu);
            
			yield return new WaitForSeconds(1.5f);

			SceneLoadManager.Instance.LoadLevelInLoading(2.0f, "shinobu_room", null);
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
