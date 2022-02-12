using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class BoatController : MonoBehaviour
{
    
    
    private Vector2 mousePosition;
    [Header("Movement")]
    public Transform startPosition;
    public float startSpeed;
    public float acceleration;
    [Range(-100.0f, 100.0f)]
    public float steeringAngleChange;
    public float steeringPower;
    private float currentSpeed;
    [Space(10)]
    
    [Header("Health and Damage")]
    public float totalHealth;
    public float minRockHitDamage;
    public float rockDamageMultiplier;
    public float maxRockHitDamage;
    private float currentHealth;
    private System.Diagnostics.Stopwatch lifetime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        lifetime = System.Diagnostics.Stopwatch.StartNew();
        this.transform.SetPositionAndRotation(startPosition.position,startPosition.rotation);
        currentSpeed = startSpeed;
        currentHealth = totalHealth;
    }

    private void Awake()
    {
        
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "rock")
        {
            
            float damage = col.relativeVelocity.magnitude * rockDamageMultiplier;
            if(damage < minRockHitDamage)
            {
                damage = minRockHitDamage;
            }
            else if(damage > maxRockHitDamage)
            {
                damage = maxRockHitDamage;
            }
            currentHealth -= damage;
            Debug.Log($"Ouch a rock! took {damage} damage. Health {currentHealth}/{totalHealth}");
        }

        if(currentHealth<=0)
        {
            Die();
        }
        
    }

    void Die()
    {
        lifetime.Stop();
        Debug.Log($"You lasted {lifetime.Elapsed.TotalSeconds} seconds");
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed += acceleration * Time.deltaTime;
        //mousePosition = Mouse.current.position.ReadValue();
        var velocity = currentDirection;
        velocity.y += currentSpeed;
        transform.Translate(velocity * Time.deltaTime);
        if(velocity.x!=0)
        {
            Vector3 rotation = new Vector3(0, 0, -velocity.x * steeringAngleChange);
            transform.Rotate(rotation * Time.deltaTime);
        }
    }



    Vector2 currentDirection;
    public void OnMove(InputValue value)
    {
        currentDirection = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        Debug.Log("Fire!");
    }
}
