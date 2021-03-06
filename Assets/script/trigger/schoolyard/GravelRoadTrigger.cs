﻿using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.schoolyard
{
	public class GravelRoadTrigger : MonoBehaviour
	{
		void Start()
		{
		}

		void Update()
		{
		}

		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.name == "yusuke" && (!SceneStatus.HasGraveRoadA ||
			    (SceneStatus.CanGetGraveRoadB && !SceneStatus.HasGraveRoadB)) && SceneStatus.Procedure == 1)
			{
				SearchButton.Instance.OnRegister(801);
			}
		}

		void OnTriggerExit2D(Collider2D other)
		{
			if (other.gameObject.name == "yusuke")
			{
				SearchButton.Instance.OnDialog();
			}
		}
	}
}
