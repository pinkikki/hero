using System.Collections;
using System.Linq;
using script.core.asset;
using script.core.camera;
using script.core.character;
using script.core.@event;
using script.core.monoBehaviour;
using script.core.operation;
using script.core.scene;
using script.trigger.classroom;
using UnityEngine;

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
				EventManager.Instance.Register(500);
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
		}

		void Update()
		{
			// TODO 1を17に変える事！
			if (isRegistered || 1 > EventManager.Instance.CompleteEventSet.Count) return;
			// TODO 1を17に変える事！
			if (EventManager.Instance.CompleteEventSet.Count(e => e < 18) != 1) return;
			isRegistered = true;
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
			SceneStatus.Procedure = 2;
			SceneLoadManager.Instance.LoadLevelInLoading(3.0f, "classroom", null);
		}

		public void Action003()
		{
			SceneStatus.Procedure = 3;
			SceneLoadManager.Instance.LoadLevelInLoading(3.0f, "classroom", null);
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
			EventManager.Instance.NextTask();
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
			var vcccAko = ako.AddComponent<VChaseCharacterController>();
			var vcccMasaki = masaki.AddComponent<VChaseCharacterController>();
			vcccAko.Target = yusuke;
			vcccAko.MinDestNum = 1.6f;
			vcccMasaki.Target = yusuke;
			vcccMasaki.MinDestNum = 0.8f;
			SceneStatus.HasQuizA = true;
			EventManager.Instance.NextTask();
		}

		public void Action013()
		{
			SceneStatus.HasBroom = true;
			EventManager.Instance.NextTask();
		}

		public void Action014()
		{
			niccYusuke = yusuke.AddComponent<NoInputCharacterController>();
			var rig = yusuke.GetComponent<Rigidbody2D>();
			rig.isKinematic = true;
			var box = yusuke.GetComponent<BoxCollider2D>();
			box.enabled = false;
			yusuke.GetComponent<SpriteRenderer>().sortingOrder = 99;
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
			var vcccAko = ako.GetComponent<VChaseCharacterController>();
			vcccAko.enabled = true;
			var vcccMasaki = masaki.GetComponent<VChaseCharacterController>();
			vcccMasaki.enabled = true;
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
				if (!niccTeacher.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			niccTeacher.WalkBackNoSpeed();
			yield return null;
			EventManager.Instance.NextTask();
		}

		IEnumerator Action010Coroutine()
		{
			niccAko.ConditionX = -1.3f;
			niccAko.WalkLeft();
			while (true)
			{
				if (!niccAko.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			niccAko.ConditionY = 1.7f;
			niccAko.WalkFront();
			while (true)
			{
				if (!niccAko.WarlkingFlg)
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
				if (!niccMasaki.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			niccMasaki.ConditionX = 0.5f;
			niccMasaki.WalkLeft();
			while (true)
			{
				if (!niccMasaki.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			EventManager.Instance.NextTask();
		}

		IEnumerator Action014Coroutine()
		{
			niccMasaki.ConditionX = 3.0f;
			niccMasaki.WalkLeft();
			while (true)
			{
				if (!niccMasaki.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			niccMasaki.WalkRightNoSpeed();
			niccAko.ConditionX = 4.0f;
			niccAko.WalkLeft();
			while (true)
			{
				if (!niccAko.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			niccAko.WalkRightNoSpeed();
			yield return new WaitForSeconds(0.5f);

			yield return StartCoroutine(niccYusuke.MoveUpOrDown(5.6f, 0.2f));

			yield return new WaitForSeconds(1.0f);

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
