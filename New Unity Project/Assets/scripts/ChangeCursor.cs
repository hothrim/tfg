using UnityEngine;
using System.Collections;

public class ChangeCursor : MonoBehaviour {
	private Texture2D cursorTexture;
	private CursorMode cursorMode;
	private Vector2 hotSpot;
 	//private Texture2D cursorTextureOther;

 
	void Start(){

		
		cursorTexture =Resources.Load("sword3") as Texture2D;//cargamos imagen
		cursorMode = CursorMode.Auto;
		hotSpot = Vector2.zero;
		
	}

	

	void OnMouseEnter(){
		//cambiamos el cursor al entrar en el collider de SeguimientoPnj a la imagen cargada
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		
		
	 }
	void OnMouseExit(){
		//cambiamos a cursor normal
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
		
	 }
}
