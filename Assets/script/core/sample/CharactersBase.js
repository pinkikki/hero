#pragma strict
var h_speed : float  = 0.0f;
var v_speed : float = 0.0f;
var warlkingFlg = false;
var collisionFlg = false;
var anim : Animator;

public class CharactersBase extends MonoBehaviour {

public function walkFront() {
		if (!collisionFlg) {
			setWalkValue(0.0f, -1.0f, true, false);
		} else {
			if (!anim.GetBool("Fwait")) {
				setWalkValue(0.0f, -1.0f, true, false);
			} else {
				walkStop();
			}
		}

	}

	public function walkBack() {
		if (!collisionFlg) {
			setWalkValue(0.0f, 1.0f, true, false);
		} else {
			if (!anim.GetBool("Bwait")) {
				setWalkValue(0.0f, 1.0f, true, false);
			} else {
				walkStop();
			}
		}
	}

	public function walkLeft() {	
		if (!collisionFlg) {
			setWalkValue(-1.0f, 0.0f, false, true);
		} else {
			if (!anim.GetBool("Lwait")) {
				setWalkValue(-1.0f, 0.0f, false, true);
			} else {
				walkStop();
			}
		}
	}

	public function walkRight() {
		if (!collisionFlg) {
			setWalkValue(1.0f, 0.0f, false, true);
		} else {
			if (!anim.GetBool("Rwait")) {
				setWalkValue(1.0f, 0.0f, false, true);
			} else {
				walkStop();
			}
		}
	}
	
	public function walkStop() {
		warlkingFlg = false;
		if (v_speed < 0.0f) {
			anim.SetBool("Fwait", true);
			anim.SetBool("Bwait", false);
			anim.SetBool("Lwait", false);
			anim.SetBool("Rwait", false);
		} else if (v_speed > 0.0f) {
			anim.SetBool("Fwait", false);
			anim.SetBool("Bwait", true);
			anim.SetBool("Lwait", false);
			anim.SetBool("Rwait", false);
		} else if (h_speed < 0.0f) {
			anim.SetBool("Fwait", false);
			anim.SetBool("Bwait", false);
			anim.SetBool("Lwait", true);
			anim.SetBool("Rwait", false);
		} else if (h_speed > 0.0f) {
			anim.SetBool("Fwait", false);
			anim.SetBool("Bwait", false);
			anim.SetBool("Lwait", false);
			anim.SetBool("Rwait", true);
		}

		h_speed = 0.0f;
		v_speed = 0.0f;
		anim.SetFloat("Hspeed", h_speed);
		anim.SetFloat("Vspeed", v_speed);
		anim.SetBool("Hstop", true);
		anim.SetBool("Vstop", true);	
	}

	private function setWalkValue(hsVal : float, vsVal : float, hsFlg : boolean, vsFlg : boolean) {
		h_speed = hsVal;
		v_speed = vsVal;
		
		anim.SetFloat("Hspeed", h_speed);
		anim.SetFloat("Vspeed", v_speed);
		anim.SetBool("Hstop", hsFlg);
		anim.SetBool("Vstop", vsFlg);
		warlkingFlg = true;
		collisionFlg = false;
	}
}