using UnityEngine;
using System.Collections;

public class Animations : MonoBehaviour {
	public static bool countAnimationSpellArmor;
	public static bool countAnimationBullet;
	public static bool countAnimationFury;
	public static bool countAnimationFast;
	// Use this for initialization
	void Start () {
		countAnimationSpellArmor = true;
		countAnimationBullet = true;
		countAnimationFury = true;
		countAnimationFast = true;
	}
	
	// Update is called once per frame
	void Update () {
		
		foreach(Character ch in SearchCharacters.characterList){
		
			if(ch.getTarget().name == "warrior" || ch.getTarget().name == "wizard" || ch.getTarget().name == "archer"){//comprueba si son personajes principales
				Pj pj = (Pj) ch;
				if(pj.getStateAnimation() == 0){//comprueba estado personajes parados
						Animation animation = pj.getTarget().animation;// asignamos animacion del GameObject correspondiente
						animation.Stop();//la paramos
						
						animation.Play("standBy");//reproducimos animacion
							
				}
				if(pj.getStateAnimation() == 1){//comprueba estado personajes caminando
						Animation animation = pj.getTarget().animation;
						animation["walk"].speed = 5;//cambiamos velocidad de animacion
						
						animation.Play("walk");
				}
				if(pj.getStateAnimation() == 2){//comprueba estado personajes atacando
						Animation animation = pj.getTarget().animation;
						animation["atack"].speed = 3;
						animation.Play("atack");
					
						
				}
				if(pj.getStateAnimation() == 3 && ch.getTarget().name == "wizard" && countAnimationSpellArmor){//comprueba estado de animacion de hechizo ArmorSpell de wizard
					Animation animation = pj.getTarget().animation;
					
					animation["armorSpell"].speed = 3;
					
					
					animation.Play("armorSpell");
					
					
					countAnimationSpellArmor = false;
					
					
				}
				if(pj.getStateAnimation() == 4 && ch.getTarget().name == "wizard" && countAnimationBullet){//comprueba estado de animacion de hechizo de MagicBullet de wizard
					Animation animation = pj.getTarget().animation;
					
					animation["bulletSpell"].speed = 3;
				
					animation.Play("bulletSpell");
					
					countAnimationBullet = false;
					
					
				}
				if(pj.getStateAnimation() == 5 && ch.getTarget().name == "warrior" && countAnimationFury){//comprueba estado de animacion de furia barbara de guerrero
					Animation animation = pj.getTarget().animation;
					animation["barbarianFury"].speed = 3;
				
					animation.Play("barbarianFury");
					
					countAnimationFury = false;
					
					
				}
				if(pj.getStateAnimation() == 6 && ch.getTarget().name == "archer" && countAnimationFast){//comprueba estado de animacion de habilidad fastArrow de arquero
					Animation animation = pj.getTarget().animation;
					animation["fastArcher"].speed = 3;
				
					animation.Play("fastArcher");
					countAnimationFast = false;
					
					
				}
			}else{//caso de personajes enemigos
				Pnj pnj = (Pnj) ch;
				if(pnj.getStateAnimation() == 0){//comprueba estado de animacion parado
					Animation animation = pnj.getTarget().animation;
					animation["standBy"].speed = 3;
					animation.Play("standBy");
					
					
				}
				if(pnj.getStateAnimation() == 1){//comprueba estado de animacion corriendo
					Animation animation = pnj.getTarget().animation;
					animation["run"].speed = 3;
					animation.Play("run");
				
					
				}
				if(pnj.getStateAnimation() == 2){//comprueba estado de animacion de ataque enemigo de corto alcance
					Animation animation = pnj.getTarget().animation;
					animation["atack"].speed = 3;
					animation.Play("atack");
				
					
				}
				if(pnj.getStateAnimation() == 3){//comprueba estado de animacion ataque de enemigo de largo alcance
					Animation animation = pnj.getTarget().animation;
					animation["atackRange"].speed = 3;
					animation.Play("atackRange");
				
					
				}
				if(pnj.getStateAnimation() == 4){//comprueba estado de animacion caminando de enemigo
					Animation animation = pnj.getTarget().animation;
					animation["walk"].speed = 3;
					animation.Play("walk");
					
					
				}
			}
		}
		foreach(CharacterNeutral ch in SearchCharacters.neutralList){//mira lista de personajes neutrales
			if(ch.getAnimation() == 0){//comprueba estado de animacion parado
				Animation animation = ch.getTarget().animation;
				animation["standBy"].speed = 3;
				animation.Play("standBy");
			}
			if(ch.getAnimation() == 1){//comprueba estado de animacion caminando
				Animation animation = ch.getTarget().animation;
				animation["walk"].speed = 3;
				animation.Play("walk");
			}
			if(ch.getAnimation() == 2){//comprueba estado de animacion corriendo
				Animation animation = ch.getTarget().animation;
				animation["run"].speed = 3;
				animation.Play("run");
			}
		}
	}
}
