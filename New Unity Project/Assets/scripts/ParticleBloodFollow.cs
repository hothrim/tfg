using UnityEngine;
using System.Collections;

public class ParticleBloodFollow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/*el sistema de particulas sigue a los personajes asociados a ellos*/
		foreach(Character ch in SearchCharacters.characterList){
			if(ch != null){
				if(ch.getTarget().name== "archer"){
					Archer ar = (Archer) ch;
					ar.getBloodParticle().transform.position = ar.getTarget().transform.position;
				}
				if(ch.getTarget().name== "warrior"){
					Warrior war = (Warrior) ch;
					war.getBloodParticle().transform.position = war.getTarget().transform.position;
				}
				if(ch.getTarget().name== "wizard"){
					Wizard wi = (Wizard) ch;
					wi.getBloodParticle().transform.position = wi.getTarget().transform.position;
				}
				if(ch.getTarget().name== "pnj_range"){
					PnjRange pnj = (PnjRange) ch;
					pnj.getBloodParticle().transform.position = pnj.getTarget().transform.position;
				}
				if(ch.getTarget().name== "pnj"){
					PnjShort pns = (PnjShort) ch;
					pns.getBloodParticle().transform.position = pns.getTarget().transform.position;
				}
			}
		}
	}
}
