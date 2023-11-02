using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

namespace _PhysicsPoser.Scripts
{
    public class GameManager : MonoBehaviour
    {
    
        public PhysicsPoser leftPhysicsPoser;
        public PhysicsPoser rightPhysicsPoser;
        private Vector3 leftPaddleVelocity;
        private Vector3 rightPaddleVelocity;
        private int _score;
        public int ballsRemaining = 10;
        public static GameManager Instance;
    
        public XRController leftController;
        public XRController rightController;
        
        public GameObject[] targetPrefabs;  // Array to hold the different target prefabs

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI ballRemainingText;
        
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            // leftController = GetComponent<XRController>();
            // rightController = GetComponent<XRController>();
            // leftPhysicsPoser = GetComponent<PhysicsPoser>();
            // rightPhysicsPoser = GetComponent<PhysicsPoser>();
            scoreText.text = "Score: " + _score;
            ballRemainingText.text = "Balls Remaining: " + ballsRemaining;
            SpawnTargets();
        }

        void Update()
        {
            leftPaddleVelocity = leftPhysicsPoser.GetRigidBodyVelocity();
            rightPaddleVelocity = rightPhysicsPoser.GetRigidBodyVelocity();
            
        }

        public void HandleBallPaddleCollision(Collision collision, Rigidbody ballRb)
        {
        
        
            if (collision.collider.gameObject.CompareTag("Left Paddle")) {
                Rigidbody paddleRb = collision.collider.gameObject.GetComponent<Rigidbody>();
                if (paddleRb == null)
                {
                    Debug.LogError("No Rigidbody found on paddle.");
                    return;
                }

                if (leftPaddleVelocity != new Vector3(0, 0, 0))
                {
                    Debug.Log("leftPaddleVelocity: " + leftPaddleVelocity);
                }

                Vector3 newVelocity = ballRb.velocity + leftPaddleVelocity;
                leftController.SendHapticImpulse(0.5f, .2f);
                return;
            }
            if (collision.collider.gameObject.CompareTag("Right Paddle")) {
                Rigidbody paddleRb = collision.collider.gameObject.GetComponent<Rigidbody>();
                if (paddleRb == null)
                {
                    Debug.LogError("No Rigidbody found on paddle.");
                    return;
                }
                if (rightPaddleVelocity != new Vector3(0, 0, 0))
                {
                    Debug.Log("leftPaddleVelocity: " + leftPaddleVelocity);
                }
                Debug.Log("rightPaddleVelocity: " + rightPaddleVelocity);
                rightController.SendHapticImpulse(0.5f, .2f);
                Vector3 newVelocity = ballRb.velocity + rightPaddleVelocity;
                return;
            }
        }
        void SpawnTargets()
        {
            foreach (GameObject targetPrefab in targetPrefabs)
            {
                // Instantiate the target at its prefab's initial position and rotation
                Instantiate(targetPrefab, targetPrefab.transform.position, targetPrefab.transform.rotation);
            }
        }

        public void HandleBallTargetCollision(Collision collision, Rigidbody ballRb)
        {
            if (collision.collider.gameObject.CompareTag("Target1"))
            {
                AddScore(1);
            }
            else if (collision.collider.gameObject.CompareTag("Target3"))
            {
                AddScore(3);
            }
            else if (collision.collider.gameObject.CompareTag("Target5"))
            {
                AddScore(5);
            }
            else if (collision.collider.gameObject.CompareTag("Target10"))
            {
                AddScore(10);
                
            }
        }
        
        public void DecrementBallsRemaining()
        {
            ballsRemaining--;
            ballRemainingText.text = "Balls Remaining: " + ballsRemaining;
            if (ballsRemaining == 0)
            {
                _score = 0;
                scoreText.text = "Score: " + _score;
                
                ballsRemaining = 10;
                ballRemainingText.text = "Balls Remaining " + ballsRemaining;
            }
        }
        public void AddScore(int points)
        {
            _score += points;
            UpdateScoreUI();
        }

        private void UpdateScoreUI()
        {
            scoreText.text = "Score: " + _score.ToString();
        }
    }
}