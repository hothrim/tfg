using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character{
	protected int idFight;
	protected string nombre;
	protected int strength;
	protected int dexterity;
	protected int constitution;
	protected int intelligence;
	protected int wisdom;
	protected int charisma;
	protected int level;
	protected int pg;
	protected int maxPg;
	protected int ca;

	protected int ataqueBase;//ataque de arma, mas adelante calse arma
	protected int armorCa;//ca de armadura puesta,mas adelante clase armadura
	protected GameObject target;
	protected ArrayList enemiesList;

	protected bool goFight;
	protected int sight;
	
	protected float distanceFight;
	protected Dictionary<Character,float> distanceEnemies;//diccionario con key personajes value distancia
	
	protected ArrayList enemiesListOn;//lista de enemigos
	protected bool death;
	protected GameObject bloodParticle;//sistema de particulas de sangre de personajes 
	/*
	 * calculo de los PG iniciales de un personaje
	 */
	public int calculatePg(){
		return (int) 10 + ((constitution - 10) / 2);
		
	}
	/*
	 * calculo de Ca de los personajes
	 */
	public int calculateCa(){
		return (int) 10 + ((dexterity - 10) / 2);
	}
	/*
	 * calculo de modificador desteza de personaje
	 */
	public int getDexteryModifier(){
		return (int)(dexterity - 10) / 2;
	}
	/*
	 * calculo de modificador constitucion de personaje
	 */
	public int getConstitutionModifier(){
		return (int)(constitution - 10) / 2;
	}
	/*
	 * calculo de modificador fuerza de personaje
	 */
	public int getStrengthModifier(){
		return (int)(strength - 10) / 2;
	}
	public void setPg(int pg){
		this.pg = pg;
	}
	/*
	 * calulo de tirada de dados  parametros a y b marca rango de caras del dado
	 */
	public int crapsThrow(int a, int b){
		
		return Random.Range(a, b);
	}
	/*
	 * calculo de la tirada de iniciativa para combates
	 */
	public int iniciativeThrow(){
		return (int) crapsThrow(1,20) + getDexteryModifier() + level/2;//falta bonus que se tenga 
	}
	/*
	 * calculo de la tirada de iniciativa maxima para combates
	 */
	public int iniciativeBigThrow(){
		return (int) 20 + getDexteryModifier() + level/2;//falta bonus que se tenga 
	}
	/*
	 *calculo de tirada de ataque 
	 */
	public int atackThrow(){
		return (int) crapsThrow(1,20) + ataqueBase + getStrengthModifier();
	}
	/*
	 * calulo de tirada de daño 
	 */ 
	public int damageThrow(){
		return (int) crapsThrow(1,ataqueBase) + getStrengthModifier();
	}
	/*
	 * calculo de tirada de defensa 
	 */ 
	public int caThrow(){
		return 10 + armorCa + getDexteryModifier();//falta bono de escudo
	}
	/*
	 * metodo que devuelve fuerza
	 */ 
	public int getStrength(){
		return	strength;
	}
	/*
	 * metodo que modifica lista de enemigos de personaje
	 */ 
	public void setEnemies(ArrayList lista){
		enemiesList = lista;
	}
	
	public void addEnemy(Character ch){
		enemiesList.Add(ch);
	}
	public void removeEnemy(Character ch){
		enemiesList.Remove(ch);
	}
	/*
	 * metodo que devuelve primer enemigo de lista de enemigos
	 */
	public Character getFirstEnemy(){
		return (Character) enemiesList[0];	
	}
	/*
	 * metodo que devuelve lista de enemigos 
	 */ 
	public ArrayList getEnemies(){
		return enemiesList;	
	}
	/*
	 * metodo que elimina primer enemigo de la lista
	 */ 
	public void deleteEnemy(){
		enemiesList.Remove(enemiesList[0]);
	}
	/*
	 * metodo que devuelve vida de personaje 
	 */ 
	public int getPg(){
		return pg;
	}
	/*
	 * metodo que devuelve nombre de personaje
	 */ 
	public string getNombre(){
		return nombre;	
	}
	/*
	 * metodo que devuelve GameObject asociado a la instancia del personaje
	 */ 
	public GameObject getTarget(){
		return target;
	}
	/*
	 * metodo que devuelve si personaje esta en lucha
	 */ 
	public bool getGoFight(){
		return goFight;
	}
	/*
	 * metodo que modifica si personaje esta en lucha
	 */
	public void setGoFight(bool goFight){
		this.goFight = goFight;
	}
	/*
	 * metodo que devuelve puntuacion de caracteristica avistar 
	 */
	public int getSight(){
		return sight;
	}
	/*
	 * metodo que devuelve collider de gameobject
	 */ 
	/*public Collider getCollider(){
		return col;	
	}*/
	/*
	 *  metodo que devuelve distancia de lucha de personaje
	 */ 
	public float getDistanceFight(){
		return distanceFight;
	}
	/*
	 * metodo que devuelve dicionario de personajes
	 */ 
	public Dictionary<Character,float> getDistanceEnemies(){
		return distanceEnemies;
	}
	
	
	public ArrayList getEnemiesList(){
		return enemiesListOn;	
	} 
	
	public void setEnemiesList(ArrayList enemiesListOn){
		this.enemiesListOn = enemiesListOn;	
	}
	/*
	 * metodo que modifica estado de vida o muerte
	 */
	public void setDeath(bool death){
		this.death = death;
	}
	/*
	 * metodo que devuelve si personaje esta muerto 
	 */
	public bool getDeath(){
		return death;
	}
	/*
	 * metodo que devuelve maxima vida que tiene personaje
	 */ 
	public int getMaxPg(){
		return maxPg;	
	}
	/*
	 * metodo que modifica destreza de personaje
	 */ 
	public void setDexterity(int dexterity){
		this.dexterity = dexterity;
	}
	/*
	 * metodo que devuelve destreza de personaje
	 */ 
	public int getDexterity(){
		return dexterity;
	}
	/*
	 * metodo que devuelve inteligencia de personaje
	 */ 
	public int getIntelligence(){
		return intelligence;
	}
	/*
	 * metodo que devuelve nivel de personaje
	 */ 
	public int getLevel(){
		return level;	
	}
	/*
	 * metodo que modifica fuerza de personaje
	 */ 
	public void setStrength(int strength){
		this.strength = strength;
		
	}
	/*
	 * metodo que devuelve sistema de particulas asociado al personaje
	 */ 
	public GameObject getBloodParticle(){
		return bloodParticle;
	}
	/*
	 * metodo que modifica id de lucha del personaje
	 */
	public void setIdFight(int idFight){
		this.idFight = idFight;
	}
	/*
	 * metodo que devuelve id de lucha del personaje
	 */
	public int getIdFight(){
		return idFight;
	}
}
