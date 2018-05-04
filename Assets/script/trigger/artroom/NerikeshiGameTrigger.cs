using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class NerikeshiGameTrigger : MonoBehaviour
	{
		private bool isFirst = true;

		public bool IsFirst
		{
			get { return isFirst; }
			set { isFirst = value; }
		}

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.name == "yusuke" && SceneStatus.Procedure == 3)
			{
				if (SceneStatus.HasGlue)
				{
					if (isFirst)
					{
						isFirst = false;
						EventManager.Instance.Register(708);
					}
					else
					{
						EventManager.Instance.Register(722);
					}
				}
				else
				{
					EventManager.Instance.Register(709);
				}
			}
		}
	}
}
