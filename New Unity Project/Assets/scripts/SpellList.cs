using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpellList{
	private ArrayList list;//lista de los diferentes hehcizos que existen y se usan de efecto inmediato(spells de daño)
	private ArrayList listSpellInAction;//lista de los hechizos que son de accion en el tiempo, hechizos de efecto
	public SpellList(){
		list = new ArrayList();	
		list.Add(new ArmorSpell());
		list.Add(new MagicBulletSpell());
		listSpellInAction = new ArrayList();
	}
	/*
	 * metodo que devuelve la lista de hechizos
	 */ 
	public ArrayList getSpellList(){
		return list;	
	}
	/*
	 * metodo que devuelve la lista de hechizos en accion
	 */
	public ArrayList getSpellListInAction(){
		return listSpellInAction;	
	}
	/*
	 * metodo que añade hechizo a lista de hechizos en accion
	 */
	public void addSpellInAction(Spell s){
		listSpellInAction.Add(s);
	}
	/*
	 * metodo que elimina hechizo de la lista de hechizos en accion
	 */
	public void removeSpellInAction(Spell s){
		listSpellInAction.Remove(s);
	}
	
}
