using UnityEngine;
using System.Collections;

public class StopTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//paramos el tiempo de ejecucion del juego en caso de que no este activado el menu de pausa
	 	if(Input.GetKeyDown("space") && !MenuPauseAparition.gamePaused){
			
			if(Time.timeScale == 1f){
				Time.timeScale = 0f;
			}else{
				Time.timeScale = 1f;
			}
			
		}
	}
}
