using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlatform : MonoBehaviour
{

    public float disableTime = 10;
    public float removeTimer = 3;

    private bool activated = false;

    public Material stoodOnMaterial;
    private Material defaultMaterial;

    //public ParticleSystem particle;
    void Start(){
        defaultMaterial = GetComponent<Renderer>().material;
    }
    void OnTriggerEnter(Collider other){
        if(activated){return;}
        if(other.CompareTag("Player")){
            StoodOn();
        }
    }

    private void StoodOn(){
       // particle.gameObject.transform.position = transform.position;
        //particle.Play();
        activated = true;
        GetComponent<Renderer>().material = stoodOnMaterial;
        Invoke("DisablePlatform",removeTimer);
    }

    private void DisablePlatform(){
        Collider[] colliders = GetComponents<Collider>();
        foreach(Collider c in colliders){
            c.enabled = false;
        }
        GetComponent<Renderer>().enabled = false;
        Invoke("EnablePlatform",disableTime);
    }

    private void EnablePlatform(){
        Collider[] colliders = GetComponents<Collider>();
        foreach(Collider c in colliders){
            c.enabled = true;
        }
        GetComponent<Renderer>().enabled = true;
        GetComponent<Renderer>().material = defaultMaterial;
        activated = false;
    }

    void OnEnable(){
        activated = false;
    }
}
