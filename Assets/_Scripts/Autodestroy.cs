using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestroy : MonoBehaviour
{
    [Tooltip("Tiempo despues del cual se destruye el objeto in segundos.")]
    public float destructionDelay;


    //Because Start is called ONLY ONCE before the first frame update, we need to modify this code so it will update the pooling when the object is enabled
    void OnEnable()
    {
        //Destroy(gameObject,destructionDelay); //OBSOLETE
        //we shouldnt use .this, this == component Autodestroy
        Invoke(methodName:"HideObject",destructionDelay);
    }

    //This will set the object to be hidden by SetActive(false)
    private void HideObject(){
        gameObject.SetActive(false);
    }

}
