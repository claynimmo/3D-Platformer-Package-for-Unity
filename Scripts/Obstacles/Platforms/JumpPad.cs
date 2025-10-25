using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPower = 500;

    public float cooldown = 0.3f;

    private bool used;

    public GameObject effectPrefab;
    public ParticleSystem effect;
    public bool hasSpawn = true;

    public GameObject DestroyOnBounce;

    void OnTriggerEnter(Collider other){
        if(used){return;}

        if (other.GetComponent<Rigidbody>()){
            used = true;
            Rigidbody rbody = other.GetComponent<Rigidbody>();
            rbody.velocity = new Vector3(rbody.velocity.x,0,rbody.velocity.z);
            rbody.AddForce(0,rbody.mass*jumpPower,0);

            if(hasSpawn){
                if(effectPrefab != null){
                    GameObject eff = Instantiate(effectPrefab);
                    eff.transform.parent = other.transform;
                    eff.transform.localPosition = Vector3.zero;
                }
            }
            else{
                if(effect!=null){
                    effect.Stop();
                    effect.Play();
                }
            }
            if(DestroyOnBounce!=null){
                Destroy(DestroyOnBounce);
            }
            Invoke("ResetUsed",cooldown);
        }
    }
    
    void ResetUsed(){
        used = false;
    }
}
