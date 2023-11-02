using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.XR.Interaction.Toolkit;

namespace _PhysicsPoser.Scripts
{
    
    public class BallPhysics : MonoBehaviour
    {
        private Vector3 lastPosition;
        private Rigidbody rb;

        
        
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            lastPosition = transform.position;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.gameObject.CompareTag("Left Paddle"))
            {

                GameManager.Instance.HandleBallPaddleCollision(other, rb);
                return;
            }

            else if (other.collider.gameObject.CompareTag("Right Paddle"))
            {
                GameManager.Instance.HandleBallPaddleCollision(other, rb);
                return;
            }
            else if (other.collider.gameObject.CompareTag("Target1") ||
                     other.collider.gameObject.CompareTag("Target3") ||
                     other.collider.gameObject.CompareTag("Target5") ||
                     other.collider.gameObject.CompareTag("Target10"))
            {
                GameManager.Instance.HandleBallTargetCollision(other, rb);
                
                Destroy(gameObject, 1f);
                return;
            }
        }


    }
}
