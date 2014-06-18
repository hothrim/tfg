using UnityEngine;
using System.Collections;

class MagicBulletSpell : Spell {
	private Pnj pnj;
	private GameObject magicBulletSpellParticle;
	private int damage;
	public MagicBulletSpell(){
		this.name = "Magic Bullet";
		this.portrait = "magicBullet_";
		this.duration = 1;
		this.numberOfSpells = 2;
		this.spellNumber = 1;
		this.activationSpell = false;
		magicBulletSpellParticle = null;
		damage = 0;
			
	}
	public MagicBulletSpell(Pnj pjSelectedForSpell){
		this.pnj = pjSelectedForSpell;
		this.name = "Bullet";
		this.portrait = "magicBullet_";
		this.duration = 0;
		this.numberOfSpells = 2;
		this.spellNumber = 1;
		this.activationSpell = false;
		magicBulletSpellParticle = null;
		damage = 0;
		pnj = pjSelectedForSpell;
		
		
	}
	/*
	 * calcula el efecto que hara a un pnj en el momento que impacte
	 */ 
	public  void efectSpell(Pnj pnj,Wizard w){
		
		
		int shots = w.getLevel() + 1;//el numero de lanzamientos que se haran para calcular el daño
		int damage_ = 0;
		if(Vector3.Distance(pnj.getTarget().transform.position,w.getTarget().transform.position) < 80 ){//calculo del daño si esta a esta distancia
			//se instancia el sistema de particulas del hechizo
			magicBulletSpellParticle = GameObject.Instantiate(Resources.Load("MagicBullet")) as GameObject;
			//se calcula la posicion donde aparece el sistema de particulas
			Vector3 newPosSpell = new Vector3(w.getTarget().transform.position.x,10,w.getTarget().transform.position.z);
			magicBulletSpellParticle.transform.position = newPosSpell;
			//calculo del daño del hechizo
			damage_ += Random.Range(1, 6*shots);
			//se asocia el daño añ hehcizo lanzado
			MagicBulletSpell m = (MagicBulletSpell)w.getSpellListOfWizard().getSpellList()[spellNumber];
			m.damage = damage_;
			//se asocia el personaje que recibira el hechizo
			m.pnj = pnj;
			numberOfSpells--;
		
		}
	}
	
	
	/*
	 * metodo que retorna el personaje al que se lance el hehizo
	 */ 
	public Pnj getPj(){
		return pnj;	
	}
	/*
	 * metodo que setea la duracion del hechizo
	 */ 
	public void reduceDuration(){
		this.duration = duration-1;
	}
	/*
	 * metodo establecemos la duracion del hechizo
	 */ 
	public void initiateDuration(){
		this.duration = 1;	
	}
	/*
	 *  metodo que devuelve el daño del hechizo
	 */
	public int getDamage(){
		return damage;
	}
	/*
	 *  metodo que devuelve el sistema de particulas
	 */ 
	public GameObject getSpellParticle(){
		return 	magicBulletSpellParticle;
	}
	
	
	
	
}
