using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Archer : Pj {
	private FastArrow fastArrow;
	private bool habilityActivate;
	
	public Archer(GameObject pj){
		selectionPj = GameObject.Instantiate(Resources.Load("SelectorPjs")) as GameObject;//creacio del GameObject que representa la aureola de seleccio
		internalCompass = GameObject.Instantiate(Resources.Load("InternalCompassArcher")) as GameObject;//creacio de la bruixola interna per fer els girs dels personatges
		bloodParticle = GameObject.Instantiate(Resources.Load("BloodParticle")) as GameObject;//creacio del sistema de particulas del personatge
		bloodParticle.particleSystem.Stop();
		nombre = "Archer";
		strength = 10;
		dexterity = 16;
		constitution = 12;
		intelligence = 16;
		wisdom = 12;
		charisma = 10;
		level = 0;
		pg = 7;
		maxPg =pg;
		ca = 13;
		ataqueBase = 1;
		armorCa = 1;
		goFight = false;
		sight = 4;
		search = 20;
		distanceFight = 80;
		target = pj;
		distanceEnemies = new Dictionary<Character,float>();
		death = false;
		portrait = "archer";
		fastArrow = new FastArrow();
		stateAnimation = 0;
		habilityActivate = false;
		idFight = -1;
	}
	
	/*
	 * metode que retorna la habilitat del arquer
	 */
	public FastArrow getEquipment(){
		return 	fastArrow;
	}
	/*
	 * metode que retorna si la habilitat ha estat activada
	 */
	public bool getFastArrowActivate(){
		return habilityActivate;
	}
	/*
	 * metode que activa o desactiva la habilitat del arquer
	 */
	public void setFastArrowActivate(bool habilityActivate){
		this.habilityActivate = habilityActivate;
	}	
}