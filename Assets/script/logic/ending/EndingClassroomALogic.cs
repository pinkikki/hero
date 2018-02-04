using System.Collections;
using System.Collections.Generic;
using script.core.character;
using script.core.@event;
using script.core.message;
using script.core.operation;
using script.core.scene;
using UnityEngine;

public class EndingClassroomALogic : MonoBehaviour {

	GameObject yusuke;
	NoInputCharacterController niccYusuke;
	
	void Start () {
		EventManager.Instance.Register(1101);
		SearchButton.Instance.Hide();
	}
	
	void Update () {
		
	}
	
	public void Action001()
	{
		yusuke = GameObject.Find("yusuke");
		niccYusuke = yusuke.GetComponent<NoInputCharacterController>();
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
		StartCoroutine(Action004Coroutine());
	}
	
	public void Action005()
	{
		StartCoroutine(Action005Coroutine());
	}
	
	public void Action006()
	{
		StartCoroutine(Action006Coroutine());
	}
	
	public void Action007()
	{
		StartCoroutine(Action007Coroutine());
	}

	IEnumerator Action001Coroutine()
	{
		niccYusuke.ConditionX = 1.1f;
		niccYusuke.WalkLeft();

		yield return checkWalking(niccYusuke);

		niccYusuke.ConditionY = 5.2f;
		niccYusuke.WalkBack();

		yield return checkWalking(niccYusuke);
		
		yield return new WaitForSeconds(0.5f);
		
		EventManager.Instance.NextTask();
	}
	
	IEnumerator Action002Coroutine()
	{
		yield return new WaitForSeconds(4.5f);
				
		EventManager.Instance.NextTask();
	}
	
	IEnumerator Action003Coroutine()
	{
		yield return new WaitForSeconds(4.5f);
				
		EventManager.Instance.NextTask();
	}
	
	IEnumerator Action004Coroutine()
	{
		
		yield return new WaitForSeconds(4.5f);
		
		MessageManager.Instance.Hide();
		
		niccYusuke.ConditionX = -0.1f;
		niccYusuke.WalkLeft();

		yield return checkWalking(niccYusuke);
		
		yield return new WaitForSeconds(0.1f);
		niccYusuke.WalkBackNoSpeed();
		
		yield return new WaitForSeconds(0.5f);
		
		EventManager.Instance.NextTask();
	}
	
	IEnumerator Action005Coroutine()
	{
		yield return new WaitForSeconds(4.5f);
				
		EventManager.Instance.NextTask();
	}
	
	IEnumerator Action006Coroutine()
	{
		yield return new WaitForSeconds(4.5f);
		
		MessageManager.Instance.Hide();
		
		yield return new WaitForSeconds(2.5f);

		var shinobu = GameObject.Find("shinobu");
		var ako = GameObject.Find("ako");
		var blank = GameObject.Find("black");
		
		var shinobuSprite = shinobu.GetComponent<SpriteRenderer>();
		var akoSprite = ako.GetComponent<SpriteRenderer>();
		var blackSprite = blank.GetComponent<SpriteRenderer>();

		shinobuSprite.sortingLayerName = "fieldUpObject";
		shinobuSprite.sortingOrder = 1;
		akoSprite.sortingLayerName = "fieldUpObject";
		akoSprite.sortingOrder = 1;
		
		var time = 0.0f;
		while (time <= 3.0f) {
			blackSprite.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, time / 3.0f));
			time += Time.deltaTime;
			yield return null;
		}
		
		yield return new WaitForSeconds(1.0f);

		var niccShinobu = shinobu.GetComponent<NoInputCharacterController>();
		niccShinobu.WalkRightNoSpeed();
		
		yield return new WaitForSeconds(2.0f);
		
		EventManager.Instance.NextTask();
	}
	
	IEnumerator Action007Coroutine()
	{
		yield return new WaitForSeconds(4.5f);
		MessageManager.Instance.Hide();
				
		SceneLoadManager.Instance.LoadLevelInLoading(1.0f, "schoolyard_a", null);
		EventManager.Instance.NextTask();
	}

	IEnumerator checkWalking(NoInputCharacterController nicc)
	{
		while (true)
		{
			if (!niccYusuke.WalkingFlg)
			{
				break;
			}
			yield return null;
		}
	}
}
