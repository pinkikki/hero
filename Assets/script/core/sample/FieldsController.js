#pragma strict

public class FieldsController extends CharactersBase {
	function Start () {

	}

	function Update () {

	}

	var rigitFlg : boolean = false;
	function OnCollisionEnter2D(other:Collision2D) {
		// if (other.gameObject.CompareTag("player")) {
		// 	other.gameObject.GetComponent(Rigidbody2D).isKinematic = false;
		// }
	}

	// function OnCollisionStay2D(other:Collision2D) {
	// 	if (other.gameObject.CompareTag("player")) {
	// 		rigit.isKinematic = true;
	// 	}
	// }

	function OnCollisionExit2D(other:Collision2D) {
		// if (other.gameObject.CompareTag("player")) {
		// 	other.gameObject.GetComponent(Rigidbody2D).isKinematic = true;
		// }
	}
}