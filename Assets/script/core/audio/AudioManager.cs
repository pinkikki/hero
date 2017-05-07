using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.script.core.asset;
using Assets.script.core.monoBehaviour;
using UnityEngine;

namespace Assets.script.core.audio
{
    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
        [SerializeField] List<string> bgmClipList;
        [SerializeField] List<string> bgmAssetBundleList;
        [SerializeField] List<string> seClipList;
        [SerializeField] List<string> seAssetBundleList;
        [SerializeField] bool destructionFlg;

        public bool DestructionFlg
        {
            get { return destructionFlg; }
        }

        AudioSource bgmSource;
        AudioSource bgmCrossFadingSource;
        readonly List<AudioSource> seSourceList = new List<AudioSource>();
        readonly Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
        readonly Dictionary<string, AudioClip> seDict = new Dictionary<string, AudioClip>();
        float delayTime;
        float crossTime;
        float startTime;
        AssetLoader.LoadStatus loadStatus = AssetLoader.LoadStatus.LoadWait;

        void Awake()
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmCrossFadingSource = gameObject.AddComponent<AudioSource>();

            if (!destructionFlg)
            {
                DontDestroyOnLoad(this);
            }
        }

        void Start()
        {
            StartCoroutine(Load());
        }

        IEnumerator Load()
        {
            loadStatus = AssetLoader.LoadStatus.LoadExecute;
            while (true)
            {
                if (AssetLoader.Instance.CurrentLoadStatus == AssetLoader.LoadStatus.LoadComplete)
                {
                    foreach (var bgmClip in bgmClipList.Select((value, index) => new {value, index}))
                    {
                        bgmDict.Add(bgmClip.value,
                            AssetLoader.Instance.LoadAudio(bgmAssetBundleList[bgmClip.index], bgmClip.value));
                    }

                    foreach (var seClip in seClipList.Select((value, index) => new {value, index}))
                    {
                        seDict.Add(seClip.value,
                            AssetLoader.Instance.LoadAudio(seAssetBundleList[seClip.index], seClip.value));
                    }
                    loadStatus = AssetLoader.LoadStatus.LoadComplete;
                    break;
                }
                yield return null;
            }
        }

        public bool IsLoadComplete()
        {
            return loadStatus == AssetLoader.LoadStatus.LoadComplete;
        }

        void Update()
        {
        }

        void FixedUpdate()
        {
            // TODO delayTimeがいるのかどうか不明なので一旦コメントアウト
//        if (bgmSource != null)
//        {
//            if (0 < bgmSource.time && bgmSource.time < delayTime)
//            {
//                bgmSource.time = delayTime;
//            }
//        }
//        if (bgmCrossFadingSource != null)
//        {
//            if (0 < bgmCrossFadingSource.time && bgmCrossFadingSource.time < delayTime)
//            {
//                bgmCrossFadingSource.time = delayTime;
//            }
//        }
            if (!bgmSource.isPlaying && bgmCrossFadingSource.isPlaying)
            {
                bgmSource.PlayDelayed(crossTime - bgmCrossFadingSource.time);
                bgmSource.time = startTime + delayTime;
            }
            else if (bgmSource.isPlaying && !bgmCrossFadingSource.isPlaying)
            {
                bgmCrossFadingSource.PlayDelayed(crossTime - bgmSource.time);
                bgmCrossFadingSource.time = startTime + delayTime;
            }
        }

        public void Destroy()
        {
            Destroy(this);
        }

        public void PlaySe(string seName)
        {
            var playSource = seSourceList.FirstOrDefault(seSource => !seSource.isPlaying);

            if (playSource == null)
            {
                playSource = gameObject.AddComponent<AudioSource>();
                seSourceList.Add(playSource);
            }

            playSource.clip = seDict[seName];
            playSource.Play();
        }

        public void StopSe(string seName)
        {
            foreach (var seSource in seSourceList)
            {
                if (seSource.clip.name != seName) continue;
                seSource.Stop();
                seSource.clip = null;
            }
        }

        public void PlayBgm(string bgmName)
        {
            bgmSource.Stop();
            bgmSource.clip = bgmDict[bgmName];
            delayTime = bgmSource.clip.length / bgmSource.clip.samples * 1152;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        public void PlayBgm(string bgmName, float tmpCrossTime)
        {
            PlayBgm(bgmName, tmpCrossTime, 0f);
        }

        public void PlayBgm(string bgmName, float tmpCrossTime, float tmpStartTime)
        {
            var bgm = bgmDict[bgmName];
            bgmSource.Stop();
            bgmSource.clip = bgm;
            bgmSource.time = 0.0f;
            delayTime = bgm.length / bgm.samples * 1152;
            // TODO delayTimeがいるのかどうか不明なので一旦コメントアウト
//	crossTime = tmpCrossTime + delayTime;
//	startTime = tmpStartTime + delayTime;
            crossTime = tmpCrossTime;
            startTime = tmpStartTime;
            bgmSource.Play();
            bgmCrossFadingSource.clip = bgm;
            bgmCrossFadingSource.PlayDelayed(crossTime);
            bgmCrossFadingSource.time = startTime;
        }

        public void StopBgm()
        {
            bgmSource.Stop();
            bgmSource.clip = null;
            bgmCrossFadingSource.Stop();
            bgmCrossFadingSource.clip = null;
        }

        public void StopBgmAtFadeOut(float interval)
        {
            StartCoroutine(StopBgmAtFadeOutCoroutine(interval));
        }

        public void DownBgmVolume(float interval, float downVolumn)
        {
            StartCoroutine(DownBgmVolumeCoroutine(interval, downVolumn));
        }

        IEnumerator StopBgmAtFadeOutCoroutine(float interval)
        {
            var bgmMaxVolume = bgmSource.volume;
            yield return DownBgmVolumeCoroutine(interval, 0.0f);
            StopBgm();
            bgmSource.volume = bgmMaxVolume;
        }

        IEnumerator DownBgmVolumeCoroutine(float interval, float downVolumn) {
            var time = 0.0f;
            var bgmMaxVolume = bgmSource.volume;
            while (time < interval)
            {
                bgmSource.volume = Mathf.Lerp(bgmMaxVolume, downVolumn, time / interval);
                bgmCrossFadingSource.volume = Mathf.Lerp(bgmMaxVolume, downVolumn, time / interval);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
