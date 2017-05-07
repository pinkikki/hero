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
		}

		void Update()
		{
			if (isRegistered || 17 > EventManager.Instance.CompleteEventSet.Count) return;
			if (EventManager.Instance.CompleteEventSet.Count(e => e < 18) != 17) return;
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
			yield return null;
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
