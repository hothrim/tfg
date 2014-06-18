using UnityEngine;
using System.Collections;

public class Pj : Character{
	private bool atackType;
	
	private Pnj enemyFollow;
	
	protected string portrait;
	protected GameObject selectionPj;
	protected GameObject internalCompass;
	protected int stateAnimation;
	protected int search;
	public Pj(){
		nombre = "PJ";
		strength = 16;
		dexterity = 10;
		constitution = 16;
		intelligence = 10;
		wisdom = 10;
		charisma = 9;
		level = 0;
		pg = 13;
		ca = 11;
		ataqueBase = 1;
		armorCa = 4;
		goFight = false;
		atackType = false;
		
		
	}
	public Pj(int strength,int dexterity,int constitution,int intelligence,int wisdom,int charisma){
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
		this.armorCa = 4;
		atackType = false;


		
	}
	/*
	 * metodo que setea  atackType
	 */
	public void setAtackType(bool atackType){
		this.atackType = atackType;
	}
	/*
	 * metodo que devuelve atackType
	 */
	public bool getAtackType(){
		return atackType;
	}
	/*
	 * metodo que que devuelve la aureola de cada personaje
	 */
	public GameObject getSelectionPj(){
		return selectionPj;	
	}
	/*
	 * metodo que setea al enemigo personaje que se va a seguir
	 */ 
	public void setFollowPnj(Pnj enemyFollow){
		this.enemyFollow = enemyFollow;
	}
	/*
	 * metodo que devuelve al personaje que se va  a seguir
	 */
	public Pnj getFollowPnj(){
		return enemyFollow; 	
	}
	/*
	 * metodo que devuelve el portrait del personaje
	 */ 
	public string getPortrait(){
		return portrait;	
	}
	/*
	 *  metdo que setea el portrait que se mostrara
	 */ 
	public void setPortrait(string portrait){
		this.portrait = portrait;	
	}
	/*
	 * metodo que devuelve el GameOBject brujula asociado al personaje
	 */ 
	public GameObject getCompass(){
		return internalCompass;	
	}
	/*
	 * metodo que devuelve estado de animacion
	 */ 
	public int getStateAnimation(){
		return stateAnimation;	
	}
	/*
	 * metodo que setea animacion
	 */ 
	public void setStateAnimation(int stateAnimation){
		this.stateAnimation = stateAnimation;	
	}
	/*
	 *  metodo que devuelve puntuacion de habilidad busqueda
	 */ 
	public int getSearch(){
		return search;	
	}
	
	
}
