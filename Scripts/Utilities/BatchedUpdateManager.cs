using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchedUpdateManager : MonoBehaviour
{
    public BatchedUpdate[] batch;
    
    void Update(){
        for(int i = 0; i < batch.Length; i++){
            batch[i].UpdateCall();
        }
    }

    void FixedUpdate(){
        for(int i = 0; i < batch.Length; i ++){
            batch[i].FixedUpdateCall();
        }
    }
}
