using script.core.asset;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.logic.school
{
	public class SchoolYardLogic : MonoBehaviour {

		void Start () {
			if (SceneStatus.Procedure == 2)
			{
				if (SceneStatus.HasNerikeshi)
				{
					changeObjName("classmateR", "classmateRA");
				}
			}
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
			SceneStatus.CanCreateNerikeshi = true;
			SceneStatus.Procedure = 2;
			EventManager.Instance.NextTask();
		}
		
		public void Action011()
		{
			changeObjName("classmateRA", "classmateRB");
			SceneStatus.CanGetMudDumplings = true;
			EventManager.Instance.NextTask();
		}
		
		public void Action012()
		{
			changeObjName("classmateS", "classmateSA");
			SceneStatus.HasMudDumplings = true;
			EventManager.Instance.NextTask();
		}
		
		public void Action013()
		{
			changeObjName("classmateSA", "classmateSB");
			SceneStatus.HasMarble = true;
			SceneStatus.Procedure = 3;
			EventManager.Instance.NextTask();
		}
		
		public void Action014()
		{
			var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/common/", "QuizD"),
				new Vector2(0.0f, 0.0f), Quaternion.identity);
			obj.name = "QuizD";
			SceneStatus.HasQuizD = true;
		}
		
		public void Action015()
		{
			var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/yard/", "UnLockGame"),
				new Vector2(0.0f, 0.0f), Quaternion.identity);
			obj.name = "UnLockGame";
			EventManager.Instance.NextTask();
		}
		
		public void Action016()
		{
			EventManager.Instance.NextTask();
		}
		
		public void Action017()
		{
			EventManager.Instance.NextTask();
		}
		
		public void Action018()
		{
			SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "grassy", null);
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
