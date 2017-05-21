﻿using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.logic.school
{
	public class BroomLogic : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke" && !SceneStatus.HasBroom && SceneStatus.Procedure == 3) {
				SearchButton.Instance.OnRegister(505);
			}
		}

		void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke") {
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
