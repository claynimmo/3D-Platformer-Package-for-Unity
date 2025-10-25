using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatformZone : MonoBehaviour
{
    public GameObject ghostPlatform;
    //offset so that it does not spawn under the player, or clipping inside of them. this should include the coordinates needed to counter the height of the object
    public float platformVerticalSpawnOffset = -0.5f;
    
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")) {
            Vector3 pos = other.transform.position;
            pos = new Vector3(pos.x, transform.position.y + platformVerticalSpawnOffset, pos.z);
            ghostPlatform.SetActive(true);
            ghostPlatform.transform.position = pos;
        }
            
    }

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            ghostPlatform.SetActive(false);
        }
    }
}
