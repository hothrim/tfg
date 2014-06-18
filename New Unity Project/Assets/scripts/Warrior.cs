using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Warrior : Pj {
	
	private BarbarianFury barbarianFury;
	private bool habilityActivate; 
	
	public Warrior(GameObject pj){
		selectionPj = GameObject.Instantiate(Resources.Load("SelectorPjs")) as GameObject;
		internalCompass = GameObject.Instantiate(Resources.Load("InternalCompassWarrior")) as GameObject;
		bloodParticle = GameObject.Instantiate(Resources.Load("BloodParticle")) as GameObject;
		bloodParticle.particleSystem.Stop();
		nombre = "Warrior";
		strength = 16;//16
		dexterity = 12;
		constitution = 16;//16
		intelligence = 10;
		wisdom = 10;
		charisma = 9;
		level = 0;
		pg = 13;
		maxPg =pg;
		ca = 11;
		ataqueBase = 2;
		armorCa = 4;
		goFight = false;
		sight = 6;
		distanceFight = 15;
		target = pj;
		distanceEnemies = new Dictionary<Character,float>();
		death = false;
		portrait = "warrior";
		barbarianFury = new BarbarianFury();
		stateAnimation = 0; 
		habilityActivate = false;
		search = 2;
		idFight = -1;
		
	}
	/*
	 * metodo que devuelve la habilidad propia del guerrero
	 */ 
	public BarbarianFury getEquipment(){
		return 	barbarianFury;
	}
	/*
	 * metodo que indica si la habilidad propia del guero esta activada
	 */
	public bool getBarbaryanFuryActivate(){
		return habilityActivate;
	}
	/*
	 *  metodo que setea el valor booleano de la actividad propia del guerrero
	 */ 
	public void setBarbaryanFuryActivate(bool habilityActivate){
		this.habilityActivate = habilityActivate;
	}
	
	
}
