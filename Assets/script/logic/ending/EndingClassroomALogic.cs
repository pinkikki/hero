using System.Collections;
using System.Collections.Generic;
using script.core.@event;
using UnityEngine;

public class EndingClassroomALogic : MonoBehaviour {

	void Start () {
		EventManager.Instance.Register(1101);
	}
	
	void Update () {
		
	}
}
