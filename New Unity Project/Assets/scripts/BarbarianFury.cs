using UnityEngine;
using System.Collections;

public class BarbarianFury : Equipment{
	private GameObject barbarianEfect;

	public BarbarianFury(){
		this.name = "BarbarianFury Fury";
		this.portrait = "barbarianFury";
		this.numberOfEquipment = 1;
		this.activation = false;
		this.duration = 20;
		this.barbarianEfect = null;
		this.maxDuration = 20;
		this.maxNumber = 1;
	}
	/*
	 * metodo que crea el sistema de particulas de la habilidad del guerrero
	 */
	public override void effect(){
		barbarianEfect = GameObject.Instantiate(Resources.Load("BarbarianFurySpell")) as GameObject;
	}
	/*
	 * metodo que destruye el sistema de particulas de la habilidad del guerrero
	 */
	public override void cancelEffect(){
		
		GameObject.Destroy(barbarianEfect);
		
	}
	
	
	
	
}
