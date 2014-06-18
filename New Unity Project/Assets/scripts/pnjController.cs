using UnityEngine;
using System.Collections;


public class pnjController : MonoBehaviour {

	private GameObject pj;
	private float speed;
	private Vector3 VectorAux;
	private Collider[] cols;
	private ArrayList characterEnemiesOfPnj;
	GameObject pjFound;
	private GameObject followByEnemy;
	private ArrayList list;

	// Use this for initialization
	void Start () {
		list = SearchCharacters.characterList;
		speed = 20f;
		
		
	}
	// Update is called once per frame
	void Update () {
		
		for(int i=0; i < list.Count;i++){
			Character pnj = (Character)list[i];
			if(pnj.GetType().FullName == "PnjShort"){
				if(pnj.getDeath()){// si pnj ha perdido alguna batalla
					list.Remove(pnj);//borro de lista de personajes	
					Destroy(pnj.getTarget());//destruccion del gameObject de esa instancia
				}
				if(pnj.getTarget()!=null){
					//miro si algun personaje esta en el radio de vision del pnj
					cols = Physics.OverlapSphere(pnj.getTarget().transform.position,pnj.getSight()*10);
					
					Pnj pnjTmp = (Pnj) pnj;
					//si no hay ningun personaje que se ha visto
					if(pnjTmp.getEnemiesForFollow().Count>0){
						//asigno el primer personaje a seguir de los que se han visto
						followByEnemy =  (GameObject) pnjTmp.getEnemiesForFollow()[0];
						
					}
					else{
						//calculo de la ruta del eprsonaje por defecto
						monotoring(pnjTmp);
						pnjTmp.setStateAnimation(4);
						
					}
					
					if(colsisPJ(cols)){//si se ve personaje
						pjFound = findEnemy(cols); //asigno GameObject de personaje visto
						assignPjAtEnemy(pjFound,pnj); // añado personaje a seguir
						
						if(followByEnemy != null){
							
							
							Character ch = getCharacterPj(followByEnemy);//casteo del personaje a seguir
							
							PnjShort tmpPnj = (PnjShort) pnj;
							if(ch != null){
								if(!ch.getDeath()){
									
									Vector3 newPosPj = new Vector3(followByEnemy.transform.position.x,followByEnemy.transform.position.y,followByEnemy.transform.position.z);
									if(!tmpPnj.getStop()){//si el pnjShort no esta parado va hacia enemigos 
										if(!tmpPnj.getGoFight()){//y no esta luchando
											updateDestinationPoint(pnjTmp);//cambiamos el siguiente punto al que ira el pnj cuando la lucha termina si gana
											tmpPnj.setStateAnimation(1);//activamos animacion de correr
										}
										//establecemos rotacion y movimiento de pnj hacia posicion de personaje controlable
										Quaternion rotation;
										rotation = Quaternion.LookRotation(newPosPj - tmpPnj.getTarget().transform.position);
										tmpPnj.getTarget().transform.rotation = Quaternion.Slerp(tmpPnj.getTarget().transform.rotation,rotation,Time.deltaTime * speed);
										CharacterController cc =  (CharacterController) pnj.getTarget().collider;
										Vector3 offset = newPosPj - pnj.getTarget().transform.position;
										if(offset.magnitude> .1f){
											offset = offset.normalized * speed;
											cc.Move(offset * Time.deltaTime);
										}
										
									}else{
										//si la distancia ente el personaje y el pnj es la de distancia de lucha paramos al pnj
										if(Vector3.Distance(newPosPj,tmpPnj.getTarget().transform.position) > ch.getDistanceFight()){
											tmpPnj.setStop(false);	
											
										}
									}
									
									
								}else{//si personaje que se sigue a muerto 
									pnjTmp.removeEnemiesForFollow(followByEnemy);//eliminamos de la lista de personajes a seguir
									tmpPnj.setStop(false);//volvemos a mover al ponj
									removeEnemiesForFollowAllPnj(followByEnemy);
									DestroyImmediate (ch.getTarget());//destruimos gameOBject del pJ asociado
									Destroy (ch.getBloodParticle());//destruimos el sistema de particulas de sangre del personaje asociado
									SearchCharacters.characterList.Remove(ch);//eliminamos de la lista de caracteres
									
									
								}
							}
								
							
						}
						
					}else{
						
						if(followByEnemy != null){
							//si personaje a seguir es arquero
							if(followByEnemy.name == "archer" && pnjTmp.getEnemiesForFollow().Count > 0){
								
								Character ch = getCharacterPj(followByEnemy);//casteamos a Character
								PnjShort tmpPnj = (PnjShort) pnj;
								if(!ch.getDeath()){//si no esta muerto el personaje a seguir
									Vector3 newPosPj = new Vector3(followByEnemy.transform.position.x,followByEnemy.transform.position.y,followByEnemy.transform.position.z);
									if(!tmpPnj.getStop()){
										//movemos al pnj hacia posicion de arquero
										if(Vector3.Distance(tmpPnj.getTarget().transform.position,newPosPj) <= ch.getDistanceFight()){
											tmpPnj.setStateAnimation(1);
											Quaternion rotation;
											rotation = Quaternion.LookRotation(newPosPj - tmpPnj.getTarget().transform.position);
											tmpPnj.getTarget().transform.rotation = Quaternion.Slerp(tmpPnj.getTarget().transform.rotation,rotation,Time.deltaTime * speed);
											CharacterController cc =  (CharacterController) pnj.getTarget().collider;
											Vector3 offset = newPosPj - pnj.getTarget().transform.position;
											if(offset.magnitude > .1f){
												offset = offset.normalized * speed;
												cc.Move(offset * Time.deltaTime);
											}
											
										}
									}
								
								
								}else{
									//eliminamos de lista de personajes a seguir,gameobject asociado y sistema de particulas
									pnjTmp.removeEnemiesForFollow(followByEnemy);
									tmpPnj.setStop(false);
									removeEnemiesForFollowAllPnj(followByEnemy);
									DestroyImmediate (ch.getTarget());
									
									
									Destroy (ch.getBloodParticle());
									SearchCharacters.characterList.Remove(ch);
									
								}
							}
							//si personaje es mago
							if(followByEnemy.name == "wizard" && pnjTmp.getEnemiesForFollow().Count > 0){
								
								Character ch = getCharacterPj(followByEnemy);//cast a Character del Pj
								PnjShort tmpPnj = (PnjShort) pnj;
								if(!ch.getDeath()){//si personaje no esta muerto
									Wizard w = (Wizard) ch;
									SpellList spells = w.getSpellListOfWizard();
									ArrayList listSpells = spells.getSpellList();
									
									if(throwSpell(listSpells,w,tmpPnj)){//miramos si tiro hechizo de ataque inmediato,muevo el pnj
										
										Vector3 newPosPj = new Vector3(followByEnemy.transform.position.x,followByEnemy.transform.position.y,followByEnemy.transform.position.z);
										
										//movemos pnj hacia el personaje a seguir
										if(!tmpPnj.getStop()){
											if(!tmpPnj.getGoFight()){
												tmpPnj.setStateAnimation(1);//activamos animacion de correr
											}
											Quaternion rotation;
											rotation = Quaternion.LookRotation(newPosPj - tmpPnj.getTarget().transform.position);
											tmpPnj.getTarget().transform.rotation = Quaternion.Slerp(tmpPnj.getTarget().transform.rotation,rotation,Time.deltaTime * speed);
											CharacterController cc =  (CharacterController) pnj.getTarget().collider;
											Vector3 offset = newPosPj - pnj.getTarget().transform.position;
											if(offset.magnitude > .1f){
												
												offset = offset.normalized * speed;
												cc.Move(offset * Time.deltaTime);
												
											}
												
											
										}
									}
								}
								else{
								
									pnjTmp.removeEnemiesForFollow(followByEnemy);
									tmpPnj.setStop(false);
									removeEnemiesForFollowAllPnj(followByEnemy);
									DestroyImmediate (ch.getTarget());
									Destroy (ch.getBloodParticle());
									SearchCharacters.characterList.Remove(ch);
									
								}
								
							}
						}
					}
					
				}	
					
					
					
	
			}

			if(pnj.GetType().FullName == "PnjRange"){
				if(pnj.getDeath()){//si muere pnj eliminamos de lista y destruimos el gameObject
					list.Remove(pnj);	
					Destroy(pnj.getTarget());
				}
				if(pnj.getTarget()!=null){
					//miro si algun personaje esta en el radio de vision del pnj
					cols = Physics.OverlapSphere(pnj.getTarget().transform.position,pnj.getSight()*10);
					Pnj pnjTmp = (Pnj) pnj;
					if(pnjTmp.getEnemiesForFollow().Count>0){
						followByEnemy =  (GameObject) pnjTmp.getEnemiesForFollow()[0];//asignamos enemigo a seguir de la lista
							
					}
					else{
						//RUTA DE GUARDIA
						monotoring(pnjTmp);
						pnjTmp.setStateAnimation(4);
						
					}
					
					if(colsisPJ(cols)){
						pjFound = findEnemy(cols);//obtenemos gameObject del personaje que hemos localizado
						assignPjAtEnemy(pjFound,pnj);//lo metemos en lista de pjs a seguir
						if(followByEnemy != null){
						
							Character ch = getCharacterPj(followByEnemy);
							
							PnjRange tmpPnj = (PnjRange) pnj;
							if(ch != null){
								if(!ch.getDeath()){
									Vector3 newPosPj = new Vector3(followByEnemy.transform.position.x,followByEnemy.transform.position.y,followByEnemy.transform.position.z);
									if(!tmpPnj.getStop()){
										//movemos pnj hacia enemigo hasta que la distancia de ataque lo permita
										if(tmpPnj.getDistanceFight() <= calculateDistance(pnj.getTarget().transform.position,newPosPj)){
											tmpPnj.setStateAnimation(1);
											Quaternion rotation;
											rotation = Quaternion.LookRotation(newPosPj - tmpPnj.getTarget().transform.position);
											tmpPnj.getTarget().transform.rotation = Quaternion.Slerp(tmpPnj.getTarget().transform.rotation,rotation,Time.deltaTime * speed);
											CharacterController cc =  (CharacterController) pnj.getTarget().collider;
											Vector3 offset = newPosPj - pnj.getTarget().transform.position;
											if(offset.magnitude > .1f){
												offset = offset.normalized * speed;
												cc.Move(offset * Time.deltaTime);
											}
										}
									}else{
										tmpPnj.setDistanceFight(10);//actualizamos distancia de lucha
									}
									
									
								}else{
									//eliminamos personaje,gameObject...
									pnjTmp.removeEnemiesForFollow(followByEnemy);
									tmpPnj.setStop(false);
									Pj pj = (Pj) ch;
									removeEnemiesForFollowAllPnj(followByEnemy);
									Destroy(pj.getSelectionPj());
									Destroy (ch.getBloodParticle());
									DestroyImmediate (ch.getTarget());
									SearchCharacters.characterList.Remove(ch);
									
									
								}
							}
							
							
						}
						
					}else{
						
						if(followByEnemy != null){
							if(followByEnemy.name == "archer" && pnjTmp.getEnemiesForFollow().Count > 0){
								
								Character ch = getCharacterPj(followByEnemy);
								PnjRange tmpPnj = (PnjRange) pnj;
								if(!ch.getDeath()){
									Vector3 newPosPj = new Vector3(followByEnemy.transform.position.x,followByEnemy.transform.position.y,followByEnemy.transform.position.z);
								
									if(!tmpPnj.getStop()){
										//movemos pnj hasta la distancia de lucha contra el enmigo
										if(tmpPnj.getDistanceFight() >= calculateDistance(pnj.getTarget().transform.position,newPosPj)){
											tmpPnj.setStateAnimation(1);
											
											Quaternion rotation;
											rotation = Quaternion.LookRotation(newPosPj - tmpPnj.getTarget().transform.position);
											tmpPnj.getTarget().transform.rotation = Quaternion.Slerp(tmpPnj.getTarget().transform.rotation,rotation,Time.deltaTime * speed);
											
											CharacterController cc =  (CharacterController) pnj.getTarget().collider;
											Vector3 offset = newPosPj - pnj.getTarget().transform.position;
											if(offset.magnitude > .1f){
												offset = offset.normalized * speed;
												cc.Move(offset * Time.deltaTime);
											}
										}
										if(tmpPnj.getGoFight()){
											
											tmpPnj.setStateAnimation(1);
											Quaternion rotation;
											rotation = Quaternion.LookRotation(newPosPj - tmpPnj.getTarget().transform.position);
											tmpPnj.getTarget().transform.rotation = Quaternion.Slerp(tmpPnj.getTarget().transform.rotation,rotation,Time.deltaTime * speed);
											CharacterController cc =  (CharacterController) pnj.getTarget().collider;
											Vector3 offset = newPosPj - pnj.getTarget().transform.position;
											if(offset.magnitude > .1f){
												offset = offset.normalized * speed;
												cc.Move(offset * Time.deltaTime);
											}
										}
									}else{//modifico distancia de ataque
										tmpPnj.setDistanceFight(10);
									}
									
									
								}else{
									
									pnjTmp.removeEnemiesForFollow(followByEnemy);
									tmpPnj.setStop(false);
									Pj pj = (Pj) ch;
									removeEnemiesForFollowAllPnj(followByEnemy);
									Destroy(pj.getSelectionPj());
									Destroy (ch.getBloodParticle());
									DestroyImmediate (ch.getTarget());
									SearchCharacters.characterList.Remove(ch);
									
								}
							}
							if(followByEnemy.name == "wizard" && pnjTmp.getEnemiesForFollow().Count > 0){
								Character ch = getCharacterPj(followByEnemy);
								PnjRange tmpPnj = (PnjRange) pnj;
								if(!ch.getDeath()){
									Wizard w = (Wizard) ch;
									SpellList spells = w.getSpellListOfWizard();
									ArrayList listSpells = spells.getSpellList();
									
									if(throwSpell(listSpells,w,tmpPnj)){//si tiro hechizo de ataque inmediato,muevo el pnj
										//movemos pnj hasta posicion del pj que tira hechizo 
										Vector3 newPosPj = new Vector3(followByEnemy.transform.position.x,followByEnemy.transform.position.y,followByEnemy.transform.position.z);
										if(!tmpPnj.getStop()){
											
											Quaternion rotation;
											rotation = Quaternion.LookRotation(newPosPj - tmpPnj.getTarget().transform.position);
											tmpPnj.getTarget().transform.rotation = Quaternion.Slerp(tmpPnj.getTarget().transform.rotation,rotation,Time.deltaTime * speed);
											CharacterController cc =  (CharacterController) pnj.getTarget().collider;
											Vector3 offset = newPosPj - pnj.getTarget().transform.position;
											if(offset.magnitude > .1f){
												
												offset = offset.normalized * speed;
												cc.Move(offset * Time.deltaTime);
												
											}
												
											
										}
									}
								}
								else{
									
									pnjTmp.removeEnemiesForFollow(followByEnemy);
									tmpPnj.setStop(false);
									removeEnemiesForFollowAllPnj(followByEnemy);
									Destroy (ch.getBloodParticle());
									DestroyImmediate (ch.getTarget());
									SearchCharacters.characterList.Remove(ch);
									
								}
								
							}
						}
					}
				}
			}
			
		
		}
	}

	
	

