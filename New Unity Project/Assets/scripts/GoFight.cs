using UnityEngine;
using System;
using System.Collections;
using QueueSpace;
using System.Collections.Generic;


public class GoFight : MonoBehaviour {

	private List<Fight> fightsList;
	private ArrayList listCharacters;
	private int idFight;
	private string smsForSend;
	public static  bool sendSms;
	public float timeForNextAtack;
	void Start () {
			fightsList = new List<Fight>();
			idFight = 0;
			listCharacters = new ArrayList(SearchCharacters.characterList);
			smsForSend = "";
			timeForNextAtack = 0.0f;
			sendSms = false;
	}
	
	// Update is called once per frame
	void Update(){ 
		if(Time.timeScale != 0){
			for(int i = 0; i < listCharacters.Count;i++){
				//iteramos la lista de personajes que son PJ y esten vivos
				if(listCharacters[i].GetType().FullName == "Warrior" || listCharacters[i].GetType().FullName == "Wizard" || listCharacters[i].GetType().FullName == "Archer"){
					Character ch = (Character)listCharacters[i];
					if(!ch.getDeath()){
				
					
						List<Character> bufferKeys = new List<Character>(ch.getDistanceEnemies().Keys);
						List<float> bufferValues = new List<float>(ch.getDistanceEnemies().Values);
						//iteramos los enemigos de los Pj
						for(int j = 0; j < bufferKeys.Count;j++){ 
							Character chE = bufferKeys[j];
							Pj pj = (Pj) ch;
							//miramos que las distancias entre los pjs y pnjs sean aquellas para tener una lucha
							if(ch.getDistanceFight() >= bufferValues[j] && pj.getAtackType() || chE.getDistanceFight() >= bufferValues[j]){
								//iniciamos una nueva pelea entre personajes que no estan involucrados en ninguna
								if(!ch.getGoFight() &&  !chE.getGoFight()){	
									
									ch.setGoFight(true);
									chE.setGoFight(true);
									pj.setIdFight(idFight);
									Debug.Log ("NUEVA LUCHA");
									Fight f = new Fight(ch,chE,idFight);	 
									fightsList.Add(f);
									idFight++;
									 
									
								}	
								
								if(!ch.getGoFight() && chE.getGoFight()){
									//si un Personaje no esta en lucha y el enemigo si, se busca la pelea y si añade al Pj 	
									//como combatient de esa pelea
									int id = findFightBegin(pj);//se busca ide de lucha de lista de luchas
									if(id != -1){
										ch.setGoFight(true);
										Fight fight = (Fight) fightsList[id];
										pj.setIdFight(id);
										if(!fight.getFighters().Contains(ch)){
											fight.addFighter(ch);
											
										}
									
										
										
										
									}
								}
								
								if(ch.getGoFight() && !chE.getGoFight()){
									//si un Personaje esta en lucha y el enemigo no, se busca la pelea y si añade al Pj 	
									//como combatient de esa pelea	
									int id = findFightBegin(pj);//se busca ide de lucha de lista de luchas
									if(id != -1){
										chE.setGoFight(true);
										Fight fight = (Fight) fightsList[id];
										pj.setIdFight(id);
										if(!fight.getFighters().Contains(chE)){
											fight.addFighter(chE);
											
										}
										//fight.addFighter(chE);
										
										
										
										
									}
								}
								//si el personaje esta en lucha
								if(ch.getGoFight()){
									sendSms = true;
									int id = findFight(pj);//busco id de pelea de ese personaje
									if(id != -1){
										
										//busco la pela basandonos en el id d
										Fight fight = (Fight) fightsList[id];
										
										if(fight.getState() == 1){
											
											fight.InitiateTurns();//tirada de turnos
											sendMessage(fight.getMessage());//envio sms correspondiente a tirada de turnos
										}else if(fight.getState() == 2){
											//se atacara en un determinado tiempo
											timeForNextAtack += Time.deltaTime*10/*Time.time*/;
											
											if(timeForNextAtack >= /*60*/1){
												timeForNextAtack = 0.0f;
												fight.atackAndDamage();//tiradas de ataque y defensa
												sendMessage(fight.getMessage());//se envie mensaje para que el HUD procese
											}
											
											
										}else if(fight.getState() == 3){
											fight.deathNegotiation();//se mira si se muere algun personaje y se gestiona
											sendMessage(fight.getMessage());
										}
									}else{//si no hay lucha
										int idForRemove = findFightForRemove(ch);//se elimina el correspondiente ID
										Fight fight =  (Fight) fightsList[idForRemove];
										foreach(Character fighters in fight.getFighters()){
											
											fighters.setGoFight(false);
											
										}
										
										
										
									}
								}
								
								
							}
							
						} 
					}
				}
			}
		}
		
	}
	/*
	 *  netodo que devuelve el ID de una lucha ya empezada
	 */
	int findFightBegin(Pj pj){
		if(!pj.getDeath()){	
			
			foreach(Fight f in fightsList){
				foreach(Character ch in f.getFighters()){
			
					
					
					
					if(ch.getIdFight() == f.getId()){
					
						return ch.getIdFight();
						
					}
				}
			}
		}
		return -1;
		
	}
	
	/*
	 * metodo que devuelve id de una lucha
	 */ 
	 
	int findFight(Pj pj){
		foreach(Fight f in fightsList){
			if(f.getId()== pj.getIdFight() && !pj.getDeath()){
				return f.getId();
			}
		}
		return -1;
		
	}
	/*
	 *  metodo que devuelve el id de una lucha para eliminar
	 */ 
	int findFightForRemove(Character ch){
		foreach(Fight f in fightsList){	
			if(!f.getFighters().Contains(ch)){
				
				return f.getId();
			}
			
		}
		
		return -1;
	}
	 
	private void sendMessage(string sms){
		smsForSend += sms;	
	}
	public string receiveMessage(){
		
		if(sendSms){
			return smsForSend;	
		}else{
			return null;	
		}
	}
	
}