using script.core.@event;
using UnityEngine;

namespace script.logic.school
{
	public class CorridorLogic : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		public void Action001()
		{
			EventManager.Instance.NextTask();
		}
	}
}
