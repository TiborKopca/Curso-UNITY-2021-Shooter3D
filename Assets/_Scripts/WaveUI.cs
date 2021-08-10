using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start() {
        _text = GetComponent<TextMeshProUGUI>();
        
        WaveManager.SharedInstance.onWaveChanged.AddListener(RefreshText);
        RefreshText();
    }

    private void RefreshText() {
        //we need to compute how many waves were, max waves - waves count
        _text.text = 
        "WAVE: "+ 
        (WaveManager.SharedInstance.MaxWaves-
        WaveManager.SharedInstance.WavesCount)+ 
        " of " + 
        WaveManager.SharedInstance.MaxWaves;
    }
}
