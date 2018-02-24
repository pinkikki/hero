using System.Collections;
using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.logic.ending
{
	public class EndingClassroomBLogic : MonoBehaviour {

		GameObject shinobu;
		NoInputCharacterController niccShinobu;
		
		void Start () {
			EventManager.Instance.Register(1601);
		}
	
		void Update () {
		
		}
		
		public void Action001()
		{
			shinobu = GameObject.Find("shinobu");
			niccShinobu = shinobu.GetComponent<NoInputCharacterController>();
			StartCoroutine(Action001Coroutine());
		}
		
		IEnumerator Action001Coroutine()
		{
			yield return new WaitForSeconds(0.5f);
			
			niccShinobu.Anim.SetBool("wave", true);
			
			yield return new WaitForSeconds(3.5f);
		
			SceneLoadManager.Instance.LoadLevelInLoading(0.5f, "artroom_a", null);
			EventManager.Instance.NextTask();
		}
	}
}
