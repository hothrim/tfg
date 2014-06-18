using UnityEngine;
using System.Collections;

public class SearchCharacters : MonoBehaviour {
	
	private GameObject[] respawns;
	private GameObject[] players;
	private GameObject neutral;
	public static ArrayList characterList = new ArrayList();
	public static ArrayList neutralList = new ArrayList();
	private ArrayList enemiesList = new ArrayList();
	private ArrayList pjsList = new ArrayList();
	private ArrayList monitoring1 = new ArrayList(); 
	private ArrayList monitoring2 = new ArrayList();
	private ArrayList monitoring3 = new ArrayList();
	private ArrayList monitoring4 = new ArrayList();
	private ArrayList monitoring5 = new ArrayList();
	private ArrayList monitoring6 = new ArrayList();
	// Use this for initialization
	void Start() {
		//segun la dificultad se carga un numero determinado de enemigos
		if(ChangeDificulty.dificulty == 0){
			
			DinamicLoadEnemiesModeEasy();
			
		}
		else{
			
			DinamicLoadEnemiesModeNormal();
			
		}
		
		
		
		players = GameObject.FindGameObjectsWithTag("Player");
		
		neutral = GameObject.Find("pnj_neutral");
		  
		
		
		for(int i =0; i < players.Length;i++){
			//se crean las diferentes instancias de los personajes y se añadenan a lista de enemigos de los enemigos
			if(players[i].name == "warrior"){
				Warrior warrior = new Warrior(players[i]);
				characterList.Add(warrior);
				pjsList.Add(warrior);
			}else if(players[i].name == "wizard"){
				Wizard wizard = new Wizard(players[i]);
				characterList.Add(wizard);
				pjsList.Add(wizard);
			}else if(players[i].name == "archer"){
				Archer archer = new Archer(players[i]);
				characterList.Add(archer);
				pjsList.Add(archer);
			}
		}
		//pasamos la lista de enemigos de los correspondientes personajes
		foreach(Character ch in pjsList){
		
			ch.setEnemiesList(enemiesList);
			
			
		}
		
		foreach(Character ch in enemiesList){
			
			ch.setEnemiesList(pjsList);
			
		}
		//instanciamos el personaje neutraly lo añadimos a la lista
		CharacterNeutral chNeutral = new CharacterNeutral(neutral);
		neutralList.Add(chNeutral);
		
	}
	void Update(){
	
	}
	/*
	 * metodo que carga dinamicamente los enemigos en modo facil
	 */
	private void DinamicLoadEnemiesModeEasy(){
		//cargamos prefabs de enemigos de largo y corto alcance
		GameObject pnjS1 = Instantiate(Resources.Load("pnj")) as GameObject;
		pnjS1.name = "pnj";
		GameObject pnjR4 = Instantiate(Resources.Load("pnj_range")) as GameObject;
		pnjR4.name = "pnj_range";
		respawns = GameObject.FindGameObjectsWithTag("Respawn");
		//añadimos puntos de ruta de guardia de enemigos
		monitoring1.Add(new Vector3(206.77f,0f,427.83f));
		monitoring1.Add(new Vector3(252.61f,0f,427.83f));
		monitoring2.Add(new Vector3(277f,0f,359.32f));
		monitoring2.Add(new Vector3(277f,0f,420.22f));
		
		//instanciamos pnj
		for(int i =0; i < respawns.Length;i++){
		
			if(respawns[i].name == "pnj"){
				PnjShort pnj = new PnjShort(i,respawns[i],monitoring2);
				//añadimos enemigos de corto alcance a la lista principal de enemigos
				characterList.Add(pnj);
				//añadimos enemigos lista de enemigos de pj principales
				enemiesList.Add(pnj);
			}else if(respawns[i].name == "pnj_range"){
				PnjRange pnj = new PnjRange(i,respawns[i],monitoring1);	
				//añadimos enemigos de largo alcance a la lista principal de enemigos
				characterList.Add(pnj);
				//añadimos lista de enemigos de pj principales
				enemiesList.Add(pnj);
			}
		
		}
	}
	/*
	 * metodo que carga dinamicamente los enemigos en modo normal
	 */
	private void DinamicLoadEnemiesModeNormal(){
		GameObject pnjS1 = Instantiate(Resources.Load("pnj")) as GameObject;
		pnjS1.name = "pnj";
		GameObject pnjR5 = Instantiate(Resources.Load("pnj_range")) as GameObject;
		pnjR5.name = "pnj_range";
		
		GameObject pnjS3 = Instantiate(Resources.Load("pnj")) as GameObject;
		pnjS3.name = "pnj";
		GameObject pnjS4 = Instantiate(Resources.Load("pnj")) as GameObject;
		pnjS4.name = "pnj";
		GameObject pnjS2 = Instantiate(Resources.Load("pnj")) as GameObject;
		pnjS2.name = "pnj";
		GameObject pnjR6 = Instantiate(Resources.Load("pnj_range")) as GameObject;
		pnjR6.name = "pnj_range";
		respawns = GameObject.FindGameObjectsWithTag("Respawn");
		monitoring1.Add(new Vector3(206.77f,0f,428f));
		monitoring1.Add(new Vector3(252.61f,0f,428f));
		monitoring2.Add(new Vector3(277.0f,0f,359.32f));
		monitoring2.Add(new Vector3(277.0f,0f,420.22f));
		monitoring3.Add(new Vector3(261.50f,0f,258.50f));
		monitoring3.Add(new Vector3(293.50f,0f,258.50f));
		monitoring4.Add(new Vector3(278f,0f,172.53f));
		monitoring4.Add(new Vector3(278f,0f,70.42f));
		
		
		monitoring5.Add(new Vector3(313.49f,0f,420.75f));
		monitoring5.Add(new Vector3(346.96f,0f,388.64f));
		monitoring6.Add(new Vector3(207.69f,0f,368.14f));
		monitoring6.Add(new Vector3(242.17f,0f,368.14f));

		
		pnjR6.transform.position = new Vector3(251.62f,0f,258.93f);
		pnjS2.transform.position = new Vector3(278.09f,0f,172.53f);
		
		PnjRange pnj1 = new PnjRange(0,pnjR5,monitoring1);
		PnjShort pnj2 = new PnjShort(1,pnjS1,monitoring2);
		PnjRange pnj3 = new PnjRange(2,pnjR6,monitoring3);
		PnjShort pnj4 = new PnjShort(3,pnjS2,monitoring4);
		PnjShort pnj5 = new PnjShort(4,pnjS3,monitoring5);	
		PnjShort pnj6 = new PnjShort(5,pnjS4,monitoring6);	
		
		characterList.Add(pnj1);
		enemiesList.Add(pnj1);
		characterList.Add(pnj2);
		enemiesList.Add(pnj2);
		characterList.Add(pnj3);
		enemiesList.Add(pnj3);
		characterList.Add(pnj4);
		enemiesList.Add(pnj4);
		characterList.Add(pnj5);
		enemiesList.Add(pnj5);
		characterList.Add(pnj6);
		enemiesList.Add(pnj6);
	}
	

}
