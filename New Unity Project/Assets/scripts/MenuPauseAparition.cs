using UnityEngine;
using System.Collections;

public class MenuPauseAparition : MonoBehaviour {
	
	public static bool gamePaused;
	
	// Use this for initialization
	void Start () {
		 gamePaused = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.P)) { 
			pause();
	
		}
	
	}
	void OnGUI(){

		if(gamePaused){	//creacion de la GUI de menu de pausa si el tiempo esta  esta parado 
			Screen.showCursor = true;
			
			GUI.backgroundColor = Color.grey;
			
			if(GUI.Button(new Rect(Screen.width/2-150,(Screen.height/2)-150,300,50),"Continuar")){
				pause();
				
			}
			
			if(GUI.Button(new Rect(Screen.width/2-150,(Screen.height/2)-100,300,50),"Salir")){
				
				Application.Quit();
			}
			
			
		
		}
	}
	private void pause(){
		
		
		gamePaused = !gamePaused;	//cambiamos valor cada vez que apretamos tecla P

		if (gamePaused == false) { 
			Time.timeScale = 1;	//paramos el tiempo de juego
			}
		else {
			Time.timeScale = 0;
			Screen.showCursor = false;	//reiniciamos el tiempo de juego
		}
		
	}

}
