using UnityEngine;

namespace script.core.character
{
	public class ChaseCharacterController : CharacterBase {
		
		public GameObject Target { get; set; }
		public GameObject OtherChaseTarget { get; set; }
		public MainCharacterController TargetController { get; set; }
	}
}
