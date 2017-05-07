using UnityEngine;

namespace Assets.script.core.character
{
	public class NoInputCharacterController : CharacterBase
	{
		float conditionX;

		public float ConditionX
		{
			get { return conditionX; }
			set { conditionX = value; }
		}

		public float ConditionY
		{
			get { return conditionY; }
			set { conditionY = value; }
		}

		float conditionY;

		void Start()
		{
			anim = gameObject.GetComponent<Animator>();
		}

		void FixedUpdate()
		{
			if (WarlkingFlg)
			{
				Vector3 pos = gameObject.transform.position;
				pos.x += hSpeed * 0.065f;
				pos.y += vSpeed * 0.065f;
				gameObject.transform.position = pos;

				if ((CurrentDirection == Direction.R && conditionX < pos.x) ||
				    (CurrentDirection == Direction.L && pos.x < conditionX) ||
				    (CurrentDirection == Direction.B && conditionY < pos.y) ||
				    (CurrentDirection == Direction.F && pos.y < conditionY)
				)
				{
					WalkStop();
				}
			}
			else
			{
				WalkStop();
			}
		}

		void Update()
		{
		}


		public void WalkFrontNoSpeed() {
			SetDirection(true, false, false, false);
		}

		public void WalkBackNoSpeed() {
			SetDirection(false, true, false, false);
		}

		public void WalkLeftNoSpeed() {
			SetDirection(false, false, true, false);
		}

		public void WalkRightNoSpeed() {
			SetDirection(false, false, false, true);
		}

		void SetDirection(bool fwFlg, bool bwFlg, bool lwFlg, bool rwFlg) {
			anim.SetBool("Fwait", fwFlg);
			anim.SetBool("Bwait", bwFlg);
			anim.SetBool("Lwait", lwFlg);
			anim.SetBool("Rwait", rwFlg);
		}
	}
}
