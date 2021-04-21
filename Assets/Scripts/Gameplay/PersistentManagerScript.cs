using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ejemplo de uso: PersistentManagerScript.Variables.LaVariableQueQuierasGuardar = .... */

//Estructura Singleton
public class PersistentManagerScript : MonoBehaviour
{
    //Variable vinculada a la clase, que guarda la única instancia de la misma.
    public static bool created;
    public bool boosIsDeath;
    public bool startTutorialHasFinished;
    public bool fatigaTutorialHasFinished;
    public bool talkedToTravis;


    private void Awake()
    {
        //Representa la primera instancia de la clase
        if (!created)
        {
            //Se guarda dicha instancia 
            created = true;
            //Se evita la destrucción de dicha clase al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Evita la creación de otra instancia al volver a la clase origen
            Destroy(gameObject);
        }
    }

}

 public class Variables {
    
    #region Persistent variables
    public bool bossIsDeath;
    #endregion

}
