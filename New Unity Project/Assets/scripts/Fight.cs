using UnityEngine;
using System.Collections;
using QueueSpace;
using System.Collections.Generic;

public class Fight : MonoBehaviour {
	private int state;
	private PriorityQueue queue;
	private ArrayList fightersList;
	private ArrayList enemiesList;
	private ArrayList playersList;
	private int id ;
	private int bigThrowMax;
	private string sms;
	
	
	public Fight(Character character1,Character character2,int id){
		//creacion de las listas
		fightersList = new ArrayList();
		enemiesList = new ArrayList();
		playersList = new ArrayList();
		state = 1;
		sms="";
		//añado a los dos primeros personajes que lucharan en un principio a la lista de luchadores
		fightersList.Add(character1);
		fightersList.Add(character2);
		
		this.id=id;
		
		foreach(Character ch in fightersList){
			//añado a los respectivos enemigos de cada bando
			if(ch.GetType().FullName == "PnjShort" || ch.GetType().FullName == "PnjRange"){
				enemiesList.Add(ch);
				if(ch.GetType().FullName == "PnjShort"){
					PnjShort pns = (PnjShort) ch;
					
				}
			}else if(ch.GetType().FullName == "Wizard" || ch.GetType().FullName == "Warrior" || ch.GetType().FullName == "Archer"){
				playersList.Add(ch);	 				
			}
		}
	}
	/*
	 * metodo que inicia los turnos
	 */
	public void InitiateTurns(){
		ArrayList bigThrow = new ArrayList();
		if(state == 1){
			//instancio nueva cola de prioridades
			queue = new PriorityQueue();
			//guardo en vector tiradas mas altas de cada luchador
			foreach(Character ch in fightersList){
				bigThrow.Add(ch.iniciativeBigThrow());
			}
			//guardo la tirada mas alta hecha por un luchador
			bigThrowMax= getGeneralIniciativeThrowFight(bigThrow) + 1;
			//guardo las prioridades segun valores de "tiempo"
			foreach(Character ch in fightersList){
				//el resultado mas bajo sera el mejor(prioridad es el tiempo de "turno"), las coloco en la cola
				queue.Enqueue(new Celda(ch,bigThrowMax-ch.iniciativeThrow()));
				if(ch.GetType().FullName == "PnjShort" || ch.GetType().FullName == "PnjRange"){
					//pongo como enemigos para los PNJS a los PJ
					ch.setEnemies(playersList);
					if(ch.GetType().FullName == "PnjShort"){
						PnjShort pns = (PnjShort) ch;
						//para movimiento y cambio a animacion de parada en caso de ser PnjShort
						pns.setStop(true);
						pns.setStateAnimation(0);
					}
					if(ch.GetType().FullName == "PnjRange"){
						PnjRange pnr = (PnjRange) ch;
						//para movimiento y cambio a animacion de parada en caso de ser PnjRange
						pnr.setStateAnimation(0);
					}
					
				}else if(ch.GetType().FullName == "Wizard" || ch.GetType().FullName == "Warrior" || ch.GetType().FullName == "Archer"){
					//pongo como enemigos para los PJS a los PNJ
					ch.setEnemies(enemiesList);
					Pj pjAtack = (Pj) ch;
					if(ch.GetType().FullName == "Wizard" || ch.GetType().FullName == "Warrior"){
						if(pjAtack.getFirstEnemy().GetType().FullName == "PnjRange"){
							//CharacterController cc = (CharacterController) pjAtack.getTarget().collider;
							
							pjAtack.setStateAnimation(1);
							
						}
						else{
							pjAtack.setStateAnimation(0);
						}
					}else if(ch.GetType().FullName == "Archer" ){
						pjAtack.setStateAnimation(0);
					}
				}
				
			}
			
			
			
			
		}
		//asigno mensaje
		sms+="Tirada de Iniciativas\n";
		sms+=queue.toString() + "\n";
		
		state = 2;
		
	}
	
