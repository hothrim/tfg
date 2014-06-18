using UnityEngine;
using System.Collections;

public class MotionControllerPJ : MonoBehaviour {
	
	public static Vector3 position;//posicion del puntero
	public float speed;
	public static GameObject pjWarrior;
	public static GameObject pjWizard;
	public static GameObject pjArcher;
	private GameObject[] pjAll;
	private int touchPj;
	private Pj wizard;
	private Pj warrior;
	private Pj archer;
	private CharacterController ccWarrior;
	private CharacterController ccWizard;
	private CharacterController ccArcher;
	NavMeshAgent navWar;
	NavMeshAgent navWiz;
	NavMeshAgent navArc;
	
	public static bool colissionWarrior;
	public static bool colissionWizard;
	public static bool colissionArcher;
	// Use this for initialization
	void Start () {
		speed = 15f;
		pjWarrior = GameObject.Find("warrior");
		pjWizard = GameObject.Find("wizard");
		pjArcher = GameObject.Find("archer");
		pjAll = GameObject.FindGameObjectsWithTag("Player");
		position = pjWarrior.transform.position;
		wizard = getWizard();
		warrior = getWarrior();
		archer = getArcher();
		ccWarrior = (CharacterController) pjWarrior.collider;
		ccWizard = (CharacterController) pjWizard.collider;
		ccArcher = (CharacterController) pjArcher.collider;
		navWar = GameObject.Find("warrior").GetComponent<NavMeshAgent>();
		navWiz = GameObject.Find("wizard").GetComponent<NavMeshAgent>();
		navArc = GameObject.Find("archer").GetComponent<NavMeshAgent>();
		
		colissionWarrior = false;
		colissionWizard = false;
		colissionArcher = false;
		
	}
	
	// Update is called once per frame
	
