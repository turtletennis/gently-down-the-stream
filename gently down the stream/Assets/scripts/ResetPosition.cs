using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    Vector3 initialPosition;
    Quaternion initialRotation;
    Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Awake()
    {

        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        
    }
}
