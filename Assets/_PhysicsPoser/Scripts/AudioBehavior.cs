using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Left Paddle") || collision.gameObject.CompareTag("Right Paddle"))
        {
            audioSource.clip = sound1;
            audioSource.Play();
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            audioSource.clip = sound2;
            audioSource.Play();
        }
        else if (collision.gameObject.CompareTag("Trampoline"))
        {
            audioSource.clip = sound3;
            audioSource.Play();
        }
        // else if (collision.gameObject.CompareTag("Target1") || collision.gameObject.CompareTag("Target3") || collision.gameObject.CompareTag("Target5") || collision.gameObject.CompareTag("Target10"))
        //
        // {
        //     audioSource.clip = sound4;
        //     audioSource.Play();
        // }
    }
}