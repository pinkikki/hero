using System.Collections;
using script.core.asset;
using script.core.character;
using script.core.@event;
using script.core.scene;
using script.logic.game;
using script.trigger.artroom;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace script.logic.school
{
	// TODO VChaseに変更する時に、忘れずにLayerをChasePlayerに変更すること！！！
	public class ArtroomLogic : MonoBehaviour {

		GameObject yusuke;
		NoInputCharacterController niccYusuke;
		GameObject ako;
		NoInputCharacterController niccAko;
		GameObject masaki;
		NoInputCharacterController niccMasaki;
		AsyncOperation ao;
		SmartBallLogic smartBallLogic;
		
		void Start () {
			if (SceneStatus.Procedure == 1)
			{
				EventManager.Instance.Register(701);
			}
			if (SceneStatus.Procedure == 2 && SceneStatus.CanCreateNerikeshi)
			{
				EventManager.Instance.Register(706);
			}
			if (SceneStatus.Procedure == 3 && SceneStatus.HasMarble)
			{
				ao = SceneManager.LoadSceneAsync("smart_ball", LoadSceneMode.Additive);
			}
		}
	
		void Update () {
		
		}
		
		public void Action001()
		{
			yusuke = GameObject.Find("yusuke");
			niccYusuke = yusuke.GetComponent<NoInputCharacterController>();
			masaki = GameObject.Find("masaki");
			niccMasaki = masaki.GetComponent<NoInputCharacterController>();
			ako = GameObject.Find("ako");
			niccAko = ako.GetComponent<NoInputCharacterController>();
			StartCoroutine(Action001Coroutine());
		}
		
		public void Action002()
		{
			Destroy(niccYusuke);
			RemoveNoInputController();
			yusuke.AddComponent<MainCharacterController>();
			AddAutoController();
			AddTrigger();
			EventManager.Instance.NextTask();
		}
		
		public void Action003()
		{
			StartCoroutine(Action003Coroutine());
		}
		
		public void Action004()
		{
			masaki.GetComponent<AutoCharacterController>().FreezeFlg = false;
			ako.GetComponent<AutoCharacterController>().FreezeFlg = false;
			EventManager.Instance.NextTask();
		}
		
		public void Action005()
		{
			// TODO スマートボール以外の終了actionを実装予定
			EventManager.Instance.NextTask();
		}
		
		public void Action006()
		{
			AddVChaseCharacterController();
			EventManager.Instance.NextTask();
		}
		
		public void Action007()
		{
			StartCoroutine(Action007CoroutineForMasaki());
			StartCoroutine(Action007CoroutineForAko());
		}
		
		public void Action008()
		{
			SceneStatus.HasGlue = true;
			EventManager.Instance.NextTask();
		}
		
		public void Action009()
		{
			StartCoroutine(Action009CoroutineForYusuke());
			StartCoroutine(Action009CoroutineForAko());
		}
		
		public void Action010()
		{
			var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/school/", "NerikeshiGame"),
				new Vector2(0.0f, 0.0f), Quaternion.identity);
			obj.name = "NerikeshiGame";
			EventManager.Instance.NextTask();
		}
		
		public void Action011()
		{
			SceneStatus.IsFinishedWashingHands = true;
			EventManager.Instance.NextTask();
		}
		
		public void Action012()
		{
			SceneStatus.HasDuster = true;
			EventManager.Instance.NextTask();
		}
		
		public void Action013()
		{
			SceneStatus.HasNerikeshi = true;
			SceneStatus.Procedure = 3;
			EventManager.Instance.NextTask();
		}
		
		public void Action014()
		{
			StartCoroutine(Action014Coroutine());
		}
		
		public void Action015()
		{
			StartCoroutine(Action015Coroutine());
		}
		
		public void Action016()
		{
			var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/common/", "QuizC"),
				new Vector2(0.0f, 0.0f), Quaternion.identity);
			obj.name = "QuizC";
			SceneStatus.HasQuizC = true;
		}
		
		IEnumerator Action001Coroutine()
		{
			var doorCollider = GameObject.Find("entrance_door").GetComponent<BoxCollider2D>();
			doorCollider.enabled = false;
			niccYusuke.ConditionY = -4.0f;
			niccYusuke.WalkBack();
			niccMasaki.ConditionY = -5.0f;
			niccMasaki.WalkBack();
			niccAko.ConditionY = -6.0f;
			niccAko.WalkBack();
			while (true)
			{
				if (!niccMasaki.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccMasaki.ConditionX = 7.7f;
			niccMasaki.WalkLeft();
			
			niccYusuke.WalkFrontNoSpeed();
			
			while (true)
			{
				if (!niccMasaki.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccMasaki.WalkRightNoSpeed();
			yield return null;
			yield return new WaitForSeconds(1.0f);
			doorCollider.enabled = true;
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action003Coroutine()
		{
			SceneLoadManager.Instance.Fade(1.0f, 1.0f);
			yield return new WaitForSeconds(1.0f);
			RemoveAutoController();
			AddNoInputController();
			yield return null;
			SetPosition(yusuke, -8.8f, 2.0f);
			niccYusuke.WalkBackNoSpeed();
			SetPosition(ako, -8.8f, 3.3f);
			niccAko.WalkFrontNoSpeed();
			SetPosition(masaki, -7.6f, 2.8f);
			niccMasaki.WalkLeftNoSpeed();
			RemoveNoInputController();
			RremoveTrigger();
			yield return new WaitForSeconds(1.5f);
			SceneStatus.Procedure = 2;
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action007CoroutineForAko()
		{
			ako.AddComponent<AkoTrigger>();	
			niccAko.ConditionX = 5.6f;
			niccAko.WalkLeft();
			
			while (true)
			{
				if (!niccAko.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccAko.ConditionY = 5.87f;
			niccAko.WalkBack();

			bool isFinishedNextTask = false;
			while (true)
			{

				if (!isFinishedNextTask && ako.transform.position.y > 0.0f)
				{
					isFinishedNextTask = true;
					Destroy(niccYusuke);
					yusuke.AddComponent<MainCharacterController>();
					EventManager.Instance.NextTask();
				}
				if (!niccAko.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccAko.ConditionX = -1.9f;
			niccAko.WalkLeft();
			
			while (true)
			{
				if (!niccAko.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccAko.WalkBackNoSpeed();
		}
		
		IEnumerator Action007CoroutineForMasaki()
		{
			masaki.AddComponent<NerikeshiGameTrigger>();
			
			niccMasaki.ConditionY = -4.4f;
			niccMasaki.WalkBack();
			while (true)
			{
				if (!niccMasaki.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccMasaki.ConditionX = -2.5f;
			niccMasaki.WalkLeft();
			
			while (true)
			{
				if (!niccMasaki.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccMasaki.ConditionY = -0.2f;
			niccMasaki.WalkBack();
			
			while (true)
			{
				if (!niccMasaki.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccMasaki.ConditionX = -5.45f;
			niccMasaki.WalkLeft();
			
			while (true)
			{
				if (!niccMasaki.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccMasaki.WalkFrontNoSpeed();
		}
		
		IEnumerator Action009CoroutineForYusuke()
		{
			Destroy(yusuke.GetComponent<MainCharacterController>());
			niccYusuke = yusuke.AddComponent<NoInputCharacterController>();
			niccYusuke.Anim = yusuke.GetComponent<Animator>();
			
//			niccYusuke.ConditionY = 0.0f;
//			niccYusuke.WalkBack();
//			
//			while (true)
//			{
//				if (!niccYusuke.WarlkingFlg)
//				{
//					break;
//				}
//				yield return null;
//			}
			
			niccYusuke.ConditionX = -4.72f;
			niccYusuke.WalkRight();
			
			while (true)
			{
				if (!niccYusuke.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccYusuke.ConditionY = -0.3f;
			niccYusuke.WalkFront();
			
			while (true)
			{
				if (!niccYusuke.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
		}
		
		IEnumerator Action009CoroutineForAko()
		{
			
			niccAko.ConditionX = -2.32f;
			niccAko.WalkLeft();
			
			while (true)
			{
				if (!niccAko.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccAko.ConditionY = -0.1f;
			niccAko.WalkFront();

			while (true)
			{
				if (!niccAko.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccAko.ConditionX = -4.05f;
			niccAko.WalkLeft();
			
			while (true)
			{
				if (!niccAko.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccAko.WalkFrontNoSpeed();
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action014Coroutine()
		{
			while (ao.progress < 0.9f) yield return null;

			if (smartBallLogic == null)
			{
				smartBallLogic = FindObjectOfType<SmartBallLogic>();	
			}
			smartBallLogic.Active();
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action015Coroutine()
		{
			if (smartBallLogic == null)
			{
				smartBallLogic = FindObjectOfType<SmartBallLogic>();	
			}
			yield return SceneLoadManager.Instance.FadeOutScene(1.0f);
			smartBallLogic.Close();
			yield return SceneLoadManager.Instance.FadeInScene(1.0f);
			EventManager.Instance.NextTask();
		}

		void AddNoInputController()
		{
			niccMasaki = masaki.AddComponent<NoInputCharacterController>();
			niccAko = ako.AddComponent<NoInputCharacterController>();
		}
		
		void RemoveNoInputController()
		{
			Destroy(niccMasaki);
			Destroy(niccAko);
		}
		
		void AddAutoController()
		{
			var autoMasaki = masaki.AddComponent<AutoCharacterController>();
			var autoAko = ako.AddComponent<AutoCharacterController>();
			autoMasaki.RepeatNum = 20;
			autoMasaki.SpeedFactor = 0.10f;
			autoAko.RepeatNum = 30;
			autoAko.SpeedFactor = 0.05f;
		}
		
		void RemoveAutoController()
		{
			Destroy(masaki.GetComponent<AutoCharacterController>());
			Destroy(ako.GetComponent<AutoCharacterController>());
		}

		void AddTrigger()
		{
			ako.AddComponent<AkoTrigger>();
			masaki.AddComponent<MasakiTrigger>();	
		}
		
		void RremoveTrigger()
		{
			Destroy(masaki.GetComponent<MasakiTrigger>());
			Destroy(ako.GetComponent<AkoTrigger>());	
		}

		void AddVChaseCharacterController()
		{
			var vcccAko = ako.AddComponent<VChaseCharacterController>();
			var vcccMasaki = masaki.AddComponent<VChaseCharacterController>();
			vcccAko.Target = yusuke;
			vcccAko.MinDestNum = 1.6f;
			vcccMasaki.Target = yusuke;
			vcccMasaki.MinDestNum = 0.8f;
			ako.layer = 9;
			masaki.layer = 9;
		}
		
		void SetPosition(GameObject obj, float posX, float posY)
		{
			var pos = obj.transform.position;
			pos.x = posX;
			pos.y = posY;
			obj.transform.position = pos;
		}
	}
}
