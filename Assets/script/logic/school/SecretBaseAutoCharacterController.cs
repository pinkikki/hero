using script.core.character;
using UnityEngine;

namespace script.logic.school
{
	public class SecretBaseAutoCharacterController : AutoCharacterController {

		int currentRepeatNum;

		void Start()
		{
			Anim = gameObject.GetComponent<Animator>();
			speedFactor = 0.05f;
		}
		
		new void FixedUpdate()
		{
			if (!FreezeFlg)
			{
				currentRepeatNum++;
				if (collisionFlg || currentRepeatNum > RepeatNum)
				{
					if (RepeatNum + 5 > currentRepeatNum)
					{
						type = Random.Range(0, 4);
						WalkNoSpeed();
						return;
					}
					type = Random.Range(0, 4);
					currentRepeatNum = 0;
				}

				Walk();

				Vector3 pos = gameObject.transform.position;
				pos.x += hSpeed * speedFactor;
				pos.y += vSpeed * speedFactor;
				gameObject.transform.position = pos;
			}
			Adjustment();
		}

		void Update()
		{
		}

		void WalkNoSpeed()
		{
			switch (type)
			{
				case 0:
					WalkBackNoSpeed();
					break;
				case 1:
					WalkFrontNoSpeed();
					break;
				case 2:
					WalkLeftNoSpeed();
					break;
				case 3:
					WalkRight();
					break;
			}
		}
		
		void WalkFrontNoSpeed() {
			SetDirection(true, false, false, false);
		}

		void WalkBackNoSpeed() {
			SetDirection(false, true, false, false);
		}

		void WalkLeftNoSpeed() {
			SetDirection(false, false, true, false);
		}

		void WalkRightNoSpeed() {
			SetDirection(false, false, false, true);
		}

		void SetDirection(bool fwFlg, bool bwFlg, bool lwFlg, bool rwFlg) {
			Anim.SetBool("Fwait", fwFlg);
			Anim.SetBool("Bwait", bwFlg);
			Anim.SetBool("Lwait", lwFlg);
			Anim.SetBool("Rwait", rwFlg);
		}
		
		void Adjustment()
		{
			var pos = transform.position;

			pos.x = pos.x < -9.0f ? -9.0f : pos.x;
			pos.x = -6.3f < pos.x ? -6.3f : pos.x;
			pos.y = pos.y < 3.8f ? 3.8f : pos.y;
			pos.y = 5.5f < pos.y ? 5.5f : pos.y;

			transform.position = pos;
		}
	}
}
