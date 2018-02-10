using System.Collections;
using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.logic.ending
{
	public class EndingSchoolyardBLogic : MonoBehaviour {

		void Start () {
			EventManager.Instance.Register(1301);
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
			niccShinobu.ConditionX = -16.4f;
			niccShinobu.WalkLeft();

			yield return CheckWalking(niccShinobu);
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkFrontNoSpeed();
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkBackNoSpeed();
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkFrontNoSpeed();
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkBackNoSpeed();
            
			yield return new WaitForSeconds(0.5f);

			niccShinobu.ConditionY = 22.0f;
			niccShinobu.WalkFront();

			yield return CheckWalking(niccShinobu);
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkLeftNoSpeed();
            
			yield return new WaitForSeconds(0.5f);
            
			niccShinobu.WalkRightNoSpeed();
            
			yield return new WaitForSeconds(0.5f);

			SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "schoolyard_c", null);
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
