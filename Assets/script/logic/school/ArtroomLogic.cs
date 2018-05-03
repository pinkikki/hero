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
			if (SceneStatus.Procedure == 3)
			{
				EventManager.Instance.Register(706);
			}
			if (SceneStatus.Procedure == 5)
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
			AddAutoController();
			AddTrigger();
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
			// Action009で、ゆうすけの移動が終わら場合の対応
			masaki.layer = 11;
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
			SceneStatus.Procedure = 4;
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
		
		public void Action017()
		{
			yusuke.AddComponent<MainCharacterController>();
			Destroy(niccYusuke);
			EventManager.Instance.NextTask();
		}
		
		public void Action018()
		{
			yusuke.AddComponent<MainCharacterController>();
			Destroy(niccYusuke);
			
			Destroy(niccAko);
			Destroy(niccMasaki);
			
			var vcccAko = ako.AddComponent<VChaseCharacterController>();
			var vcccMasaki = masaki.AddComponent<VChaseCharacterController>();
			vcccAko.Target = yusuke;
			vcccAko.OtherChaseTarget = masaki;
			vcccAko.TargetController = yusuke.GetComponent<MainCharacterController>();
			vcccAko.MaxDestNum = 0.1f;
			vcccAko.MinDestNum = 1.6f;
			ako.layer = 9;
			vcccMasaki.Target = yusuke;
			vcccMasaki.OtherChaseTarget = ako;
			vcccMasaki.TargetController = vcccAko.TargetController;
			vcccMasaki.MaxDestNum = 0.1f;
			vcccMasaki.MinDestNum = 0.771f;
			masaki.layer = 9;
			
			EventManager.Instance.NextTask();
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
				if (!niccMasaki.WalkingFlg)
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
				if (!niccMasaki.WalkingFlg)
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
			switch (SceneStatus.LastSearchedArtObject)
			{
				case SceneStatus.ArtObject.Smartball:
					SetPosition(yusuke, -8.8f, 2.0f);
					SetPosition(ako, -8.8f, 3.3f);
					SetPosition(masaki, -7.6f, 2.8f);
					SceneStatus.Procedure = 2;
					break;
				case SceneStatus.ArtObject.ArtworkA:
					SetPosition(yusuke, -8.8f, 4.0f);
					SetPosition(ako, -8.8f, 5.3f);
					SetPosition(masaki, -7.6f, 4.8f);
					break;
				case SceneStatus.ArtObject.ArtworkB:
					SetPosition(yusuke, -8.8f, 0.5f);
					SetPosition(ako, -8.8f, 1.8f);
					SetPosition(masaki, -7.6f, 1.3f);
					break;
				case SceneStatus.ArtObject.ArtworkC:
					SetPosition(yusuke, -8.8f, -1.0f);
					SetPosition(ako, -8.8f, 0.3f);
					SetPosition(masaki, -7.6f, -0.2f);
					break;
				case SceneStatus.ArtObject.ArtworkD:
					SetPosition(yusuke, -8.8f, -2.5f);
					SetPosition(ako, -8.8f, -1.2f);
					SetPosition(masaki, -7.6f, -1.7f);
					break;
				case SceneStatus.ArtObject.ArtworkE:
					SetPosition(yusuke, -8.8f, -4.0f);
					SetPosition(ako, -8.8f, -2.7f);
					SetPosition(masaki, -7.6f, -3.2f);
					break;
				case SceneStatus.ArtObject.ArtworkF:
					SetPosition(yusuke, -8.8f, -5.5f);
					SetPosition(ako, -8.8f, -4.2f);
					SetPosition(masaki, -7.6f, -4.7f);
					break;
				case SceneStatus.ArtObject.ArtworkG:
					SetPosition(yusuke, -8.8f, -7.0f);
					SetPosition(ako, -8.8f, -5.7f);
					SetPosition(masaki, -7.6f, -6.2f);
					break;
			}

			niccYusuke.WalkBackNoSpeed();
			niccAko.WalkFrontNoSpeed();
			niccMasaki.WalkLeftNoSpeed();
			yield return null;
			RemoveNoInputController();
			RremoveTrigger();
			yield return new WaitForSeconds(1.5f);
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action007CoroutineForAko()
		{
			ako.AddComponent<AkoTrigger>();	
			niccAko.ConditionX = 5.6f;
			niccAko.WalkLeft();
			
			while (true)
			{
				if (!niccAko.WalkingFlg)
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
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccAko.ConditionX = -1.9f;
			niccAko.WalkLeft();
			
			while (true)
			{
				if (!niccAko.WalkingFlg)
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
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccMasaki.ConditionX = -2.5f;
			niccMasaki.WalkLeft();
			
			while (true)
			{
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccMasaki.ConditionY = -0.2f;
			niccMasaki.WalkBack();
			
			while (true)
			{
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccMasaki.ConditionX = -5.45f;
			niccMasaki.WalkLeft();
			
			while (true)
			{
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccMasaki.WalkFrontNoSpeed();
		}
		
		IEnumerator Action009CoroutineForYusuke()
		{
			masaki.layer = 9;
			Destroy(yusuke.GetComponent<MainCharacterController>());
			niccYusuke = yusuke.AddComponent<NoInputCharacterController>();
			niccYusuke.Anim = yusuke.GetComponent<Animator>();
			yield return null;
			
			niccYusuke.ConditionX = -4.72f;
			niccYusuke.WalkRight();
			
			while (true)
			{
				if (!niccYusuke.WalkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccYusuke.ConditionY = -0.3f;
			niccYusuke.WalkFront();
			
			while (true)
			{
				if (!niccYusuke.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			masaki.layer = 11;
		}
		
		IEnumerator Action009CoroutineForAko()
		{
			
			niccAko.ConditionX = -2.32f;
			niccAko.WalkLeft();
			
			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccAko.ConditionY = -0.1f;
			niccAko.WalkFront();

			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}

			if (-3.6f < niccAko.ConditionX)
			{
				niccAko.ConditionX = -3.7f;
				niccAko.WalkLeft();

				while (true)
				{
					if (!niccAko.WalkingFlg)
					{
						break;
					}

					yield return null;
				}
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
