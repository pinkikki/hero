using script.common.dao;
using script.common.entity;
using script.core.audio;
using script.core.@event;
using UnityEngine;

namespace script.core.ui
{
	public class UiCloseMonoBehaviour : MonoBehaviour
	{
		MusicEntity entity;
		
		void Start()
		{
		}

		void Update()
		{
		}

		public void Close()
		{
			PlaySe();
			Destroy(gameObject);
		}
		
		public void CloseAndEventNext()
		{
			PlaySe();
			Destroy(gameObject);
			EventManager.Instance.NextTask();
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
