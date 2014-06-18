using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UpdateDistances : MonoBehaviour {
	private float distance;
	// Use this for initialization
	void Start () {
		distance = -1;
	}
	
	// Update is called once per frame
	void Update () {
		//calculamos las distancias entre los personajes principales y los enemigos
		foreach(Character ch in SearchCharacters.characterList){	
			foreach(Character chA in ch.getEnemiesList()){
				
					
					distance = Vector3.Distance(ch.getTarget().transform.position,chA.getTarget().transform.position);
				
					//añadimos los enemigos como clave y como valor las distancias
				
					if(ch.GetType().FullName == "Warrior"){
					
				
							if(!ch.getDistanceEnemies().ContainsKey(chA)){				
								ch.getDistanceEnemies().Add(chA,distance);
							}else{
								ch.getDistanceEnemies()[chA] = distance;
						}
					}
					if(ch.GetType().FullName == "Wizard"){
				
							if(!ch.getDistanceEnemies().ContainsKey(chA)){				
								ch.getDistanceEnemies().Add(chA,distance);
							}else{
								ch.getDistanceEnemies()[chA] = distance;
						}
					}
					if(ch.GetType().FullName == "Archer"){
							
							if(!ch.getDistanceEnemies().ContainsKey(chA)){				
								ch.getDistanceEnemies().Add(chA,distance);
							}else{
								ch.getDistanceEnemies()[chA] = distance;
						}
					}
					if(ch.GetType().FullName == "PnjShort"){
							
							if(!ch.getDistanceEnemies().ContainsKey(chA)){				
								ch.getDistanceEnemies().Add(chA,distance);
							}else{
								ch.getDistanceEnemies()[chA] = distance;
						}
					}
					if(ch.GetType().FullName == "PnjRange"){
							
							if(!ch.getDistanceEnemies().ContainsKey(chA)){				
								ch.getDistanceEnemies().Add(chA,distance);
							}else{
								ch.getDistanceEnemies()[chA] = distance;
						}
					}
				
				
				
			}	
		}
 		
		
	}
}

 