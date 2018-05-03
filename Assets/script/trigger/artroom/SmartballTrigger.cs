using System.Collections;
using script.core.asset;
using script.core.character;
using script.core.operation;
using script.core.scene;
using script.logic.game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace script.trigger.artroom
{
	public class SmartballTrigger : MonoBehaviour
	{
		void Start () {
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke") {
				if (SceneStatus.Procedure == 1 || SceneStatus.Procedure == 2 || SceneStatus.Procedure == 3 || SceneStatus.Procedure == 4)
				{
					SearchButton.Instance.OnRegister(() =>
					{
						var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/school/", "Smartball"),
							new Vector2(0.0f, 0.0f), Quaternion.identity);
						obj.name = "Smartball";
						SceneStatus.LastSearchedArtObject = SceneStatus.ArtObject.Smartball;
						other.gameObject.GetComponent<MainCharacterController>().FreezeFlg = true;
					});
				}
				else if (SceneStatus.Procedure == 5 && SceneStatus.HasMarble)
				{
					SearchButton.Instance.OnRegister(719);
				}
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
