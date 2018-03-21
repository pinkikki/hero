using System.Collections;
using System.Linq;
using script.core.@event;
using UnityEngine;
using UnityEngine.UI;

namespace script.logic.ending
{
	public class CreditLogic : MonoBehaviour {

		void Start () {
			EventManager.Instance.Register(2101);
		}
	
		void Update () {
		
		}
		
		public void Action001()
		{
			StartCoroutine(Action001Coroutine());
		}
		
		public void Action002()
		{
			EventManager.Instance.NextTask();
		}

		IEnumerator Action001Coroutine()
		{
			var text1 = GameObject.Find("Text1").GetComponent<Text>();
			var text2 = GameObject.Find("Text2").GetComponent<Text>();
			var text3 = GameObject.Find("Text3").GetComponent<Text>();
			
			text2.text = "ヒーロー";
			yield return TextIn(text2);
			yield return new WaitForSeconds(5.0f);
			yield return TextOut(text2);
			Clear(text1, text2, text3);
			
			text2.text = "スペシャルサンクス";
			yield return TextIn(text2);
			yield return new WaitForSeconds(3.0f);
			yield return TextOut(text2);
			Clear(text1, text2, text3);
			
			text1.text = "アートモバイル　原案・音楽・歌";
			yield return TextIn(text1);
			yield return new WaitForSeconds(5.0f);
			
			text2.text = "ぽくり　原案・絵・シナリオ";
			yield return TextIn(text2);
			yield return new WaitForSeconds(5.0f);
			
			text3.text = "ピンキッキ　原案・プログラム";
			yield return TextIn(text3);
			yield return new WaitForSeconds(5.0f);
			yield return AllTextOut(text1,text2,text3);
			Clear(text1, text2, text3);
			
			text2.text = "おわり";
			yield return TextIn(text2);
			yield return new WaitForSeconds(5.0f);
			yield return TextOut(text2);
			Clear(text1, text2, text3);

			yield return new WaitForSeconds(3.0f);
			
			EventManager.Instance.NextTask();
		}

		IEnumerator TextIn(Text text)
		{
			var time = 0.0f;
			var fadeOutInterval = 2.0f;
			while (time <= fadeOutInterval)
			{
				text.color = new Color(255, 255, 255, Mathf.Lerp(0f, 1f, time / fadeOutInterval));
				time += Time.deltaTime;
				yield return null;
			}
		}
		
		IEnumerator TextOut(Text text)
		{
			var time = 0.0f;
			var fadeOutInterval = 2.0f;
			while (time <= fadeOutInterval)
			{
				text.color = new Color(255, 255, 255, Mathf.Lerp(1f, 0f, time / fadeOutInterval));
				time += Time.deltaTime;
				yield return null;
			}
		}
		
		IEnumerator AllTextOut(params Text[] args)
		{
			var time = 0.0f;
			var fadeOutInterval = 2.0f;
			while (time <= fadeOutInterval)
			{
				foreach (var text in args)
				{
					text.color = new Color(255, 255, 255, Mathf.Lerp(1f, 0f, time / fadeOutInterval));
					time += Time.deltaTime;
					yield return null;	
				}
			}
		}

		void Clear(params Text[] args)
		{
			args.Select(t => t.text = "");
		}
	}
}
