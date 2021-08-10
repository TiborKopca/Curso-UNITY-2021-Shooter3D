using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro; //if we were using TextMeshPro 

[RequireComponent(typeof(Image))]       //this we need to access the images
public class LifeBar : MonoBehaviour
{
    [Tooltip("Vida objetivo que reflejara la barra")]
    public Life targetLife; //this is variable that we would be showing

    private Image _image; //for not to expose it publically


    //we put into Cache the Image Component of the target=players or bases lifebar
    private void Awake() {
        _image = GetComponent<Image>(); 

    }

    //we can re-render the images many times per second
    //actual life = current life / total maximum life (which is 100) 
    private void Update() {
        //fillAmount == is component parameter of the image in Inspector
        //Amount is in range 0-1, so we need to divide by total/maximum life

        //maximum life should NOT be/contain magic number
        //_image.fillAmount = targetLife.Amount / 100.0f 
        _image.fillAmount = targetLife.Amount / targetLife.maximumLife;
    }
}
