using UnityEngine;
using System.Collections;

class ArmorSpell : Spell {
	private Pj pj;
	private bool flag;
	private GameObject armorSpellParticle;
	

	public ArmorSpell(){
		this.name = "Armor";
		this.portrait = "armorSpell_";
		this.duration = 20;
		this.numberOfSpells = 3;
		this.spellNumber = 0;
		this.activationSpell = false;
		this.flag = false;
		armorSpellParticle = null;

		
	}
	public ArmorSpell(Pj pjSelectedForSpell){
		this.pj = pjSelectedForSpell;
		this.name = "Armor";
		this.portrait = "armorSpell_";
		this.duration = 20;
		this.numberOfSpells = 3;
		this.spellNumber = 0;
		this.activationSpell = false;
		armorSpellParticle = null;
	

		
		
	}
	/*
	 * metodo que activa el efecto del hehcizo una vez lanzado 
	 */
	public  void efectSpell(Pj pj){
		
		if(!this.activationSpell){
			flag =false;
			
		}
		else{
			if(!flag){
				pj.setDexterity(pj.getDexterity() + 2);// sumamos valor de destreza al personaje aliado seleccionado
				armorSpellParticle = GameObject.Instantiate(Resources.Load("ArmorSpell")) as GameObject;//instanciamos el sistema de aprticulas del hechizo
				Vector3 posPj = new Vector3(pj.getTarget().transform.position.x,25,pj.getTarget().transform.position.z);//colocamos el hechizo en la posicion del personaje seleccionado
				armorSpellParticle.transform.position = posPj;// cambiamos la posicion delsistema de particulas el hechizo en funcion del movimento del personaje al que le afecta el hechizo
				flag = true;
			}
			this.numberOfSpells--;
			
		}
		
	}
	/*
	*reducimos la duracion del hechizo
	*/
	public void reduceDuration(){
		this.duration = duration-1;
	}
	/*
	* metodo que establece la duracion del hechizo 
	*/ 
	public void initiateDuration(){
		this.duration = 20;	
	}
	/*
	 * metodo que devuelve el personaje al que se le ha asignado el hechizo
	 */
	public Pj getPj(){
		return pj;	
	}
	/*
	 * metodo que destruye el sistema de particulas del hechizo armorspell
	 */
	public void cancelSpellEffect(){
		GameObject.Destroy(armorSpellParticle);	
	}
	/*
	 * metodo que devuelve el sistema de particulas del hechizo armorspell
	 */
	public GameObject getSystemParticles(){
		return 	armorSpellParticle;
	}
	/*public bool getTimeOutSpell(){
		return timeOutSpell;	
	}
	public void setTimeOutSpell(bool time){
		timeOutSpell = time;	
	}*/
	
}
