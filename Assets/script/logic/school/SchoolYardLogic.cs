using script.core.asset;
using script.core.audio;
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
				if (SceneStatus.HasNerikeshi)
				{
					changeObjName("girl_c", "girl_ca");
				}
			}
			AudioManager.Instance.DownBgmVolume(0.1f, 1.0f);
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
			changeObjName("girl_c", "girl_ca");
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
		
		public void Action019()
		{
			SceneStatus.IsCompletedGhostStories1 = true;
			if (SceneStatus.IsCompletedGhostStories2)
			{
				changeObjName("boy_b", "boy_ba");
			}
			if (SceneStatus.IsCompletedGhostStories3)
			{
				changeObjName("girl_b", "girl_ba");
			}
			EventManager.Instance.NextTask();
		}
		
		public void Action020()
		{
			SceneStatus.IsCompletedGhostStories4 = true;
			if (SceneStatus.IsCompletedGhostStories5 && SceneStatus.IsCompletedGhostStories6)
			{
				changeObjName("girl_a", "girl_ab");
			}
			EventManager.Instance.NextTask();
		}
		
		public void Action021()
		{
			SceneStatus.IsCompletedGhostStories2 = true;
			if (SceneStatus.IsCompletedGhostStories1)
			{
				changeObjName("boy_b", "boy_ba");
			}
			if (SceneStatus.IsCompletedGhostStories3)
			{
				changeObjName("girl_a", "girl_aa");
			}
			EventManager.Instance.NextTask();
		}
		
		public void Action022()
		{
			SceneStatus.IsCompletedGhostStories5 = true;
			if (SceneStatus.IsCompletedGhostStories4 && SceneStatus.IsCompletedGhostStories6)
			{
				changeObjName("girl_b", "girl_bb");
			}
			EventManager.Instance.NextTask();
		}
		
		public void Action023()
		{
			SceneStatus.IsCompletedGhostStories3 = true;
			if (SceneStatus.IsCompletedGhostStories1)
			{
				changeObjName("girl_b", "girl_ba");
			}
			if (SceneStatus.IsCompletedGhostStories2)
			{
				changeObjName("girl_a", "girl_aa");
			}
			EventManager.Instance.NextTask();
		}
		
		public void Action024()
		{
			SceneStatus.IsCompletedGhostStories6 = true;
			if (SceneStatus.IsCompletedGhostStories4 && SceneStatus.IsCompletedGhostStories5)
			{
				changeObjName("boy_b", "boy_bb");
			}
			EventManager.Instance.NextTask();
		}
		
		public void Action025()
		{
			changeObjName("boy_d", "boy_da");
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
