using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [Space(10)]
    public float minRockHitDamage;
    public float rockDamageMultiplier;
    public float maxRockHitDamage;
    [Space(10)]
    public float minSideHitDamage;
    public float sideDamageMultiplier;
    public float maxSideHitDamage;

    [Space(10)]
    [Header("UI")]
    public HealthDisplay healthUI;
    public EndGameText endGameText;
    [Range(0,60)]
    public float timeToWaitBeforeReset = 3;

    private float currentHealth;
    private System.Diagnostics.Stopwatch lifetime;
    private List<ResetPosition> objectsToReset;
    private Rigidbody2D rigidBody;
    
    
    // Start is called before the first frame update
    void Start()
    {
        objectsToReset = FindObjectsOfType<ResetPosition>().ToList();
        rigidBody = GetComponent<Rigidbody2D>();

        if(PlayerStats.initialised)
        {

            acceleration = PlayerStats.acceleration;
            steeringPower = PlayerStats.steeringPower;
            steeringPower = PlayerStats.steeringPower;
            steeringAngleChange = PlayerStats.steeringAngleChange;
            startSpeed = PlayerStats.startSpeed;
            totalHealth = PlayerStats.totalHealth;
        }
        else
        {
            PlayerStats.acceleration = acceleration;
            PlayerStats.flatDamageResistance = 0;
            PlayerStats.percentDamageResistance = 0;
            PlayerStats.steeringPower = steeringPower;
            PlayerStats.steeringAngleChange = steeringAngleChange;
            PlayerStats.coins = 0;
            PlayerStats.startSpeed = startSpeed;
            PlayerStats.totalHealth = totalHealth;
            PlayerStats.initialised = true;
        }

        Reset();
    }

    private void Reset(float timeToShowTextForBeforeStarting=0,bool returnToTitle = false)
    {
        
        
        lifetime = System.Diagnostics.Stopwatch.StartNew();
        this.transform.SetPositionAndRotation(startPosition.position,startPosition.rotation);
        rigidBody.velocity = Vector2.zero;
        rigidBody.angularVelocity = 0;
        
        StartCoroutine(Restart(timeToShowTextForBeforeStarting,returnToTitle));
        
        currentSpeed = startSpeed;
        currentHealth = totalHealth;
        healthUI.TotalHealth = totalHealth;
        healthUI.CurrentHealth = currentHealth;
        Debug.Log("Set current health to " + currentHealth);
        foreach (var thing in objectsToReset)
        {
            thing.Reset();
        }
        
        
        
    }
    
    private IEnumerator Restart(float secondsDelay,bool returnToTitle)
    {
        
        yield return new WaitForSeconds(secondsDelay);
        endGameText.Hide();
        if (returnToTitle)
        {
            SceneManager.LoadScene("Title");
        }


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
        else if (col.gameObject.tag == "stream")
        {

            float damage = col.relativeVelocity.magnitude * sideDamageMultiplier;
            if (damage < minSideHitDamage)
            {
                damage = minSideHitDamage;
            }
            else if (damage > maxSideHitDamage)
            {
                damage = maxSideHitDamage;
            }
            currentHealth -= damage;
            Debug.Log($"Ouch a bank! took {damage} damage. Health {currentHealth}/{totalHealth}");
        }
        healthUI.CurrentHealth = currentHealth;

        if (currentHealth<=0)
        {
            Die();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="finish line")
        {
            Win();
        }
    }

    void Win()
    {
        lifetime.Stop();
        Debug.Log($"You won! You lasted {lifetime.Elapsed.TotalSeconds} seconds");
        endGameText.Win((int) lifetime.Elapsed.TotalSeconds, currentHealth);
        Reset(timeToWaitBeforeReset,true);
        

    }

    void Die()
    {
        lifetime.Stop();
        Debug.Log($"You lasted {lifetime.Elapsed.TotalSeconds} seconds");
        endGameText.Lose((int)lifetime.Elapsed.TotalSeconds);
        PlayerStats.totalHealth++;
        Reset(timeToWaitBeforeReset, true);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed += acceleration * Time.deltaTime;
        //mousePosition = Mouse.current.position.ReadValue();
        var velocity = currentDirection;
        velocity.x *= steeringPower;
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
