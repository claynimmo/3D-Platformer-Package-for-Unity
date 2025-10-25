using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] positions; 
    public float speed = 5f; 
    public float pauseDuration = 0.5f;

    private int targetIndex = 0;

    private Vector3 direction;

    public bool paused;

    private void Start(){
        transform.SetParent(null);
        MoveToNextPosition();
    }

    private void FixedUpdate(){
        if(paused){return;}

        if(Vector3.Distance(transform.position, positions[targetIndex].position) < 0.1f){
            MoveToNextPosition();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position,positions[targetIndex].position, speed * Time.fixedDeltaTime);
    }

    private void MoveToNextPosition(){
        targetIndex = (targetIndex + 1) % positions.Length;
        direction = (positions[targetIndex].position - transform.position).normalized;

        if(pauseDuration > 0){
            paused = true;
            Invoke("ResetPaused",pauseDuration);
        }
    }

    private void ResetPaused(){
        paused = false;
    }

    public void ToggleActive(bool active){
        this.enabled = active;
    }

}