	public void atackAndDamage(){
		
		sms="";
		//reduzco el contador de las celdas mientras ninguna llegue a 0
		if(queue.celdaAt0() == null){
			queue.reduceCount();
		}else{
			
			//cojo la celda que ha llegado a 0
			Celda c = queue.celdaAt0();
			//coloco la celda que ha llegado antes a 0  la coloco como primera de la cola
			queue.strain(c);
			//cojo el caracter asociado a esa celda
			Character chActual = queue.firstQueue();
			
			sms+="Turno de :"+ chActual.getNombre() + "\n";
			//cojo el primer enemigo de ese personaje !!!OJO!!! (COGER ENEMIGO QUE SE SELECCIONO) // pasar el personaje con el que se hace raycast
			
			
			
			Character enemyActual = null;
			//Character enemyActual = chActual.getFirstEnemy();
			if(chActual.GetType().FullName == "Wizard" || chActual.GetType().FullName == "Warrior" || chActual.GetType().FullName == "Archer"){
				
				
				Pj pjActual = (Pj) chActual;
				Pnj pnj = pjActual.getFollowPnj();
				enemyActual = (Character) pnj;
				
				if(enemyActual == null || enemyActual.getDeath()){
					enemyActual = chActual.getFirstEnemy();
				}
				
				//Character enemyActual = chActual.getFirstEnemy();
			}	
			else if(chActual.GetType().FullName == "PnjRange" || chActual.GetType().FullName == "PnjShort" ){
				enemyActual = chActual.getFirstEnemy();
			}
			
			
			//si esta vivo el pj
			if(chActual.calculatePg() > 0){
				
				sms+="Tirada de Ataque\n";
				//si la distancia de lucha es la correcta
				
				if(chActual.getDistanceFight() >= Vector3.Distance(chActual.getTarget().transform.position,enemyActual.getTarget().transform.position)) {
					//lanzamiento de ataque por parte del personaje que tiene el turno
					int atackThrow = chActual.atackThrow();
					
					sms+=chActual.getNombre()+ " saca " + atackThrow + "\n";
					
					sms+="Tirada de CA\n";
					//lanzamiento de defensa por parte del personaje al que se ataca
					int caThrow = enemyActual.caThrow();
					
					sms+=enemyActual.getNombre()+ " saca " + caThrow+"\n";
					//activamos animacion de ataque
					if(chActual.GetType().FullName == "Wizard" || chActual.GetType().FullName == "Warrior" || chActual.GetType().FullName == "Archer"){
						Pj pjAtack = (Pj) chActual;
						pjAtack.setStateAnimation(2);
						if(enemyActual.GetType().FullName == "PnjShort"){
							PnjShort pnj = (PnjShort) enemyActual;
							
							pnj.setStop(false);
						}
						
						
						
					}
					//activamos animacion de ataque a corta distancia
					if(chActual.GetType().FullName == "PnjShort" ){
						Pnj pnjAtack = (Pnj) chActual;
						PnjShort pnjShort = (PnjShort) pnjAtack;
						
						
						pnjAtack.setStateAnimation(2);
					
					}
					//activamos animacion de ataque a larga distancia
					if(chActual.GetType().FullName == "PnjRange"){
						Pnj pnjAtack = (Pnj) chActual;
						
						pnjAtack.setStateAnimation(3);
					}
					//si la tirada de ataque  tiene exito
					if(atackThrow >= caThrow){
						
						sms += chActual.getNombre()+ " tiene exito en tirada de ataque\n" ;
						
						sms += "Tirada de daño\n";
						//tirada de daño por parte del personaje que tiene el turno
						int damageEnemy = chActual.damageThrow();
						
						//quito vida al enemigo del personaje que tiene el turno
						sms += enemyActual.getNombre()+ " pierde " + damageEnemy + "puntos de golpe\n";
						enemyActual.setPg(enemyActual.getPg() - damageEnemy);
						
						//reproduzco el sistema de particulas 
						enemyActual.getBloodParticle().particleSystem.Play();
						
						
						
						
							
					}else{
						
						sms += chActual.getNombre()+ " falla en tirada de ataque\n";
						
					}			
				}else{
					
					if(chActual.GetType().FullName == "PnjShort"){
						Pnj pnj = (Pnj) chActual;
						
						pnj.setStateAnimation(1);
					}
					sms += chActual.GetType().FullName + " No puede atacar.Enemigo fuera de alcance\n";
				}
				
				
			}
			
			state = 3;
		}
		
		
	}
	
