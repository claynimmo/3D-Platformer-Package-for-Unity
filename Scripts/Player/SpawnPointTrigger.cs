using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointTrigger : MonoBehaviour
{
    public Vector3 localOffset;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            DeathState death = other.GetComponent<DeathState>();
            if(death == null){return;}
            
            Vector3 spawnPoint = transform.TransformPoint(localOffset);

            death.SetSpawnPoint(spawnPoint);
        }
    }
}
