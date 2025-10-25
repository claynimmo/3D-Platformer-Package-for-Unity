using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainfullTrigger : MonoBehaviour
{
    public float damage;

    public bool ignorePlayer = true;

    void OnTriggerEnter(Collider other){
        if(other.GetComponent<PlayerHealth>()&&!ignorePlayer){
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        else if(other.GetComponent<IHealth>() != null){
            other.GetComponent<IHealth>().TakeDamage(damage);
        }
    }
}
