using System.Collections;
using script.common.dao;
using script.common.entity;
using script.core.asset;
using script.core.audio;
using script.core.camera;
using script.core.character;
using script.core.@event;
using script.core.hint;
using script.core.operation;
using script.core.scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace script.logic.game
{
    public class ChickenRoomLogic : MonoBehaviour
    {
        GameObject yusuke;
        NoInputCharacterController niccYusuke;
        GameObject ako;
        NoInputCharacterController niccAko;
        GameObject masaki;
        NoInputCharacterController niccMasaki;
        GameObject chickenTarget;
        ScaleCamera scaleCamera;

        void Start()
        {
            HelpManager.Instance.Hide();
            MusicEntity entity = MusicDao.SelectByPrimaryKey(1);
            if (!AudioManager.Instance.Playing(entity.MusicName))
            {
                AudioManager.Instance.PlayBgm(entity.MusicName, float.Parse(entity.Time));
                SearchButton.Instance.Show();
            }

            EventManager.Instance.Register(901);
        }

        void Update()
        {
        }

        public void Action001()
        {
            yusuke = GameObject.Find("yusuke");
            niccYusuke = yusuke.GetComponent<NoInputCharacterController>();
            masaki = GameObject.Find("masaki");
            niccMasaki = masaki.GetComponent<NoInputCharacterController>();
            ako = GameObject.Find("ako");
            niccAko = ako.GetComponent<NoInputCharacterController>();
            chickenTarget = GameObject.Find("chicken_target");
            StartCoroutine(Action001Coroutine());
        }

        public void Action002()
        {
            StartCoroutine(Action002Coroutine());
        }

        public void Action003()
        {
            StartCoroutine(Action003Coroutine());
        }

        public void Action004()
        {
            StartCoroutine(Action004Coroutine());
        }
        
        public void Action005()
        {
            StartCoroutine(Action005Coroutine());
        }
        
        public void Action006()
        {
            StartCoroutine(Action006Coroutine());
        }
        
        public void Action007()
        {
            var obj = (GameObject) Instantiate(AssetLoader.Instance.LoadPrefab("prefab/common/", "QuizE"),
                new Vector2(0.0f, 0.0f), Quaternion.identity);
            obj.name = "QuizE";
            SceneStatus.HasQuizE = true;
        }
        
        public void Action008()
        {
            StartCoroutine(Action008Coroutine());
        }

        IEnumerator Action001Coroutine()
        {
            var doorCollider = GameObject.Find("entrance_door").GetComponent<BoxCollider2D>();
            doorCollider.enabled = false;
            niccYusuke.ConditionY = -1.0f;
            niccYusuke.WalkBack();
            niccMasaki.ConditionY = -2.3f;
            niccMasaki.WalkBack();
            niccAko.ConditionY = -3.4f;
            niccAko.WalkBack();
            while (true)
            {
                if (!niccYusuke.WalkingFlg && !niccMasaki.WalkingFlg && !niccAko.WalkingFlg)
                {
                    break;
                }
                yield return null;
            }

            niccYusuke.WalkLeftNoSpeed();
            niccMasaki.WalkLeftNoSpeed();
            niccAko.WalkLeftNoSpeed();
            yield return null;
            yield return new WaitForSeconds(1.0f);
            doorCollider.enabled = true;
            EventManager.Instance.NextTask();
        }

        IEnumerator Action002Coroutine()
        {
            scaleCamera = FindObjectOfType<ScaleCamera>();
            scaleCamera.Initialization = false;
            scaleCamera.Target = chickenTarget;
            scaleCamera.LeapBaseValue = 1.0f;

            while (true)
            {
                if (0 <= scaleCamera.transform.position.x)
                {
                    break;
                }
                yield return null;
            }

            yield return new WaitForSeconds(1.0f);

            EventManager.Instance.NextTask();
        }

        IEnumerator Action003Coroutine()
        {
            scaleCamera.Initialization = false;
            scaleCamera.Target = yusuke;
            scaleCamera.LeapBaseValue = 1.5f;

            while (true)
            {
                if (scaleCamera.transform.position.x <= yusuke.transform.position.x)
                {
                    break;
                }
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            EventManager.Instance.NextTask();
        }
        
        IEnumerator Action004Coroutine()
        {            
            var masakiRigidbody = masaki.GetComponent<Rigidbody2D>();
            masakiRigidbody.velocity = Vector2.zero;
            masakiRigidbody.isKinematic = true;
            
            var akoRigidbody = ako.GetComponent<Rigidbody2D>();
            akoRigidbody.velocity = Vector2.zero;
            akoRigidbody.isKinematic = true;

            niccMasaki.ConditionX = -1.15f;
            niccMasaki.WalkLeft();
            niccAko.ConditionX = 2.19f;
            niccAko.WalkLeft();
            while (true)
            {
                if (!niccAko.WalkingFlg)
                {
                    break;
                }
                yield return null;
            }
            
            niccAko.ConditionY = 1.8f;
            niccAko.WalkBack();
            
            while (true)
            {
                if (!niccMasaki.WalkingFlg)
                {
                    break;
                }
                yield return null;
            }
            
            niccMasaki.ConditionY = 1.8f;
            niccMasaki.WalkBack();

            while (true)
            {
                if (!niccMasaki.WalkingFlg)
                {
                    break;
                }
                yield return null;
            }
            
            niccMasaki.WalkFrontNoSpeed();
            niccAko.WalkFrontNoSpeed();
            yield return null;
            
            var controller = chickenTarget.AddComponent<ChickenController>();
            controller.RepeatNum = 50;
            controller.SpeedFactor = 0.075f;
                        
            Destroy(niccYusuke);

            EventManager.Instance.NextTask();
        }

        IEnumerator Action005Coroutine()
        {
            scaleCamera.LeapBaseValue = 50.0f;
            yield return new WaitForSeconds(1.0f);
            yusuke.AddComponent<ChickenMainCharacterController>();
            yusuke.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            HelpManager.Instance.Show();
            EventManager.Instance.NextTask();
        }

        IEnumerator Action006Coroutine()
        {
            Destroy(yusuke.GetComponent<ChickenMainCharacterController>());
            niccYusuke = yusuke.AddComponent<NoInputCharacterController>();
            
            SceneLoadManager.Instance.FadeOut(1.0f);
            yield return new WaitForSeconds(1.0f);

            SetPosition(yusuke, 5.0f, 0.0f);
            SetPosition(masaki, 4.5f, -1.5f);
            SetPosition(ako, 4.0f, -0.5f);
            niccYusuke.WalkFrontNoSpeed();
            niccMasaki.WalkBackNoSpeed();
            niccAko.WalkRightNoSpeed();
            
            SceneLoadManager.Instance.FadeIn(1.0f);
            
            EventManager.Instance.NextTask();
        }
        
        IEnumerator Action008Coroutine()
        {
            var doorCollider = GameObject.Find("entrance_door").GetComponent<BoxCollider2D>();
            doorCollider.enabled = false;
            niccYusuke.ConditionX = 6.8f;
            niccYusuke.WalkRight();
            while (true)
            {
                if (!niccYusuke.WalkingFlg)
                {
                    break;
                }
                yield return null;
            }

            niccYusuke.ConditionY = -9.0f;
            niccYusuke.WalkFront();
            while (true)
            {
                if (niccYusuke.transform.position.y < -2f)
                {
                    break;
                }
                yield return null;
            }

            niccMasaki.ConditionX = 6.8f;
            niccMasaki.WalkRight();
            while (true)
            {
                if (5.0f < niccMasaki.transform.position.x)
                {
                    break;
                }
                yield return null;
            }
            niccAko.ConditionX = 6.8f;
            niccAko.WalkRight();
            while (true)
            {
                if (!niccMasaki.WalkingFlg)
                {
                    break;
                }
                yield return null;
            }

            niccMasaki.ConditionY = -7.8f;
            niccMasaki.WalkFront();
            
            while (true)
            {
                if (!niccAko.WalkingFlg)
                {
                    break;
                }
                yield return null;
            }

            niccAko.ConditionY = -6.6f;
            niccAko.WalkFront();
            EventManager.Instance.NextTask();

            SceneStatus.EntranceNo = 5;
            SceneLoadManager.Instance.LoadLevelInLoading(2.0f, "schoolyard", null);
        }

        void SetPosition(GameObject obj, float x, float y)
        {
            var pos = obj.transform.position;
            pos.x = x;
            pos.y = y;
            obj.transform.position = pos;
        }
    }
}