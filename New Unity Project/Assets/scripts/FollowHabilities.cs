using UnityEngine;
using System.Collections;

public class FollowHabilities : MonoBehaviour {
	GameObject spellBarbarian;
	GameObject spellArrow;
	// Use this for initialization
	void Start () {
		spellBarbarian = GameObject.Find("BarbarianFurySpell(Clone)");
		spellArrow = GameObject.Find("FastArrow(Clone)");
	}
	
	// Update is called once per frame
	/*asignamos la posicion del sistema de particulas sobre el personaje que la activa 
	 * en caso de existir ese sistema de particulas
	 */
	void Update () {
		spellBarbarian = GameObject.Find("BarbarianFurySpell(Clone)");
		spellArrow = GameObject.Find("FastArrow(Clone)");
		if(spellBarbarian != null){
			if(GameObject.Find("warrior") != null){
				spellBarbarian.transform.position = GameObject.Find("warrior").transform.position;	
			}
		}
		if(spellArrow != null){
			if(GameObject.Find("archer") != null){
				Vector3 posArrow = new Vector3(GameObject.Find("archer").transform.position.x,25,GameObject.Find("archer").transform.position.z);
				spellArrow.transform.position = posArrow;	
			}
		}
	}
}
