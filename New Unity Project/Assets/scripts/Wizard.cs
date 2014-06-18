using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Wizard : Pj {
	
	
	private bool stop;
	private SpellList spells;
	public Wizard(GameObject pj){
		selectionPj = GameObject.Instantiate(Resources.Load("SelectorPjs")) as GameObject;
		internalCompass = GameObject.Instantiate(Resources.Load("InternalCompassWizard")) as GameObject;
		bloodParticle = GameObject.Instantiate(Resources.Load("BloodParticle")) as GameObject;
		bloodParticle.particleSystem.Stop();
		nombre = "Wizard";
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
		spells = new SpellList();
		stateAnimation = 0;
		search = 2;
		distanceFight = 15;
		target = pj;
		
		distanceEnemies = new Dictionary<Character,float>();
		stop = false;
		death = false;
		portrait = "wizard";
		idFight = -1;
		
	}
	/*
	 * metodo que devuelve la clase SpelListOfWizard
	 */ 
	public SpellList getSpellListOfWizard(){
		return (SpellList)spells;
	}
	/*
	 * metodo que setea si lanza un hechizo
	 */
	public void setStop(bool stop){
		this.stop = stop;
	}
	/*
	 * metodo que devuevle si el mago va ha hechizar
	 */
	public bool getStop(){
		return stop;	
	}
}