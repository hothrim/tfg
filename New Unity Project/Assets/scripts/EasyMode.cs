using UnityEngine;
using System.Collections;

public class EasyMode : MonoBehaviour {
	private Texture2D easy;
	private Texture2D easySelected;
	private GameObject obj;
	
	void Start(){
		//cargo imagenes de la ruta especificada
		easy =Resources.Load("easy") as Texture2D;
		easySelected =Resources.Load("easySelected") as Texture2D;
		obj = GameObject.Find("easy");
	}
	
	void OnMouseDown(){
		obj.audio.Play();//activo audio al pulsar GameObject plano
		ChangeDificulty.dificulty = 0;//cambo dificultad
		Application.LoadLevel("menu");//cargo escena de menu
		
		
	}
	void OnMouseEnter(){
		obj.renderer.material.mainTexture = easySelected;//cambio textura de imagen del plano al entrar en el collider del objeto
			 
		
		
		
	}
	void OnMouseExit(){
		obj.renderer.material.mainTexture = easy;//cambio textura de imagen del plano al salir del collider del objeto
		
	}
}
