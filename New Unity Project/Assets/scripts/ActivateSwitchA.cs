using UnityEngine;
using System.Collections;

public class ActivateSwitchA : MonoBehaviour {
	public static bool switchATouch;
	// Use this for initialization
	void Start () {
		switchATouch = false; 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if(transform.collider.name == "switchA"){//miramos si un collider entra en contacto con el collider
			switchATouch = true;				// del gameobject y asignamos a cierto la variable
			
		}
	}
}
