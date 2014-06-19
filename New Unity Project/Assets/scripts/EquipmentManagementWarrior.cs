using UnityEngine;
using System.Collections;

public class EquipmentManagementWarrior : MonoBehaviour {
	private GameObject warrior;
	Warrior w;
	bool cont;
	// Use this for initialization
	void Start () {
		warrior = GameObject.Find("warrior");
		w = null;
		cont = false;
		InvokeRepeating("imfurius",1.0f,1.0f);//metode que funciona com contador y llama al metodo imfurious a cada segundo
	}
	
	// Update is called once per frame
	void Update () {
		if(warrior != null){
			
			w= getWarrior();
			
			if(w.getEquipment().getActivation()){
				cont = true;	
			}
		
		}
		
		
		
	}
	
	/*metodo que devuelve el objecto Warrior*/
	private Warrior getWarrior(){
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.getTarget() == warrior){
				Warrior w = (Warrior) ch;//cogemos el objeto warrior
				return w;	
			}
		}
		return null;
	}
	/*
	 * metodo que va reduciendo el tiempo de la habilidad y cancela el efecto cuando la duracion acaba
	 */
	 private void imfurius(){
		if(warrior != null && cont){
			
			w= getWarrior();
			
			if(w.getEquipment().getActivation()){
				
				w.getEquipment().reduceDuration();
				if(w.getEquipment().getDuration() <= 0){
					w.getEquipment().setActivation(false);
					w.setStrength(w.getStrength()-4);
					w.getEquipment().setDuration(20);
					
					w.getEquipment().cancelEffect();
					cont = false;
					
				}
			}
		}
		
	}
	
	
}
