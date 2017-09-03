﻿using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class UnLockGameTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.Procedure == 3 && SceneStatus.HasQuizD)
			{
				if (SceneStatus.IsFinishedFirstUnLocking)
				{
					EventManager.Instance.Register(805);
				}
				else
				{
					EventManager.Instance.Register(804);
				}
			}
		}
	}
}