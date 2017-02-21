#pragma strict
private var fButton : GameObject;
private var bButton : GameObject;
private var lButton : GameObject;
private var rButton : GameObject;
public var buttonTouchCount : int;
private var rigit : Rigidbody2D;

public class SampleCharactersController extends CharactersBase {

	function Start () {
		anim = gameObject.GetComponent(Animator);
		buttonTouchCount = 0;
		rigit = gameObject.GetComponent(Rigidbody2D);
	}
	
	public function getButtonObject() {
		fButton = GameObject.Find("Fbutton");
		bButton = GameObject.Find("Bbutton");
		lButton = GameObject.Find("Lbutton");
		rButton = GameObject.Find("Rbutton");
	}

	function FixedUpdate () {

		if (!warlkingFlg) {
			if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow)) {
				walkBack();
			} else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow)) {
				walkFront();
			} else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow)) {
				walkLeft();
			} else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow)) {
				walkRight();
			}
		}
			
		if (!collisionFlg) {
			if(warlkingFlg) {
				var pos : Vector3;
				if (Input.GetKey(KeyCode.UpArrow) ||
						Input.GetKey(KeyCode.DownArrow) ||
						Input.GetKey(KeyCode.LeftArrow) ||
						Input.GetKey(KeyCode.RightArrow)) {
					pos = gameObject.transform.position;
					pos.x += h_speed * 0.05f;
					pos.y += v_speed * 0.05f;
					gameObject.transform.position = pos;
				} else {
					walkStop();
				}
			}
		} else {
			walkStop();
		}
	}

	function OnCollisionEnter2D(other:Collision2D) {
		collisionFlg = true;
		// if (other.gameObject.CompareTag("player")) {
		// 	// rigit.isKinematic = true;
		// }
	}

	// function OnCollisionStay2D(other:Collision2D) {
	// 	if (other.gameObject.CompareTag("player")) {
	// 		rigit.isKinematic = true;
	// 	}
	// }

	function OnCollisionExit2D(other:Collision2D) {
		collisionFlg = false;
		// if (!other.gameObject.CompareTag("player")) {
		// 	rigit.isKinematic = true;
		// }
	}

}