	public void deathNegotiation(){
		
		
		sms="";
		Character chActual = queue.celdaAt0().getCharacter();
		Character enemyActual = null;
		if(chActual.GetType().FullName == "Wizard" || chActual.GetType().FullName == "Warrior" || chActual.GetType().FullName == "Archer"){
			Pj pjActual = (Pj) chActual;
			Pnj pnj = pjActual.getFollowPnj();
			enemyActual = (Character) pnj;
				
			if(enemyActual == null || enemyActual.getDeath()){
				enemyActual = chActual.getFirstEnemy();
			}
			
		}
		else if(chActual.GetType().FullName == "PnjShort" || chActual.GetType().FullName == "PnjRange"){
			enemyActual = chActual.getFirstEnemy();
			
		}
		
		
		
		//si el enemigo tiene vida 0
		if(enemyActual.getPg() <= 0){
			//modifico la animacion a parado de Pj
			if(chActual.GetType().FullName == "Wizard" || chActual.GetType().FullName == "Warrior" || chActual.GetType().FullName == "Archer"){
				Pj pjAtack = (Pj) chActual;
				pjAtack.setStateAnimation(0);
			}
			if(chActual.GetType().FullName == "PnjShort" || chActual.GetType().FullName == "PnjRange"){
				//modifico la animacion a parado de PNJ
				Pnj pjAtack = (Pnj) chActual;
				pjAtack.setStateAnimation(0);
			}
			sms += enemyActual.getNombre()+ " Ha muerto\n";
			
			//elimino el enemigo del personaje que ha vencido 
			//chActual.deleteEnemy();
			
			//elimino de la cola la celda con el actual enemigo
			queue.deleteQueue(enemyActual);
			
			queue.toString();
			//elimino de la lista de luchadores al enemigo actual
			fightersList.Remove(enemyActual);
			//si el enemigo es un PJ
			if(enemyActual.GetType().FullName == "Warrior" || enemyActual.GetType().FullName == "Wizard" || enemyActual.GetType().FullName == "Archer"){
				
				playersList.Remove(enemyActual);//lo elimino de su lista
				
				Pnj pnjTmp = (Pnj) chActual;
				
				
				//elimina personaje a perseguir de la lista de followers de pnjController
				
				
				//creo la muerte y asigno como muerto a los PJ
				createDeadPnj(enemyActual,enemyActual.getTarget().transform.position);
				killCharacter(enemyActual);
				foreach(Character fighters in fightersList){
					if(fighters.GetType().FullName == "PnjRange" || fighters.GetType().FullName == "PnJShort"){
						//asigno aniamciondes de parada
						Pnj pnj = (Pnj) fighters;
						pnj.setStateAnimation(0);
						fighters.setGoFight(false);
						GoFight.sendSms = false;
						
					}
				}
				
				
			}
			//si enemigo en un PNJ
			else if(enemyActual.GetType().FullName == "PnjRange" || enemyActual.GetType().FullName == "PnjShort"){
				
				//elimino de la lista de enemigos al enemigo actual
				
				enemiesList.Remove(enemyActual);
				Pnj pnjTmp = (Pnj) enemyActual;
				//creo muerte del personaje y asigno a personaje cmo muerto
				Destroy(enemyActual.getBloodParticle());
				Destroy(pnjTmp.getFollowPnj());
				createDeadPnj(enemyActual,enemyActual.getTarget().transform.position);
				
				killCharacter(enemyActual);
				
				foreach(Character fighters in fightersList){
					//cuando acaba la batalla pongo la animacion de parada
					if(fighters.GetType().FullName == "Warrior" || fighters.GetType().FullName == "Wizard" || fighters.GetType().FullName == "Archer"){
						Pj pj = (Pj) fighters;
						
						pj.setStateAnimation(0);
						fighters.setGoFight(false);
						GoFight.sendSms = false;
					}
				}
				
			}
			
			chActual.getEnemies().Remove(enemyActual);
			
			foreach(Character ch in SearchCharacters.characterList){
				ch.getDistanceEnemies().Remove(enemyActual);
				ch.getEnemiesList().Remove(enemyActual);
				
			}
			
			
			
			
		}
		if(enemyActual.GetType().FullName == "Warrior" || enemyActual.GetType().FullName == "Wizard" || enemyActual.GetType().FullName == "Archer"){
			//ordena la cola y pongo en estado de ataque de nuevo
			
			if(playersList.Count > 0){
				
				queue.Dequeue();
	
				state = 2;
			}else{
				chActual.setGoFight(false);
				
			
			}
		}
		if(enemyActual.GetType().FullName == "PnjShort" || enemyActual.GetType().FullName == "PnjRange"){
			//ordena la cola y pongo en estado de ataque de nuevo
			
			if(enemiesList.Count > 0){
				
				queue.Dequeue();
				
				state = 2;
			}else{
				
				foreach(Character ch in fightersList){
					//Pj pj = (Pj) ch;
					
					ch.setIdFight(-1);
					//pj.setIdFight(-1);
				}
				
			
			}
			
			
		}
		
		
	}
	
