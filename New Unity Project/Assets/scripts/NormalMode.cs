using UnityEngine;
using System.Collections;

public class NormalMode : MonoBehaviour {
	private Texture2D normal;
	private Texture2D normalSelected;
	private GameObject obj;
	
	void Start(){
		//cargo imagenes de la ruta especificada
		normal = Resources.Load("normal") as Texture2D;
		normalSelected =(Texture2D)Resources.Load("normalSelected") as Texture2D;
		obj = GameObject.Find("normal");
	}
	
	void OnMouseDown(){
		obj.audio.Play();
		ChangeDificulty.dificulty = 1;
		Application.LoadLevel("menu");//cargo opciones
		
		
	}
	void OnMouseEnter(){
		obj.renderer.material.mainTexture = normalSelected;//cambio textura al entrar en el collider del objeto
			 
		
		
		
	}
	void OnMouseExit(){
		obj.renderer.material.mainTexture = normal;//cambio textura al salir del collider del objeto
		
	}
}
