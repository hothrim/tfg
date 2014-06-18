using UnityEngine;
using System.Collections;

public class FollowArmorSpell : MonoBehaviour {
	GameObject wizard;
	GameObject archer;
	GameObject warrior;
	Wizard w;
	
	// Use this for initialization
	void Start () {
		wizard = GameObject.Find("wizard");
		archer = GameObject.Find("archer");
		warrior = GameObject.Find("warrior");
		w = getWizard();
		
	}
	
	// Update is called once per frame
	

	void Update () {
		w = getWizard();
		if(w != null){
			ArrayList spellList = w.getSpellListOfWizard().getSpellListInAction();//miramos la lista de los hechizos en accion(armor) 
			
			foreach(Spell s in spellList){
			
				if(s.getId() == 0){//si es armor
					ArmorSpell armor = (ArmorSpell) s;
					//si gameObject no ha sido destruido i el personaje que es asignado para ese hechizo es wizard
					if(wizard != null && armor.getPj().GetType().FullName == "Wizard" ){
						
						//asignamos la posicion del sistema de particulas a la del personaje selecionado
						Vector3 posSpell = new Vector3(armor.getPj().getTarget().transform.position.x,25,armor.getPj().getTarget().transform.position.z);	
						armor.getSystemParticles().transform.position = posSpell;
					}
					//si gameObject no ha sido destruido i el personaje que es asignado para ese hechizo es warrior
					if(warrior != null && armor.getPj().GetType().FullName == "Warrior" ){
						//asignamos la posicion del sistema de particulas a la del personaje selecionado
						Vector3 posSpell = new Vector3(armor.getPj().getTarget().transform.position.x,25,armor.getPj().getTarget().transform.position.z);	
						armor.getSystemParticles().transform.position = posSpell;
						
					}
					//si gameObject no ha sido destruido i el personaje que es asignado para ese hechizo es archer
					if(archer != null && armor.getPj().GetType().FullName == "Archer" ){
						//asignamos la posicion del sistema de particulas a la del personaje selecionado
						Vector3 posSpell = new Vector3(armor.getPj().getTarget().transform.position.x,25,armor.getPj().getTarget().transform.position.z);	
						armor.getSystemParticles().transform.position = posSpell;
						
					}
				}
			}
		}	
	}
	
	/*
	 * metodo que devuelve el objecto Wizard
	 */ 
	private Wizard getWizard(){
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.getTarget() == wizard){
				Wizard w = (Wizard) ch;
				return w;	
			}
		}
		return null;
	}
}
