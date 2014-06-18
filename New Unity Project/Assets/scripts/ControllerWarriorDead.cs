using UnityEngine;
using System.Collections;

public class ControllerWarriorDead : MonoBehaviour {
	private Wizard wBeforeDied;
	private int countParticleSystem;
	private GameObject effect;
	private GameObject reward;
	private bool gameOver;
	private GameObject wizard; 
	private GameObject warrior;
	private GameObject archer;
	// Use this for initialization
	void Start () {
		countParticleSystem = -1;
		effect = null;
		reward = null;
		gameOver = false;
		warrior = GameObject.Find("warrior");
		wizard = GameObject.Find("wizard");
		archer = GameObject.Find("archer");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GameObject.Find("warrior") == null){ // miramos que no exista el GameObject que corresponde al guerrero, estara muerto
			if(GameObject.Find("BarbarianFurySpell(Clone)") != null){ //destruimos el sistema de particulas si no existe el guerror
				Destroy(GameObject.Find("BarbarianFurySpell(Clone)"));	
			}
			if(GameObject.Find("wizard") != null){ //destruimos el sistema de particulas si esta activado el hechizo ArmorSpell
				Wizard w = getWizard();			   //en caso de que exista el mago, pero el guerrero este muerto
				if(w != null){
					ArrayList spellList = w.getSpellListOfWizard().getSpellListInAction();
					foreach(Spell s in spellList){
						if(s.getId() == 0){
							ArmorSpell armor = (ArmorSpell) s;
							if(armor.getPj().GetType().FullName == "Warrior"){
								Destroy (armor.getSystemParticles());
							}
						}
					}
				}
			}
			if(GoControllerPJ.pjsInSelectionBox != null && GoControllerPJ.pjsInSelectionBox.Contains(warrior)){
				
				GoControllerPJ.pjsInSelectionBox.Remove(warrior);//eliminamos de la lista de seleccion al guerrero
			}
			
		}
		if(GameObject.Find("archer") == null){// miramos que no exista el GameObject que corresponde al arquero, estara muerto
			if(GameObject.Find("FastArrow(Clone)") != null){ //destruimos el sistema de particulas si no existe el arquero
				Destroy(GameObject.Find("FastArrow(Clone)"));	
			}
			if(GameObject.Find("wizard") != null){//destruimos el sistema de particulas si esta activado el hechizo ArmorSpell
				Wizard w = getWizard();	          //en caso de que exista el mago, pero el arquero este muerto
				
				if(w != null){
					ArrayList spellList = w.getSpellListOfWizard().getSpellListInAction();
					
					foreach(Spell s in spellList){
						if(s.getId() == 0){
							
							ArmorSpell armor = (ArmorSpell) s;
							if(armor.getPj().GetType().FullName == "Archer"){
								Destroy (armor.getSystemParticles());
							}
						}
					}
				}
			}
			if(GoControllerPJ.pjsInSelectionBox != null && GoControllerPJ.pjsInSelectionBox.Contains(archer)){
				
				GoControllerPJ.pjsInSelectionBox.Remove(archer);//eliminamos de la lista de seleccion al arquero
			}
		}
		
		if(GameObject.Find("wizard") == null){//eliminamos sistemas de particulas en caso de que el mago este muerto
			if(GameObject.Find("ArmorSpell(Clone)") != null){
				Destroy(GameObject.Find("ArmorSpell(Clone)"));	
			}
			if(GameObject.Find("MagicBullet(Clone)") != null){
				Destroy(GameObject.Find("MagicBullet(Clone)"));	
			}
			
			
			if(GoControllerPJ.pjsInSelectionBox != null && GoControllerPJ.pjsInSelectionBox.Contains(wizard)){
				
				GoControllerPJ.pjsInSelectionBox.Remove(wizard);//eliminamos de la lista de seleccion al mago
			}
		}else{
			wBeforeDied = getWizard();
		}
		if(GameObject.Find("ShockFlame(Clone)") != null){//eliminamos el efecto de la explosion de MagicBulletSpell una vez que ha parado
			GameObject explosion = GameObject.Find("ShockFlame(Clone)"); 
			ParticleSystem explosionParticleSystem= explosion.particleSystem;
			if(explosionParticleSystem.isStopped){
				Destroy (explosion);
			}
		}
		
		if((GameObject.FindGameObjectsWithTag("Respawn").Length <= 0) && !gameOver){//instanciamos el sistema de particulas de aparcion de espada
			countParticleSystem ++;									 //y la espada si no exxisten los enemigos
			Vector3 posReward = new Vector3(281.73f,5f,424.79f);
			if(countParticleSystem == 0){
				effect = GameObject.Instantiate(Resources.Load("Christmas Flare")) as GameObject;
				reward = GameObject.Instantiate(Resources.Load("RewardSword")) as GameObject;
				effect.transform.position = posReward;	
				reward.transform.position = posReward;
			}
			if(countParticleSystem > 1000){// si contador llega paramos el sistema de particulas y audio de victoria
				effect.particleSystem.Stop();
				effect.audio.Stop();
				
			}
		}
		if(GameObject.FindGameObjectsWithTag("Player").Length <= 0){//si mueren los personajes principales, cambiamos valor de variable
			gameOver = true;
			
			
		}
	}
	
	
	
	/*metodo que devuelve el mago en la lista*/
	private Wizard getWizard(){
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.GetType().FullName == "Wizard"){
				Wizard w = (Wizard) ch;
				return w;	
			}
		}
		return null;
	}
	/**metodo que carga imagen de fin del juego y elimina instancias de la lista de personajes al finalizar */
	void OnGUI(){
		if(gameOver){
			GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/9,500,500),Resources.Load("Game_over") as  Texture);	
			
				for(int i = 0; i < SearchCharacters.characterList.Count;i++){
					Character ch = (Character) SearchCharacters.characterList[i];
					Destroy (ch.getTarget());	
					SearchCharacters.characterList.Remove(SearchCharacters.characterList[i]);
				}
				for(int i = 0; i < SearchCharacters.neutralList.Count;i++){
					CharacterNeutral ch = (CharacterNeutral) SearchCharacters.neutralList[i];
					
					SearchCharacters.neutralList.Remove(SearchCharacters.neutralList[i]);
				}
			if(Input.GetMouseButtonDown(0)){//aprentando el boton del raton volvemos a la escena de menu principal
				
				
				Application.LoadLevel("menu");
			}
		}
	}
	
	
}
