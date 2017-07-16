using script.core.asset;
using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class SmartballTrigger : MonoBehaviour {

		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke" && SceneStatus.Procedure == 1) {
				SearchButton.Instance.OnRegister(() =>
				{
					var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/school/", "Smartball"),
						new Vector2(0.0f, 0.0f), Quaternion.identity);
					obj.name = "Smartball";
					SceneStatus.LastSearchedArtObject = SceneStatus.ArtObject.Smartball;
				});
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
