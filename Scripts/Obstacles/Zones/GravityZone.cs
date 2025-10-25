using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : BatchedUpdate, IButton
{

    public float force;

    private bool primary=true;

    public Material primaryMaterial;
    public Material secondaryMaterial;
    public MeshRenderer[] rends;


    private List<Rigidbody> bodies = new();

    //since there may be several of these in a scene, the fixed updated call is batched. Add this object 
    public override void FixedUpdateCall(){
        foreach(Rigidbody body in bodies){
            if(primary){
                body.AddForce(body.mass * force * transform.up, ForceMode.Acceleration);
            }
            else{
                body.AddForce(body.mass * force * -transform.up, ForceMode.Acceleration);
            }
        }
    }


    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){ //for this to apply to all rigidbodies, remove this. It is just performance intensive to check get component on every collision
            Rigidbody body = other.GetComponent<Rigidbody>();
            if(body == null){return;}
        
            bodies.Add(body);
        }
    }
    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){ //for this to apply to all rigidbodies, remove this. It is just performance intensive to check get component on every collision
            Rigidbody body = other.GetComponent<Rigidbody>();
            if(body == null){return;}
            if(bodies.Contains(body)){
                bodies.Remove(body);
            }
        }
    }

    public void PrimaryButtonPressed(){
        primary = !primary;
        foreach(MeshRenderer r in rends){
            if(primary){
                r.material = primaryMaterial;
            }
            else{
                r.material = secondaryMaterial;
            }
        }
    }

    public void SecondaryButtonPressed(){
        return;
    }
}
