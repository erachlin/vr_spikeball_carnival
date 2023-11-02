using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetMover : MonoBehaviour
{
    public enum TargetType { Type1, Type3, Type5, Type10 }
    public TargetType targetType;

    public float speed = 3.0f;
    public float distance = 10.0f;
    public int pointValue;
    public AudioClip sound4;

    private AudioSource audioSource;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        // Set point value based on targetType
        switch (targetType)
        {
            case TargetType.Type1:
                pointValue = 1;
                break;
            case TargetType.Type3:
                pointValue = 3;
                break;
            case TargetType.Type5:
                pointValue = 5;
                break;
            case TargetType.Type10:
                pointValue = 10;
                break;
        }
    }

    void Update()
    {
        // Different behavior based on targetType
        switch (targetType)
        {
            case TargetType.Type1:
                MoveHorizontally();
                break;
            case TargetType.Type3:
               // MoveVertically();
               MoveHorizontally();
                break;
            case TargetType.Type5:
                //MoveDiagonally();
                MoveHorizontally();
                break;
            case TargetType.Type10:
                //MoveInCircle();
                MoveHorizontally();
                break;
        }
    }

    void MoveHorizontally()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = initialPosition + new Vector3(offset, 0, 0);
    }

    void MoveVertically()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = initialPosition + new Vector3(0, offset, 0);
    }

    void MoveDiagonally()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = initialPosition + new Vector3(offset, offset, 0);
    }

    void MoveInCircle()
    {
        float x = Mathf.Sin(Time.time * speed) * distance;
        float y = Mathf.Cos(Time.time * speed) * distance;
        transform.position = initialPosition + new Vector3(x, y, 0);
    }
}

