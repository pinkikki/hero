using script.common.dao;
using script.common.entity;
using script.core.audio;
using script.core.operation;
using UnityEngine;

namespace script.logic.game
{
    public class SmartBallLogic : MonoBehaviour
    {
        GameObject ball;
        GameObject bar;
        Vector3 ballPos;
        Vector3 barPos;
        Camera camera;
        GameObject subButton;
        MusicEntity entity;
        
        void Start()
        {
            ball = GameObject.Find("ball");
            ballPos = ball.transform.position;
            bar = GameObject.Find("bar");
            barPos = bar.transform.position;
            camera = GameObject.Find("SubCamera").GetComponent<Camera>();
            camera.enabled = false;
            subButton = GameObject.Find("SubButton");
            subButton.SetActive(false);
        }

        void Update()
        {
        }

        public void Active()
        {
            camera.enabled = true;
            subButton.SetActive(true);
            SetPosition(bar, barPos);
            SetPosition(ball, ballPos);
            SearchButton.Instance.Hide();
            MusicEntity entity = MusicDao.SelectByPrimaryKey(6);
            AudioManager.Instance.PlayBgm(entity.MusicName, float.Parse(entity.Time));
        }
        
        public void NonActive()
        {
            camera.enabled = false;
            subButton.SetActive(false);
            SearchButton.Instance.Show();
        }
        
        public void Restart()
        {
            PlaySe();
            SetPosition(bar, barPos);
            SetPosition(ball, ballPos);
        }
        
        public void Close()
        {
            PlaySe();
            NonActive();
            MusicEntity entity = MusicDao.SelectByPrimaryKey(1);
            AudioManager.Instance.PlayBgm(entity.MusicName, float.Parse(entity.Time));
        }

        void SetPosition(GameObject obj, Vector3 vec)
        {
            obj.transform.position = vec;
        }
        
        void PlaySe()
        {
            if (entity == null)
            {
                entity = MusicDao.SelectByPrimaryKey(7);
            }
            AudioManager.Instance.PlaySe(entity.MusicName);
        }
        
    }
}
