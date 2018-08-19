using System;
using System.Collections.Generic;
using System.Linq;
using script.common.dao;
using script.common.entity;
using script.core.monoBehaviour;
using script.core.scene;

namespace script.core.hint
{
	public class HintRepository : SingletonMonoBehaviour<HintRepository>
	{
		private List<HintEntity> hintEntityList = new List<HintEntity>();
		private int currentHintId;
		
		void Awake()
		{
			DontDestroyOnLoad(this);
		}
		
		void Start () {
			// migration対応
//			hintEntityList = HintDao.SelectAll();
		}
	
		void Update () {
		
		}

		public void Load()
		{
			hintEntityList = HintDao.SelectAll();
		}
		
		public bool HasNext()
		{
			var hintRange = GetHintRange();

			if (hintRange.MaxHintId <= currentHintId)
			{
				return false;
			}

			if (currentHintId < hintRange.MinHintId)
			{
				currentHintId = hintRange.MinHintId - 1;
				return true;
			}

			return true;
		}

		public String GetNextHint()
		{
			currentHintId++;
			return hintEntityList.First(entity => entity.HintId == currentHintId).Message;
		}

		HintRange GetHintRange()
		{
			
			if (SceneStatus.HasQuizE)
			{
				return new HintRange
				{
					MaxHintId = 51,
					MinHintId = 48
				};
			}
			
			if (SceneStatus.IsFinishedFirstUnLocking)
			{
				return new HintRange
				{
					MaxHintId = 45,
					MinHintId = 44
				};
			}
			
			if (SceneStatus.HasQuizD)
			{
				return new HintRange
				{
					MaxHintId = 43,
					MinHintId = 40
				};
			}
			
			if (SceneStatus.HasQuizC)
			{
				return new HintRange
				{
					MaxHintId = 39,
					MinHintId = 33
				};
			}
			
			if (SceneStatus.HasMarble)
			{
				return new HintRange
				{
					MaxHintId = 32,
					MinHintId = 31
				};
			}
			
			if (SceneStatus.HasMudDumplings)
			{
				return new HintRange
				{
					MaxHintId = 30,
					MinHintId = 30
				};
			}
			
			if (SceneStatus.CanGetMudDumplings)
			{
				return new HintRange
				{
					MaxHintId = 29,
					MinHintId = 29
				};
			}
			
			if (SceneStatus.HasNerikeshi)
			{
				return new HintRange
				{
					MaxHintId = 28,
					MinHintId = 28
				};
			}
			
			if (SceneStatus.HasGlue)
			{
				return new HintRange
				{
					MaxHintId = 27,
					MinHintId = 21
				};
			}
			
			if (SceneStatus.CanCreateNerikeshi)
			{
				return new HintRange
				{
					MaxHintId = 20,
					MinHintId = 20
				};
			}
			
			if (SceneStatus.HasMatomari)
			{
				return new HintRange
				{
					MaxHintId = 19,
					MinHintId = 19
				};
			}
			
			if (SceneStatus.CanSearchMatomari)
			{
				return new HintRange
				{
					MaxHintId = 18,
					MinHintId = 16
				};
			}
			
			if (SceneStatus.CanSearchMarble)
			{
				return new HintRange
				{
					MaxHintId = 15,
					MinHintId = 14
				};
			}
			
			if (SceneStatus.HasQuizB)
			{
				return new HintRange
				{
					MaxHintId = 13,
					MinHintId = 11
				};
			}
			
			if (SceneStatus.HasQuizA)
			{
				return new HintRange
				{
					MaxHintId = 10,
					MinHintId = 5
				};
			}
			
			if (SceneStatus.CanComeInClassroom)
			{
				return new HintRange
				{
					MaxHintId = 4,
					MinHintId = 2
				};
			}
			
			if (SceneStatus.Starting)
			{
				return new HintRange
				{
					MaxHintId = 1,
					MinHintId = 1
				};
			}

			return null;
		}

		class HintRange
		{
			public int MaxHintId { get; set; }
			public int MinHintId { get; set; }
		}
	}
}
