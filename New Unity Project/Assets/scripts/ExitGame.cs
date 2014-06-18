using UnityEngine;
using System.Collections;

public class ExitGame : MonoBehaviour {
	
	private Texture2D exit;
	private Texture2D exitSelected;
	private GameObject obj;
	void Start(){
		//cargo imagenes de la ruta especificada
		exit = Resources.Load("exit") as Texture2D;
		exitSelected = Resources.Load("exitSelected") as Texture2D;
		obj = GameObject.Find("exit");
	}
	
	void OnMouseDown(){
		obj.audio.Play();//cargo sonido de boton
		Application.Quit();//salgo del juego
		
	}
	void OnMouseEnter(){
		obj.renderer.material.mainTexture = exitSelected;//cambio textura al entrar en el collider del objeto
			 
		
		
		
	}
	void OnMouseExit(){
		obj.renderer.material.mainTexture = exit;//cambio textura al salir del collider del objeto
		
	}
}
