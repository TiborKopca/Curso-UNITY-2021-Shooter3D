using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public static WaveManager SharedInstance;
    
    [SerializeField]
    private List<WaveSpawner> waves;    //this holds the list of the wave

    public UnityEvent onWaveChanged;    //This will launch the event listener

    public int WavesCount{              //This will return count of Waves, we use this in GameModeWaves to resolve win/lose
        get => waves.Count;
    }
    [SerializeField]
    private int maxWaves;               //this will hold information about how many waves we had to this point

    public int MaxWaves{
        get => maxWaves;
    }


    //Before Start we create list of waves
    void Awake()
    {
        if(SharedInstance == null){
            SharedInstance = this;
            waves = new List<WaveSpawner>();
        }else{
            Destroy(this);
        }
    }


    //Each time there is called this function from WaveSpawner, it will add 1 to the list of waves
    public void AddWave(WaveSpawner wave){
        maxWaves++;                     //we add 1 to the max number of waves
        waves.Add(wave);
        onWaveChanged.Invoke();         //We invoke Unity Event to trigger EventListener
    }

    //Called from WaveSpawner
    public void RemoveWave(WaveSpawner wave){
        waves.Remove(wave);
        onWaveChanged.Invoke();
    }
}

