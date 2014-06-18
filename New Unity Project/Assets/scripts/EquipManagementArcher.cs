using UnityEngine;
using System.Collections;

public class EquipManagementArcher : MonoBehaviour {
	private GameObject archer;
	bool cont;
	// Use this for initialization
	void Start () {
		archer = GameObject.Find("archer");
		cont = false;
		InvokeRepeating("imfast",1.0f,1.0f);//metode que funciona com contador y llama al metodo imfast a cada segundo
	}
	
	// Update is called once per frame
	void Update () {
		
		if(archer != null){
			
			Archer a = getArcher();//cogemos el objeto Archer
			
			if(!a.getEquipment().getActivation()){
				cont = true;
		
			}
		}
	}
	
	/*metodo que devuelve el objecto Archer*/
	private Archer getArcher(){
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.getTarget() == archer){
				Archer a = (Archer) ch;
				return a;	
			}
		}
		return null;
	}
	/*
	 * metodo que va reduciendo el tiempo de la habilidad y cancela el efecto cuando la duracion acaba
	 */
	 private void imfast(){
		if(archer != null && cont){
			Archer a = getArcher();
			
			
			if(a.getEquipment().getActivation()){
				a.getEquipment().reduceDuration();
				
				if(a.getEquipment().getDuration() <= 0){
					//en caso de acabar la duracion del hechizo cancelamos la mejora de caracterisitca y desaftivamos el sistema de particulas	
					a.getEquipment().setActivation(false);
					a.setDexterity(a.getDexterity()-2);
					a.getEquipment().setDuration(20);
					
					
					a.getEquipment().cancelEffect();
				}
			}
		}
	
	}
}
