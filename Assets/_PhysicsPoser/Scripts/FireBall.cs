using System.Collections;
using System.Collections.Generic;
using _PhysicsPoser.Scripts;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public Transform playerTransform;
    public float minFiringForce = .75f;
    public float maxFiringForce = .9f;
    private int _count;
    private float _previousLeftTriggerValue = 0f;

    //private float _previousRightTriggerValue = 0f;

    public XRController leftController;
    //public XRController rightController;
    //private int _triggerCount = 0;

    private void Awake()
    {
        leftController = GetComponent<XRController>();
       
    }

    void Start()
    {
        // Start firing balls every 3 seconds
        //InvokeRepeating("Fire", 0f, 3f);
    }

    void Update()
    {
        
        

        float leftTriggerValue;
        leftController.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out leftTriggerValue);
        

        
        if (leftTriggerValue != _previousLeftTriggerValue)
        {
            if (leftTriggerValue == 1f || leftTriggerValue == 0f)
            {
                _previousLeftTriggerValue = leftTriggerValue;
                if (leftTriggerValue == 0f)
                {
        
                    Fire();
                    
        
                }
            }
        }
        


        //Debug.Log("right trigger: " + rightTriggerValue + " left trigger: " + leftTriggerValue);

    }


    private void Fire()
    {
        // Move spawn point randomly along x-axis
        float randomLeftX = Random.Range(-8f, -6f);
        float randomRightX = Random.Range(6f, 8f);
        if (_count % 2 == 0)
        {
            spawnPoint.position = new Vector3(randomLeftX, spawnPoint.position.y, spawnPoint.position.z);
        }
        else
        {
            spawnPoint.position = new Vector3(randomRightX, spawnPoint.position.y, spawnPoint.position.z);
        }

        

        // Instantiate a new ball at the spawn point
        GameObject newBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = newBall.GetComponent<Rigidbody>();
        GameManager.Instance.DecrementBallsRemaining();
        
        // Disable gravity initially
        rb.useGravity = false;

        // Start the coroutine to delay the firing
        StartCoroutine(DelayedFire(rb, newBall));
    }

    private IEnumerator DelayedFire(Rigidbody rb, GameObject newBall)
    {
        // Wait for 1 second
        yield return new WaitForSeconds(.5f);
        
        // Enable gravity before firing
        rb.useGravity = true;

        // Calculate firing direction towards the player
        Vector3 toPlayer = playerTransform.position - spawnPoint.position;
        Vector3 horizontalDirection = new Vector3(toPlayer.x, 0, toPlayer.z).normalized;

        // Add an upward component to create an arc
        Vector3 verticalDirection = Vector3.up;  // Straight up
        float verticalFactor = .75f;  // Adjust this to control the height of the arc

        // Combine the horizontal and vertical components
        Vector3 firingDirection = (horizontalDirection + verticalFactor * verticalDirection).normalized;

        // Randomize firing force within a range
        float randomFiringForce = Random.Range(minFiringForce, maxFiringForce);

        // Apply force to the ball
        rb.AddForce(firingDirection * randomFiringForce, ForceMode.Impulse);
        _count++;

        // Destroy the ball after 5 seconds
        Destroy(newBall, 5f);
    }

}