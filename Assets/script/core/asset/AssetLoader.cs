using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using script.core.monoBehaviour;
using UnityEngine;
using Object = UnityEngine.Object;

namespace script.core.asset
{
    public class AssetLoader : SingletonMonoBehaviour<AssetLoader>
    {
        [SerializeField] bool isLoadFromAsssetBandles;
        Dictionary<string, AssetBundle> assetBundleDic;
        public Dictionary<string, int> AssetBundleInfoDic { get; set; }

        public enum LoadStatus
        {
            LoadWait,
            LoadExecute,
            LoadComplete
        }

        public LoadStatus CurrentLoadStatus { get; private set; }

        enum LoadType
        {
            CacheDownload,
            CreateFile
        }

        [SerializeField] LoadType loadType = LoadType.CreateFile;
        private static readonly string Prefix = "/AssetBundles/android/";

        public AssetLoader()
        {
            CurrentLoadStatus = LoadStatus.LoadWait;
        }

        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void Start()
        {
        }

        public void Load()
        {
            if (AssetBundleInfoDic == null)
            {
                CurrentLoadStatus = LoadStatus.LoadComplete;
                return;
            }
            StartCoroutine(Download());
        }

        IEnumerator Download()
        {
            CurrentLoadStatus = LoadStatus.LoadExecute;
            assetBundleDic = new Dictionary<string, AssetBundle>();
            foreach (var assetBundleInfoPair in AssetBundleInfoDic)
            {
                var url = assetBundleInfoPair.Key;
                var version = assetBundleInfoPair.Value;

                if (assetBundleDic.ContainsKey(url))
                {
                    continue;
                }

                AssetBundle assetBundle;

                switch (loadType)
                {
                    case LoadType.CacheDownload:
                        while (!Caching.ready)
                        {
                            yield return null;
                        }
                        var www = WWW.LoadFromCacheOrDownload("file://" + Path.Combine(Application.streamingAssetsPath
                                                                                       + Prefix, url), version);
//                        var www = WWW.LoadFromCacheOrDownload(
//                            "http://pinkikki.jp/crino-r/AssetBundles/android/f96/tsuyoshiyume", 4);
                        yield return www;
                        if (www.error != null)
                        {
                            throw new Exception("通信障害が発生しました" + www.error + "url : " +
                                                Path.Combine(Application.streamingAssetsPath + Prefix, url));
                        }
                        assetBundle = www.assetBundle;
                        www.Dispose();
                        break;
                    case LoadType.CreateFile:
                        assetBundle =
                            AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath + Prefix, url));

//                        var www = new WWW("http://pinkikki.jp/crino-r" + PREFIX + url);
//                        yield return www;
//                        if (www.error != null)
//                        {
//                            throw new Exception("通信障害が発生しました");
//                        }
//                        assetBundle = www.assetBundle;

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                assetBundleDic.Add(url, assetBundle);
            }
            CurrentLoadStatus = LoadStatus.LoadComplete;
        }

        public AudioClip LoadAudio(string assetBundleName, string assetName)
        {
            if (isLoadFromAsssetBandles)
            {
                var assetBundle = assetBundleDic[assetBundleName];
                return assetBundle == null ? null : (AudioClip) assetBundle.LoadAsset(assetName);
            }
            return Resources.Load<AudioClip>(assetBundleName + assetName);
        }

        public Object LoadPrefab(string assetBundleName, string assetName)
        {
            if (isLoadFromAsssetBandles)
            {
                var assetBundle = assetBundleDic[assetBundleName];
                return assetBundle == null ? null : assetBundle.LoadAsset(assetName);
            }
            return Resources.Load<Object>(assetBundleName + assetName);
        }

        public void Unload()
        {
            if (assetBundleDic == null) return;
            foreach (var assetBundlePair in assetBundleDic)
            {
                if (assetBundlePair.Key.IndexOf("prefab/fieldmenu", StringComparison.Ordinal) == -1)
                {
                    assetBundlePair.Value.Unload(true);
                }
            }
            assetBundleDic = null;
            AssetBundleInfoDic = null;
        }

        public void UnloadExcludingAudios()
        {
            if (assetBundleDic == null) return;
            foreach (var assetBundlePair in assetBundleDic)
            {
                if (assetBundlePair.Key.IndexOf("prefab", StringComparison.Ordinal) != -1 &&
                    assetBundlePair.Key.IndexOf("prefab/fieldmenu", StringComparison.Ordinal) == -1)
                {
                    assetBundlePair.Value.Unload(true);
                }
            }
            assetBundleDic = null;
            AssetBundleInfoDic = null;
        }

        void Update()
        {
        }
    }
}
