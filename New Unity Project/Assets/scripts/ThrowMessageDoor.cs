using UnityEngine;
using System.Collections;

public class ThrowMessageDoor : MonoBehaviour {
	private bool exit = false;
	private int exitShot;
	private bool warriorEnter = false; 
	private bool wizardEnter = false; 
	private bool archerEnter  = false;
	private bool sendMessage;
	private string sms;
	// Use this for initialization
	void Start () {
		exit = false;
		exitShot = 10;
		sendMessage = false;
		sms = "";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if(!exit){//en caso de no haber tenido exito en la tirada de dados
			if(other.gameObject.name == "warrior" && !warriorEnter){//miramos si el collider es un guerrero y solo ha entrado en la zona de trigger una sola vez
				int shot = throwSearch(other.gameObject);//hacemos tirada de busqueda
				if(shot >= exitShot){//si tenemos exito en la tirada asignamos el mensaje a la variable sms
					sms +=  ("*El guerrero ve un mecanismo que va de la puerta a dos " +
						"extraños paneles con un pentagrama dibujado*");
					exit = true;
					sendMessage = true;
				}		
			}
			if(other.gameObject.name == "wizard" && !wizardEnter){//miramos si el collider es un mago y solo ha entrado en la zona de trigger una sola vez
				
				int shot = throwSearch(other.gameObject);//hacemos tirada de busqueda
				if(shot >= exitShot){//si tenemos exito en la tirada asignamos el mensaje a la variable sms
					sms +=  ("*El mago ve un mecanismo que va de la puerta a dos " +
						"extraños paneles con un pentagrama dibujado*");
					exit = true;
					sendMessage = true;
				}
			}
			if(other.gameObject.name == "archer" && !archerEnter){//miramos si el collider es un arquero y solo ha entrado en la zona de trigger una sola vez
				int shot = throwSearch(other.gameObject);//hacemos tirada de busqueda
				if(shot >= exitShot){//si tenemos exito en la tirada asignamos el mensaje a la variable sms
					sms += ("*La arquera ve un mecanismo que va de la puerta a dos " +
						"extraños paneles con un pentagrama dibujado*");
					exit = true;
					sendMessage = true;
				}
			}
		}
	}
	/*
	 * metodo que hace la tirada segun el gameObject que se le pasa
	 */
	private int throwSearch(GameObject gObj){
	
		
		Pj pj = findPjInCollider(gObj.name);
		wasThreeIncollider(gObj.name);
		return pj.crapsThrow(1,20) + pj.getSearch();
		
	}
	private Pj findPjInCollider(string name){
		foreach(Character ch in SearchCharacters.characterList){
			if(name ==  ch.getTarget().name){
				Pj pj = (Pj) ch;
				return pj; 	
			}
		}
		return null;
	}
	/*
	 * metodo que desactiva la opcion de volver a lanzar dados si se vuelva a entrar en el trigger
	 */
	private void wasThreeIncollider(string name){
		if(name == "warrior"){
			warriorEnter = true;
		}
		if(name == "wizard"){
		 	wizardEnter = true;
		}
		if(name == "archer"){
			archerEnter = true;
		}
			
			
	}
	
	public bool getSendMessage(){
		return sendMessage;	
	}
	public void setSendMessage(bool send_){
		sendMessage = send_;
	}
	public string getSms(){
		return sms;
	}
}
