using System.Collections;
using System.Collections.Generic;
using script.core.character;
using script.core.@event;
using UnityEngine;

namespace script.logic.game
{
	public class ChickenRoomLogic : MonoBehaviour {

		GameObject yusuke;
		NoInputCharacterController niccYusuke;
		GameObject ako;
		NoInputCharacterController niccAko;
		GameObject masaki;
		NoInputCharacterController niccMasaki;
		
		void Start () {
			EventManager.Instance.Register(901);
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
			EventManager.Instance.NextTask();
		}
		
		public void Action003()
		{
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action001Coroutine()
		{
			var doorCollider = GameObject.Find("entrance_door").GetComponent<BoxCollider2D>();
			doorCollider.enabled = false;
			niccYusuke.ConditionY = -1.0f;
			niccYusuke.WalkBack();
			niccMasaki.ConditionY = -2.3f;
			niccMasaki.WalkBack();
			niccAko.ConditionY = -3.4f;
			niccAko.WalkBack();
			while (true)
			{
				if (!niccYusuke.WarlkingFlg && !niccMasaki.WarlkingFlg && !niccAko.WarlkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccYusuke.WalkLeftNoSpeed();
			niccMasaki.WalkLeftNoSpeed();
			niccAko.WalkLeftNoSpeed();
			yield return null;
			yield return new WaitForSeconds(1.0f);
			doorCollider.enabled = true;
			EventManager.Instance.NextTask();
		}
	}
}