	float calculateDistance(Vector3 a, Vector3 b){
		float x = a.x - b.x;
		float y = a.y - b.y;
		float z = a.z - b.z;
		float distance = Mathf.Sqrt(Mathf.Pow(x,2)+Mathf.Pow(y,2)+Mathf.Pow(z,2));
		return distance;
	}
	/*
	 * metodo que devuelve  true si pnj ha visto enemigo
	 */ 
	bool colsisPJ(Collider[] cols){
		for(int i=0; i < cols.Length;i++){
			if(cols[i].name == "warrior" || cols[i].name == "wizard" || cols[i].name == "archer"){
				
				return true;	
			}
		}
		return false;
	}
	/*
	 * metodo que devuelve GameOBject de personaje
	 */ 
	GameObject findEnemy(Collider[] cols){
		for(int i=0; i < cols.Length;i++){
			
			if(cols[i].name == "warrior"){
				
				
				return cols[i].gameObject;
			
			}else if(cols[i].name == "wizard"){
				
				return cols[i].gameObject;
			}
			else if(cols[i].name == "archer"){
				
				return cols[i].gameObject;
			}
			
		}
		return null;
			
	}
	bool atackRange(){
		foreach(Character ch in list){
			if(ch.getTarget().name == "wizard"){
				Pj pj = (Pj) ch;
				if(pj.getAtackType()){
					return true;	
				}
			}
		}
		return false;	
		
	}
	/*
	 * metodo que añade personaje a seguir a lsta de enemigos vistos de pnj 
	 */ 
	private void assignPjAtEnemy(GameObject pj,Character pnj){
		
		
		if(pnj.getTarget().name == "pnj" || pnj.getTarget().name == "pnj_range"){
			Pnj pnjTmp = (Pnj) pnj;
			if(pj.name == "wizard" || pj.name == "warrior" || pj.name == "archer"){
				pnjTmp.addEnemiesForFollow(pj);
			}
			
			
		}
		
		
	}
	/*
	 * metodo que devuelve la personaje a seguir
	 */ 
	private Character getCharacterPj(GameObject gameOb){
		
		foreach(Character ch in list){
			if((GameObject)ch.getTarget() == gameOb){
				return ch;
			}
		}
		return null;
	}
	/*
	 * metodo que devuelve si mago lanza hechizo magicBulletSpell
	 */
	private bool throwSpell(ArrayList listSpells,Wizard w,Pnj pnj){
		foreach(Spell s in listSpells){
			if(s.getId() == 1 ){//tiro hechizo de ataque inmediato (Magic Bullet)
				if(Vector3.Distance(w.getTarget().transform.position,pnj.getTarget().transform.position) < 80){
					return true;;
				}
			}
		}
		return false;
	}



	

					
					
					
					
