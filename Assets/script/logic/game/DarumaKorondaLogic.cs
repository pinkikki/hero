using script.core.character;
using script.core.@event;
using UnityEngine;

namespace script.logic.game
{
	public class DarumaKorondaLogic : AutoCharacterController
	{
		GameObject yusuke;

		bool isFirst = true;
		protected override void Walk()
		{
			if (isFirst)
			{
				WalkFront();
				isFirst = false;
				type = 1;
				if (IsGameOver(true)) Finish();
				return;
			}
			if (transform.position.y < 3)
			{
				WalkBack();
				type = 0;
				if (IsGameOver(false)) Finish();
				return;
			}
			switch (type)
			{
				case 0:
					WalkBack();
					if (IsGameOver(false)) Finish();
					break;
				default:
					WalkFront();
					if (IsGameOver(true)) Finish();
					break;
			}
		}

		bool IsGameOver(bool isFront)
		{
			if (yusuke == null)
			{
				yusuke = GameObject.Find("yusuke");
				
			}
			var yusukePos = yusuke.transform.position;
			if (!isFront && (yusukePos.x < -3.9 && yusukePos.y < 17.6))
			{
				return true;
			}

			if (yusukePos.y <= transform.position.y)
			{
				return true;
			}

			return false;
		}
		
		void Finish()
		{
			FreezeFlg = true;
			EventManager.Instance.Register(601);
		}
	}
}
