using UnityEngine;
using System.Collections;

public class LifeFollowPnj : MonoBehaviour {
	Camera cam;
	Vector3 offset;
	private Transform target;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		
		target = getTransformPj();//posicio de PNJ
		if(target !=null){//si existe el pnj asignado a esa barra de vida
			transform.position = cam.WorldToViewportPoint(target.position);//asignamos la posicion del guiTexture a la posicion del enemigo 
		}
		else{
			destroyTexture();//destruimos la barra	
		}
		
	}
	/*
	 *  metodo que devuelve la poscion del PNj asignado a la guitexture barra de vida
	 */ 
	private Transform getTransformPj(){
		
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.getTarget().name == "pnj" || ch.getTarget().name == "pnj_range"){
				Pnj pnj = (Pnj) ch;
				
				if(pnj.getLifeTexture().transform == transform){
					float damage = (float)pnj.getPg()/(float)pnj.getMaxPg()*pnj.getDeathTexture().guiTexture.pixelInset.width;
					if(damage <= 0){
						damage = 0;	
					}
					pnj.setLifeTexture(damage);
					return pnj.getTarget().transform;
				}
				
			}
		}
		return null;
	}
	/*
	 * metodo que destruye el gameObject
	 */
	private void destroyTexture(){
		

		Destroy((GameObject)transform.gameObject);
	}
		
}
