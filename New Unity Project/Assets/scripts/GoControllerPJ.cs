using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoControllerPJ : MonoBehaviour {
	public Vector3 initPosition;
	private GameObject pj;
	//public static bool init;
	public static Vector3 nextPositionWarrior;
	public static Vector3 nextPositionWizard;
	public static Vector3 nextPositionArcher;
	public static Vector3 lastPositionWizard;
	public static Vector3 lastPositionWarrior;
	public static Vector3 lastPositionArcher;
	public static Vector3[] lastPositionAll; 
	public static Vector3[] nextPositionAll; 
	public static Vector3[] nextPositionSelected;
	public static Vector3[] lastPositionSelected;
	//public bool flag;
	private float oldTime;
	private float newTime;
	private float oldTimeInPause;
	private float newTimeInPause;
	private Vector2 initSelectionSquarePosition;
	

	
	private Rect rect;
	private GameObject selectionBox;
	private bool selecting;
	private Vector3 selectionOrigin;
	private Rect bound;
	public static List<GameObject> pjs;
	public static bool selectionDone;
	public static List<GameObject> pjsInSelectionBox;
	
	public static GameObject pnjAtFollowWarrior;
	public static GameObject pnjAtFollowWizard;
	
	public static Pnj assignPnjForSpell;
	
	
	private CursorMode cursorMode;
	public static Quaternion rotarWarrior;
	public static Quaternion rotarWizard;
	public static Quaternion rotarArcher;
	private Quaternion temp;

	
	private float hitDist;
	


	
	
	void Start () {
		cursorMode = CursorMode.Auto;
		//init = true;
		lastPositionWarrior = new Vector3(0,0,0);
		lastPositionWizard = new Vector3(0,0,0);
		lastPositionArcher = new Vector3(0,0,0);
		lastPositionAll = new Vector3[GameObject.FindGameObjectsWithTag("Player").Length];
		nextPositionAll = new Vector3[GameObject.FindGameObjectsWithTag("Player").Length];
		for(int i = 0; i < GameObject.FindGameObjectsWithTag("Player").Length;i++){
			lastPositionAll[i] = GameObject.FindGameObjectsWithTag("Player")[i].transform.position;
			
		}
		
		
		//flag = false;
		selecting = false;
		nextPositionWarrior = GameObject.Find("warrior").transform.position; 
		nextPositionWizard = GameObject.Find("wizard").transform.position;  
		nextPositionArcher = GameObject.Find("archer").transform.position;
		
		pjs = new List<GameObject>();
		pjs.Add(GameObject.Find("warrior"));
		pjs.Add(GameObject.Find("wizard"));
		pjs.Add(GameObject.Find("archer"));
		selectionDone = false;
	
	
				
		
	}
			
	// Update is called once per frame
	void Update () {
		updatePositionCompass();
		
		if(!MenuPauseAparition.gamePaused){
			//se mira si la posicion del raton este entre el espacio de las 2 GuiBOX
			if(Input.mousePosition.x > Hud.limitWithRight && Input.mousePosition.x < Hud.limitWithLeft){
				
				if(PjSelection.touchPj == 1){//en caso de controlar al guerrero 
					
					Character ch = caughtPj(GameObject.Find("warrior"));
					if(ch != null){
						//se calcula la rotacion de la brujula, es decir hacia donde girar el guerrero
						Pj pj = (Pj) ch;
						Plane plano = new Plane(Vector3.up, pj.getCompass().transform.position);
						Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
						if(plano.Raycast(ray_,out hitDist)){
							Vector3 posPlano =ray_.GetPoint(hitDist);
							rotarWarrior = Quaternion.LookRotation(posPlano - pj.getCompass().transform.position);
							//pj.getCompass().transform.position = GameObject.Find("warrior").transform.position ;
							pj.getCompass().transform.rotation = Quaternion.Slerp(pj.getCompass().transform.rotation,rotarWarrior,Time.deltaTime * 10);
							
							
						
							
						
							
							
						}
					}
				}
				else if(PjSelection.touchPj == 2){
					//se calcula la rotacion de la brujula, es decir hacia donde girar el mago
					Character ch = caughtPj(GameObject.Find("wizard"));
					if(ch != null){
						Pj pj = (Pj) ch;
						Plane plano = new Plane(Vector3.up, pj.getCompass().transform.position);
						Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
						if(plano.Raycast(ray_,out hitDist)){
							Vector3 posPlano =ray_.GetPoint(hitDist);
							rotarWizard = Quaternion.LookRotation(posPlano - pj.getCompass().transform.position);
							pj.getCompass().transform.rotation = Quaternion.Slerp(pj.getCompass().transform.rotation,rotarWizard,Time.deltaTime * 10);
							//pj.getCompass().transform.position = GameObject.Find("wizard").transform.position ;
							
							
						}
					}
				}
				else if(PjSelection.touchPj == 3){
					//se calcula la rotacion de la brujula, es decir hacia donde girar el arquero
					Character ch = caughtPj(GameObject.Find("archer"));
					if(ch != null){
						Pj pj = (Pj) ch;
						Plane plano = new Plane(Vector3.up, pj.getCompass().transform.position);
						Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
						if(plano.Raycast(ray_,out hitDist)){
							Vector3 posPlano =ray_.GetPoint(hitDist);
							
							rotarArcher = Quaternion.LookRotation(posPlano - pj.getCompass().transform.position);
							pj.getCompass().transform.rotation = Quaternion.Slerp(pj.getCompass().transform.rotation,rotarArcher,Time.deltaTime * 10);
							//pj.getCompass().transform.position = GameObject.Find("archer").transform.position ;
							
						}		
					}
				}
				else if(PjSelection.touchPj == -1){//en caso de seleccion de cuadro
					//se calcula la rotacion de la brujula, de cada personaje seleccionad por el cuadro de selecion
					foreach(GameObject obj in pjsInSelectionBox){
						if(obj != null){
							if(obj.name == "warrior"){
								Character ch = caughtPj(GameObject.Find("warrior"));
								if(ch != null){
									Pj pj = (Pj) ch;
									Plane plano = new Plane(Vector3.up, pj.getCompass().transform.position);
									Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
									if(plano.Raycast(ray_,out hitDist)){
										Vector3 posPlano =ray_.GetPoint(hitDist);
										rotarWarrior = Quaternion.LookRotation(posPlano - pj.getCompass().transform.position);
										pj.getCompass().transform.rotation = Quaternion.Slerp(pj.getCompass().transform.rotation,rotarWarrior,Time.deltaTime * 10);
										//pj.getCompass().transform.position = GameObject.Find("warrior").transform.position ;
										
										
									}
								}
							}
							else if(obj.name == "wizard"){
								
								Character ch = caughtPj(GameObject.Find("wizard"));
								if(ch != null){
									Pj pj = (Pj) ch;
									Plane plano = new Plane(Vector3.up, pj.getCompass().transform.position);
									Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
									if(plano.Raycast(ray_,out hitDist)){
										Vector3 posPlano =ray_.GetPoint(hitDist);
										rotarWizard = Quaternion.LookRotation(posPlano - pj.getCompass().transform.position);
										pj.getCompass().transform.rotation = Quaternion.Slerp(pj.getCompass().transform.rotation,rotarWizard,Time.deltaTime * 10);
										//pj.getCompass().transform.position = GameObject.Find("wizard").transform.position ;
										
										
									}
								}
							}
							else if(obj.name == "archer"){
								Character ch = caughtPj(GameObject.Find("archer"));
								if(ch != null){
									Pj pj = (Pj) ch;
									Plane plano = new Plane(Vector3.up, pj.getCompass().transform.position);
									Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
									if(plano.Raycast(ray_,out hitDist)){
										Vector3 posPlano =ray_.GetPoint(hitDist);
										rotarArcher = Quaternion.LookRotation(posPlano - pj.getCompass().transform.position);
										pj.getCompass().transform.rotation = Quaternion.Slerp(pj.getCompass().transform.rotation,rotarArcher,Time.deltaTime * 10);
										//pj.getCompass().transform.position = GameObject.Find("archer").transform.position ;
										
										
									}
								}
							}
					
						}
					}
					
				}
				else if(PjSelection.touchPj == 0){//en caso de seleccionar a todos los personajes
					GameObject[] listPjs = GameObject.FindGameObjectsWithTag("Player");
					//se calcula la rotacion de la brujula, de cada personaje del array de personajes
					for(int i = 0; i < listPjs.Length;i++){
										
						if(listPjs[i].name == "warrior"){
							Character ch = caughtPj(GameObject.Find("warrior"));
							if(ch != null){
								Pj pj = (Pj) ch;
								Plane plano = new Plane(Vector3.up, pj.getCompass().transform.position);
								Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
								if(plano.Raycast(ray_,out hitDist)){
									Vector3 posPlano =ray_.GetPoint(hitDist);
									rotarWarrior = Quaternion.LookRotation(posPlano - pj.getCompass().transform.position);
									pj.getCompass().transform.rotation = Quaternion.Slerp(pj.getCompass().transform.rotation,rotarWarrior,Time.deltaTime * 10);
									//pj.getCompass().transform.position = GameObject.Find("warrior").transform.position ;
									
									
								}
							}
						}
						if(listPjs[i].name == "wizard"){
							Character ch = caughtPj(GameObject.Find("wizard"));
							if(ch != null){
								Pj pj = (Pj) ch;
								Plane plano = new Plane(Vector3.up, pj.getCompass().transform.position);
								Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
								if(plano.Raycast(ray_,out hitDist)){
									Vector3 posPlano =ray_.GetPoint(hitDist);
									rotarWizard = Quaternion.LookRotation(posPlano - pj.getCompass().transform.position);
									pj.getCompass().transform.rotation = Quaternion.Slerp(pj.getCompass().transform.rotation,rotarWizard,Time.deltaTime * 10);
									//pj.getCompass().transform.position = GameObject.Find("wizard").transform.position ;
									
									
								}
							}
						}	
						if(listPjs[i].name == "archer"){
							Character ch = caughtPj(GameObject.Find("archer"));
							if(ch != null){
								Pj pj = (Pj) ch;
								Plane plano = new Plane(Vector3.up, pj.getCompass().transform.position);
								Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
								if(plano.Raycast(ray_,out hitDist)){
									Vector3 posPlano =ray_.GetPoint(hitDist);
									rotarArcher = Quaternion.LookRotation(posPlano - pj.getCompass().transform.position);
									pj.getCompass().transform.rotation = Quaternion.Slerp(pj.getCompass().transform.rotation,rotarArcher,Time.deltaTime * 10);
									//pj.getCompass().transform.position = GameObject.Find("archer").transform.position ;
									
								}
							}
						}
					
					}
					
				}
				//si pulsamos raton
				if(Input.GetMouseButtonDown(0)){
					Cursor.SetCursor(null, Vector2.zero, cursorMode);//restablecer a cursor normal
					
					if(Time.timeScale != 0f){ //si no hemos parado el tiempo de juego
						oldTime = (float)Time.time;//asignamos valor de tiermpo al pulsar boton de raton
						initSelectionSquarePosition = Input.mousePosition;//asignamos posicion de inicio de cuadro de seleccion
					}
					else{
						oldTimeInPause = Time.realtimeSinceStartup; //asignamos valor de tiempo real al pulsar boton de raton
						initSelectionSquarePosition = Input.mousePosition;//asignamos posicion de inicio de cuadro de seleccion
						
					}
					
					
					
				}
				if(Input.GetMouseButton(0)){//si mantenemos pulsado el boton
					newTime = (float)Time.time;//calculamos nuevo valor de tiempo
					
					
					
					if(Time.timeScale != 0f){//si tiempo no parado
						
						if(newTime - oldTime >= 1){//miranos el tiempo pasado y segun este tiempo
												  	
							
							
							drawSelectionBox();//dibujamos cuadro de seleccion
							updateSelectionBox();//actualizamos personajes dentro de cuadro de seleccion 
												 //para su movimiento
						}
					}else{
						//si tiempo parado
						newTimeInPause = Time.realtimeSinceStartup;//se mira el tiempo en el momento que este parado 
						if(newTimeInPause-oldTimeInPause>=1){//miramos le timpo pasado y segun una unidad de tiempo
							drawSelectionBox();//dibujamos cuadrado
							updateSelectionBox();//actualizamos personajes dentro de cuadro de seleccion
						}						//para su movimiento
							
						
					}
					
				}
				
				if(Input.GetMouseButtonUp(0)){//si soltamos boton de raton
					
					if(selectionBox != null){//si existe cuadro de seleecion
						
						Destroy(selectionBox);//destruimos cuadro de seleccion
						selecting = false;
					}
					//calculamos nuevos tiempo
					newTime = (float)Time.time;
					newTimeInPause = Time.realtimeSinceStartup;
					
					//miramos si los tiempoes son mores a cierta unidad de tiempo
					if(newTime - oldTime < 1||   newTimeInPause - oldTimeInPause < 1 ){
						
						//init = false;
						//lanzamos rayo de camara al puntero de raton
						Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
						//flag = true;
						RaycastHit hit;
						//miramos sobre que collider recae
						if(Physics.Raycast(ray, out hit)){
							if (hit.collider != null){
								MotionControllerPJ.position=hit.point;
								
								if(PjSelection.touchPj == -1){//en caso de personajes seleccionados por cuadro de seleccion
									for(int i=0;i < pjsInSelectionBox.Count;i++){
										//si  uno es el guerrero
										if(pjsInSelectionBox[i].name == "warrior"){
											//si el rayo va contra objeto SeguimientoPj
											if(hit.collider.name == "SeguimientoPnj(Clone)"){
												//seleccionamos posicion siguiente de guerrero como la posicion de ese objeto
												pnjAtFollowWarrior = pnjSelected(hit.collider.gameObject).getTarget();
												nextPositionWarrior = pnjAtFollowWarrior.transform.position;
											}else{
												//seleccionamos posicion de escenario
												nextPositionWarrior = hit.point;
											}
											
											colliderSelected(hit,pjsInSelectionBox[i]);//miramos el collider seleccionado por el raycast
											Character ch = caughtPj(GameObject.Find("warrior"));//buscamos el Character asociado al gameObject de personajes seleccionados
											Pj pj = (Pj) ch;	
											//rotamos el guerrero segun la rotacion de la brujula
											GameObject.Find("warrior").transform.rotation = pj.getCompass().transform.rotation;
											
										}
										//si uno es el mago
										if(pjsInSelectionBox[i].name == "wizard"){
											//si el rayo va contra objeto SeguimientoPj	
											if(hit.collider.name == "SeguimientoPnj(Clone)"){
												//seleccionamos posicion siguiente de de mago como la posicion de ese objeto
												pnjAtFollowWizard = pnjSelected(hit.collider.gameObject).getTarget();
												nextPositionWizard = pnjAtFollowWizard.transform.position;
											}
											else{
												//seleccionamos posicion de escenario
												nextPositionWizard = hit.point;
											}
											colliderSelected(hit,pjsInSelectionBox[i]);//miramos el collider seleccionado por el raycast
											Character ch = caughtPj(GameObject.Find("wizard"));//buscamos el Character asociado al gameObject de personajes seleccionados
											Pj pj = (Pj) ch;
											//rotamos al mago segun la rotacion de la brujula
											GameObject.Find("wizard").transform.rotation = pj.getCompass().transform.rotation;
											
											
											
										}
										if(pjsInSelectionBox[i].name == "archer"){
											nextPositionArcher = hit.point;//seleccionamos posicion de escenario
											colliderSelected(hit,pjsInSelectionBox[i]);//miramos el collider seleccionado por el raycast
											Character ch = caughtPj(GameObject.Find("archer"));//buscamos el Character asociado al gameObject de personajes seleccionados
											Pj pj = (Pj) ch;
											//rotamos al arquero segun la rotacion de la brujula
											GameObject.Find("archer").transform.rotation = pj.getCompass().transform.rotation;
											
											
											
										}
									}
								}
								if(PjSelection.touchPj == 0){//en caso de que todos los personajes esten seleccionados
									GameObject[] listPjs = GameObject.FindGameObjectsWithTag("Player");
									//miramos todos los personajes con tag players 
									for(int i = 0; i < listPjs.Length;i++){
										
										if(listPjs[i].name == "warrior"){//si es un guerrero
											if(hit.collider.name == "SeguimientoPnj(Clone)"){//si el collider seleccionado es 
																							 //seguimientoPnj
												//asignamos enemigo que seguira el guerrero
												pnjAtFollowWarrior = pnjSelected(hit.collider.gameObject).getTarget();
											
											}
										}
										if(listPjs[i].name == "wizard"){//si es un mago
											if(hit.collider.name == "SeguimientoPnj(Clone)"){//si el collider seleccionado es 
																							 //seguimientoPnj
												
												//asignamos enemigo que seguira el mago
												pnjAtFollowWizard = pnjSelected(hit.collider.gameObject).getTarget();
											}
										}
										//movemos los personajes segun rotacion de brujula
										Character ch = caughtPj(listPjs[i].gameObject);
										Pj pj = (Pj) ch;	
										listPjs[i].transform.rotation = pj.getCompass().transform.rotation;
										nextPositionAll[i] = hit.point;	
										colliderSelected(hit,listPjs[i]);//miramos el collider seleccionado por el raycast
									
									}
									
								}
								if(PjSelection.touchPj == 1){
								 	//si guerrero esta seleccionado para movimiento
									if(PjSelection.moveWarrior){
										
										
										if(hit.collider.name == "SeguimientoPnj(Clone)"){//si el collider selecionado es SeguimientoPnj
											//seleccionamos pnj que seguir y asignamos posicion de guerrero
											pnjAtFollowWarrior = pnjSelected(hit.collider.gameObject).getTarget();
											nextPositionWarrior = pnjAtFollowWarrior.transform.position;
											
											
											
										}else{
											//asignamo posicion de escenario
											nextPositionWarrior = hit.point;
											
										}
										Character ch = caughtPj(GameObject.Find("warrior"));
										Pj pj = (Pj) ch;
										
										GameObject.Find("warrior").transform.rotation = pj.getCompass().transform.rotation;	
										
										
										
										
										colliderSelected(hit,GameObject.Find("warrior"));//miramos el collider seleccionado por el raycast
										
										
									}
									PjSelection.moveWarrior = true;
									PjSelection.moveWizard = false;
									PjSelection.moveArcher = false;	
									
								}
								if(PjSelection.touchPj == 2 ){
									//si mago esta seleccionado para movimiento
									Wizard w = getWizard ();
									if(w != null){
										if(!w.getStop()){//si mago esta en movimiento(no hechiza)
											if(PjSelection.moveWizard){
												if(hit.collider.name == "SeguimientoPnj(Clone)"){//si el collider selecionado es SeguimientoPnj
												//seleccionamos pnj que seguir y asignamos posicion de mago
													pnjAtFollowWizard = pnjSelected(hit.collider.gameObject).getTarget();
													nextPositionWizard = pnjAtFollowWizard.transform.position;
												}else{
													
													//seleccionamos posicion del terreno
													nextPositionWizard = hit.point;	
												}
												colliderSelected(hit,GameObject.Find("wizard"));//miramos el collider seleccionado por el raycast
												
											}
										
											PjSelection.moveWarrior = false;
											PjSelection.moveWizard = true;
											PjSelection.moveArcher = false;	
										}
										else{//mago si hechiza
											colliderSelected(hit,GameObject.Find("wizard"));//miramos el collider seleccionado por el raycast
											nextPositionWizard = GameObject.Find("wizard").transform.position;	
												
										}
										Character ch = caughtPj(GameObject.Find("wizard"));
										Pj pj = (Pj) ch;
										//rotamos mago segun posicion brujula
										GameObject.Find("wizard").transform.rotation = pj.getCompass().transform.rotation;
									}
								}
								if(PjSelection.touchPj == 3 ){
									//personaje seleccionado arquero
									if(PjSelection.moveArcher){
										nextPositionArcher = hit.point;	
										colliderSelected(hit,GameObject.Find("archer"));//miramos el collider seleccionado por el raycast
										Character ch = caughtPj(GameObject.Find("archer"));
										Pj pj = (Pj) ch;
										//rotamos el arquero segun la brujula
										GameObject.Find("archer").transform.rotation = pj.getCompass().transform.rotation;
									
									}
									PjSelection.moveWarrior = false;
									PjSelection.moveWizard = false;
									PjSelection.moveArcher = true;
								
								}	
									
								
							}
						}
					}
				}
				
			}
		}
		
	}
	
	/*metodo que dibuja el cuadro de seleccion*/
	void drawSelectionBox(){
		//si no existe el cuadro
		if (!selecting){
			
			selecting = true;
            //punto donde nacera el cuadro de seleccion
			selectionOrigin = initSelectionSquarePosition;
            //cargamos el guitexture asociado   
			selectionBox = GameObject.Instantiate(Resources.Load("SelectionBox")) as GameObject;
			//le asignamos un tamaño inicial
			selectionBox.guiTexture.pixelInset = new Rect(selectionOrigin.x, selectionOrigin.y, 1, 1);
 				
                    
		}
		else{

            //Estos son los límites de nuestro cuadro de selección
			bound = selectionBox.guiTexture.pixelInset;
			//agrandamos segun arrastramos el raton
			bound.xMax = Input.mousePosition.x;
			bound.yMin =  Input.mousePosition.y; 
            //Cambiamos el pixelInset de nuestro cuadro de selección
			selectionBox.guiTexture.pixelInset = bound;
			
				
			
			
        }
	}
	
	void updateSelectionBox(){
		//si existe cuadro de seleccion
		if(selecting){
			//lanzamos rayast
			RaycastHit hit;
            Ray ray;
			pjsInSelectionBox = new List<GameObject>();
			Vector3 selectionFinal = Input.mousePosition;
			ray = Camera.main.ScreenPointToRay(selectionFinal);
			if (Physics.Raycast(ray, out hit)){
				//asignamos puntos segun el desplazamiento del raton para la creacion del cuadro de selecion
				float minX = Mathf.Min(selectionOrigin.x,selectionFinal.x);
				float maxY = Mathf.Max(selectionOrigin.y,selectionFinal.y);
				float maxX = Mathf.Max(selectionOrigin.x,selectionFinal.x);
				float minY = Mathf.Min(selectionOrigin.y,selectionFinal.y);
				
				//miramos si losgameobjects estan el lista
				if(pjs.Contains(hit.collider.gameObject)){
					pjsInSelectionBox.Add(hit.collider.gameObject);
					
					
				}
				else{
					foreach(GameObject pj in pjs){
						
						Character ch = caughtPj (pj);
						if(ch != null){
							Pj pjSelectedForCercle = (Pj) ch;
							//añadimoa vector de personajes selecioandos si estan dentro del cuadro de seleccion
							if((!pjsInSelectionBox.Contains(pj)) && (Camera.main.WorldToScreenPoint(pj.transform.position).x <= maxX && Camera.main.WorldToScreenPoint(pj.transform.position).x >= minX && Camera.main.WorldToScreenPoint(pj.transform.position).y >= minY && Camera.main.WorldToScreenPoint(pj.transform.position).y <= maxY)){
								pjsInSelectionBox.Add(pj);
								//dibujamos aureolas,(selectores de pj)
								pjSelectedForCercle.getSelectionPj().renderer.enabled = true;
								selectionDone = true;
								
							}else{
								//quitamos  personajes de vector
								pjsInSelectionBox.Remove(pj);
								//hacemos invisibles las  aureolas
								pjSelectedForCercle.getSelectionPj().renderer.enabled = false;
								if(pjsInSelectionBox.Count <=0) {
									if(PjSelection.touchPj == 0){
										pjSelectedForCercle.getSelectionPj().renderer.enabled = true;
									}
									selectionDone = false;	
								}
								
							}
						}
					}
					
				}
				//calculamos nuevas posiciones de personajes que seran movidos
				nextPositionSelected = new Vector3[pjsInSelectionBox.Count];
				lastPositionSelected = new Vector3[pjsInSelectionBox.Count];
				
					
			
			}
		}
		
		
	}
	/*
	 * metodo que hace actuar al personaje selecionado de una forma u otra segun el collider seleccionado
	 */ 
	public void colliderSelected(RaycastHit hit,GameObject oplayer){
		Collider collider = hit.collider;
		Character pnjSelect;
		
		if(collider.name == "SeguimientoPnj(Clone)"){//si collider selecionado es seguimientoPnj
			pnjSelect = pnjSelected(collider.gameObject);
			
			if(pnjSelect.getTarget().name == "pnj" || pnjSelect.getTarget().name == "pnj_range"){
				
				if(PjSelection.touchPj == -1){
					
					foreach(GameObject pj in pjsInSelectionBox){
						//hacemos que personajes vayan hacia la posicion sin que se paren
						Character ch = caughtPj(pj);
						
						Pj pjTmp = (Pj) ch;
						pjTmp.setStateAnimation(1);
						pjTmp.setAtackType(true);
						assignPnj(oplayer,pnjSelect);
						assignPjAtEnemy(oplayer,pnjSelect);
						if(ch.getTarget().name == "warrior"){
							Warrior w = (Warrior) ch;
							w.setBarbaryanFuryActivate(false);
						}
						if(ch.getTarget().name == "archer"){
							Archer a = (Archer) ch;
							a.setFastArrowActivate(false);
						}
						
						
						
						
					}
				}else if(PjSelection.touchPj == 0){
					//hacemos que personajes vayan hacia la posicion sin que se paren
					foreach(GameObject pj in pjs){
						Character ch = caughtPj(pj);

						if(pj != null){
							Pj pjTmp = (Pj) ch;
							pjTmp.setStateAnimation(1);
							pjTmp.setAtackType(true);
							assignPnj(oplayer,pnjSelect);
							assignPjAtEnemy(oplayer,pnjSelect);
							if(ch.getTarget().name == "warrior"){
								Warrior w = (Warrior) ch;
								w.setBarbaryanFuryActivate(false);
							}
							if(ch.getTarget().name == "archer"){
								Archer a = (Archer) ch;
								a.setFastArrowActivate(false);
							}
						}
						
					}
				}else if(PjSelection.touchPj == 1){
					//hacemos que guerero vaya hacia la posicion sin que se pare
					Character ch = caughtPj(GameObject.Find("warrior"));
					Pj pjTmp = (Pj) ch;
					pjTmp.setStateAnimation(1);
					pjTmp.setAtackType(true);
					assignPnj(oplayer,pnjSelect);//asignar pnj como enemigo para seguir
					assignPjAtEnemy(oplayer,pnjSelect);//asignar pj a enemigo para seguir
					
					Warrior w = (Warrior) pjTmp;
					w.setBarbaryanFuryActivate(false);
					
				}else if(PjSelection.touchPj == 2){
					//aqui mirar si el wizard esta parado para hacer hehcizo o no
					Character ch = caughtPj(GameObject.Find("wizard"));
					Pj pjTmp = (Pj) ch;	
					Wizard w = (Wizard) pjTmp;
					if(!w.getStop()){//en caso de no hechizar
						pjTmp.setAtackType(true);
						pjTmp.setStateAnimation(1);
						assignPnj(oplayer,pnjSelect);//asignar pnj como enemigo para seguir
						assignPjAtEnemy(oplayer,pnjSelect);//asignar pj a enemigo para seguir
					}else{//escojo al pnj al que tiro el hechizo(Magic BUllet Spell) y lo paso al Hud para que active el hechizo correspondiente 
						if(Vector3.Distance(w.getTarget().transform.position, pnjSelect.getTarget().transform.position) < 80){
							assignPjAtEnemy(oplayer,pnjSelect);//asignar pnj como enemigo para seguir
							assignPnjForSpell = (Pnj) pnjSelect;//asignar pj a enemigo para seguir
						}
					}
					
					
				}else if(PjSelection.touchPj == 3){
					
					Character ch = caughtPj(GameObject.Find("archer"));
					Pj pjTmp = (Pj) ch;	
					pjTmp.setAtackType(true);
					pjTmp.setStateAnimation(1);
					assignPnj(oplayer,pnjSelect);
					assignPjAtEnemy(oplayer,pnjSelect);
					Archer a = (Archer) pjTmp;
					a.setFastArrowActivate(false);
				}
				
				
			}
			
		}
		else if(collider.name == "Terrain"){//en caso de selecionar el terrain
											
			if(PjSelection.touchPj == -1){
				if(pjsInSelectionBox.Contains(GameObject.Find("warrior"))){
					//deja mover a pj,activa animacion
					Character c2 = caughtPj(GameObject.Find("warrior"));
					Pj pj2 = (Pj) c2;
					pj2.setStateAnimation(1);
					pj2.setAtackType(false);
					Warrior war = (Warrior) pj2;
					war.setBarbaryanFuryActivate(false);
					
				}
				if(pjsInSelectionBox.Contains(GameObject.Find("wizard"))){
					//deja mover a pj,activa animacion
					Character c1 = caughtPj(GameObject.Find("wizard"));
					Pj pj1 = (Pj) c1;
					pj1.setStateAnimation(1);
					pj1.setAtackType(false);
					
 				}
				if(pjsInSelectionBox.Contains(GameObject.Find("archer"))){
					//deja mover a pj,activa animacion
					Character c3 = caughtPj(GameObject.Find("archer"));
					Pj pj3 = (Pj) c3;
					pj3.setStateAnimation(1);
					pj3.setAtackType(false);
					Archer a = (Archer)pj3;
					a.setFastArrowActivate(false);
				}
			}
			
			
			if(PjSelection.touchPj == 0 ){
				Character c2 = caughtPj(GameObject.Find("warrior"));
				//deja mover a pj,activa animacion
				if(c2 != null){
					Pj pj2 = (Pj) c2;
					pj2.setStateAnimation(1);
					pj2.setAtackType(false);
					Warrior w = (Warrior) c2;
					w.setBarbaryanFuryActivate(false);
				}
				
				Character c1 = caughtPj(GameObject.Find("wizard"));
				//deja mover a pj,activa animacion
				if(c1 != null){
					Pj pj1 = (Pj) c1;
					pj1.setStateAnimation(1);
					pj1.setAtackType(false);
				}
				
				Character c3 = caughtPj(GameObject.Find("archer"));
				//deja mover a pj,activa animacion
				if(c3 != null){
					Pj pj3 = (Pj) c3;
					pj3.setStateAnimation(1);
					pj3.setAtackType(false);
					Archer a = (Archer) c3;
					a.setFastArrowActivate(false);
				}
				
				
				
				
						
				
				
				
				
			}
			if(PjSelection.touchPj == 1 ){
				//deja mover a pj,activa animacion
				Character c2 = caughtPj(GameObject.Find("warrior"));
				Pj pj2 = (Pj) c2;
				pj2.setStateAnimation(1);
				pj2.setAtackType(false);
				Warrior war = (Warrior) pj2;//pongo a false la habilidad para que el pj camine
				war.setBarbaryanFuryActivate(false);
				
				
			}
			if(PjSelection.touchPj == 2 ){
				//deja mover a pj,activa animacion
				Character c1 = caughtPj(GameObject.Find("wizard"));
				Pj pj1 = (Pj) c1;
				pj1.setAtackType(false);
				pj1.setStateAnimation(1);
				Wizard wiz = (Wizard) c1;
				wiz.setStop(false);//el mago se podra mover, esto es para despues de activar hechizo
				
			}
			if(PjSelection.touchPj == 3 ){
				//deja mover a pj,activa animacion
				Character c3 = caughtPj(GameObject.Find("archer"));
				Pj pj3 = (Pj) c3;
				pj3.setStateAnimation(1);
				pj3.setAtackType(false);
				Archer a = (Archer) pj3;
				a.setFastArrowActivate(false);
			}
			
			
		}
		else if(collider.name == "warrior"){
			//para al wizard para hacer hechizo Armor
			Character ch = caughtPj(GameObject.Find("wizard"));
			Wizard w = (Wizard) ch;
			if(w.getStop()){
				w.setStop(false);
			}
			
			
		}
		else if(collider.name == "wizard"){
			//para al wizard para hacer hechizo Armor
			Character ch = caughtPj(GameObject.Find("wizard"));
			Wizard w = (Wizard) ch;
			if(w.getStop()){
				w.setStop(false);	
			}
			
			
		}
		else if(collider.name == "archer"){
			//para al wizard para hacer hechizo Armor
			Character ch = caughtPj(GameObject.Find("wizard"));
			Wizard w = (Wizard) ch;
			if(w.getStop()){
				w.setStop(false);	
			}
			
			
		}
		
		
		
	}
	/*
	 * metodo que añade a lista de personajes que seguira el enemigo
	 */ 
	private void assignPjAtEnemy(GameObject pj,Character pnj){
		
		
		if(pnj.getTarget().name == "pnj" || pnj.getTarget().name == "pnj_range"){
			Pnj pnjTmp = (Pnj) pnj;
			if(pj.name == "archer"){
				pnjTmp.addEnemiesForFollow(pj);
				
			}
			
			
		}
		
		
	}
	/*
	 * metodo que asigna el enemigo que seguira el personaje principal
	 */ 
	private void assignPnj(GameObject pj,Character pnj){
		
		
		if(pnj.getTarget().name == "pnj" || pnj.getTarget().name == "pnj_range"){
			Character ch = caughtPj(pj);
			Pj pjTmp = (Pj) ch;
			
			Pnj pnjTmp = (Pnj) pnj;
			//personaje que va a luchar contra mi, esto es para
			//ver a quien voi a atacar
			
			pjTmp.setFollowPnj(pnjTmp);
			
			
		}
		
		
	}
	/*
	 * metodo que devuelve el Character correspondiente al objeto SeguimientoPnj asociado a el
	 */
	private Character pnjSelected(GameObject follow){
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.GetType().FullName == "PnjShort"){
				Pnj p = (Pnj) ch;
				if(p.getFollowPnj() == follow){
					return ch;
				}
			}else if( ch.GetType().FullName == "PnjRange"){
				Pnj p = (Pnj) ch;
				if(p.getFollowPnj() == follow){
					return ch;
				}
			}
		}
		return null;
	}
	/*
	 * metodo que devuelve Character segun gameobject
	 */
	private Character caughtPj(GameObject pj){
		if(pj == null){
			return null;	
		}
		foreach(Character ch in SearchCharacters.characterList){
			if(ch.getTarget() == null){
			
				return null;
			}
			if(ch.getTarget().name == pj.name){
				return ch;
			}
			
		}
		return null;
	}
	/*
	*metodo que devuelve wizard
	*/   
	public Wizard getWizard(){
		foreach(Character ch in SearchCharacters.characterList){
			if(GameObject.Find("wizard") == ch.getTarget()){
				Wizard pjWizard = (Wizard) ch;
				return pjWizard;
			}
		}
		return null;
	}
	
	public void updatePositionCompass(){
		foreach(Character ch in SearchCharacters.characterList){
			
			if(ch.GetType().FullName == "Wizard" || ch.GetType().FullName == "Warrior" || ch.GetType().FullName == "Archer" ){
				Pj pj = (Pj) ch;
				pj.getCompass().transform.position = new Vector3(pj.getTarget().transform.position.x,pj.getTarget().transform.position.y,pj.getTarget().transform.position.z);
			}
		}
		
	}

}
