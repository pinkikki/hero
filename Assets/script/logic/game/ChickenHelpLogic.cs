using UnityEngine;

namespace script.logic.game
{
	public class ChickenHelpLogic : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}

		public void SlowChicken()
		{
			GameObject.Find("chicken_target").GetComponent<ChickenController>().SpeedFactor = 0.03f;
		}
	}
}
