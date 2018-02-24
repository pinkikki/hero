using System.Collections;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.logic.ending
{
	public class EndingSchoolyardDLogic : MonoBehaviour {

		void Start () {
			EventManager.Instance.Register(1801);
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
		
			SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "chickenroom_a", null);
			EventManager.Instance.NextTask();
		}
	}
}
