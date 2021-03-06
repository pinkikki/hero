﻿using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.classroom
{
	public class BroomTrigger : MonoBehaviour
	{
		void Start()
		{
		}

		void Update()
		{
		}

		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke" && !SceneStatus.HasBroom && SceneStatus.Procedure == 3)
			{
				SearchButton.Instance.OnRegister(505);
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
}
