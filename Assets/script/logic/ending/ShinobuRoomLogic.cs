using System.Collections;
using script.core.asset;
using script.core.character;
using script.core.@event;
using script.core.scene;
using UnityEngine;

namespace script.logic.ending
{
	public class ShinobuRoomLogic : MonoBehaviour {

		GameObject shinobu;
		NoInputCharacterController niccShinobu;
		GameObject bookA;
		GameObject bookB;
		
		void Start () {

			if (SceneStatus.IsCompletedShinobuRoomA)
			{
				shinobu = GameObject.Find("shinobu");
				niccShinobu = shinobu.GetComponent<NoInputCharacterController>();
				bookA = GameObject.Find("book_a");
				bookB = GameObject.Find("book_b");
				bookA.gameObject.SetActive(false);
				bookB.gameObject.SetActive(false);
				EventManager.Instance.Register(1502);
			}
			else
			{
				shinobu = GameObject.Find("shinobu");
				niccShinobu = shinobu.GetComponent<NoInputCharacterController>();
				bookA = GameObject.Find("book_a");
				bookB = GameObject.Find("book_b");
				bookB.gameObject.SetActive(false);
				EventManager.Instance.Register(1501);
			}
		}
	
		void Update () {
		
		}
		
		public void Action001()
		{
			StartCoroutine(Action001Coroutine());
		}
		
		public void Action002()
		{
			StartCoroutine(Action002Coroutine());
		}

		IEnumerator Action001Coroutine()
		{
			niccShinobu.Anim.SetBool("sit", true);
			
			yield return new WaitForSeconds(5.0f);
			
			bookA.gameObject.SetActive(false);
			bookB.gameObject.SetActive(true);
			
			yield return new WaitForSeconds(3.0f);

			SceneStatus.IsCompletedShinobuRoomA = true;
			
			SceneLoadManager.Instance.LoadLevelInLoading(2.0f, "shinobu_room", null);
			EventManager.Instance.NextTask();
			
		}
		
		IEnumerator Action002Coroutine()
		{
			niccShinobu.Anim.SetBool("sit", true);
			shinobu.GetComponent<SpriteRenderer>().sortingLayerName = "fieldUpObject";
			
			yield return new WaitForSeconds(2.0f);
			
			var quizBig = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/ending/", "quizpaper_big"),
				new Vector2(0.0f, -0.57f), Quaternion.identity);

			yield return new WaitForSeconds(2.0f);
			
			niccShinobu.Anim.SetBool("write", true);
			
			yield return new WaitForSeconds(7.0f);
			
			Destroy(quizBig);
			
			yield return new WaitForSeconds(0.2f);
			
			Instantiate(AssetLoader.Instance.LoadPrefab("prefab/ending/", "quizpaper_small"),
				new Vector2(0.0f, -0.50f), Quaternion.identity);

			yield return new WaitForSeconds(1.0f);
			
			Instantiate(AssetLoader.Instance.LoadPrefab("prefab/ending/", "quizpaper_small"),
				new Vector2(-0.25f, -0.50f), Quaternion.identity);

			yield return new WaitForSeconds(1.0f);
			
			Instantiate(AssetLoader.Instance.LoadPrefab("prefab/ending/", "quizpaper_small"),
				new Vector2(0.25f, -0.50f), Quaternion.identity);

			yield return new WaitForSeconds(1.0f);
			
			Instantiate(AssetLoader.Instance.LoadPrefab("prefab/ending/", "quizpaper_small"),
				new Vector2(-0.50f, -0.50f), Quaternion.identity);

			yield return new WaitForSeconds(1.0f);
			
			Instantiate(AssetLoader.Instance.LoadPrefab("prefab/ending/", "quizpaper_small"),
				new Vector2(0.50f, -0.50f), Quaternion.identity);

			yield return new WaitForSeconds(1.0f);
			
			SceneLoadManager.Instance.LoadLevelInLoading(2.0f, "classroom_b", null);
			EventManager.Instance.NextTask();
			
		}
	}
}
