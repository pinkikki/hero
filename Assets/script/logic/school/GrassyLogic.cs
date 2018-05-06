using System.Collections;
using script.common.dao;
using script.core.asset;
using script.core.audio;
using script.core.camera;
using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;
using UnityEngine.UI;

namespace script.logic.school
{
	public class GrassyLogic : MonoBehaviour {

		GameObject yusuke;
		NoInputCharacterController niccYusuke;
		GameObject ako;
		NoInputCharacterController niccAko;
		GameObject masaki;
		NoInputCharacterController niccMasaki;
	
		void Start () {
			EventManager.Instance.Register(1000);
		}
	
		void Update () {
		
		}

		public void ActionBgmVolumeDown()
		{
			StartCoroutine(ActionBgmVolumeDownCoroutine());
		}

		public void Action001()
		{
			yusuke = GameObject.Find("yusuke");
			masaki = GameObject.Find("masaki");
			ako = GameObject.Find("ako");
			StartCoroutine(Action001Coroutine());
		}

		public void Action002()
		{
			StartCoroutine(Action002Coroutine());
		}
		
		public void Action003()
		{
			StartCoroutine(Action003Coroutine());
		}
	
		public void Action004()
		{
			Destroy(yusuke.GetComponent<NoInputCharacterController>());
			Destroy(masaki.GetComponent<NoInputCharacterController>());
			Destroy(ako.GetComponent<NoInputCharacterController>());
			yusuke.AddComponent<MainCharacterController>();
			masaki.AddComponent<SubCharacterController>();
			ako.AddComponent<SubCharacterController>();
			SceneStatus.CanFlowEndRoll = true;
			EventManager.Instance.NextTask();
		}
		
		public void Action005()
		{
			masaki.name = "masaki2";
			EventManager.Instance.NextTask();
		}
		
		public void Action006()
		{
			masaki.name = "masaki3";
			EventManager.Instance.NextTask();
		}
		
		public void Action007()
		{
			masaki.name = "masaki4";
			EventManager.Instance.NextTask();
		}
		
		public void Action008()
		{
			masaki.name = "masaki5";
			EventManager.Instance.NextTask();
		}
		
		public void Action009()
		{
			ako.name = "ako2";
			EventManager.Instance.NextTask();
		}
		
		public void Action010()
		{
			ako.name = "ako3";
			EventManager.Instance.NextTask();
		}
		
		public void Action011()
		{
			ako.name = "ako4";
			EventManager.Instance.NextTask();
		}
		
		public void Action012()
		{
			ako.name = "ako5";
			EventManager.Instance.NextTask();
		}
		
		public void Action013()
		{
			StartCoroutine(Action013Coroutine());
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
			StartCoroutine(Action016Coroutine());
		}
		
		public void SelectAButton()
		{
			StartCoroutine(SelectAButtonCoroutine());
			EventManager.Instance.RegisterByForce(1003);
		}
		
		public void SelectBButton()
		{
			EventManager.Instance.NextTask();
			EventManager.Instance.RegisterByForce(1004);
		}

