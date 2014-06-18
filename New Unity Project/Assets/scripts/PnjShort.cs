using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PnjShort : Pnj{

	private bool stop;
	public PnjShort(int id,GameObject pnj,ArrayList monitoring){
		
		this.monitoring = monitoring;
		followPnj = GameObject.Instantiate(Resources.Load("SeguimientoPnj")) as GameObject;
		lifeTexture = GameObject.Instantiate(Resources.Load("LifeTexture")) as GameObject;
		deathTexture = GameObject.Instantiate(Resources.Load("DeathTexture")) as GameObject;
		bloodParticle = GameObject.Instantiate(Resources.Load("BloodParticle")) as GameObject;
		bloodParticle.particleSystem.Stop();

		nombre = "PNJ";
		strength = 16;//16
		dexterity = 10;
		constitution = 10;
		intelligence = 10;
		wisdom = 10;
		charisma = 9;
		level = 0;
		pg = 13;
		maxPg =pg;
		ca = 11;
		ataqueBase = 1;
		target = pnj;
		armorCa= 4;
		goFight = false;
		sight = 6;
		stop = false;
		distanceFight = 15;
		distanceEnemies = new Dictionary<Character,float>();
		death = false;
		idFight = -1;
	}
	public PnjShort(int id,int strength,int dexterity,int constitution,int intelligence,int wisdom,int charisma){
		this.strength = strength;
		this.dexterity = dexterity;
		this.constitution = constitution;
		this.intelligence = intelligence;
		this.wisdom = wisdom;
		this.charisma = charisma;
		this.level = 1;
		this.pg = calculatePg();
		this.ca = calculateCa();
		this.ataqueBase = 1;
		this.armorCa= 4;
		
		
	}
	/*
	 *metodo que setea el movimiento del pnjRange
	 */
	public void setStop(bool stop){
		this.stop = stop;
	}
	/*
	 *metodo que devuelve boolean si el pnjRange ha parado el movimiento  
	*/
	public bool getStop(){
		return stop;	
	}

}
