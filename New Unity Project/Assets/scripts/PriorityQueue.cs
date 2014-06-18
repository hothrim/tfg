using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace QueueSpace
{
	public class PriorityQueue{
		private ArrayList listCharacters;
		private IComparer myComparer;
		
		public PriorityQueue(){
			listCharacters = new ArrayList();
			myComparer = new ComparerCelda();
			
			
		}
		/*
		 * metodo que devuelve el primer elemento Character de la lista
		 */ 
		public Character firstQueue(){
			Celda celda =  (Celda)listCharacters[0];
			return celda.getCharacter();
		}
		/*
		 * metodo que devuelve el primer elemento Celda de la lista
		 */ 
		public Celda getFirstCelda(){
			Celda celda =  (Celda)listCharacters[0];
			return celda;	
		}
		/*
		 * metodo que añade una Celda y la ordena por la prioridad
		 */ 
		public void Enqueue(Celda celda){
			listCharacters.Add(celda);
			listCharacters.Sort(myComparer);

		}
		/*
		 * metodo que reduce contador de cada elemento de la lista 
		 */
		public void reduceCount(){
			for(int i = 0; i < listCharacters.Count;i++){
				Celda celda = (Celda)listCharacters[i];
				int count = celda.getCount();
				if(celda.getCount() > 0){
					count = celda.getCount() - 1;
					celda.setCount(count);
					
				}
				
			}
			
		}
		/*
		 * metodo que elimina celda dado el Character
		 */ 
		public void deleteQueue(Character ch){
			for(int i= 0;i < listCharacters.Count;i++){
				Celda celda = (Celda)listCharacters[i];
				if(celda.getCharacter() == ch){
					listCharacters.Remove(celda);	
				}
			}
		}
		/*
		 * metodo que devulve longitud de la lista
		 */ 
		public int lenght(){
			return listCharacters.Count;	
		}
		/*
		 * metodo que pone primer elemento de la lista al final y pone count al vaoir de su prioridad 
		 */
		public void Dequeue(){
			
			Celda tmp = (Celda)listCharacters[0];
			
			for(int i =1;i < listCharacters.Count;i++){
				listCharacters[i-1] = listCharacters[i];
			}
			listCharacters[listCharacters.Count-1 ] = tmp;
			
			if(tmp.getCount() <= 0){
					
				tmp.setCount(tmp.getPrioridad());	
				
					
			}
			
		}
		/*
		 * metodo que devuelve variable string como  mensaje inicial de prioridades
		 */
		public string toString(){
			int i = 0;
			string sms = "";
			foreach(Celda c  in listCharacters){
				i++;
				sms += ("i:"+ i + " Prioridad:" + c.getPrioridad() + " Personaje:" + c.getCharacter().getNombre()+ " Count:" + c.getCount());
			}
			return sms;
		}
		public void toStringLog(){
			int i = 0;
			foreach(Celda c  in listCharacters){
				i++;
				Debug.Log("i:"+ i + " Prioridad:" + c.getPrioridad() + " Personaje:" + c.getCharacter().getNombre()+ " Count:" + c.getCount());
			}
		}
		/*
		 * metodo que devuelve la celda en el que el contador ha llegado a 0
		 */ 
		public Celda celdaAt0(){
			foreach(Celda c in listCharacters){
				if(c.getCount() <= 0){
					return c;	
				}
			}
			return null;
		}
		/*
		 * Metodo que reorganiza las posiciones de las celdas en la lista
		 */
		public void strain(Celda c){
			toString();
			Celda tmp = c;
			int pos = -1;
			for(int i =0;i < listCharacters.Count;i++){
				Celda cA = (Celda)listCharacters[i];
				if(cA== c){
					
					pos = i;
				}
			}
			
			
			ArrayList listCharactersTmp = new ArrayList(listCharacters);
			for(int i =1;i < pos+1 ;i++){	
				listCharactersTmp[i] = listCharacters[i-1];
				
			}
			listCharactersTmp[0] = tmp;
			listCharacters = listCharactersTmp;
			toString();
		}
		
	}
}
  