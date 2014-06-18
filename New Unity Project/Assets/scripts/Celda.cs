using UnityEngine;
using System.Collections;

/*
 * definicion de la clase celda
 */
public class Celda{
	
	private Character character;
	private int priority;
	private int count;
	
	public Celda(Character character, int priority){
		this.character = character;
		this.priority = priority;
		this.count = priority;
		
	}
	public Character getCharacter(){
		return character;	
	}
	public int getPrioridad(){
		return priority;	
	}
	public void setCount(int priority){
		count = priority;
	}
	public int getCount(){
		return count;	
	}
	
	
}
