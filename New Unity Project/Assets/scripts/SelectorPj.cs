using UnityEngine;
using System.Collections;

public class SelectorPj : MonoBehaviour {

	// Use this for initialization

	void Start () {
		
		foreach(Character ch in SearchCharacters.characterList){
			//posicionamos los selectores(aureolas en los personajes principales)
			if(ch.GetType().FullName == "Wizard" || ch.GetType().FullName == "Warrior" || ch.GetType().FullName == "Archer" ){
				Pj pj = (Pj) ch;
				Vector3 positionSelector = new Vector3( pj.getTarget().transform.position.x,0.5f,pj.getTarget().transform.position.z);
				pj.getSelectionPj().transform.position = positionSelector;
				
				//incializacion de quien sera el primer personaje que tenga el selector visible
				if(ch.GetType().FullName == "Warrior"){
					pj.getSelectionPj().renderer.enabled = true;
				}else{
					pj.getSelectionPj().renderer.enabled = false;	
				}
			}
		}
		
			
			
	}
	
	// Update is called once per frame
	void Update () {
		/*
		 * la aureolas(selectores) siguen a los personajes principales
		 */ 
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.GetType().FullName == "Wizard" || ch.GetType().FullName == "Warrior" || ch.GetType().FullName == "Archer"){
				Pj pj = (Pj) ch;
				Vector3 positionSelector = new Vector3( pj.getTarget().transform.position.x,0.5f,pj.getTarget().transform.position.z);
				pj.getSelectionPj().transform.position = positionSelector;
				
			}
		}
	}
	

}
