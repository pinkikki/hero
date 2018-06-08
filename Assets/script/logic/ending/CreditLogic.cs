using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using script.common.dao;
using script.common.entity;
using script.core.audio;
using script.core.@event;
using UnityEngine;
using UnityEngine.UI;

namespace script.logic.ending
{
    public class CreditLogic : MonoBehaviour
    {

        void Start()
        {
            EventManager.Instance.Register(2101);
        }

        void Update()
        {
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
            var text1Obj = GameObject.Find("Text1");
            var text2Obj = GameObject.Find("Text2");
            var text3Obj = GameObject.Find("Text3");
            var text1 = text1Obj.GetComponent<Text>();
            var text2 = text2Obj.GetComponent<Text>();
            var text3 = text3Obj.GetComponent<Text>();
            var text1_1 = text1Obj.transform.Find("Text1-1").GetComponent<Text>();
            var text2_1 = text2Obj.transform.Find("Text2-1").GetComponent<Text>();
            var text3_1 = text3Obj.transform.Find("Text3-1").GetComponent<Text>();
            var pokuri = text1Obj.transform.Find("Text1-1/Icon").GetComponent<Image>();
            var pinkikki = text2Obj.transform.Find("Text2-1/Icon").GetComponent<Image>();
            var artmobile = text3Obj.transform.Find("Text3-1/Icon").GetComponent<Image>();
            var logo = GameObject.Find("Logo").GetComponent<Image>();
            var specialThanks = GameObject.Find("SpecialThanks").GetComponent<Text>();


            text2.text = "夏休みのヒーロー";
            yield return TextIn(text2);
            yield return new WaitForSeconds(3.0f);
            yield return TextOut(text2);
            Clear(text1, text2, text3);

            text1.text = "Special Thanks";
            yield return TextIn(text1);
            yield return new WaitForSeconds(2.0f);
            yield return TextIn(specialThanks);
            yield return new WaitForSeconds(2.0f);
            yield return AllTextOut(text1, specialThanks);
            Clear(text1, text2, text3, specialThanks);

            text1.text = "絵・シナリオ・原案";
            yield return TextIn(text1);
            yield return new WaitForSeconds(1.0f);
            yield return TextAndImageIn(text1_1, pokuri);
            yield return new WaitForSeconds(2.0f);

            text2.text = "システム・原案";
            yield return TextIn(text2);
            yield return new WaitForSeconds(1.0f);
            yield return TextAndImageIn(text2_1, pinkikki);
            yield return new WaitForSeconds(2.0f);

            text3.text = "音楽・歌・原案";
            yield return TextIn(text3);
            yield return new WaitForSeconds(1.0f);
            yield return TextAndImageIn(text3_1, artmobile);
            yield return new WaitForSeconds(2.0f);

            yield return AllTextAndImageOut(text1, text2, text3, text1_1, text2_1, text3_1, pokuri, pinkikki, artmobile);
            Clear(text1, text2, text3, text1_1, text2_1, text3_1);

            text2.text = "presented by torokkorio";

            yield return TextAndImageIn(text2, logo);
            yield return new WaitForSeconds(5.0f);
            yield return TextAndImageOut(text2, logo);
            Clear(text1, text2, text3);

            yield return new WaitForSeconds(5.0f);

            MusicEntity entity = MusicDao.SelectByPrimaryKey(8);
            AudioManager.Instance.PlayBgm(entity.MusicName, float.Parse(entity.Time));

            yield return new WaitForSeconds(5.0f);

            EventManager.Instance.NextTask();
        }

        IEnumerator Action002Coroutine()
        {
            yield return new WaitForSeconds(3.0f);

            var text2 = GameObject.Find("Text2").GetComponent<Text>();
            text2.text = "おわり";
            yield return TextIn(text2);
            yield return new WaitForSeconds(5.0f);
            yield return TextOut(text2);
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
            text.color = new Color(255, 255, 255, 1f);
            yield return null;
        }

        IEnumerator TextAndImageIn(Text text, Image image)
        {
            var time = 0.0f;
            var fadeOutInterval = 2.0f;
            while (time <= fadeOutInterval)
            {
                text.color = new Color(255, 255, 255, Mathf.Lerp(0f, 1f, time / fadeOutInterval));
                image.color = new Color(255, 255, 255, Mathf.Lerp(0f, 1f, time / fadeOutInterval));
                time += Time.deltaTime;
                yield return null;
            }
            text.color = new Color(255, 255, 255, 1f);
            image.color = new Color(255, 255, 255, 1f);
            yield return null;
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
            text.color = new Color(255, 255, 255, 0f);
            yield return null;
        }

        IEnumerator TextAndImageOut(Text text, Image image)
        {
            var time = 0.0f;
            var fadeOutInterval = 2.0f;
            while (time <= fadeOutInterval)
            {
                text.color = new Color(255, 255, 255, Mathf.Lerp(1f, 0f, time / fadeOutInterval));
                image.color = new Color(255, 255, 255, Mathf.Lerp(1f, 0f, time / fadeOutInterval));
                time += Time.deltaTime;
                yield return null;
            }
            text.color = new Color(255, 255, 255, 0f);
            image.color = new Color(255, 255, 255, 0f);
            yield return null;
        }

        IEnumerator AllTextAndImageOut(params Object[] args)
        {
            var time = 0.0f;
            var fadeOutInterval = 2.0f;
            while (time <= fadeOutInterval)
            {
                foreach (var obj in args)
                {
                    if (obj.GetType() == typeof(Text))
                    {
                        ((Text) obj).color = new Color(255, 255, 255, Mathf.Lerp(1f, 0f, time / fadeOutInterval));
                    }
                    else if(obj.GetType() == typeof(Image))
                    {
                        ((Image) obj).color = new Color(255, 255, 255, Mathf.Lerp(1f, 0f, time / fadeOutInterval));
                    }
                    time += Time.deltaTime;
                    yield return null;
                }
            }
            foreach (var obj in args)
            {
                if (obj.GetType() == typeof(Text))
                {
                    ((Text) obj).color = new Color(255, 255, 255, 0f);
                }
                else if(obj.GetType() == typeof(Image))
                {
                    ((Image) obj).color = new Color(255, 255, 255, 0f);
                }
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
            foreach (var text in args)
            {
                text.color = new Color(255, 255, 255, 0f);
                yield return null;
            }
        }

        void Clear(params Text[] args)
        {
            args.Select(t => t.text = "");
        }
    }
}