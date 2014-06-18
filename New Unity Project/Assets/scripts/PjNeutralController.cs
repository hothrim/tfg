using UnityEngine;
using System.Collections;

public class PjNeutralController : MonoBehaviour {
	private ArrayList list;
	private int sight;
	private Collider[] cols;
	private float timeForNextAtack;
	private bool seeEnemies;
	private Vector3 finishObject;
	public static string sms;
	public static bool alert;
	// Use this for initialization
	void Start () {
		list = SearchCharacters.neutralList;
		sight = 30;
		timeForNextAtack = 0;
		seeEnemies = false;
		finishObject = new Vector3(280.91f,0f,269.02f);
		sms = "";
		alert = false;
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < list.Count;i++){
			CharacterNeutral ch = (CharacterNeutral) list[i];
			 
			if(!seeEnemies){
				seePj(ch);
			}
			Vector3 point = goToNextDestinationPoint(ch.getPointsGuard());
			moveToNextDestinationPoint(ch);
			if(ch.getStateIA() == 0){
				//en caso de no ver enemigos, se va hacia un punto 
				if(!seeEnemies){	
					Vector3 newPos = new Vector3(ch.getTarget().transform.position.x,0,ch.getTarget().transform.position.z) ;
					ch.setAnimation(1);
					if((Vector3)ch.getPointsGuard()[0] == newPos){// cuando llega al punto
						ch.setStateIA(1);
						
						
					}
				}else{
					//si llega al nuevo punto se destruye el objeto
					Vector3 newPos = new Vector3(ch.getTarget().transform.position.x,0,ch.getTarget().transform.position.z) ;
					if((Vector3)ch.getPointsGuard()[0] == newPos){
						if((Vector3)ch.getPointsGuard()[0] == finishObject){
							Destroy(ch.getTarget());
							list.Remove(ch);
						}	
						else{
							ch.setStateIA(1);		
						}
						
						
					}
				}
			}
			if(ch.getStateIA() == 1){
				//se actualiza el nuevo destino del pj neutral	
				updateDestinationPoint(ch);
				point = goToNextDestinationPoint(ch.getPointsGuard());
				moveToNextDestinationPoint(ch);
				ch.setStateIA(0);
				
					
			}
			//en el estado 2 
			if(ch.getStateIA() == 2){
				if(timeForNextAtack < 500){// cuando trasncurra este tiempo
					ch.getAgentNavigation().Stop();
					ch.setAnimation(0);
					//saldra un mensaje por pantalla, de esto se encargara el hud 
					sms = "Atrancaz laz puertaz, vienen a recuperar la ezpada anceztral";
					alert = true;
					timeForNextAtack += Time.time;
					//y el audi de las puertas cerrandose se ejecutara 
					ch.getTarget().audio.Play();
				}else{
					ch.setStateIA(3);
					
				}
			}
			//en el estado 3 cambia velocidad y aceleracion y se mueve hasta el punto finishObject  
			if(ch.getStateIA() == 3){
				ch.setAnimation(2);
				ch.getAgentNavigation().speed = 20;
				ch.getAgentNavigation().acceleration = 15;
				
				ArrayList lisAux = new ArrayList();
				
				lisAux.Add(finishObject);
				ch.setPointsGuard(lisAux);
				ch.setStateIA(0);
				
			}
		}
	}
	/*
	 * metodo que cambia valor de variable seeEnemies y el estado del personaje neutral si ve a personajes principales
	 */ 
	public void seePj(CharacterNeutral ch){
		cols = Physics.OverlapSphere(ch.getTarget().transform.position,sight);	
		foreach(Collider c in cols ){
			if(c.name == "warrior" || c.name == "wizard" || c.name == "archer"){
				ch.setStateIA(2);
				seeEnemies = true;
			}
		}
		
		
		
	}
	/*
	 * metodo que cambia punto de ruta de lista
	 */ 
	public Vector3 goToNextDestinationPoint(ArrayList points){
		Vector3 nextPoint = (Vector3)points[0];
		return nextPoint;
		
	}
	/*
	 * metodo que actualiza punto destino
	 */ 
	public void updateDestinationPoint(CharacterNeutral ch){
		
		ArrayList points = ch.getPointsGuard();
		if(points.Count > 0){
			Vector3 tmp = (Vector3) points[0];
			points[0] = (Vector3)points[1];
			points[points.Count-1] = tmp;
			
			
			ch.setPointsGuard(points);
			
		}
	}
	/*
	 * metodo que mueve al eprsonaje neutral
	 */ 
	public void moveToNextDestinationPoint(CharacterNeutral ch){
		ch.getAgentNavigation().SetDestination((Vector3) ch.getPointsGuard()[0]);
		
	}
	
}
