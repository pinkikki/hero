using System.Collections;
using System.Linq;
using script.common.dao;
using script.common.entity;
using script.core.asset;
using script.core.audio;
using script.core.camera;
using script.core.character;
using script.core.@event;
using script.core.monoBehaviour;
using script.core.operation;
using script.core.quiz;
using script.core.scene;
using script.trigger.classroom;
using UnityEngine;
using UnityEngine.UI;

namespace script.logic.school
{
	public class ClassroomLogic : SingletonMonoBehaviour<ClassroomLogic>
	{
		GameObject teacher;
		NoInputCharacterController niccTeacher;
		GameObject yusuke;
		NoInputCharacterController niccYusuke;
		GameObject ako;
		NoInputCharacterController niccAko;
		GameObject masaki;
		NoInputCharacterController niccMasaki;
		bool isRegistered;

		void Start()
		{
			if (SceneStatus.Procedure == 1)
			{
				// Continue対応によりコメントアウト
//				EventManager.Instance.Register(500);
			}
			else if (SceneStatus.Procedure == 2)
			{
				EventManager.Instance.Register(502);
			}
			else if (SceneStatus.Procedure == 3)
			{
				while (true)
				{
					var chair = GameObject.Find("chair_yusuke");
					if (chair != null)
					{
						chair.AddComponent<QuizPaperATrigger>();
						break;
					}
				}
			}
			SearchButton.Instance.Show();
			MusicEntity entity = MusicDao.SelectByPrimaryKey(1);
			if (!AudioManager.Instance.Playing(entity.MusicName))
			{
				AudioManager.Instance.PlayBgm(entity.MusicName, float.Parse(entity.Time));
			}
		}

		void Update()
		{
			if (isRegistered || 1 > EventManager.Instance.CompleteEventSet.Count) return;
			if (EventManager.Instance.CompleteEventSet.Count(e => e < 18) != 1) return;
			isRegistered = true;
			SceneStatus.CanComeInClassroom = true;
			EventManager.Instance.Register(501);
		}

		public void Action001()
		{
			var mccArr = FindObjectsOfType<MainCharacterController>();
			foreach (var mcc in mccArr)
			{
				mcc.FreezeFlg = true;
			}
			teacher = GameObject.Find("teacherA");
			niccTeacher = teacher.GetComponent<NoInputCharacterController>();
			StartCoroutine(Action001Coroutine());
		}

		public void Action002()
		{
			StartCoroutine(Action002Coroutine());
		}

		public void Action003()
		{
			SceneStatus.Procedure = 3;
			SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "classroom", null);
			EventManager.Instance.NextTask();
		}

		public void Action004()
		{
			var classmateC = GameObject.Find("classmateC");
			classmateC.name = "classmateCA";
			EventManager.Instance.NextTask();
		}

		public void Action005()
		{
			var classmateH = GameObject.Find("classmateH");
			classmateH.name = "classmateHA";
			EventManager.Instance.NextTask();
		}

		public void Action006()
		{
			var classmateHA = GameObject.Find("classmateHA");
			classmateHA.name = "classmateHB";
			EventManager.Instance.NextTask();
		}

		public void Action007()
		{
			var classmateHB = GameObject.Find("classmateHB");
			classmateHB.name = "classmateHC";
			EventManager.Instance.NextTask();
		}

