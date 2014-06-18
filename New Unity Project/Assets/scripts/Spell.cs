using UnityEngine;
using System.Collections;

public class Spell {
	protected string name;
	protected string portrait;
	protected int duration; //duracion de hechizo(T=0 es instantaneo)
	protected int numberOfSpells;	//numero de hechizos que tienes en lvl 1
	protected int spellNumber;//numero del hechizo
	protected bool activationSpell;//hechizo en curso
	
	/*
	 * metodo que devuelve el nombre del hechizo
	 */
	public string getName(){
		return name;
	}
	/*
	 * metodo que devuelve el nombre de la imagen
	 */ 
	public string getPortrait(){
		return portrait;
	}
	/*
	 * metodo que devuelve la duracion del hechizo
	 */ 
	public int getDuration(){
		return duration;
	}
	/*
	 * metodo que devuelve el numero de hehcizos restates 
	 */
	public int getNumberOfSpells(){
		return numberOfSpells;
	}
	/*
	 * metodo que devuelve si hehcizo esta activado
	 */
	public bool getActivactionSpell(){
		return activationSpell;	
	}
	/*
	 * metodo que devuelve el identificador del hechizo
	 */
	public int getId(){
		return spellNumber;	
	}
	/*
	 * metodo que setea la activacion de un hechizo 
	 */ 
	public void setActivationSpell(bool activationSpell){
		this.activationSpell = activationSpell;	
	}
	/*
	 * metodo que reduce el numero de hechizos
	 */ 
	public void reduceNumberOfSpell(){
		this.numberOfSpells = this.numberOfSpells - 1; 
	}
	/*
	 * metodo que setea el numero de hechizos
	 */ 
	public void setNumberOfSpell(int numberOfSpells){
		this.numberOfSpells = numberOfSpells; 
	}
	
	
}
