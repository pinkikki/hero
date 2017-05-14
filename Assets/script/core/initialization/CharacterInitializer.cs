﻿using System.Collections.Generic;
using script.common.dao;
using script.common.entity;
using script.core.asset;
using script.core.camera;
using script.core.monoBehaviour;
using script.core.scene;
using UnityEngine;

namespace script.core.initialization
{
    public class CharacterInitializer : SingletonMonoBehaviour<CharacterInitializer>
    {
        [SerializeField] List<string> cameraTargetList;
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

            List<LocationEntity> locationList = LocationDao.SelectBySceneStatus(SceneStatus.SceneId,
                SceneStatus.EntranceNo, SceneStatus.Procedure);

            if (locationList.Count == 0)
            {
                loadStatus = AssetLoader.LoadStatus.LoadComplete;
                return;
            }

            foreach (var location in locationList)
            {
                var prefab = AssetLoader.Instance.LoadPrefab(location.AssetBandlesName, location.AssetName);
                var obj = (GameObject) Instantiate(prefab,
                    new Vector2(float.Parse(location.PositionX), float.Parse(location.PositionY)), Quaternion.identity);
                obj.name = location.ObjectName;
                if (cameraTargetList[SceneStatus.Procedure - 1] == location.ObjectName)
                {
                    FindObjectOfType<ScaleCamera>().Target = obj;
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

        public bool IsLoadComplete()
        {
            return loadStatus == AssetLoader.LoadStatus.LoadComplete;
        }
    }
}
