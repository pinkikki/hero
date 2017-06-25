using script.core.monoBehaviour;

namespace script.core.ui
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
