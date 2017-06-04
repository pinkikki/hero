using System.Collections;
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
			Anim = gameObject.GetComponent<Animator>();
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
			Anim.SetBool("Fwait", fwFlg);
			Anim.SetBool("Bwait", bwFlg);
			Anim.SetBool("Lwait", lwFlg);
			Anim.SetBool("Rwait", rwFlg);
		}

		public IEnumerator MoveUpOrDown(float upOrDownY, float interval) {
			var pos = transform.position;
			var currentY = pos.y;
			var time = 0.0f;
			while (time <= interval) {
				pos.y = Mathf.Lerp(currentY, upOrDownY, time / interval);
				transform.position = pos;
				time += Time.deltaTime;
				yield return null;
			}
		}
	}
}
