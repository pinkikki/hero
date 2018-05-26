using UnityEngine;

namespace script.logic.school
{
	public class GrassyHelpLogic : MonoBehaviour
	{
		[SerializeField] GameObject helpTrigger;
		
		void Start ()
		{
			if (helpTrigger == null)
			{
				helpTrigger = GameObject.Find("help_trigger").gameObject;
			}
			
			helpTrigger.SetActive(false);
		}
	
		void Update () {
		
		}

		public void Help()
		{
			helpTrigger.SetActive(true);
		}
	}
}
