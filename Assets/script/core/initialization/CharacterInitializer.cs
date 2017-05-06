using System;
using System.Collections.Generic;
using Assets.script.common.dao;
using Assets.script.common.entity;
using Assets.script.core.asset;
using Assets.script.core.camera;
using Assets.script.core.monoBehaviour;
using Assets.script.core.scene;
using UnityEngine;

namespace Assets.script.core.initialization
{
    public class CharacterInitializer : SingletonMonoBehaviour<CharacterInitializer>
    {
        [SerializeField] String cameraTarget;
        AssetLoader.LoadStatus loadStatus = AssetLoader.LoadStatus.LoadWait;

        public AssetLoader.LoadStatus LoadStatus
        {
            get { return loadStatus; }
            set { loadStatus = value; }
        }

        void Start()
        {
            // TODO これは消す（SceneLoadManagerで呼び出すはず）
            Load();
        }

        void Load()
        {
            loadStatus = AssetLoader.LoadStatus.LoadExecute;

            // TODO 何かしらの方法でEntranceNoを取得
            List<LocationEntity> locationList = LocationDao.SelectBySceneStatus(SceneStatus.SceneId,
                SceneStatus.EntranceNo, SceneStatus.Procedure);

            if (locationList.Count == 0)
            {
                loadStatus = AssetLoader.LoadStatus.LoadComplete;
                return;
            }


//            var lines : String[];
//            var filePath = "file://" + Path.Combine(Application.streamingAssetsPath + PREFIX, fileName);
//// 	var filePath = "http://pinkikki.jp/crino-r" + PREFIX + fileName;
//            if (filePath.Contains("://")) {
//                var www = new WWW(filePath);
//
//                yield www;
//                if(www.error != null) {
//                    throw System.Exception("通信障害が発生しました");
//                }
//
//                lines = www.text.Split("\r"[0], "\n"[0]);
//
//            } else {
//                lines = File.ReadAllLines(filePath);
//            }
//
            foreach (var location in locationList)
            {
//                var loadInfoDto = new LoadInfoDto();
//                var infos = parse(lines[j]);
                var prefab = AssetLoader.Instance.LoadPrefab(location.AssetBandlesName, location.AssetName);
                var obj = (GameObject) Instantiate(prefab,
                    new Vector2(float.Parse(location.PositionX), float.Parse(location.PositionY)), Quaternion.identity);
                obj.name = location.ObjectName;
                if (cameraTarget == location.ObjectName)
                {
                    FindObjectOfType<ScaleCamera>().SetTarget(obj);
                }

                var directionNum = location.Direction;
                // アニメーションが付与されている場合
                if (directionNum < 4)
                {
                    var anim = obj.GetComponent<Animator>();
                    if (directionNum == 0)
                    {
                        WalkFrontNoSpeed(anim);
                    }
                    else if (directionNum == 1)
                    {
                        WalkBackNoSpeed(anim);
                    }
                    else if (directionNum == 2)
                    {
                        WalkLeftNoSpeed(anim);
                    }
                    else
                    {
                        WalkRightNoSpeed(anim);
                    }
                }
            }

            loadStatus = AssetLoader.LoadStatus.LoadComplete;
        }

        void Update()
        {
        }

        void WalkFrontNoSpeed(Animator animObj)
        {
            SetDirection(true, false, false, false, animObj);
        }

        void WalkBackNoSpeed(Animator animObj)
        {
            SetDirection(false, true, false, false, animObj);
        }

        void WalkLeftNoSpeed(Animator animObj)
        {
            SetDirection(false, false, true, false, animObj);
        }

        void WalkRightNoSpeed(Animator animObj)
        {
            SetDirection(false, false, false, true, animObj);
        }

        void SetDirection(bool fwFlg, bool bwFlg, bool lwFlg, bool rwFlg, Animator animObj)
        {
            animObj.SetBool("Fwait", fwFlg);
            animObj.SetBool("Bwait", bwFlg);
            animObj.SetBool("Lwait", lwFlg);
            animObj.SetBool("Rwait", rwFlg);
        }
    }
}