		IEnumerator ActionBgmVolumeDownCoroutine()
		{
			yield return AudioManager.Instance.DownBgmVolume(0.5f, 0.2f);
			EventManager.Instance.NextTask();
		}
		IEnumerator Action001Coroutine()
		{
			SceneLoadManager.Instance.FadeOut(0.3f);
			Destroy(yusuke.GetComponent<MainCharacterController>());
			Destroy(masaki.GetComponent<VChaseCharacterController>());
			Destroy(ako.GetComponent<VChaseCharacterController>());
			yield return new WaitForSeconds(0.3f);
			niccYusuke = yusuke.AddComponent<NoInputCharacterController>();
			niccMasaki = masaki.AddComponent<NoInputCharacterController>();
			niccAko = ako.AddComponent<NoInputCharacterController>();
			
			yield return new WaitForSeconds(0.7f);
		
			SetPosition(yusuke, -7.7f, 6.0f);
			SetPosition(masaki, -7.7f, 5.5f);
			SetPosition(ako, -7.7f, 5.5f);
			niccYusuke.WalkFrontNoSpeed();
			niccMasaki.WalkFrontNoSpeed();
			niccAko.WalkFrontNoSpeed();
            
			SceneLoadManager.Instance.FadeIn(1.0f);
		
			yield return new WaitForSeconds(0.3f);

			niccMasaki.ConditionX = -6.5f;
			niccMasaki.WalkRight();
			niccAko.ConditionY = 5.0f;
			niccAko.WalkFront();

			while (true)
			{
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
		
			Destroy(niccMasaki);
			Destroy(niccAko);
	
			masaki.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			masaki.layer = 11;
			ako.layer = 11;
			masaki.AddComponent<SecretBaseAutoCharacterController>();
			ako.AddComponent<SecretBaseAutoCharacterController>();

			EventManager.Instance.NextTask();
		
		}

		IEnumerator Action002Coroutine()
		{
			yield return new WaitForSeconds(3.0f);
			Destroy(ako.GetComponent<SecretBaseAutoCharacterController>());
			niccAko = ako.AddComponent<NoInputCharacterController>();
			niccAko.Anim = niccAko.gameObject.GetComponent<Animator>();

			Adjustment(niccAko);
			
			yield return new WaitForSeconds(1.0f);
			
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action003Coroutine()
		{
			Destroy(masaki.GetComponent<SecretBaseAutoCharacterController>());
			niccMasaki = masaki.AddComponent<NoInputCharacterController>();
			niccMasaki.Anim = niccMasaki.gameObject.GetComponent<Animator>();

			Adjustment(niccMasaki);
			
			yield return new WaitForSeconds(1.0f);
			
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action013Coroutine()
		{
			var scaleCamera = FindObjectOfType<ScaleCamera>();
			scaleCamera.Initialization = false;
			scaleCamera.Target = null;

			niccYusuke.ConditionY = 8.0f;
			niccYusuke.SpeedFactor = 0.15f;
			niccYusuke.WalkBack();

			while (true)
			{
				if (!niccYusuke.WalkingFlg)
				{
					break;
				}
				yield return null;
			}

			niccYusuke.ConditionX = 2.5f;
			niccYusuke.WalkRight();
			
			while (true)
			{
				if (!niccYusuke.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			EventManager.Instance.NextTask();
		}
		
		IEnumerator Action014Coroutine()
		{
			niccMasaki.ConditionY = 8.0f;
			niccMasaki.SpeedFactor = 0.2f;
			niccMasaki.WalkBack();

			while (true)
			{
				if (!niccMasaki.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccMasaki.ConditionX = 2.5f;
			niccMasaki.WalkRight();
			
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
		
		IEnumerator Action015Coroutine()
		{
			niccAko.ConditionX = -7.7f;
			niccAko.WalkRight();

			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccAko.ConditionY = 6.0f;
			niccAko.WalkBack();
			
			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			yield return new WaitForSeconds(0.1f);
			
			niccAko.WalkFrontNoSpeed();
			
			yield return new WaitForSeconds(2.0f);
			
			niccAko.ConditionY = 8.0f;
			niccAko.SpeedFactor = 0.1f;
			niccAko.WalkBack();
			
			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			niccAko.ConditionX = 2.5f;
			niccAko.WalkRight();
			
			while (true)
			{
				if (!niccAko.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			SceneLoadManager.Instance.FadeOut(2.0f);

			if (AudioManager.Instance != null)
			{
				yield return AudioManager.Instance.DownBgmVolume(3.0f, 0.0f);
				yield return AudioManager.Instance.DownSeVolume(MusicDao.SelectByPrimaryKey(5).MusicName ,3.0f, 0.0f);
			}

			var childLayer = GameObject.Find("BaseLayer/ChildLayer");
			var lastText = (GameObject) Instantiate(
				AssetLoader.Instance.LoadPrefab("prefab/common/", "LastText"), new Vector2(0.0f, 0.0f),
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
			
			yield return new WaitForSeconds(8.0f);
			
			AudioManager.Instance.Destroy();
			SceneLoadManager.Instance.LoadLevelInLoading(10.0f, "classroom_a", null);
			
			EventManager.Instance.NextTask();
		}

		IEnumerator Action016Coroutine()
		{
			Destroy(yusuke.GetComponent<MainCharacterController>());
			niccYusuke = yusuke.AddComponent<NoInputCharacterController>();
			yield return null;
			niccYusuke.ConditionY = 6.0f;
			niccYusuke.WalkFront();

			while (true)
			{
				if (!niccYusuke.WalkingFlg)
				{
					break;
				}
				yield return null;
			}
			
			Destroy(niccYusuke);
			yusuke.AddComponent<MainCharacterController>();
			
			EventManager.Instance.NextTask();
		}

		IEnumerator SelectAButtonCoroutine()
		{
			SceneLoadManager.Instance.FadeOut(0.3f);
			Destroy(yusuke.GetComponent<MainCharacterController>());
			Destroy(masaki.GetComponent<SubCharacterController>());
			Destroy(ako.GetComponent<SubCharacterController>());
			yield return new WaitForSeconds(0.3f);
			niccYusuke = yusuke.AddComponent<NoInputCharacterController>();
			niccMasaki = masaki.AddComponent<NoInputCharacterController>();
			niccAko = ako.AddComponent<NoInputCharacterController>();
			
			yield return new WaitForSeconds(0.7f);
		
			SetPosition(yusuke, -7.7f, 6.0f);
			SetPosition(masaki, -7.7f, 5.0f);
			SetPosition(ako, -8.7f, 5.5f);
			niccYusuke.WalkFrontNoSpeed();
			niccMasaki.WalkBackNoSpeed();
			niccAko.WalkRightNoSpeed();
            
			SceneLoadManager.Instance.FadeIn(1.0f);
		
			yield return new WaitForSeconds(1.0f);

			EventManager.Instance.NextTask();
		}
		
		void SetPosition(GameObject obj, float x, float y)
		{
			var pos = obj.transform.position;
			pos.x = x;
			pos.y = y;
			obj.transform.position = pos;
		}
		
		void Adjustment(NoInputCharacterController nicc)
		{
			if (nicc.transform.position.y > 5.0f)
			{
				if (nicc.transform.position.x < niccYusuke.transform.position.x)
				{
					nicc.WalkRightNoSpeed();
				}
				else
				{
					nicc.WalkLeftNoSpeed();
				}
			}
			
			nicc.WalkBackNoSpeed();
		}
	}
}
