using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.logic.school
{
	public class SchoolYardLogic : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}

		public void Action001()
		{
			changeObjName("classmateS", "classmateSA");
			changeObjName("classmateR", "classmateRA");
			EventManager.Instance.NextTask();
		}
		
		public void Action002()
		{
			SceneStatus.CanSearchMatomari = true;
			changeObjName("classmateRA", "classmateRB");
			if (SceneStatus.HasGraveRoadA)
			{
				changeObjName("classmateO", "classmateOA");
			}
			EventManager.Instance.NextTask();
		}
		
		public void Action003()
		{
			if (!SceneStatus.HasGraveRoadA)
			{
				SceneStatus.HasGraveRoadA = true;
				if (SceneStatus.CanSearchMatomari)
				{
					changeObjName("classmateO", "classmateOA");
				}
			}
			else
			{
				SceneStatus.HasGraveRoadB = true;
				changeObjName("classmateOD", "classmateOE");
			}
			EventManager.Instance.NextTask();
		}
		
		public void Action004()
		{
			// TODO まとまりくん出す
			EventManager.Instance.NextTask();
		}
		
		public void Action005()
		{
			SceneStatus.HasMatomari = true;
			changeObjName("classmateOB", "classmateOF");
			changeObjName("classmateRB", "classmateRC");
			EventManager.Instance.NextTask();
		}
		
		public void Action006()
		{
			changeObjName("classmateOC", "classmateOD");
			EventManager.Instance.NextTask();
		}
		
		public void Action007()
		{
			SceneStatus.CanGetGraveRoadB = true;
			EventManager.Instance.NextTask();
		}
		
		public void Action008()
		{
			// TODO まとまりくんを出す
			EventManager.Instance.NextTask();
		}
		
		public void Action009()
		{
			SceneStatus.HasMatomari = true;
			changeObjName("classmateOE", "classmateOF");
			changeObjName("classmateRB", "classmateRC");
			EventManager.Instance.NextTask();
		}
		
		public void Action010()
		{
			changeObjName("classmateRC", "classmateRD");
			EventManager.Instance.NextTask();
		}
		
		public void SelectAButton()
		{
			SceneStatus.HasCicada = true;
			EventManager.Instance.NextTask();
			changeObjName("classmateOA", "classmateOB");
			EventManager.Instance.RegisterByForce("classmateOB");
		}

		public void SelectBButton()
		{
			EventManager.Instance.NextTask();
			changeObjName("classmateOA", "classmateOC");
			EventManager.Instance.RegisterByForce("classmateOC");
		}

		private void changeObjName(string getName, string setName)
		{
			var classmate = GameObject.Find(getName);
			classmate.name = setName;
		}
	}
}