	private void monotoring(Pnj pnj){
		int speed = 10;
		Vector3 posFinish = (Vector3)pnj.getMonitoring()[0];
		Vector3 posActual = new Vector3(pnj.getTarget().transform.position.x,0,pnj.getTarget().transform.position.z);
		
		if(((  (int)posFinish.x== (int)posActual.x) && ( (int)posFinish.z== (int)posActual.z))){
			
			updateDestinationPoint(pnj);
			
			
		}else{
			CharacterController cc = (CharacterController) pnj.getTarget().collider;
			Vector3 offset = posFinish - posActual;
			
			if(offset.magnitude > .1f){
				Quaternion rotation;
				rotation = Quaternion.LookRotation(posFinish - posActual);
				pnj.getTarget().transform.rotation = Quaternion.Slerp(pnj.getTarget().transform.rotation,rotation,Time.deltaTime * speed);								
				offset = offset.normalized * speed;
				cc.Move(offset * Time.deltaTime);
			}										
			
			
		}
		
	}
	
	private void updateDestinationPoint(Pnj pnj){
		
			ArrayList points = pnj.getMonitoring();
			if(points.Count > 0){
				Vector3 tmp = (Vector3) points[0];
				points[0] = (Vector3)points[1];
				points[points.Count-1] = tmp;
				pnj.setMonitoring(points);
				
			}
		
	}
	private void removeEnemiesForFollowAllPnj(GameObject followByEnemy){
		
		foreach(Character ch in list){
			if(ch.GetType().FullName == "PnjRange" || ch.GetType().FullName == "PnjShort"){
				Pnj pnj = (Pnj) ch;
				if(pnj.getEnemiesForFollow().Contains(followByEnemy)){
					
					pnj.removeEnemiesForFollow(followByEnemy);
				}
			}
		}
	}
	
	
  
}  
  
