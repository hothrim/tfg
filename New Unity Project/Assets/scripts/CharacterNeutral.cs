using UnityEngine;
using System.Collections;

public class CharacterNeutral{
	
	
	private int stateAnimation;
	private GameObject target;
	private NavMeshAgent navigation;
	private ArrayList pointsGuard = new ArrayList();
	private int state;
	
	public CharacterNeutral(GameObject pjNeutral){
		state = 0;
		target = pjNeutral;
		
		stateAnimation = 0;
		navigation = pjNeutral.GetComponent<NavMeshAgent>();
		pointsGuard.Add(new Vector3(257.041f,0f,40.74544f));
		pointsGuard.Add(new Vector3(301.58f,0f,40.74544f));
		
	}
	/*
	 * metodo que devuelve el GameObject asociado al personaje neutral
	 */ 
	public GameObject getTarget(){
		return target;	
	}
	/*
	 * metodo que devuelve el estado de la animacion
	 */
	public int getAnimation(){
		return stateAnimation;	
	}
	/*
	 * metodo que devuelve el collider asociado al personajeneutral
	 */
	/*public Collider getCollider(){
		return myCollider;	
	}*/
	/*
	 * metodo que devuelve el NavMeshAgent asociado al personaje
	 */
	public NavMeshAgent getAgentNavigation(){
		return navigation;	
	}
	/*
	 * metodo que devuelve la lista de los puntos de la ruta de guarda
	 */
	public ArrayList getPointsGuard(){
		return pointsGuard;	
	}
	/*
	 * metodo que modifica la lista de los puntos de ruta de guarda
	 */
	public void setPointsGuard(ArrayList pointsGuard){
		this.pointsGuard = pointsGuard;	
	}
	/*
	 * metodo que devuelve el estado del personaje neutral
	 */
	public int getStateIA(){
		return state;	
	}
	/*
	 *  metodo que modifica el estado del personaje neutral
	 */
	public void setStateIA(int state){
		this.state = state;	
	}
	/*
	 * metodo que modifica la animacion del personaje neutral
	 */
	public void setAnimation(int stateAnimation){
		this.stateAnimation = stateAnimation;	
	}
	
}
