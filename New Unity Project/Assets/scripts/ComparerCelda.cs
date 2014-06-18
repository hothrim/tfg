using UnityEngine;
using System.Collections;

public class ComparerCelda : IComparer {
	/*sobreescritura del metodo compare de la interficie IComparer para la ordenacion de la cola de prioridad*/
	int IComparer.Compare(object a, object b){
      Celda c1=(Celda)a;
      Celda c2=(Celda)b;
      if (c1.getPrioridad() < c2.getPrioridad())
         return -1;
      if (c1.getPrioridad() > c2.getPrioridad())
         return 1;
      else
         return 0;
   }
}
