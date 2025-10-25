using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour, IButton
{

    public GameObject barricadeObj;
    public bool startOff = false;

    public bool alternating = false;
    public float alternatingTimer = 3f;

    private bool barricadeOn = true;

    Coroutine alternatingCo;

    public bool activateOnEnable;


    // Start is called before the first frame update
    void Start()
    {
        if(startOff){
            ToggleBarricade();
        }
        if(alternating){
            alternatingCo = StartCoroutine(AlternatingFunction());
        }
    }

    void OnEnable(){
        if(activateOnEnable){
            barricadeOn = true;
            barricadeObj.SetActive(barricadeOn);
        }
    }

    void ToggleBarricade(){
        barricadeOn = !barricadeOn;
        barricadeObj.SetActive(barricadeOn);
    }
    void SetBarricade(bool on){
        barricadeOn = on;
        barricadeObj.SetActive(on);
    }

    //primary button will turn the barricade on/off
    public void PrimaryButtonPressed(){
        ToggleBarricade();
    }

    //secondary button will toggle the alternating
    public void SecondaryButtonPressed(){
        SetBarricade(false);
        if(alternating){
            alternating = false;
        }
        else{
            alternating = true;
            if(alternatingCo != null){
                StopCoroutine(alternatingCo);
            }
            alternatingCo = StartCoroutine(AlternatingFunction());
        }
    }

    IEnumerator AlternatingFunction(){
        WaitForSeconds delay = new WaitForSeconds(alternatingTimer);
        while(alternating){
            ToggleBarricade();
            yield return delay;
        }
    }


}
