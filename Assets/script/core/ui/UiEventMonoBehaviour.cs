using Assets.script.core.monoBehaviour;

namespace Assets.script.core.ui
{
	public class UiEventMonoBehaviour : SingletonMonoBehaviour<UiEventMonoBehaviour>
	{
		void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		void Start()
		{
		}

		void Update()
		{
		}
	}
}
