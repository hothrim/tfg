using UnityEngine;
using System.Collections;

public class openDoor : MonoBehaviour {
	
	private bool switchBTouch;
	private GameObject doorL;
	private GameObject doorR;
	private AudioClip openDoor_;
	// Use this for initialization
	
	void Start () {
		switchBTouch = false;
		doorL = GameObject.Find("bisagra1_002");
		doorR = GameObject.Find("bisagra1_000");
		openDoor_ = (AudioClip) Resources.Load("openDoor_",typeof(AudioClip)); 
		
		
		
	}
	
	// Update is called once per frame
	void Update(){
		//se abre puerta izquierda cuando estan activados los paneles hasta que el angulo es 90
		if(switchBTouch && ActivateSwitchA.switchATouch && doorL.transform.localRotation.eulerAngles.y < 90 ){
			//rotacion de la puerta
			doorL.transform.Rotate(Vector3.forward,15*Time.deltaTime);
		}
		//se abre puerta derecha cuando estan activados los paneles hasta que el angulo es 180
		if(ActivateSwitchA.switchATouch && switchBTouch &&doorR.transform.localRotation.eulerAngles.y > 180){
			//se activa el sonido
			doorR.audio.PlayOneShot(openDoor_,0.1f);
			//rotacion de la puerta
			doorR.transform.Rotate(Vector3.forward,15*Time.deltaTime);
			
			
				
		}
		else{
			doorR.audio.Stop();
		}
		
	}
	
	
	 void OnTriggerEnter(Collider other){
		if(transform.collider.name == "switchB"){
			switchBTouch = true;
			
		}
	}
}
