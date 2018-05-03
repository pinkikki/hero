using System;
using System.Collections.Generic;
using script.core.asset;
using script.core.character;
using script.core.operation;
using script.core.scene;
using UnityEngine;

namespace script.trigger.artroom
{
	public class ArtworkTrigger : MonoBehaviour
	{
		private static readonly Dictionary<String, SceneStatus.ArtObject> mapping = new Dictionary<string, SceneStatus.ArtObject>()
		{
			{"ArtworkA", SceneStatus.ArtObject.ArtworkA},
			{"ArtworkB", SceneStatus.ArtObject.ArtworkB},
			{"ArtworkC", SceneStatus.ArtObject.ArtworkC},
			{"ArtworkD", SceneStatus.ArtObject.ArtworkD},
			{"ArtworkE", SceneStatus.ArtObject.ArtworkE},
			{"ArtworkF", SceneStatus.ArtObject.ArtworkF},
			{"ArtworkG", SceneStatus.ArtObject.ArtworkG}
		};
		
		void Start () {
		
		}
	
		void Update () {
		
		}
		
		void OnCollisionEnter2D(Collision2D other) {
			if (other.gameObject.name == "yusuke") {
				SearchButton.Instance.OnRegister(() =>
				{
					var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/school/", gameObject.name),
						new Vector2(0.0f, 0.0f), Quaternion.identity);
					obj.name = gameObject.name + "Preview";
					SceneStatus.LastSearchedArtObject = mapping[gameObject.name];
					other.gameObject.GetComponent<MainCharacterController>().FreezeFlg = true;
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
