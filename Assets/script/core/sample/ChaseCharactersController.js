#pragma strict
var target : GameObject;
var destX : float;
var destY : float;
var defaultWalkSpeed : float = 0.07;
var defaultCollisionWalkSpeed : float = 0.04;
private var walkSpeed : float = defaultWalkSpeed;
private var targetAnim : Animator;
private var rigit : Rigidbody2D;

public class ChaseCharactersController extends CharactersBase {
	function Start () {
		anim = gameObject.GetComponent(Animator);
		targetAnim = target.GetComponent(Animator);
		rigit = gameObject.GetComponent(Rigidbody2D);
	}

	function Update () {
		var targetPos = target.transform.position;
		var selfPos = gameObject.transform.position;

		var targetX = targetPos.x;
		var targetY = targetPos.y;
		var selfX = selfPos.x;
		var selfY = selfPos.y;

		if (Mathf.Abs(targetX - selfX) > destX) {
			var inclementNumX : float;
			if (targetX < selfX) {
				inclementNumX = -walkSpeed;
				walkLeft();
			} else {
				inclementNumX = walkSpeed;
				walkRight();
			}
			selfPos.x += inclementNumX;
			gameObject.transform.position = selfPos;
			return;
		}
		if (Mathf.Abs(targetY - selfY) > destY) {
			var inclementNumY : float;
			if (targetY < selfY) {
				inclementNumY = -walkSpeed;
				walkFront();
			} else {
				inclementNumY = walkSpeed;
				walkBack();
			}
			selfPos.y += inclementNumY;
			gameObject.transform.position = selfPos;
			return;
		}
	}

	function OnCollisionEnter2D(other : Collision2D) {
		collisionFlg = true;
		walkSpeed = defaultCollisionWalkSpeed;
		// if (!other.gameObject.CompareTag("player")) {
		// 	rigit.isKinematic = false;
		// }
	}

	// function OnCollisionStay2D(other : Collision2D) {
	// 	if (other.gameObject.CompareTag("player")) {
	// 		rigit.isKinematic = true;
	// 	}
	// }

	function OnCollisionExit2D(other : Collision2D) {
		collisionFlg = false;
		walkSpeed = defaultWalkSpeed;
		// if (!other.gameObject.CompareTag("player")) {
		// 	rigit.isKinematic = true;
		// }
	}
}