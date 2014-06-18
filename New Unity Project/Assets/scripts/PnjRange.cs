using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PnjRange : Pnj {

	
	private bool stop;
	public PnjRange(int id,GameObject pnjRange,ArrayList monitoring){
		
		this.monitoring = monitoring;
		followPnj = GameObject.Instantiate(Resources.Load("SeguimientoPnj")) as GameObject;
		lifeTexture = GameObject.Instantiate(Resources.Load("LifeTexture")) as GameObject;
		deathTexture = GameObject.Instantiate(Resources.Load("DeathTexture")) as GameObject;
		bloodParticle = GameObject.Instantiate(Resources.Load("BloodParticle")) as GameObject;
		bloodParticle.particleSystem.Stop();
		nombre = "PNJRANGE";
		strength = 10;//10
		dexterity = 16;
		constitution = 12;//12
		intelligence = 16;
		wisdom = 12;
		charisma = 10;
		level = 0;
		pg = 7;
		maxPg =pg;
		ca = 13;
		ataqueBase = 1;
		target = pnjRange;
		armorCa= 1;
		goFight = false;
		sight = 6;
		stop = false;
		distanceFight = 60;
		distanceEnemies = new Dictionary<Character,float>();
		death = false;
		idFight = -1;
	}
	public PnjRange(int id,int strength,int dexterity,int constitution,int intelligence,int wisdom,int charisma){

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
	/*
	 *  cambio de distancia de lucha
	 */ 
	public void setDistanceFight(int distanceFight){
		this.distanceFight = distanceFight;
	}
	
}

