using UnityEngine;
using System.Collections;

public class CollisionPjWarrior : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		 	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/*metodo que varia el flag del script MotionControllerPj segun las  los colliders con los que colisiones*/
	void OnControllerColliderHit(ControllerColliderHit hit){
		
		if((hit.collider.name != "Terrain" && hit.collider.name != "InternalCompassWarrior(Clone)"&& hit.collider.name != "BloodDecal(Clone)" )){
			MotionControllerPJ.colissionWarrior = true;
		}
	}

}
