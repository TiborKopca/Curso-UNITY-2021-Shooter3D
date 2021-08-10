using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.Events; 

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager SharedInstance;
    //WITHIN THIS LIST/ARRAY WHERE WE CAN ADD OR DESTROY OBJECT WE TRACK ENEMIES 
    //LIST IS AN CLASS/OBJECT
    
    public UnityEvent onEnemyChanged;  //Unity Event when something changes this will hold that Event

    [SerializeField]
    private List<Enemy> enemies;
    
    //ENEMY COUNT WILL COUNT THE ENEMY INSTANCES
    public int EnemyCount{
        get => enemies.Count;
    }


    //this nees to happen before Enemy.cs with Start() gets executed.
    private void Awake(){
        if(SharedInstance == null){
            SharedInstance = this;
            enemies = new List<Enemy>();
        }else{
            Destroy(this);
        }
    }

    //This is because the list of enemies is private!
    public void AddEnemy(Enemy enemy){
        //print("Enemey Added.");
        enemies.Add(enemy);
        //Recall the Event --> this will inform GameModeWave.cs
        onEnemyChanged.Invoke(); //this will invoke Global onEnemyChanged Event
    }

    public void RemoveEnemy(Enemy enemy){
        enemies.Remove(enemy);
        onEnemyChanged.Invoke();
    }
}
