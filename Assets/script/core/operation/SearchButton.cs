using Assets.script.core.@event;
using Assets.script.core.monoBehaviour;
using UnityEngine.UI;

namespace Assets.script.core.operation
{
	public class SearchButton : SingletonMonoBehaviour<SearchButton>
	{
		Button button;

		void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}

		void Start () {
			// 非アクティブ状態だとインスタンスを取得できなくなるので、ここで取得しておく
			var obj = Instance;
			gameObject.SetActive(false);
			button = gameObject.GetComponent<Button>();
			OnDialog();
		}

		void Update () {

		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void OnDialog()
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(Dialog);
		}

		public void Dialog()
		{
			EventManager.Instance.Register(99999);
		}

		public void OnRegister(int eventId)
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => Register(eventId));
		}

		public void Register(int eventId)
		{
			EventManager.Instance.Register(eventId);
		}

		public void OnNop()
		{
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(Nop);
		}

		public void Nop()
		{

		}
	}
}
