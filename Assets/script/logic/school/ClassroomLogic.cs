using System.Collections;
using System.Linq;
using Assets.script.core.camera;
using Assets.script.core.character;
using Assets.script.core.@event;
using Assets.script.core.monoBehaviour;
using Assets.script.core.scene;
using UnityEngine;

namespace Assets.script.logic.school
{
	public class ClassroomLogic : SingletonMonoBehaviour<ClassroomLogic>
	{
		GameObject teacher;
		NoInputCharacterController niccTeacher;
		bool isRegistered;

		void Start()
		{
			if (SceneStatus.Procedure == 2)
			{
				EventManager.Instance.Register(502);
			}
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
