using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

public class PlayerBulletsUI : MonoBehaviour
{
    //With TextMeshPro UGUI !!, private!
    private TextMeshProUGUI _text;

    [Tooltip("Locate the origin of the bullets-who is shooting.")]
    public PlayerShooting targetShooting;

    //Before start we obtain TextMeshPro component
    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update(){
        _text.text = "BULLETS REMAING: "+targetShooting.bulletsAmount; //concatenating consumes memory, not recommended
    }
}
