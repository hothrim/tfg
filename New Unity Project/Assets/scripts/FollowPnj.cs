using UnityEngine;
using System.Collections;

public class FollowPnj : MonoBehaviour {
		
	private float speed;
	private float step;
	// Use this for initialization
	void Start () {
		
		speed = 10f;
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.GetType().FullName == "PnjShort" || ch.GetType().FullName == "PnjRange"){
				Pnj pnj = (Pnj) ch;
				pnj.getFollowPnj().transform.position = pnj.getTarget().transform.position;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//calculo del movimiento del objecte SeguimientoPnj, la posicion de este objeto sera la misma que la del enemigo
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.GetType().FullName == "PnjShort" || ch.GetType().FullName == "PnjRange"){
				Pnj pnj = (Pnj) ch;
				step = speed *  Time.deltaTime;
				if(pnj.getFollowPnj() != null){
					Vector3 newPos = new Vector3(pnj.getFollowPnj().transform.position.x,pnj.getFollowPnj().transform.position.y,pnj.getFollowPnj().transform.position.z);
				
					pnj.getFollowPnj().transform.position = Vector3.MoveTowards(newPos,pnj.getTarget().transform.position,step);
				}
		
			}	
	
		}
	}
}
