using UnityEngine;
using System.Collections;

public class DeathFollowPnj : MonoBehaviour {
	Camera cam;
	Vector3 offset;
	private Transform target;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
		target = getTransformPj();//posicio de un enemigo
		if(target !=null){//posicionamos las coordenadas de pantalla
			transform.position = cam.WorldToViewportPoint(target.position) ;
		}else{
			destroyTexture();//destruimos la textura	
		}
		
	}
	/*
	 * metodo que devuelve la posicion de cada enemigo
	 */
	private Transform getTransformPj(){
		
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.getTarget().name == "pnj" || ch.getTarget().name == "pnj_range"){
				Pnj pnj = (Pnj) ch;
				
				if(pnj.getDeathTexture().transform == transform){
					
					return pnj.getTarget().transform;
				}
				
			}
		}
		return null;
	}
	/*
	 * metodo que destruye el GameObject asociado a la textura
	 */
	private void destroyTexture(){
		Destroy((GameObject)transform.gameObject);
	}	
}