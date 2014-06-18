using UnityEngine;
using System.Collections;

abstract public class Equipment{
	protected string name;
	protected string portrait;
	protected int numberOfEquipment;
	protected bool activation;
	protected int duration;
	protected int maxDuration;
	protected int maxNumber;
	/*
	 *metodo que devuelve el nombre de habilidad 
	 */ 
	public string getName(){
		return name;
	}
	/*
	 *metodo que devuelve el nombre de portrait habilidad
	 */
	public string getPortrait(){
		return portrait;
	}
	/*
	 *metodo que devuelve numero de habilidades
	 */
	public int getNumberOfEquipment(){
		return numberOfEquipment;
	}
	/*
	 * metodo que devuelve si habilidad esta activa
	 */
	public bool getActivation(){
		return activation;
	}
	/*
	 * metodo que setea activacion de habilidad
	 */
	public void setActivation(bool activation){
		this.activation = activation; 	
	}
	/*
	 *metodo que devuelve duracion de habilidad 
	 */
	public int getDuration(){
		return duration;
	}
	/*
	 * metodo que reduce habilidad
	 */
	public void reduceDuration(){
		this.duration--;
	}
	/*
	 * metodo que setea duracion de habilidad
	 */
	public void setDuration(int duration){
		this.duration = duration;
	}
	/*
	 * metodo que reduce el numero de habilidades
	 */
	public void reduceNumberOfEquipment(){
		this.numberOfEquipment--;
	}
	/*
	 * metodo que setea el numero de habilidades
	 */
	public void setNumberOfEquipment(int number){
		this.numberOfEquipment = number;
	}
	/*
	 * metodo que devuelve el numero maximo de habilidad 
	 */
	public int getMaxNumber(){
		return maxNumber;
	}
	/*
	 * metodo que devuelve el numero maximo de duracion
	 */
	public int getMaxDuration(){
		return maxDuration;
	}
	
	public abstract void effect();
	public abstract void cancelEffect();
	
}
