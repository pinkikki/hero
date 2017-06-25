using System.Collections;
using script.core.character;
using script.core.@event;
using script.core.scene;
using script.trigger.artroom;
using UnityEngine;

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
		
		void Start () {
			if (SceneStatus.Procedure == 1)
			{
				EventManager.Instance.Register(701);
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
		
		IEnumerator Action001Coroutine()
		{
			var doorCollider = GameObject.Find("door").GetComponent<BoxCollider2D>();
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
