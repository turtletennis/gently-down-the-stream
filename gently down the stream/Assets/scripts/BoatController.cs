using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    public InputAction fireAction;
    PlayerInput playerInput;
    private Vector2 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if(playerInput==null)
        {
            Debug.LogError("Failed to find player input component");
        }
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mousePosition = Mouse.current.position.ReadValue();

        transform.Translate(currentDirection * Time.deltaTime);

    }
    Vector3 currentDirection;
    public void OnMove(InputValue value)
    {
        currentDirection = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        Debug.Log("Fire!");
    }
}
