using UnityEngine;
using System.Collections;

public class SpellManagement : MonoBehaviour {
	GameObject wizard;
	
	
	// Use this for initialization
	void Start () {
		wizard = GameObject.Find("wizard");//localizo gameOBject wizard
		InvokeRepeating("imArmor",1.0f,1.0f);//a cada segundo llamo a la funcion imArmor
		
	}
	
	// Update is called once per frame
	void Update () {
		//miro que hechizos tiene activados el wizard
		
		if(wizard != null){
			Wizard w = getWizard();
			
			SpellList spellList = w.getSpellListOfWizard();
			ArrayList spellsInAction = spellList.getSpellListInAction();//lista de hehcizos para subir habildades de pjs
			ArrayList spellsNotTime = spellList.getSpellList();
			for(int i = 0; i < spellsNotTime.Count;i++){
				Spell s = (Spell) spellsNotTime[i];
				if(s.getId() == 1){//si esta activado el hehcizo magicBullet
					managementBulletMagicSpell(s,spellList);//desactivo el hechizo
				}
			}
			
		}
			
	}
	
	/*
	 * miramos si el hechizo esta activado(boton de HUD), si es correcto reducimos la duracion
	 * del hechizo, y en caso de que la duracion del hechizo termine restablecemos la destreza inicial
	 * y cancelamos el sistema de particulas asociado al hechizo
	*/
	private void managementArmorSpell(Spell s,SpellList spellList){
		
		ArmorSpell armorspell = (ArmorSpell) s;
		
		if(armorspell.getActivactionSpell()){
			armorspell.reduceDuration();
				
			if(s.getDuration() > 0 ){
				armorspell.setActivationSpell(true);
			}
			else{
				armorspell.initiateDuration();	
				armorspell.setActivationSpell(false);
				armorspell.getPj().setDexterity(armorspell.getPj().getDexterity()-2);
				armorspell.cancelSpellEffect();
				spellList.removeSpellInAction(armorspell);
			
				
			}
		}
	}
	private void managementBulletMagicSpell(Spell s,SpellList spellList){
		MagicBulletSpell magicBulletspell = (MagicBulletSpell) s;
		if(magicBulletspell.getActivactionSpell()){
			magicBulletspell.setActivationSpell(false);
		}	
		
		
	}
	/*
	 * metodo que devuelve el objeto Wizard 
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
	
	private void imArmor(){
		if(wizard != null){
			Wizard w = getWizard();
			SpellList spellList = w.getSpellListOfWizard();
			ArrayList spellsInAction = spellList.getSpellListInAction();//lista de hehcizos para subir habildades de pjs
			ArrayList spellsNotTime = spellList.getSpellList();//lista de hecizos de tiempo inmediato
			//gestion de los distintos hechizos de la lista del mago
			for(int i = 0; i < spellsInAction.Count;i++){
				Spell s = (Spell) spellsInAction[i];
				if(s.getId() == 0){//si hechizo es Armor gestiono el hechizo Armor
					managementArmorSpell(s,spellList);	
				}

			}	
		}
	}

}
