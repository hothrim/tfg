using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Hud : MonoBehaviour {
	//private CursorMode cursorMode;
	//private Vector2 hotSpot;
	public GUISkin mySkin;
	private int height;
	private ArrayList listPjs;
	private Texture2D texturePortrait;
	private Texture2D textureLife;
	private Texture2D textureDeath;
	private Texture2D textureSpellArmor;
	private Texture2D textureSpellMagicBullet;
	private Texture2D textureSleep;
	public static int limitWithRight;//variable que se utilitza para limitar el movimiento del personaje  en la zona de juego(GuiBox no actuara raycast)
	public static int limitWithLeft;//variable que se utiliza para limitar el movimiento del personaje  en la zona de juego(GuiBox no actuara el Raycast)
	private int timeGame;
	private bool sleep;
	private GameObject fightSmsObject;
	private GameObject throwSmsDoor;
	private GoFight goFight;
	private ThrowMessageDoor throwMessageDoor;
	Vector2 scrollPosition;
	private Character pjSelectedForSpell;
	private Pnj pnjSelectedForSpell;
	private bool spellArmorInit;//variable para mirar si se ha seleccionado el boton de hechizo armor
	private bool spellMagicBulletInit;//variable para mirar si se ha seleccionado el boton de hechizo magic bullet
	private Texture2D textureBf;
	private Texture2D texturefA;
	private bool countArmor;
	private int countBullet;
	private string sms;
	private bool needSleep;//al activar Boton De Descanso
	private float timeImageSleep;
	private bool sleepDone;
	void Start () {
		height = Screen.height;
		listPjs = new ArrayList(SearchCharacters.characterList);
		textureLife = Resources.Load("life_bar") as Texture2D;
		textureDeath = Resources.Load("death_bar") as Texture2D;
		textureSleep = Resources.Load ("eye") as Texture2D;
		limitWithRight = 150;
		limitWithLeft = Screen.width - 150;
		InvokeRepeating("timer",1,1);
		scrollPosition = new Vector2(0, Mathf.Infinity);
		fightSmsObject = GameObject.Find("Cube");
		goFight = fightSmsObject.GetComponent<GoFight>();
		throwSmsDoor  = GameObject.Find("AlertMesaggeDoor");
		throwMessageDoor = throwSmsDoor.GetComponent<ThrowMessageDoor>();
		pjSelectedForSpell = null;
		spellArmorInit = false;
		spellMagicBulletInit = false;
		countArmor = false;
		countBullet = -1;
		sms = "";
		needSleep = false; 
		timeImageSleep = 0;
		sleepDone = false;
		timeGame = 0;
		sleep = false;
	}
	
	
	// Update is called once per frame
	void Update(){
		
	}
	
	//funcion para controlar cuando pueden descansar los personajes
	void timer(){
		timeGame++;
		if(sleep && timeGame< 10){
			
			sleep = false;
		}
		if(timeGame>=10 && !sleep){
			
			sleep = true;
			
		}
		
	}
	
	
	void OnGUI() {
		//Ceacion de los portraits laterales 
		GUI.skin = mySkin;
		GUI.Box (new Rect (0,0,Screen.width/7,Screen.height), "");
		GUI.Box (new Rect ((Screen.width-Screen.width/6),0,Screen.width/6,Screen.height), "");
		
		//creo un area para insertar un TextArea con scrollBar
		GUILayout.BeginArea(new Rect(Screen.width/4, Screen.height-(Screen.height/4), Screen.width/2,Screen.height/4));
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width (Screen.width/2), GUILayout.Height (Screen.height/4));
		
		
		
		//inserto los mensajes de las batallas
		if(goFight.receiveMessage() != null){
			if(goFight.receiveMessage().Length != 0 ){
				sms = goFight.receiveMessage();
			}
			
		}
		//inserto mensaje de scriptQuest indicador de acercarse a paneles
		if(throwMessageDoor.getSendMessage()){
			
			sms = throwMessageDoor.getSms();
			throwMessageDoor.setSendMessage(false);
			
		}
		//inserto mensaje de script Quest alerta de PJ Neutral
		if(PjNeutralController.alert){
			sms = PjNeutralController.sms;	
			PjNeutralController.alert = false;
		}
		
		
		//inserto sms de no descanso
		if(!sleepDone && needSleep){
			sms = "Los aventureros no han podido descansar\n";	
			needSleep = false;
			
		}
		//inserto sms de descanso
		if(sleepDone && needSleep){
			sms = "Los aventureros han hecho un breve descanso\n";	
			sleepDone = false;
			needSleep = false;	
			
		}
		
		
		//fin de area de TextArea
		GUILayout.TextArea(sms, GUILayout.ExpandHeight(true));
		GUILayout.EndScrollView ();
    	GUILayout.EndArea();
		
		
		foreach(Character ch in listPjs){
			//miramos que el personaje sea el guerrero y creamos su hud personalizado
			if(ch.GetType().FullName == "Warrior"){
				Pj pj= (Pj) ch;	
				if(!ch.getDeath()){//si no esta muerto cargamos portrait en color
					texturePortrait = Resources.Load(pj.getPortrait()) as Texture2D;
					
					
				}else{//cargamos portrait en escala grises
					texturePortrait =Resources.Load("warrior_death") as Texture2D;
					
				}
				//si seleccionamos el portrait el pesonaje sera seleccionado para su control
				if(GUI.Button(new Rect(Screen.width - 160 + 10,10,130,130),texturePortrait) && !MenuPauseAparition.gamePaused && !ch.getDeath()){
					PjSelection.touchPj	= 1;
				}
				GUI.DrawTexture(new Rect(Screen.width - 160 + 10, 140, 130, 10),textureDeath );//pintamos textura roja
				
				
				
				float damage =(float)pj.getPg()/(float)pj.getMaxPg()*130;//calculo del tamaño de la textura verde segun la vida que tengamos
				if(damage <= 0){
					damage = 0;
				}
				GUI.DrawTexture(new Rect(Screen.width - 160 + 10, 140, damage, 10), textureLife);//pintamos textura verde con valores de daño encima de la roja
				if(PjSelection.touchPj == 1){//si esta el guerrero seleccionado
					Warrior bf = (Warrior) ch;
					
					Pj pjW = (Pj) bf;
					if(pjW.getSelectionPj() != null){
						pjW.getSelectionPj().renderer.enabled = true;//renderizamos aureola de seleccion
					}
					//desactivamos las aureolas de los compañeros
					Character ch1 = caughtPj(GameObject.Find ("wizard"));
					Pj pj1 = (Pj) ch1;
					Character ch2 = caughtPj(GameObject.Find ("archer"));
					Pj pj2 = (Pj) ch2;
					if(pj1 != null){
						pj1.getSelectionPj().renderer.enabled = false;
					}
					if(pj2 != null){
						pj2.getSelectionPj().renderer.enabled = false;
					}
					
					
					textureBf = Resources.Load(bf.getEquipment().getPortrait()) as Texture2D;//carga de imagen de habilidad de guerrero
					GUI.Label(new Rect(70,100,50,50), bf.getEquipment().getNumberOfEquipment()+" ");//carga de numero de habilidades restante del guerrero 
					if((GUI.Button(new Rect(50,50,50,50),textureBf) && bf.getEquipment().getNumberOfEquipment() > 0) && !MenuPauseAparition.gamePaused){//comprobacion de pulsar boton de habilidad guerrero
						
						bf.setBarbaryanFuryActivate(true);//pongo a true para que se pare el warrior 
						bf.setStateAnimation(5);//activo la animacion de furia barbara
						//activo la habilidad especial del warrior
						if(!bf.getEquipment().getActivation()){
							
							bf.getEquipment().setActivation(true);
							bf.setStrength(bf.getStrength() + 4);
							bf.getEquipment().effect();
						}
						bf.getEquipment().reduceNumberOfEquipment();//reduccion de numero de habilidades
					}
					
				}
			}
			if(ch.GetType().FullName == "Wizard"){////miramos que el personaje sea el mago y creamos su hud personalizado
				Pj pj= (Pj) ch;
				if(!ch.getDeath()){
					texturePortrait =Resources.Load(pj.getPortrait()) as Texture2D;//cargamos imagen del personaje a color 
				}
				else{	
					texturePortrait =Resources.Load("wizard_death") as Texture2D;//cargamos imagen de escala de grises
				}
				//seleccionamos el mago en caso de pulsar su portrait
				if(GUI.Button(new Rect(Screen.width - 160 + 10,180,130,130),texturePortrait) && !MenuPauseAparition.gamePaused && !ch.getDeath()){
					PjSelection.touchPj	= 2;
				}
				GUI.DrawTexture(new Rect(Screen.width - 160 + 10, 310, 130, 10), textureDeath);//dibujamos imagen de rojo
				float damage =(float)pj.getPg()/(float)pj.getMaxPg()*130;//calculamos imagen de verde
				if(damage <= 0){
					damage = 0;
				}
				GUI.DrawTexture(new Rect(Screen.width - 160 + 10, 310, damage, 10),textureLife );//dibujamos la imagen de verde segun valores de vida
				if(PjSelection.touchPj == 2){//dibujamos aureola del mago y desactivamos la de los otros personajes en caso de pulsar sobre su portrait
					Wizard spellsOfWizard = (Wizard) ch;
					Pj pjW = (Pj) spellsOfWizard;
					if(pjW.getSelectionPj() != null){
						pjW.getSelectionPj().renderer.enabled = true;
					}
					
					
					
					Character ch1 = caughtPj(GameObject.Find ("warrior"));
					Pj pj1 = (Pj) ch1;
					Character ch2 = caughtPj(GameObject.Find ("archer"));
					Pj pj2 = (Pj) ch2;
					if(pj1 != null){
						pj1.getSelectionPj().renderer.enabled = false;
					}
					if(pj2 != null){
						pj2.getSelectionPj().renderer.enabled = false;
					}
					
					
					//miramos hehcizos del mago
					SpellList spellListGeneral = spellsOfWizard.getSpellListOfWizard();
					ArrayList spellist =  spellListGeneral.getSpellList();
					foreach(Spell s in spellist){
						if(s.getId() == 0){//en caso de ser hehizo armor cargamos la imagen del hechizo asociado
							textureSpellArmor = Resources.Load(s.getPortrait()) as Texture2D;
							GUI.Label(new Rect(70,100,50,50), s.getNumberOfSpells()+" ");
						}
						if(s.getId() == 1){//en caso de ser hehizo bullet cargamos la imagen del hechizo asociado
							textureSpellMagicBullet = Resources.Load(s.getPortrait()) as Texture2D;
							GUI.Label(new Rect(70,180,50,50), s.getNumberOfSpells()+" ");
							
						}
						
						
						//accionas el hechizo armor
						if(GUI.Button(new Rect(50,50,50,50),textureSpellArmor) && !MenuPauseAparition.gamePaused){
							spellsOfWizard.setStateAnimation(0);
							
							spellArmorInit = true;
							
							spellsOfWizard.setStop(true);//para al mago si activo hechizo
							PjSelection.pjSelectefForSpell = getPj();//establecemos el personaje receptor del hechizo
							
						
						}
						
						if(spellArmorInit){//si se ha pulsado el boton correspondiente al hehcizo
							
							pjSelectedForSpell = PjSelection.pjSelectefForSpell;
							if(pjSelectedForSpell != null){
								
								Pj pj_ = (Pj) pjSelectedForSpell;
								ArmorSpell armor = new ArmorSpell(pj_);//instanciamos el hechizo  
								
								if( s.getNumberOfSpells() == 0){
									countArmor = true;	
								}
								if(!countArmor){	
									
									spellListGeneral.addSpellInAction(armor);	//añadimos  hechizo a lista de hehcizos en accion
									foreach(ArmorSpell aS in spellListGeneral.getSpellListInAction()){//activamos hechizos de la lista en caso de estar desactivados
										if(!aS.getActivactionSpell()){
											aS.setActivationSpell(true);
											aS.efectSpell(pj_);
										}
										
									}
									spellsOfWizard.setStateAnimation(3);//activamos animacion de hechizo
									Animations.countAnimationSpellArmor = true;
								}
								
								spellArmorInit = false;
								PjSelection.pjSelectefForSpell = null;//deseleccionamos personaje destino para otro hechizo posterior
							}
							if(s.getNumberOfSpells() > 0 && pjSelectedForSpell != null){
								s.reduceNumberOfSpell();
							}
							
							
						}
						
						if(GUI.Button(new Rect(50,130,50,50),textureSpellMagicBullet) && !MenuPauseAparition.gamePaused){//miramos si se pulsa el boton de este hechizo
							spellMagicBulletInit = true;
							
							spellsOfWizard.setStop(true);//para al mago si activo hechizo
							GoControllerPJ.assignPnjForSpell = null;
							spellsOfWizard.setStateAnimation(0);
						}
						
						if(spellMagicBulletInit){//si hechizo ha sido activado
							pnjSelectedForSpell = GoControllerPJ.assignPnjForSpell;//asignamos personaje seleccoinado
							if(pnjSelectedForSpell != null){
								
								foreach(Spell bS in spellListGeneral.getSpellList()){
									//comprobamos que el hechizo escogido es magicBullet Spell
									if((!bS.getActivactionSpell() && bS.getNumberOfSpells() > 0 && bS.getId() == 1 )){
										//activamos el hechizo
										bS.setActivationSpell(true);
										MagicBulletSpell bSx = (MagicBulletSpell) bS;
										bSx.efectSpell(pnjSelectedForSpell,spellsOfWizard);
										spellMagicBulletInit = false;
										//comprobamos que la distanica del hechizo entra en rango para lanzarlo
										if(bS.getNumberOfSpells() > countBullet){
											if(Vector3.Distance(spellsOfWizard.getTarget().transform.position, pnjSelectedForSpell.getTarget().transform.position) < 80){
												spellsOfWizard.setStateAnimation(4);
											}
											Animations.countAnimationBullet = true;
											countBullet++;
										}
										
										
										
										spellsOfWizard.setStop(false);
									}
									
								}
								
								
							}
						}
						
						
					}	
						
						GUI.skin = mySkin;
				}
				
			}
			if(ch.GetType().FullName == "Archer"){//miramos si esta el guerrero en la lista
				Pj pj= (Pj) ch;	
				if(!ch.getDeath()){//si no esta muerto cargamos imagen en color
					texturePortrait = Resources.Load(pj.getPortrait()) as Texture2D;
					
				}else{//en caso de muerte selecionamos imagen de grises
					texturePortrait = Resources.Load("archer_death") as Texture2D;
					
				}//seleccionamos al arquero como personaje controlable
				if(GUI.Button(new Rect(Screen.width - 160 + 10,350,130,130),texturePortrait) && !MenuPauseAparition.gamePaused && !ch.getDeath()){
					PjSelection.touchPj	= 3;
				}
				GUI.DrawTexture(new Rect(Screen.width - 160 + 10, 480, 130, 10),textureDeath );//dibujamos imagen roja
				float damage =(float)pj.getPg()/(float)pj.getMaxPg()*130;//calculamos ancho de imagen verde
				if(damage <= 0){
					damage = 0;
				}
				GUI.DrawTexture(new Rect(Screen.width - 160 + 10, 480, damage, 10), textureLife);//dibujamos imagen verde segn valor de vida
				if(PjSelection.touchPj == 3){//si seleccionamos arquero dibujamos aureola de arquero y borramos la de los otros personajes
					Archer fA = (Archer) ch;
					Pj pjA = (Pj) fA;
					if(pjA.getSelectionPj() != null){
						pjA.getSelectionPj().renderer.enabled = true;
					}
					Character ch1 = caughtPj(GameObject.Find ("warrior"));
					Pj pj1 = (Pj) ch1;
					Character ch2 = caughtPj(GameObject.Find ("wizard"));
					Pj pj2 = (Pj) ch2;
					if(pj1 != null){
						pj1.getSelectionPj().renderer.enabled = false;
					}
					if(pj2 != null){
						pj2.getSelectionPj().renderer.enabled = false;
					}
					
					
					texturefA = Resources.Load(fA.getEquipment().getPortrait()) as Texture2D;//cargamos iamgen de habilidad arquero 
					GUI.Label(new Rect(70,100,50,50), fA.getEquipment().getNumberOfEquipment()+" ");//mostramos numero de habilidades restantes en label
					if((GUI.Button(new Rect(50,50,50,50),texturefA) && fA.getEquipment().getNumberOfEquipment() > 0) && !MenuPauseAparition.gamePaused ){
						
						//activo la habilidad especial del warrior
						fA.setFastArrowActivate(true);//pongo a true para que se pare el archer
						fA.setStateAnimation(6);//activo la animacion de fast arrow
						if(!fA.getEquipment().getActivation()){
							fA.getEquipment().reduceNumberOfEquipment();
							fA.getEquipment().setActivation(true);
							fA.setDexterity(fA.getDexterity() + 2);
							fA.getEquipment().effect();
							
						}
					}
				}
			}
			
			
			//miramos si activamos boton de deescanso
			if(GUI.Button(new Rect(Screen.width - 160 + 50,Screen.height-60,50,50),textureSleep)){
				needSleep = true;
				//calculamos distancias entre personajes seleccionables y miramos si no estan esparcidos por el escenario
				if(GameObject.Find("warrior") != null && GameObject.Find("archer") != null && GameObject.Find("wizard") != null){
					float distanceArcher_Warrior = Vector3.Distance(GameObject.Find("warrior").transform.position,GameObject.Find("archer").transform.position);
					float distanceWizard_Archer = Vector3.Distance(GameObject.Find("wizard").transform.position,GameObject.Find("archer").transform.position);
					float distanceWizard_Warrior = Vector3.Distance(GameObject.Find("wizard").transform.position,GameObject.Find("warrior").transform.position);
					if((distanceArcher_Warrior < 20 && distanceWizard_Archer< 20 && distanceWizard_Warrior <20) && sleep ){
						//los personajes solo descansan cuando no estan luchando y no estan muertos
						sleeping();
	
					}
				}
				//si dos de los personajes estan muertos solo descansara uno de ellos
				else if(GameObject.Find("wizard") == null && GameObject.Find("warrior") == null && GameObject.Find("archer") != null){
					sleeping();
				}
				else if(GameObject.Find("archer") == null && GameObject.Find("warrior") == null && GameObject.Find("wizard") != null){
					sleeping();
				}
				else if(GameObject.Find("archer") == null && GameObject.Find("wizard") == null && GameObject.Find("warrior") != null){
					sleeping();
				}
				//calulo de distancias si esta muerto uno de los persoanjes
				else if(GameObject.Find("wizard") == null && GameObject.Find("warrior") != null && GameObject.Find("archer") != null){
					float distanceArcher_Warrior = Vector3.Distance(GameObject.Find("warrior").transform.position,GameObject.Find("archer").transform.position);
					if((distanceArcher_Warrior < 20)){
						sleeping();
					}
				}
				else if(GameObject.Find("warrior") == null && GameObject.Find("wizard") != null && GameObject.Find("archer") != null){
					float distanceWizard_Archer = Vector3.Distance(GameObject.Find("wizard").transform.position,GameObject.Find("archer").transform.position);
					if((distanceWizard_Archer < 20)){
						sleeping();
					}
				}
				else if(GameObject.Find("archer") == null  && GameObject.Find("warrior") != null && GameObject.Find("wizard") != null){
					float distanceWizard_Warrior = Vector3.Distance(GameObject.Find("wizard").transform.position,GameObject.Find("warrior").transform.position);
					if((distanceWizard_Warrior < 20)){
						sleeping();
					}
				}
				
				
				
				
				
			}
			
			
		}
		
		
		
	}
	/*
	 * metodo que devuelve el personaje seleccionado desde script PJSelection
	 */
	 public Character getPj(){
		return	PjSelection.pjSelectefForSpell; 	
	}
	/*
	 * metodo que devuelve el objecto Character dado el gameobject correspondiente al personaje selecionado
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
	 * metodo que restaura vida y magias de los personajes
	 */
	public void sleeping(){
		//needSleep = true;
		sleepDone  = false;
		foreach(Character pj in listPjs){
			if(pj.GetType().FullName == "Warrior" || pj.GetType().FullName == "Wizard" || pj.GetType().FullName == "Archer"){
				if(!pj.getDeath() && !pj.getGoFight()){
					
					if(pj.GetType().FullName == "Warrior"){//guerrero solo podra descansar si su habilidad ha cesado
						Warrior war = (Warrior) pj;
						
						if(war.getEquipment().getDuration() >= war.getEquipment().getMaxDuration()){
							pj.setPg(pj.getMaxPg());
							war.getEquipment().setNumberOfEquipment(war.getEquipment().getMaxNumber());
							sleepDone = true;
						}else{
							sleepDone = false;	
						}
						
						
					}
					if(pj.GetType().FullName == "Archer"){//arquero solo descansara si su habilidad ha cesado
						Archer ar = (Archer) pj;
						if(ar.getEquipment().getDuration() >= ar.getEquipment().getMaxDuration()){
							ar.getEquipment().setNumberOfEquipment(ar.getEquipment().getMaxNumber());
							pj.setPg(pj.getMaxPg());
							sleepDone = true;
						}else{
							sleepDone = false;	
						}
							
						
					}
					if(pj.GetType().FullName == "Wizard"){//mago descansara si su habilidad ha cesado
						Wizard wi = (Wizard) pj;
						SpellList spellListGeneral = wi.getSpellListOfWizard();
						ArrayList spellist =  spellListGeneral.getSpellList();
						if(spellListGeneral.getSpellListInAction().Count <= 0){
							foreach(Spell s in spellist){
								if(s.getId() == 0){
									s.setNumberOfSpell(3);
								}
								if(s.getId() == 1){
									s.setNumberOfSpell(2);
								}
							}
							pj.setPg(pj.getMaxPg());
							sleepDone = true;
						}
						else{
							sleepDone = false;
						}
						
					}
					
				}
				
			}
		}
		
		timeGame = 0;//aunque luchen si intentan descansar se vertan perjudicados 
			
		
	}
	
}
