using System.Collections;
using System.Collections.Generic;
using script.core.operation;
using script.core.scene;
using UnityEngine;

public class MudDumplingsTrigger : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.name == "yusuke" && SceneStatus.CanGetMudDumplings && !SceneStatus.HasMudDumplings && SceneStatus.Procedure == 2)
		{
			SearchButton.Instance.OnRegister(802);
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.name == "yusuke")
		{
			SearchButton.Instance.OnDialog();
		}
	}
}
