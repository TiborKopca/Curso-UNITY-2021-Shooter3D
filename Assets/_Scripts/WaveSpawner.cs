using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [Tooltip("Prefab de Enemigo a generar.")]
    public GameObject prefab;

    [Tooltip("Tiempo en el que se inicia y finaliza la oleada.")]
    public float startTime, endTime; //Tienpo entre enemigos

    [Tooltip("Tiempo entre la generacion de enemigos.")]
    public float spawnRate;//Candidad de enemigos <-> tiempo inicio / tiempo fin


    void Start()
    {
        //THIS WILL INSTANCIATE NEW WAVE
        //we add the wave to the list(to track the number of waves)
        WaveManager.SharedInstance.AddWave(this); 

        //InvokeRepeating(function in string format, time, repeatRate )
        InvokeRepeating(methodName:"SpawnEnemy", startTime, spawnRate);
        //we need to cancel the spawn. This will be called only once!
        Invoke(methodName:"EndWave", endTime); //CancelInvoke == is from UNITY, only cancel, nothing more
    }


    //Custom Function we will be calling in Start()
    void SpawnEnemy(){
        //Spawn Object, X, Y position, this has fixed position
        Instantiate(prefab, transform.position, transform.rotation); //OBSOLETE

        //Instantiate with Randomization of the spawn position Y
        //Quaternion is in Radiants, Euler is in Grades, it will generate the matrix of numbers
        //Range can be in whole numbers 45degrees or in float numbers 45.0f degrees
        // Quaternion q = Quaternion.Euler(x:0, y:transform.rotation.eulerAngles.y + Random.Range(-45.0f,45.0f), z:0
        // );
        // //We replace rotation for the spawned object with Quaternion
        // Instantiate(prefab, transform.position, q);
    }
    
    //This will End the spawn of the enemies according our set time, and it will do it with CancelInvoke()
    //We also need to call Wavemanager to give it know that this wave is over
    void EndWave(){
        WaveManager.SharedInstance.RemoveWave(this);
        CancelInvoke();
    }
}