using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void Collect()
    {
        enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
