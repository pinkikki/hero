using System;
using System.Collections.Generic;
using System.IO;
using script.common.dao;
using script.common.entity;
using script.core.assetbandle;
using script.core.camera;
using script.core.monoBehaviour;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace script.core.initialization
{
    public class CharactersInitializer : SingletonMonoBehaviour<CharactersInitializer> {

        bool selectFileFlg;
        LoadAssetBandles.LoadStatus loadStatus = LoadAssetBandles.LoadStatus.LoadWait;

        void Start () {
	
        }

        void Load() {
            loadStatus = LoadAssetBandles.LoadStatus.LoadExecute;

            var sceneId = SceneManager.GetActiveScene().name;
            // TODO 何かしらの方法でEntranceNoを取得
            int entranceId = 0;
            List<LocationEntity> locationList = LocationDao.SelectBySceneIdAndEntranceId(sceneId, entranceId);

            if (locationList.Count == 0)
            {
                loadStatus = LoadAssetBandles.LoadStatus.LoadComplete;
                return;
            }


            // TODO ここから！！！

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
//            for(var j = 0; j < lines.length; j++) {
//                var loadInfoDto = new LoadInfoDto();
//                var infos = parse(lines[j]);
//                var prefab = labInstance.loadPrefab(infos[0], infos[1]);
//                var obj : GameObject = Instantiate(prefab, Vector2 (float.Parse(infos[3]), float.Parse(infos[4])), Quaternion.identity);
//                obj.name = infos[2];
//                if (cameraTarget == infos[1]) {
//                    FindObjectOfType(ScaleCamera).setTarget(obj);
//                }
//
//                var directionNum = int.Parse(infos[6]);
//                // アニメーションが付与されている場合
//                if (directionNum < 4) {
//                    var anim : Animator = obj.GetComponent(Animator);
//                    if (directionNum == 0) {
//                        walkFrontNoSpeed(anim);
//                    } else if (directionNum == 1) {
//                        walkBackNoSpeed(anim);
//                    } else if (directionNum == 2) {
//                        walkLeftNoSpeed(anim);
//                    } else {
//                        walkRightNoSpeed(anim);
//                    }
//                }
//
//
//
//            }

            loadStatus = LoadAssetBandles.LoadStatus.LoadComplete;
        }
	
        void Update () {
        }
    }
}
