using UnityEngine;
using System.Collections;

public class FastArrow : Equipment{
	private GameObject arrowEfect;
	
	public FastArrow(){
		this.name = "Fast Arrow";
		this.portrait = "fastArrow_";
		this.numberOfEquipment = 1;
		this.activation = false;
		this.duration = 20;
		this.maxDuration = 20;
		this.maxNumber = 1;
	}
	/*
	 * metodo que instancia el sistema de particulas 
	 */ 
	public override void effect(){
		arrowEfect = GameObject.Instantiate(Resources.Load("FastArrow")) as GameObject;
	}
	/*
	 * metodo que destruye el sistema de particulas
	 */ 
	public override void cancelEffect ()
	{
		GameObject.Destroy(arrowEfect);
	}
	
	
}
