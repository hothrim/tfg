using UnityEngine;
using System.Collections;

public class Pnj : Character {
	
	protected GameObject followPnj;
	protected ArrayList enemiesForFollow = new ArrayList();
	protected GameObject lifeTexture; 
	protected GameObject deathTexture;
	protected int stateAnimation;
	protected ArrayList monitoring;
	
	/*
	 * metodo que devuelve barra de vida
	 */ 
	public GameObject getLifeTexture(){
		return lifeTexture;	
	}
	/*
	 * metodo que devuelve la barra de muerte
	 */ 
	public GameObject getDeathTexture(){
		return deathTexture;	
	}
	/*
	 * metodo que calcula la bueva anchura de la barra de vida de pnj
	 */ 
	public void setLifeTexture(float width){
		Rect insetRect = lifeTexture.guiTexture.pixelInset;
		insetRect.width = width; 
		lifeTexture.guiTexture.pixelInset = insetRect;
		
		
	}
	/*
	 * metodo que devuelve el GameObject followPNJ asociado
	 */ 
	public GameObject getFollowPnj(){
		return followPnj;	
	}
	/*
	 * metodo que añade a enemigo que seguir y atacar de la lista enemiesForFollow
	 */ 
	public void addEnemiesForFollow(GameObject gobject){
		if(!enemiesForFollow.Contains(gobject)){
			enemiesForFollow.Add(gobject);
		}
	}
	/*
	 * metodo que elimina a enemigo que seguir y atacar de la lista enemiesForFollow
	 */ 
	public void removeEnemiesForFollow(GameObject gobject){
		enemiesForFollow.Remove(gobject);
	}
	/*
	 * metodo que devuelvo de la lista de enemigos a seguir
	 */ 
	public ArrayList getEnemiesForFollow(){
		return enemiesForFollow;	
	}
	/*
	 * metodo que setea la lista de enmigos a seguir  
	 */ 
	public void setEnemiesForFollow(ArrayList enemiesForFollow){
		this.enemiesForFollow = enemiesForFollow;	
	}
	/*
	 * metodo que devuelve el estado de la animacion
	 */ 
	public int getStateAnimation(){
		return stateAnimation;	
	}
	/*
	 * metodo que setea la animacion
	 */ 
	public void setStateAnimation(int stateAnimation){
		this.stateAnimation = stateAnimation;	
	}
	/*
	 *  metodo que devuelve la lista de la ruta que hace el pnj
	 */ 
	public ArrayList getMonitoring(){
		return monitoring;
	}
	/*
	 *  metodo que setea la lista de la ruta que hace el pnj
	 */ 
	public void setMonitoring(ArrayList monitoring){
		this.monitoring = monitoring;
	}
	
	
}