	/*
	 * metodo que elimina de la lista de luchadores un personaje 
	 */ 
	public void removeFighter(Character character){
		fightersList.Remove(character);
	}
	/*
	 * metodo que añade a un ppersonaje a lista de luchadores
	 */ 
	public void addFighter(Character character){
		fightersList.Add(character);
		if(character.GetType().FullName == "PnjShort" || character.GetType().FullName == "PnjRange"){
			enemiesList.Add(character);	 				
		}else if(character.GetType().FullName == "Wizard" || character.GetType().FullName == "Warrior" || character.GetType().FullName == "Archer"){
			playersList.Add(character);	 				
		}
		state = 1;
	}
	/*
	 * metodo que devuelve id
	 */
	public int getId(){
		return id;	
	}
	/*
	 *  metodo que devuelvo lista de luchadores
	 */ 
	public ArrayList getFighters(){
		return fightersList; 	
	}
	/*
	 * metodo que devuelve estado de la lucha
	 */ 
	public int getState(){
		return state;	
	}
	/*
	 * metodo que setea el estado de la lucha
	 */ 
	public void setId(int id){
		this.id = id;	
	}
	/*
	 * metodo que setea el estado
	 */ 
	public void setState(int state){
		this.state = state;
	}
	/*
	 * metodo que asigna a true la variable death
	 */ 
	public void killCharacter(Character chA){
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.getTarget() == chA.getTarget()){
				
				ch.setDeath(true);
				
			}
		}
		
	}
	/*
	 * metodo que devulve la tirada mas alta efectuada por un personaje
	 */
	public int getGeneralIniciativeThrowFight(ArrayList bigThrow){
		int[] vector = new int[bigThrow.Count];
		for(int i= 0; i<bigThrow.Count;i++){
			vector[i] = (int)bigThrow[i];	
		}
		
		return Mathf.Max(vector);
	}
	/*
	 * metodo que retorna mensaje de las luchas
	 */ 
	public string getMessage(){
		return sms;	
	}
	/*
	 * metodo que crea cadaveres y decals de las muertes
	 */ 
	public void createDeadPnj(Character pj,Vector3 positionCadaver ){
		GameObject decalBlood = GameObject.Instantiate(Resources.Load("BloodDecal")) as GameObject;
		decalBlood.transform.position = positionCadaver;
		GameObject cadaver = null;
		if(pj.getTarget().name == "warrior"){
			
			cadaver = GameObject.Instantiate(Resources.Load("warrior_cadaver")) as GameObject;
		}else if(pj.getTarget().name == "wizard"){
			cadaver = GameObject.Instantiate(Resources.Load("cadaver_wizard")) as GameObject;
		}else if(pj.getTarget().name == "archer"){
			cadaver = GameObject.Instantiate(Resources.Load("archer_cadaver")) as GameObject;
		}else if(pj.getTarget().name == "pnj" || pj.getTarget().name == "pnj_range"){
			cadaver = GameObject.Instantiate(Resources.Load("goblin_cadaver")) as GameObject;
		}
	
		cadaver.transform.position = positionCadaver;
	}
	
	

}
