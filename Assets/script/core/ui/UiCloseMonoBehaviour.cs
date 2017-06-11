using UnityEngine;

namespace Assets.script.core.ui
{
	public class UiCloseMonoBehaviour : MonoBehaviour
	{
		void Start()
		{
		}

		void Update()
		{
		}

		public void Close()
		{
			Destroy(gameObject);
		}
	}
}