		public void Action008()
		{
			var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/common/", "QuizA"),
				new Vector2(0.0f, 0.0f), Quaternion.identity);
			obj.name = "QuizA";
			var yusuke = GameObject.Find("yusuke");
			yusuke.gameObject.GetComponent<MainCharacterController>().FreezeFlg = true;
		}

		public void Action009()
		{
			Destroy(GameObject.Find("desk_sets"));
			Instantiate(AssetLoader.Instance.LoadPrefab("prefab/school/", "desk_sets_rib"),
				new Vector2(0.0f, 0.0f), Quaternion.identity);
			ako = GameObject.Find("ako");
			var akoPos = ako.transform.position;
			Destroy(ako);
			ako = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/mainCharacter/", "ako_no_input"),
				akoPos, Quaternion.identity);
			ako.name = "ako";
			masaki = GameObject.Find("masaki");
			var masakiPos = masaki.transform.position;
			Destroy(masaki);
			masaki = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/mainCharacter/", "masaki_no_input"),
				masakiPos, Quaternion.identity);
			masaki.name = "masaki";
			Destroy(QuizPaperATrigger.Instance);
			Destroy(GameObject.Find("QuizA"));
			EventManager.Instance.NextTask();
		}

		public void Action010()
		{
			niccAko = ako.GetComponent<NoInputCharacterController>();
			StartCoroutine(Action010Coroutine());
		}

		public void Action011()
		{
			niccMasaki = masaki.GetComponent<NoInputCharacterController>();
			StartCoroutine(Action011Coroutine());
		}

		public void Action012()
		{
			yusuke = GameObject.Find("yusuke");
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
			SceneStatus.HasQuizA = true;
			QuizManager.Instance.Show();
			EventManager.Instance.NextTask();
		}

		public void Action013()
		{
			SceneStatus.HasBroom = true;
			EventManager.Instance.NextTask();
		}

		public void Action014()
		{
			// Continue対応
			yusuke = GameObject.Find("yusuke");
			masaki = GameObject.Find("masaki");
			ako = GameObject.Find("ako");

			niccYusuke = yusuke.AddComponent<NoInputCharacterController>();
			var rig = yusuke.GetComponent<Rigidbody2D>();
			rig.isKinematic = true;
			var box = yusuke.GetComponent<BoxCollider2D>();
			box.enabled = false;
			var vcccAko = ako.GetComponent<VChaseCharacterController>();
			vcccAko.enabled = false;
			var vcccMasaki = masaki.GetComponent<VChaseCharacterController>();
			vcccMasaki.enabled = false;
			StartCoroutine(Action014Coroutine());
		}

		public IEnumerator Action015()
		{
			yield return StartCoroutine(Action015Coroutine());
			Destroy(niccYusuke);
			var rig = yusuke.GetComponent<Rigidbody2D>();
			rig.isKinematic = false;
			var box = yusuke.GetComponent<BoxCollider2D>();
			box.enabled = true;
			yusuke.GetComponent<SpriteRenderer>().sortingOrder = 0;
			Destroy(ako.GetComponent<NoInputCharacterController>());
			var vcccAko = ako.GetComponent<VChaseCharacterController>();
			vcccAko.enabled = true;
			Destroy(masaki.GetComponent<NoInputCharacterController>());
			var vcccMasaki = masaki.GetComponent<VChaseCharacterController>();
			vcccMasaki.enabled = true;
		}

		public void Action016()
		{
			StartCoroutine(Action016Coroutine());
		}

		public void Action017()
		{
			var mccArr = FindObjectsOfType<MainCharacterController>();
			foreach (var mcc in mccArr)
			{
				mcc.FreezeFlg = false;
			}
			EventManager.Instance.NextTask();
			Destroy(niccYusuke);
			var rig = yusuke.GetComponent<Rigidbody2D>();
			rig.isKinematic = false;
			var box = yusuke.GetComponent<BoxCollider2D>();
			box.enabled = true;
			yusuke.GetComponent<SpriteRenderer>().sortingOrder = 0;
			Destroy(ako.GetComponent<NoInputCharacterController>());
			var vcccAko = ako.GetComponent<VChaseCharacterController>();
			vcccAko.enabled = true;
			Destroy(masaki.GetComponent<NoInputCharacterController>());
			var vcccMasaki = masaki.GetComponent<VChaseCharacterController>();
			vcccMasaki.enabled = true;
			SceneStatus.HasQuizB = true;
			SceneStatus.Procedure = 4;
		}

		IEnumerator Action001Coroutine()
		{
			var scaleCamera = FindObjectOfType<ScaleCamera>();
			scaleCamera.Initialization = false;
			scaleCamera.Target = teacher;
			scaleCamera.LeapBaseValue = 3.0f;
			niccTeacher.ConditionX = 0;
			niccTeacher.WalkRight();
			while (true)
			{
				if (!niccTeacher.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			niccTeacher.WalkBackNoSpeed();
			yield return null;
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action002Coroutine()
		{
			SceneLoadManager.Instance.FadeOut(1.0f);
			yield return new WaitForSeconds(1.5f);
			var childLayer = GameObject.Find("BaseLayer/ChildLayer");
			var lastText = (GameObject) Instantiate(
				AssetLoader.Instance.LoadPrefab("prefab/common/", "OneWeekText"), new Vector2(0.0f, 0.0f),
				Quaternion.identity);
			lastText.transform.SetParent(childLayer.transform);
			var rect = lastText.GetComponent<RectTransform>();
			var rectPos = rect.anchoredPosition;
			rectPos.x = 0.0f;
			rectPos.y = 0.0f;
			rect.anchoredPosition = rectPos;
				
			var canvasScaler = GameObject.Find("BaseLayer").AddComponent<CanvasScaler>();
			canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			lastText.SetActive(false);
			yield return null;
			lastText.SetActive(true);

			yield return new WaitForSeconds(5.0f);
			
			SceneStatus.Procedure = 2;
			SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "classroom", null);
		}

		IEnumerator Action010Coroutine()
		{
			niccAko.ConditionX = -1.3f;
			niccAko.WalkLeft();
			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			niccAko.ConditionY = 2.0f;
			niccAko.WalkFront();
			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			niccAko.WalkRightNoSpeed();
			yield return null;
			EventManager.Instance.NextTask();
		}

		IEnumerator Action011Coroutine()
		{
			niccMasaki.ConditionY = 1.9f;
			niccMasaki.WalkFront();
			while (true)
			{
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			niccMasaki.ConditionX = 0.5f;
			niccMasaki.WalkLeft();
			while (true)
			{
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			EventManager.Instance.NextTask();
		}

		IEnumerator Action014Coroutine()
		{
			niccMasaki = masaki.AddComponent<NoInputCharacterController>();
			niccAko = ako.AddComponent<NoInputCharacterController>();
			MainCharacterController yusukeMcc = null;
			var mccArr = FindObjectsOfType<MainCharacterController>();
			foreach (var mcc in mccArr)
			{
				mcc.FreezeFlg = true;
				if (mcc.gameObject.name == "yusuke")
				{
					yusukeMcc = mcc;
				}
			}

			yield return null;

			
			niccMasaki.ConditionY = 4.56f;
			if (4.56f < masaki.transform.position.y)
			{
				niccMasaki.WalkFront();
			}
			else
			{
				niccMasaki.WalkBack();				
			}
			
			while (true)
			{
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccMasaki.ConditionX = 3.0f;
			if (3.0f < masaki.transform.position.x)
			{
				niccMasaki.WalkLeft();
			}
			else
			{
				niccMasaki.WalkRight();				
			}
			
			while (true)
			{
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccMasaki.WalkRightNoSpeed();
			
			
			niccAko.ConditionY = 4.56f;
			if (4.56f < ako.transform.position.y)
			{
				niccAko.WalkFront();
			}
			else
			{
				niccAko.WalkBack();				
			}
			
			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccAko.ConditionX = 4.0f;
			if (4.0f < ako.transform.position.x)
			{
				niccAko.WalkLeft();
			}
			else
			{
				niccAko.WalkRight();				
			}
			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccAko.WalkRightNoSpeed();
			yield return new WaitForSeconds(0.5f);

			
			niccYusuke.ConditionY = 4.56f;
			if (0.6f < Mathf.Abs(ClassroomDeskStatus.DeskX - yusuke.transform.position.x)) {
				if (4.56f < yusuke.transform.position.y)
				{
					niccYusuke.WalkFront();
				}
				else
				{
					niccYusuke.WalkBack();				
				}
				while (true)
				{
					if (!niccYusuke.WalkingFlg)
					{
						break;
					}
					yield return null;
				}
			}
			
			if (0.2f < Mathf.Abs(ClassroomDeskStatus.DeskX - yusuke.transform.position.x))
			{
				niccYusuke.ConditionX = ClassroomDeskStatus.DeskX;
				if (ClassroomDeskStatus.DeskX < yusuke.transform.position.x)
				{
					niccYusuke.WalkLeft();
				}
				else
				{
					niccYusuke.WalkRight();				
				}
				
				while (true)
				{
					if (!niccYusuke.WalkingFlg)
					{
						break;
					}
					yield return null;
				}
			}
			yusuke.GetComponent<SpriteRenderer>().sortingOrder = 99;
			niccYusuke.WalkBackNoSpeed();
			
			yield return StartCoroutine(niccYusuke.MoveUpOrDown(5.6f, 0.2f));

			yield return new WaitForSeconds(1.0f);

			if (SceneStatus.HasBroom)
			{
				yusukeMcc.Anim.SetBool("BroomUp", true);
			}
			else
			{
				yusukeMcc.Anim.SetBool("HandsUp", true);
			}

			yield return new WaitForSeconds(2.0f);

			if (SceneStatus.HasBroom)
			{
				yusukeMcc.Anim.SetBool("BroomUp", false);
			}
			else
			{
				yusukeMcc.Anim.SetBool("HandsUp", false);
			}

			yield return null;

			EventManager.Instance.NextTask();

			if (SceneStatus.HasBroom)
			{
				EventManager.Instance.RegisterByForce(506);
				SceneStatus.IsCompletedQuizA = true;
			}
			else
			{
				EventManager.Instance.RegisterByForce(507);
			}
		}

		IEnumerator Action015Coroutine()
		{
			yield return StartCoroutine(niccYusuke.MoveUpOrDown(4.4f, 0.2f));

			yield return new WaitForSeconds(1.0f);

			var mccArr = FindObjectsOfType<MainCharacterController>();
			foreach (var mcc in mccArr)
			{
				mcc.FreezeFlg = false;
			}

			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action016Coroutine()
		{
			yield return StartCoroutine(niccYusuke.MoveUpOrDown(4.4f, 0.2f));

			yield return new WaitForSeconds(1.0f);
			
			var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/common/", "QuizB"),
				new Vector2(0.0f, 0.0f), Quaternion.identity);
			obj.name = "QuizB";
		}

		public void SelectAButton()
		{
			SceneStatus.HasCicada = true;
			EventManager.Instance.NextTask();
			EventManager.Instance.RegisterByForce("classmateHA");
			var classmateH = GameObject.Find("classmateH");
			classmateH.name = "classmateHZ";
		}

		public void SelectBButton()
		{
			EventManager.Instance.NextTask();
			EventManager.Instance.RegisterByForce("classmateHB");
			var classmateH = GameObject.Find("classmateH");
			classmateH.name = "classmateHZ";
		}
	}
}
