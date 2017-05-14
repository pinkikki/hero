using System.Collections;
using System.Linq;
using script.core.asset;
using script.core.camera;
using script.core.character;
using script.core.@event;
using script.core.monoBehaviour;
using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.logic.school
{
	public class ClassroomLogic : SingletonMonoBehaviour<ClassroomLogic>
	{
		GameObject teacher;
		NoInputCharacterController niccTeacher;
		GameObject ako;
		NoInputCharacterController niccAko;
		GameObject masaki;
		NoInputCharacterController niccMasaki;
		bool isRegistered;

		void Start()
		{
			if (SceneStatus.Procedure == 2)
			{
				EventManager.Instance.Register(502);
			}
			else if (SceneStatus.Procedure == 3)
			{
				GameObject.Find("chair_yusuke").AddComponent<QuizPaperALogic>();
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
			Destroy(QuizPaperALogic.Instance);
			Destroy(GameObject.Find("QuizA"));
			EventManager.Instance.NextTask();
		}

		public void Action010()
		{
			ako = GameObject.Find("ako");
			niccAko = ako.GetComponent<NoInputCharacterController>();
			masaki = GameObject.Find("masaki");
			niccMasaki = masaki.GetComponent<NoInputCharacterController>();
			StartCoroutine(Action010Coroutine());
		}

		public void Action011()
		{
			masaki = GameObject.Find("masaki");
			niccMasaki = masaki.GetComponent<NoInputCharacterController>();
			StartCoroutine(Action011Coroutine());
		}

		public void Action012()
		{
			var yusuke = GameObject.Find("yusuke");
			var vcccAko = ako.AddComponent<VChaseCharacterController>();
			var vcccMasaki = masaki.AddComponent<VChaseCharacterController>();
			vcccAko.Target = yusuke;
			vcccAko.MinDestNum = 1.6f;
			vcccMasaki.Target = yusuke;
			vcccMasaki.MinDestNum = 0.8f;
			EventManager.Instance.NextTask();
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
			niccMasaki.ConditionY = 1.7f;
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

		public void SelectAButton()
		{
			SceneStatus.HasItem1 = true;
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
