using UnityEngine;
using System.Collections;

public class Torchelight : MonoBehaviour {
	
	public GameObject TorchLight;
	public GameObject Etincelles;
	public float MaxLightIntensity;
	public float IntensityLight;
	

	void Start () {
		//asignamos la intensidad de luz a la variable intensidad del punto de luz
		TorchLight.light.intensity=IntensityLight;
		//asignamos esa misma intensidad de luz a la variable emissionRate del sistema de particulas etincelles
		Etincelles.GetComponent<ParticleSystem>().emissionRate=IntensityLight*7f;

		
		
	}
	

	void Update () {
		//asignamos los valores de intensidad de luz 
		if (IntensityLight<0){
			IntensityLight=0;
			
		}
		if (IntensityLight>MaxLightIntensity){
			IntensityLight=MaxLightIntensity;		
		
		}
		//calculamos la intensidad de luz basandonos en la intensidad de la luz
		TorchLight.light.intensity=IntensityLight/2f+Mathf.Lerp(IntensityLight-0.1f,IntensityLight+0.1f,Mathf.Cos(Time.time*30));
		//calculamos el color de la luz basandonos en la intensidad de la luz
		TorchLight.light.color=new Color(Mathf.Min(IntensityLight/1.5f,1f),Mathf.Min(IntensityLight/2f,1f),0f);
		//calculamos la emision de las particualas basandonos en la intensidad de la luz
		Etincelles.GetComponent<ParticleSystem>().emissionRate=IntensityLight*7f;
		
	}
}
