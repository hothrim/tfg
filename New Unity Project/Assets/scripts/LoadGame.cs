using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {
	private Texture2D play;
	private Texture2D playSelected;
	private GameObject obj;

	void Start(){
		//cargo imagenes de la ruta especificada
		play =Resources.Load("play") as Texture2D;
		playSelected =Resources.Load("playSelected") as Texture2D;
		obj = GameObject.Find("play");
		
	}
	
	void OnMouseDown(){
		obj.audio.Play();//cargo sonido de boton
		Application.LoadLevel("tfg_scene");//cargo escena de juego
		
	}
	void OnMouseEnter(){
		obj.renderer.material.mainTexture = playSelected;//cambio textura al entrar en el collider del objeto
		
		
		
		
	}
	void OnMouseExit(){
		obj.renderer.material.mainTexture = play;//cambio textura al salir del collider del objeto
		
	}
}
