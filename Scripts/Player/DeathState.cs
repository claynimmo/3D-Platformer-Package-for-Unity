using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathState : MonoBehaviour
{

    public Movement moveCode;

    public PlayerHealth health;

    public int maxDeathTimer = 8;
    public float respawnDelay = 2;
    private int currentDeathTimer;

    private bool isDead;


    private List<IDead> deathObjs = new();
    public Vector3 spawnPoint;


    void Awake(){
        currentDeathTimer = maxDeathTimer;
        spawnPoint = transform.position;
    }

    public void AddDeathObj(IDead dead){
        deathObjs.Add(dead);
    }

    public void SetSpawnPoint(Vector3 position){
        spawnPoint = position;
    }

    public void KillPlayer(){
        if(isDead){return;}
        transform.position = spawnPoint;

        isDead = true;
        currentDeathTimer = maxDeathTimer;

        ResetScripts();
        StartCoroutine(DeathScreen());
    }

    private void ResetScripts(){
        foreach(IDead dead in deathObjs){
            if(dead == null){continue;}
            dead.PlayerDied();
            deathObjs.Remove(dead);
        }
    }

    IEnumerator DeathScreen(){
        moveCode.StopMovement();

        //add code to display the death message

        //the following commented code relies on the Fade script from the UI pack. This can be removed, or implemented into the future with the required package
        //WaitForSeconds fadeDelay = new WaitForSeconds(1 / deathScreenFade.fadeSpeed);
        //yield return fadeDelay;

        WaitForSeconds oneSecond = new WaitForSeconds(1f);
        yield return oneSecond;

        while (currentDeathTimer > 0){
            currentDeathTimer -= 1;
            //deathTimerText.text = $"{currentDeathTimer}";
            yield return oneSecond;
        }
        //add code to hide the death message

        //yield return fadeDelay;

        yield return new WaitForSeconds(respawnDelay);
        isDead = false;
        moveCode.StartMovement();
        health.PlayerRespawned();
    }

   
}
