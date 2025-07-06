using UnityEngine;

public class CarCollisionHandler : MonoBehaviour
{
    private float originalSpeed;
    private CarMover carMover;
    private bool isStopped = false;
 

    
  
    public GameObject obj1; 
    public GameObject obj2; 
   

    void Start()
    {
        carMover = GetComponent<CarMover>();
        if (carMover != null)
        {
            originalSpeed = carMover.speed;
        }

        
   


    }

    void Update()
    {
        if (isStopped)
        {
            if (obj1 != null && obj2 != null)
            {
                float distanceSqr = (obj1.transform.position - obj2.transform.position).sqrMagnitude;
                if (distanceSqr > 100)
                {
                    RestoreSpeed();
                }
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Debug.Log("Collision detected! Stopping...");
            if (carMover != null)
            {
                carMover.speed = 0f; // Stop AI car
                isStopped = true;

            }
        }
    }

    void RestoreSpeed()
    {
        if (carMover != null)
        {
            Debug.Log("Front car moved, resuming...");
            carMover.speed = originalSpeed;
            isStopped = false;
           
        }
    }
}
