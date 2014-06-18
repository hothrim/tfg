using UnityEngine;
using System.Collections;

public class OptionsGame : MonoBehaviour {
	private Texture2D options;
	private Texture2D optionsSelected;
	private GameObject obj;
	
	
	void Start(){
		//cargo imagenes de la ruta especificada
		options =Resources.Load("options") as Texture2D;
		optionsSelected =Resources.Load("optionsSelected") as Texture2D;
		obj = GameObject.Find("options");
		
	}
	
	void OnMouseDown(){
		obj.audio.Play();//activo audio al pulsar boton
		Application.LoadLevel("menuOptions");//cargo escena menu opciones
		
		
	}
	void OnMouseEnter(){
		obj.renderer.material.mainTexture = optionsSelected;//cambio textura al entrar en el collider del objeto plano
			 
		
		
		
	}
	void OnMouseExit(){
		obj.renderer.material.mainTexture = options;//cambio textura al salir del collider del objeto plano
		
	}
}
