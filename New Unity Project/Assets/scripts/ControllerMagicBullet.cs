using UnityEngine;
using System.Collections;

public class ControllerMagicBullet : MonoBehaviour {
	Wizard wizard;
	GameObject tmp;
	MagicBulletSpell mUpdated;
	// Use this for initialization
	void Start () {
		wizard = getWizard();
		tmp = null;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Wizard wizard = getWizard ();//buscamos el wizard en la lista de personajes
		if(wizard != null){
			ArrayList listSpell = wizard.getSpellListOfWizard().getSpellList();// asignamos la lista de hechizos
			foreach(Spell s in listSpell){//buscamos el hehchizo BulletMagicSpell
				if(s.getId() == 1){
					mUpdated = (MagicBulletSpell) s;
					//movemos el rigibody del GameObject asociado al hehcizo hacia el personaje enemigo
					Vector3 direction = (mUpdated.getPj().getTarget().transform.position - mUpdated.getSpellParticle().transform.position).normalized;
					float speed = 50f;
					mUpdated.getSpellParticle().rigidbody.MovePosition(mUpdated.getSpellParticle().transform.position + direction*speed*Time.deltaTime);
				}
			}
		}
		
	}
	
	void OnCollisionEnter(Collision collision) {
		
		if(collision.collider.name == "SeguimientoPnj(Clone)"){//miramos si colisiona con el GameObject SeguimientoPJ asociado al enemigo
			Wizard wizard = getWizard ();
			if(wizard != null){
				MagicBulletSpell m = getSpellBullet();//buscamos le hehcizo que se ha lanzado
				int damage = m.getDamage();//calculamos el daño que hara el hechizo al personaje seleccionado
				Pnj pnj = m.getPj();
				pnj.setPg(pnj.getPg() - damage);//modificamos la vida del personaje seleccionado
				tmp = m.getSpellParticle(); 
	    		Destroy(tmp); //destruimos el sistema de particulas del hechizo
				GameObject explosion = GameObject.Instantiate(Resources.Load("ShockFlame")) as GameObject;//instanciamos la explosion
				explosion.transform.position = pnj.getTarget().transform.position;//posicionamos la explosion en el personaje que colisiona dicho sistema de particulas
				
			}
		}
		else if(collision.collider.name != "wizard"){// en el caso de no colisionar con enemigo o con el mago generamos la explosion
			Wizard wizard = getWizard ();
			if(wizard != null){
				MagicBulletSpell m = getSpellBullet();
				tmp = m.getSpellParticle();
	    		Destroy(tmp); 
				GameObject explosion = GameObject.Instantiate(Resources.Load("ShockFlame")) as GameObject;
				explosion.transform.position = collision.collider.transform.position;
			}
		}
    }
	
	/*metodo que devuelve el mago de la lista de persnajes*/
	private Wizard getWizard(){
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.getTarget() == GameObject.Find("wizard")){
				Wizard w = (Wizard) ch;
				return w;	
			}
		}
		return null;
	}
	/*metodo que devuelve el hechizo MagicBulletSpell lanzado*/
	private MagicBulletSpell getSpellBullet(){
		ArrayList listSpell = wizard.getSpellListOfWizard().getSpellList();
		MagicBulletSpell m = null;		
		foreach(Spell s in listSpell){
			
			if(s.getId() == 1){
				m = (MagicBulletSpell) s;
			}
			
		}
		return m;
	}
}
