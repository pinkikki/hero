using script.core.character;
using script.core.ui;
using UnityEngine;

namespace script.logic.school
{
	public class QuizUiCloseMonoBehaviour : UiCloseMonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		public override void Close()
		{
			GameObject.Find("yusuke").GetComponent<MainCharacterController>().FreezeFlg = false;
			base.Close();
		}
		
		public override void CloseAndEventNext()
		{
			GameObject.Find("yusuke").GetComponent<MainCharacterController>().FreezeFlg = false;
			base.CloseAndEventNext();
		}
	}
}
