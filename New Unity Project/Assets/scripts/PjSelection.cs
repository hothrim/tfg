using UnityEngine;
using System.Collections;

public class PjSelection : MonoBehaviour {	
	public static int touchPj;
	public static bool firstTouch;
	private GameObject pjWarrior;
	private GameObject pjWizard;
	private GameObject pjArcher;
	public static Character pjSelectefForSpell;
	

	public static bool  moveWizard;
	public static bool moveWarrior;
	public static bool moveArcher;
 
	
	
	
	// Use this for initialization
	void Start () {
		touchPj = 1;
		firstTouch = true;
		pjWarrior = GameObject.Find("warrior");
		pjWizard = GameObject.Find("wizard");
		pjArcher = GameObject.Find("archer");
		
		
		
		moveWizard = false;
		moveWarrior = true;
		moveArcher = false;
	
				
	}
	
	// Update is called once per frame
	void Update(){
		//si se pulsa la tecla A se selecionan todos los Gameobjects con tag Player
		if(Input.GetKeyDown(KeyCode.A) && !MenuPauseAparition.gamePaused){
			GoControllerPJ.selectionDone = false;
			touchPj = 0;//establecemos la variable de control de personaje seleccionado
			GameObject[] list = GameObject.FindGameObjectsWithTag("Player");
			for(int i = 0; i < list.Length;i++){
				//hacemos que se visualiza la aureola de cada personaje,selector de pjs
				Character ch = caughtPj(list[i]);
				Pj pj = (Pj) ch;
				pj.getSelectionPj().renderer.enabled = true;
			}
		}
		//si se ha hecho una seleccion por cuadro de selccion establecemos a -1 
		if(GoControllerPJ.selectionDone){
			touchPj = -1;
		}
    	
    }

	
		
	
 	void OnMouseUpAsButton(){
		//si se ha soltado el boton del raton
		if(!spellActivate()){//comprobamos que no se ha escogido un hechizo
			if(transform.gameObject.name == "warrior"){//comprobamos que el pj seleccionado  es el guerrero
				//asignamos variable de control y visualizamos la aureola solo en el eprsonaje seleccionado
				touchPj = 1;
				Character ch = caughtPj(pjWarrior);
				Pj pj = (Pj) ch;
				pj.getSelectionPj().renderer.enabled = true;
				Character ch1 = caughtPj(pjWizard);
				Pj pj1 = (Pj) ch1;
				Character ch2 = caughtPj(pjArcher);
				Pj pj2 = (Pj) ch2;
				if(pj1 != null){
					pj1.getSelectionPj().renderer.enabled = false;
				}
				if(pj2 != null){
					pj2.getSelectionPj().renderer.enabled = false;
				}
				
				
				moveWizard = false;
				moveWarrior = false;
				moveArcher = false;
				GoControllerPJ.selectionDone = false;
				
				
	   		
			
			
			}else if(transform.gameObject.name == "wizard"){//comprobamos que el pj seleccionado  es el mago
				//asignamos variable de control y visualizamos la aureola solo en el personaje seleccionado
				touchPj = 2;
				Character ch = caughtPj(pjWizard);
				Pj pj = (Pj) ch;
				pj.getSelectionPj().renderer.enabled = true;
				Character ch1 = caughtPj(pjWarrior);
				Pj pj1 = (Pj) ch1;
				Character ch2 = caughtPj(pjArcher);
				Pj pj2 = (Pj) ch2;
				if(pj1 != null){
					
					pj1.getSelectionPj().renderer.enabled = false;
				}
				if(pj2 != null){
					
					pj2.getSelectionPj().renderer.enabled = false;
				}
				
				moveWarrior = false;
				moveWizard = false;
				moveArcher = false;
				GoControllerPJ.selectionDone = false;
				
			    
	   		
			
			
			
			}
			else if(transform.gameObject.name == "archer"){//comprobamos que el pj seleccionado  es el arquero
				
				//asignamos variable de control y visualizamos la aureola solo en el personaje seleccionado
				touchPj = 3;
				Character ch = caughtPj(pjArcher);
				Pj pj = (Pj) ch;
				pj.getSelectionPj().renderer.enabled = true;
				Character ch1 = caughtPj(pjWarrior);
				Pj pj1 = (Pj) ch1;
				Character ch2 = caughtPj(pjWizard);
				Pj pj2 = (Pj) ch2;
				if(pj1 != null){
					
					pj1.getSelectionPj().renderer.enabled = false;
				}
				if(pj2 != null){
					
					pj2.getSelectionPj().renderer.enabled = false;
				}
				
				moveWarrior = false;
				moveWizard = false;
				moveArcher = false;
				GoControllerPJ.selectionDone = false;
				
			    
	   		
			
			
			
			}
		 
		}
		else{
			//seleccion de personajes para hechizar
			
			if(transform.gameObject.name == "warrior"){
				
			
				
				//se le aplicara el hechizo al guerrero
				pjSelectefForSpell = caughtPj(pjWarrior);
				
			}
			else if(transform.gameObject.name == "wizard"){
				
				//se le aplicara el hechizo al mago
				Character w = caughtPj(pjWizard);
				pjSelectefForSpell = w;
				
				
				
				
				
			}
			else if(transform.gameObject.name == "archer"){
				
				
				//se le aplicara el hechizo al arquero
				pjSelectefForSpell = caughtPj(pjArcher);

				
			}else{
				pjSelectefForSpell = null;
			}
			
			
		}
		
	}
	/*
	 * metodo que devuelve el Character delGameOBject pasado por argumento
	 */ 
	private Character caughtPj(GameObject pj){
		if(pj != null){
			foreach(Character ch in SearchCharacters.characterList){
				
					if(ch.getTarget().name == pj.name){
						return ch;
					}
				
				
			}
		}
		return null;
	}
	/*
	 *metodo que devuelve si se lanza un hechizo
	 */ 
	private bool spellActivate(){
		Character w =	caughtPj(GameObject.Find("wizard"));
		Wizard wizard = (Wizard) w;
		if(wizard != null){
			
			if(wizard.getStop()){
				return true;	
			}
			return false;
		}
		return false;
	}
	


}
	