using UnityEngine;
using System.Collections;


public class CamFollowPJ : MonoBehaviour {
	public static Transform target;
	public float distance;
	public float height;
	private float heightDamping;
	private float rotationDamping;
	private GameObject pj;
	private float widthScreen;
	private Vector3 posMouse;
	private float wantedRotationAngle;
	private float wantedHeight;
	private float currentRotationAngle;
	private float currentHeight;
	private Quaternion currentRotation;
	private Vector3 tmp;
	private Quaternion rotation;

	// Use this for initialization
	void Start () {
		
		distance = 173f;
		height = 208f;
		heightDamping = 105f;
		rotationDamping = 165f;
		pj = GameObject.Find("warrior");//selecionamos este  GameObject como oersonaje inicial en el que centrara la camara
		target = pj.transform;//asignacion de las coordenadas del objeto
		widthScreen = Screen.width;//asignacion del ancho de la pantalla
		
		 
	}
	
	/*
	 * establecemos una camara fija que siga al personaje seleccionado a una distancia, angulo y altura determinada
	 */
	void Update(){
		
		//miramos que personaje seleccionamos y lo asignamos como GameObject en el que se centrara la camara
		if(PjSelection.touchPj == 1){
			pj = GameObject.Find("warrior");
		}else if(PjSelection.touchPj == 2){
			pj = GameObject.Find("wizard");
		}else if(PjSelection.touchPj == 3){
			pj = GameObject.Find("archer");
		}
		if(pj != null){
			target = pj.transform;//asignamos corrdeanda del GameObject seleccionado
			posMouse = Input.mousePosition;
			if(posMouse.x > 0 && posMouse.x < widthScreen){
				if(!target){
					return;
				}
				
				wantedRotationAngle = 0;
				
			    wantedHeight = target.position.y + height;
				currentRotationAngle = transform.eulerAngles.y;
				currentHeight = transform.position.y;
				
				currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping);
				currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
				currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);//calulo de la rotacion de la caamara
				
				tmp= new Vector3(target.position.x,target.position.y,target.position.z);
				tmp -= currentRotation * Vector3.one* distance;//calculo de la orientacion 	de la camara respecto al personaje
				tmp.y = currentHeight;//asignamos la altura de la camara
				transform.position = tmp;//hacemos que la camara siga al personaje
				
				transform.LookAt(target);//miramos hacia el objetivo
			}
		}
	}
}



			
			
		
		