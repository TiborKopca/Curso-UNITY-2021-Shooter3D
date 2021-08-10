using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesUI : MonoBehaviour
{
    private TextMeshProUGUI _text;


    private void Start() {
        //obtain TextMeshPro text
        _text = GetComponent<TextMeshProUGUI>();

        //On change we invoke function to rewrite UI text
        EnemyManager.SharedInstance.onEnemyChanged.AddListener(RefreshText);
        
        //to see the first text on UI at the start of the game, before any Event, invoke once this function.
        RefreshText();
    }

    //This isnt update, we can use the Listener listening to Event onEnemyChanged

    private void RefreshText() {
        _text.text = "REMAING ENEMIES: "+ EnemyManager.SharedInstance.EnemyCount; //concatenating consumes memory, not recommended
    }
}