	void Update () {
		
		
		//seleccion por cuadro de seleccion
		if(PjSelection.touchPj == -1){
			// asignamos la siguiente posicion del vector de asignacion de todos los personajes a la posicion
			//especificada
			GoControllerPJ.nextPositionWarrior.y = 0;
			GoControllerPJ.nextPositionWizard.y = 0;
			GoControllerPJ.nextPositionArcher.y = 0;
			for(int i = 0; i < pjAll.Length;i++){
				if(pjAll[i] != null){
					if(pjAll[i].name == "warrior"){
						GoControllerPJ.nextPositionAll[i] = GoControllerPJ.nextPositionWarrior;
					}
					if(pjAll[i].name == "wizard"){
						GoControllerPJ.nextPositionAll[i] =GoControllerPJ.nextPositionWizard;
						
					}
					if(pjAll[i].name == "archer"){
						GoControllerPJ.nextPositionAll[i] =GoControllerPJ.nextPositionArcher;
						
					}
				}
			}
			
			//movemos personaje mago
			if(pjWizard != null){
				wizard = getWizard();
				if(wizard != null){
					//siguiendo el punto del terreno sin atacar
					if(!wizard.getAtackType()){						
						movePj(ccWizard,GoControllerPJ.nextPositionWizard,pjWizard.transform.position,speed);
					}
					else{
						//siguiendo a pnj se va calculando la rotacion a cada momento
						if(GoControllerPJ.pnjAtFollowWizard != null){
							Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWizard.transform.position - pjWizard.transform.position);
							pjWizard.transform.rotation = Quaternion.Slerp(pjWizard.transform.rotation,rotarPlayer,Time.deltaTime * 10);
							movePj(ccWizard,GoControllerPJ.pnjAtFollowWizard.transform.position,pjWizard.transform.position,speed);
						
						}
						
					}
				}
				
			}
			//movemos personaje arquero
			if(pjArcher != null){
				archer = getArcher();
				if(archer != null){
					Archer a = (Archer) archer;
					//si no esta atacando
					if(!archer.getAtackType()){
						if(!a.getFastArrowActivate()){
							movePj(ccArcher,GoControllerPJ.nextPositionArcher,pjArcher.transform.position,speed);
						}
					}
					else{//hacemos avanzar al personaje archer hasta una cierta distancia de ataque
					
						if(archer.getFollowPnj().getTarget() != null ){
							if(!a.getFastArrowActivate()){
								if(archer.getDistanceFight() < Vector3.Distance(archer.getTarget().transform.position,archer.getFollowPnj().getTarget().transform.position)){
									movePj(ccArcher,GoControllerPJ.nextPositionArcher,pjArcher.transform.position,speed);
								}
							}
						}
					}
				}
			}
			//movemos guerrero
			if(pjWarrior != null){
				Warrior wa = (Warrior) warrior;
				//si va a atacar
				if(warrior.getAtackType()){
					//si seleciona un enemigo y no esta haciendo habilidad
					if(GoControllerPJ.pnjAtFollowWarrior != null){
						if(!wa.getBarbaryanFuryActivate()){	
							//rotamos y movemos
							Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWarrior.transform.position - pjWarrior.transform.position);
							pjWarrior.transform.rotation = Quaternion.Slerp(pjWarrior.transform.rotation,rotarPlayer,Time.deltaTime * 10);	
							movePj(ccWarrior,GoControllerPJ.pnjAtFollowWarrior.transform.position,pjWarrior.transform.position,speed);
						}
					}	
				}else{
					//no seleccionamos habilidad
					if(!wa.getBarbaryanFuryActivate()){
						movePj(ccWarrior,GoControllerPJ.nextPositionWarrior,pjWarrior.transform.position,speed);
					}
				}
			}
			
		
			
			
		}
		//seleccion total de personajes
		if(PjSelection.touchPj == 0){
			//movemos a todos los personajes
			for(int i = 0; i < pjAll.Length;i++){
				GoControllerPJ.nextPositionAll[i].y = 0;
				if(pjAll[i] != null){
					//en caso de ser guerrero
					if(pjAll[i].name == "warrior"){
						Warrior wa = (Warrior) warrior;
						//si seguimos a un pnj rotamos al warrior
						if(warrior.getAtackType()){
							
							if(GoControllerPJ.pnjAtFollowWarrior != null){
								if(!wa.getBarbaryanFuryActivate()){
									Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWarrior.transform.position - pjWarrior.transform.position);
									pjWarrior.transform.rotation = Quaternion.Slerp(pjWarrior.transform.rotation,rotarPlayer,Time.deltaTime * 10);	
									
									movePj((CharacterController)pjAll[i].collider,GoControllerPJ.pnjAtFollowWarrior.transform.position,pjAll[i].transform.position,speed);
								}
							}
						
						}else{
							if(!wa.getBarbaryanFuryActivate()){
							
								movePj((CharacterController)pjAll[i].collider,GoControllerPJ.nextPositionAll[i],pjAll[i].transform.position,speed);
							}
						}
					}
					//caso de ser mago
					if(pjAll[i].name == "wizard"){
						wizard = getWizard();
						if(wizard != null){
							if(!wizard.getAtackType()){
								
								movePj((CharacterController)pjAll[i].collider,GoControllerPJ.nextPositionAll[i],pjAll[i].transform.position,speed);
							}
							else{//rotamos si se sigue a enemgigo segun se avanza
								if(GoControllerPJ.pnjAtFollowWizard != null){
							
									Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWizard.transform.position - pjWizard.transform.position);
									pjWizard.transform.rotation = Quaternion.Slerp(pjWizard.transform.rotation,rotarPlayer,Time.deltaTime * 10);
									
									movePj((CharacterController)pjAll[i].collider,GoControllerPJ.pnjAtFollowWizard.transform.position,pjAll[i].transform.position,speed);
								}
								
							}
						
						}	
						
					}
					//caso de ser arquero
					if(pjAll[i].name == "archer"){
						archer = getArcher();
						if(archer != null){
							Archer a = (Archer) archer;
							//si no sigue a enemigo
							if(!archer.getAtackType()){
								if(!a.getFastArrowActivate()){
								
									movePj((CharacterController)pjAll[i].collider,GoControllerPJ.nextPositionAll[i],pjAll[i].transform.position,speed);
								}
							}
							else{//hacemos avanzar al personaje arquero hasta una cierta distancia de ataque
					
								if(archer.getFollowPnj().getTarget() != null ){
									if(!a.getFastArrowActivate()){
										if(archer.getDistanceFight() < Vector3.Distance(archer.getTarget().transform.position,archer.getFollowPnj().getTarget().transform.position)){
											
											movePj((CharacterController)pjAll[i].collider,GoControllerPJ.nextPositionAll[i],pjAll[i].transform.position,speed);
										}
									}
								}
							}
						
						}	
						
					}
					
				}
			}
			//actualizamos los valores del  vector de posicion 
			for(int i = 0; i < pjAll.Length;i++){
				if(pjAll[i] != null){
					if(pjAll[i].name == "warrior"){
						GoControllerPJ.nextPositionWarrior = GoControllerPJ.nextPositionAll[i];
					}else if(pjAll[i].name == "wizard"){
						GoControllerPJ.nextPositionWizard = GoControllerPJ.nextPositionAll[i];
					}else if(pjAll[i].name == "archer"){
						GoControllerPJ.nextPositionArcher = GoControllerPJ.nextPositionAll[i];
					}
					
				}
			}
			
			
			
			
		}
		//caso guerror seleccionado
		if(PjSelection.touchPj == 1){
			
			
			
			
			GoControllerPJ.nextPositionWarrior.y = 0;
			GoControllerPJ.nextPositionWizard.y = 0;
			GoControllerPJ.nextPositionArcher.y = 0;
			
			//actualizamos valor de vector de posicion de seleccion total
			for(int i = 0; i < pjAll.Length;i++){
				if(pjAll[i] != null){
					if(pjAll[i].name == "warrior"){
						
						GoControllerPJ.nextPositionAll[i] = GoControllerPJ.nextPositionWarrior;
						
					}
					if(pjAll[i].name == "wizard"){
						
						GoControllerPJ.nextPositionAll[i] =GoControllerPJ.nextPositionWizard;
					}
					if(pjAll[i].name == "archer"){
						
						GoControllerPJ.nextPositionAll[i] =GoControllerPJ.nextPositionArcher;
					}
				}
			}
			//si es guerrero 
			if(pjWarrior != null){
				Warrior w = (Warrior) warrior;
				//si va a atacar siguiendo al pnj
				if(warrior.getAtackType()){
					if(GoControllerPJ.pnjAtFollowWarrior != null){
						if(!w.getBarbaryanFuryActivate()){	
							//rotamos al personaje 
							Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWarrior.transform.position - pjWarrior.transform.position);
							
							pjWarrior.transform.rotation = Quaternion.Slerp(pjWarrior.transform.rotation,rotarPlayer,Time.deltaTime * 10);
							movePj(ccWarrior,GoControllerPJ.pnjAtFollowWarrior.transform.position,pjWarrior.transform.position,speed);
							
						}
					}	
					
				}else{
					if(!w.getBarbaryanFuryActivate()){//si la habilidad de furia no ha sido activada mover el warrior
						movePj(ccWarrior,GoControllerPJ.nextPositionWarrior,pjWarrior.transform.position,speed);
					}
					
				}
			}
			//si es mago
			wizard = getWizard();
			if(wizard != null){
				Wizard w = (Wizard) wizard;
				//si no esta en accion de hechizo y no sigue a nadie
				if(!w.getStop()){
					if(!wizard.getAtackType()){
						//movemos
						if(pjWizard != null){
							
							movePj(ccWizard,GoControllerPJ.nextPositionWizard,pjWizard.transform.position,speed);
						}
					}else{
						if(pjWizard != null){
							if(GoControllerPJ.pnjAtFollowWizard != null){
								//rotamos mago en caso de seguir a enemigo	
								Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWizard.transform.position - pjWizard.transform.position);
								pjWizard.transform.rotation = Quaternion.Slerp(pjWizard.transform.rotation,rotarPlayer,Time.deltaTime * 10);
								
								movePj(ccWizard,GoControllerPJ.pnjAtFollowWizard.transform.position,pjWizard.transform.position,speed);
							
							}
							
						}
					}
				}
			}
			//si es arquero
			archer = getArcher();
			if(archer != null){
				Archer a = (Archer) archer;
				//si no ataca  y no habilita habilidad movemos arquero
				if(!archer.getAtackType()){
					if(pjArcher != null){
						if(!a.getFastArrowActivate()){
							movePj(ccArcher,GoControllerPJ.nextPositionArcher,pjArcher.transform.position,speed);
						}
					}
				}else{
					//si arquero persigue a enemigo y no activa habilidad muve jhasta determinada distancia
					if(pjArcher != null){
						if(archer.getFollowPnj().getTarget() != null ){
							if(!a.getFastArrowActivate()){
								if(archer.getDistanceFight() < Vector3.Distance(archer.getTarget().transform.position,archer.getFollowPnj().getTarget().transform.position)){
									movePj(ccArcher,GoControllerPJ.nextPositionArcher,pjArcher.transform.position,speed);
									
								}
							}
						}
					}
				}
			}
			
			
			
		//si el mago es seleecionado
		}if(PjSelection.touchPj == 2){
			
			GoControllerPJ.nextPositionWarrior.y = 0;
			GoControllerPJ.nextPositionWizard.y = 0;
			GoControllerPJ.nextPositionArcher.y = 0;
			for(int i = 0; i < pjAll.Length;i++){
				if(pjAll[i] != null){
					if(pjAll[i].name == "warrior"){
						GoControllerPJ.nextPositionAll[i] = GoControllerPJ.nextPositionWarrior;
					}
					if(pjAll[i].name == "wizard"){
						GoControllerPJ.nextPositionAll[i] =GoControllerPJ.nextPositionWizard;
					}
					if(pjAll[i].name == "archer"){
						GoControllerPJ.nextPositionAll[i] =GoControllerPJ.nextPositionArcher;
					}
				}
			}
			
			
			wizard = getWizard();
			if(wizard != null){
				Wizard w = (Wizard) wizard;//casting a Wizard para mirar si personaje esta parado,(tira hechizo)
				//si no esta hechizando ni atacando
				if(!w.getStop()){
					if(!wizard.getAtackType()){ 
						if(pjWizard != null){
							//movemos
							movePj(ccWizard,GoControllerPJ.nextPositionWizard,pjWizard.transform.position,speed);
						}
					}else{//en caso de seguir a enemigo rotamos
						if(GoControllerPJ.pnjAtFollowWizard != null){
							Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWizard.transform.position - pjWizard.transform.position);
							pjWizard.transform.rotation = Quaternion.Slerp(pjWizard.transform.rotation,rotarPlayer,Time.deltaTime * 10);	
								
							movePj(ccWizard,GoControllerPJ.pnjAtFollowWizard.transform.position,pjWizard.transform.position,speed);
							
						}
						
						
						
						
						
						
					}
				}else{
					wizard.setAtackType(false);
				}
				
			}
			archer = getArcher();
			if(archer != null){
				Archer a = (Archer) archer;
				if(!archer.getAtackType()){
					if(pjArcher != null){
						
						if(!a.getFastArrowActivate()){
							movePj(ccArcher,GoControllerPJ.nextPositionArcher,pjArcher.transform.position,speed);
						}
					}
				}else{//hacemos avanzar al personaje wizard hasta una cierta distancia de ataque
					
					if(archer.getFollowPnj().getTarget() != null ){
						if(!a.getFastArrowActivate()){
							if(archer.getDistanceFight() < Vector3.Distance(archer.getTarget().transform.position,archer.getFollowPnj().getTarget().transform.position)){
								
								movePj(ccArcher,GoControllerPJ.nextPositionArcher,pjArcher.transform.position,speed);
							}
						}
					}
				}
			}
			if(!warrior.getAtackType()){
				//movemos guerrero si no sigue a enemigo ni activa habilidad
				if(pjWarrior != null){
					
					Warrior wa = (Warrior) warrior;
					if(!wa.getBarbaryanFuryActivate()){
						movePj(ccWarrior,GoControllerPJ.nextPositionWarrior,pjWarrior.transform.position,speed);
					}
				}
			}else{
				if(pjWarrior != null){
					//si sigue a enemigo rotamos mientras se mueve
					if(GoControllerPJ.pnjAtFollowWarrior != null){
						Warrior wa = (Warrior) warrior;
						if(!wa.getBarbaryanFuryActivate()){	
						
							Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWarrior.transform.position - pjWarrior.transform.position);
							pjWarrior.transform.rotation = Quaternion.Slerp(pjWarrior.transform.rotation,rotarPlayer,Time.deltaTime * 10);
							movePj(ccWarrior,GoControllerPJ.pnjAtFollowWarrior.transform.position,pjWarrior.transform.position,speed);
						}
					}
				}
			}
			
			
			
		}
		//arquero seleccionado
		if(PjSelection.touchPj == 3){
			
			//actualizamos valores de vector de seleccion total
			GoControllerPJ.nextPositionWarrior.y = 0;
			GoControllerPJ.nextPositionWizard.y = 0;
			GoControllerPJ.nextPositionArcher.y = 0;
			for(int i = 0; i < pjAll.Length;i++){
				if(pjAll[i] != null){
					if(pjAll[i].name == "warrior"){
						GoControllerPJ.nextPositionAll[i] = GoControllerPJ.nextPositionWarrior;
					}
					if(pjAll[i].name == "wizard"){
						GoControllerPJ.nextPositionAll[i] =GoControllerPJ.nextPositionWizard;
					}
					if(pjAll[i].name == "archer"){
						GoControllerPJ.nextPositionAll[i] =GoControllerPJ.nextPositionArcher;
					}
				}
			}
			
			//si es wizard
			wizard = getWizard();
			Wizard w = (Wizard) wizard;
			//si no va ha hechizar
			if(w != null){
				if(!w.getStop()){
					if(wizard != null){
						//ni sigue a enemigo se mueve normal
						if(!wizard.getAtackType()){
							if(pjWizard != null){
								
								
								movePj(ccWizard,GoControllerPJ.nextPositionWizard,pjWizard.transform.position,speed);
							}
						}else{//hacemos avanzar al personaje wizard hasta una cierta distancia de ataque siguiendo a enemigo y rotandolo
							if(GoControllerPJ.pnjAtFollowWizard != null){
								Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWizard.transform.position - pjWizard.transform.position);
								pjWizard.transform.rotation = Quaternion.Slerp(pjWizard.transform.rotation,rotarPlayer,Time.deltaTime * 10);	
								
								movePj(ccWizard,GoControllerPJ.pnjAtFollowWizard.transform.position,pjWizard.transform.position,speed);
								
							}
							
						}
					}
				}
			}
			//en caso de ser arquero
			archer = getArcher();
			if(archer != null){
				Archer a = (Archer) archer;
				if(!archer.getAtackType()){
					if(pjArcher != null){
						//pjWizard.transform.position = Vector3.MoveTowards(pjWizard.transform.position,GoControllerPJ.nextPositionWizard,step);
						
						if(!a.getFastArrowActivate()){
							movePj(ccArcher,GoControllerPJ.nextPositionArcher,pjArcher.transform.position,speed);
						}
					}
				}else{//hacemos avanzar al personaje wizard hasta una cierta distancia de ataque
					
					if(archer.getFollowPnj().getTarget() != null ){
						if(!a.getFastArrowActivate()){
							if(archer.getDistanceFight() < Vector3.Distance(archer.getTarget().transform.position,archer.getFollowPnj().getTarget().transform.position)){
								
								
								movePj(ccArcher,GoControllerPJ.nextPositionArcher,pjArcher.transform.position,speed);
							}
						}
						
					}
				}
			}
			//si es guerrero y no ataca
			if(!warrior.getAtackType()){
				//movemos guerrero si no sigue a enemigo y no activa habilidad 
				if(pjWarrior != null){
		
					Warrior wa = (Warrior) warrior;
					if(!wa.getBarbaryanFuryActivate()){
						movePj(ccWarrior,GoControllerPJ.nextPositionWarrior,pjWarrior.transform.position,speed);
					}
				}
			}else{
				//si sigue a enemigo rotamos a medida que sigue objetivo
				if(pjWarrior != null){
					
					if(GoControllerPJ.pnjAtFollowWarrior != null){
						Warrior wa = (Warrior) warrior;
						if(!wa.getBarbaryanFuryActivate()){	
						
							Quaternion rotarPlayer = Quaternion.LookRotation(GoControllerPJ.pnjAtFollowWarrior.transform.position - pjWarrior.transform.position);
							pjWarrior.transform.rotation = Quaternion.Slerp(pjWarrior.transform.rotation,rotarPlayer,Time.deltaTime * 10);	
							movePj(ccWarrior,GoControllerPJ.pnjAtFollowWarrior.transform.position,pjWarrior.transform.position,speed);
						}
					}
				}
			}
			
			
			
		}
		
		//ponemos animacion de parada cuando personajes llegan a su destino en caso de estar todos selecionados
		//o individualmente
		if(PjSelection.touchPj == 0){
			for(int i = 0; i < pjAll.Length;i++){
				if(pjAll[i] != null){
					if(pjAll[i].name == "warrior"){
						if((GoControllerPJ.nextPositionAll[i].x == warrior.getTarget().transform.position.x) && (GoControllerPJ.nextPositionAll[i].z == warrior.getTarget().transform.position.z)){
							
							warrior.setStateAnimation(0);
						}
					}
					if(pjAll[i].name == "wizard"){
						if((GoControllerPJ.nextPositionAll[i].x == wizard.getTarget().transform.position.x) && (GoControllerPJ.nextPositionAll[i].z == wizard.getTarget().transform.position.z)){
							wizard.setStateAnimation(0);
						}
					}
					if(pjAll[i].name == "archer"){
						if((GoControllerPJ.nextPositionAll[i].x == archer.getTarget().transform.position.x) && (GoControllerPJ.nextPositionAll[i].z == archer.getTarget().transform.position.z)){
							archer.setStateAnimation(0);	
						}
						
					}
				}
			}
		}else{
			
			
						
			
			if(pjWarrior != null){
				if(((int)warrior.getTarget().transform.position.x == (int)GoControllerPJ.nextPositionWarrior.x) && ((int)warrior.getTarget().transform.position.z == (int)GoControllerPJ.nextPositionWarrior.z)){
					Warrior wa = (Warrior) warrior;
					Animation animation = wa.getTarget().animation;
					if(animation.IsPlaying("barbarianFury")){
						
						
						
						
					}else{
						if(!warrior.getGoFight()){
							
							warrior.setStateAnimation(0);
						}
						
					}
				}
			}
			
			if(pjArcher != null){
				if(((int)archer.getTarget().transform.position.x == (int)GoControllerPJ.nextPositionArcher.x) && ((int)archer.getTarget().transform.position.z == (int)GoControllerPJ.nextPositionArcher.z)){
					Archer a = (Archer) archer;
					Animation animation = a.getTarget().animation;
					if(animation.IsPlaying("fastArcher")){
				
					}else{
						
							archer.setStateAnimation(0);
						
						
					}	
					
				}
			}
			if(pjWizard != null){
				if(((int)wizard.getTarget().transform.position.x == (int)GoControllerPJ.nextPositionWizard.x) && ((int)wizard.getTarget().transform.position.z == (int)GoControllerPJ.nextPositionWizard.z)){
					Wizard w = (Wizard) wizard;
					
					Animation animation = w.getTarget().animation;
					
					if(animation.IsPlaying("armorSpell") ){
						
						
					}
					else if(animation.IsPlaying("bulletSpell")){
						
						
					}else{
						if(!wizard.getGoFight()){
							wizard.setStateAnimation(0);
						}
						
					}
					
				}
			}
		}
		
	}
	/*
	 * metodo qeu devuelve PJ si se selecciona arquero
	 */
	public Pj getArcher(){
		foreach(Character ch in SearchCharacters.characterList){
			if(GameObject.Find("archer") == ch.getTarget()){
				Pj pjArcher = (Pj) ch;
				return pjArcher;
			}
		}
		return null;
	}
	/*
	 * metodo qeu devuelve PJ si se selecciona mago
	 */ 
	public Pj getWizard(){
		foreach(Character ch in SearchCharacters.characterList){
			if(GameObject.Find("wizard") == ch.getTarget()){
				Pj pjWizard = (Pj) ch;
				return pjWizard;
			}
		}
		return null;
	}
	/*
	 * metodo qeu devuelve PJ si se selecciona guerrero
	 */ 
	public Pj getWarrior(){
		foreach(Character ch in SearchCharacters.characterList){
			if(GameObject.Find("warrior") == ch.getTarget()){
				Pj pjWarrior = (Pj) ch;
				return pjWarrior;
			}
		}
		return null;
	}
	/*
	 * metodo que mueve personajes
	 */ 
	public void movePj(CharacterController cc,Vector3 nextPositionPj,Vector3 positionPj,float speed){
		
		//si algun personaje colisiona con collider de pared o  elemento del terreno se para
		if(cc.name == "warrior" && colissionWarrior){
			nextPositionPj = pjWarrior.transform.position;
			GoControllerPJ.nextPositionWarrior = nextPositionPj;
			colissionWarrior = false;
			
		
		}
		if(cc.name == "wizard" && colissionWizard){
			nextPositionPj = pjWizard.transform.position;
			GoControllerPJ.nextPositionWizard = nextPositionPj;
			colissionWizard = false;
			
		}
		if(cc.name == "archer" && colissionArcher){
			nextPositionPj = pjArcher.transform.position;
			GoControllerPJ.nextPositionArcher = nextPositionPj;
			colissionArcher = false;
			
		}
		if(PjSelection.touchPj == 0){
			
			for(int i = 0; i < pjAll.Length;i++){
				
				if(pjAll[i] != null){
					if(pjAll[i].name == "archer" && colissionArcher){
						nextPositionPj = pjArcher.transform.position;
						GoControllerPJ.nextPositionAll[i] = nextPositionPj;
						
					}	
					if(pjAll[i].name == "wizard"  && colissionWizard){
						nextPositionPj = pjWizard.transform.position;
						GoControllerPJ.nextPositionAll[i] = nextPositionPj;
						
					}
					if(pjAll[i].name == "warrior"  && colissionWarrior){
						nextPositionPj = pjWarrior.transform.position;
						GoControllerPJ.nextPositionAll[i] = nextPositionPj;
						
					}
				}
			}
		}
		
		
		//movimiento de personajes
		Vector3 offset = nextPositionPj - positionPj;
		if(offset.magnitude > .1f){
			offset = offset.normalized * speed;
			if(offset.x == 0 && offset.z == 0){
				
				if(warrior != null && cc.name == "warrior" ){
					warrior.setStateAnimation(0);
				}
				if(archer != null && cc.name == "archer" ){
					archer.setStateAnimation(0);
				}
				if(wizard != null && cc.name == "wizard" ){
					wizard.setStateAnimation(0);
				}
			}
			cc.Move(offset * Time.deltaTime);
			
			
		}
	}
	
	
	
}
	
	
	
	
	
	
