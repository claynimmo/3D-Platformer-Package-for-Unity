using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{
    public float aliveTime = 5;

    void Awake(){
        Destroy(gameObject, aliveTime);
    }
}
