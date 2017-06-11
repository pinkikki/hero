using System.Collections;
using Assets.script.core.character;
using Assets.script.core.@event;
using Assets.script.core.scene;
using UnityEngine;

namespace Assets.script.logic.school
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
			Destroy(niccMasaki);
			Destroy(niccAko);
			yusuke.AddComponent<MainCharacterController>();
			var autoMasaki = masaki.AddComponent<AutoCharacterController>();
			var autoAko = ako.AddComponent<AutoCharacterController>();
			autoMasaki.RepeatNum = 20;
			autoMasaki.SpeedFactor = 0.10f;
			autoAko.RepeatNum = 30;
			autoAko.SpeedFactor = 0.05f;
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
	}
}
