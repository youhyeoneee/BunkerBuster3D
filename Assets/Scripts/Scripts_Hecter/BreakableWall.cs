using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] float force;
    List<Rigidbody> rb = new List<Rigidbody>();
    void Start()
    {
        rb.Add(transform.GetChild(0).GetComponent<Rigidbody>());
        rb.Add(transform.GetChild(1).GetComponent<Rigidbody>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision other)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))    
        {
            rb[0].AddForce(Vector3.up * force, ForceMode.Impulse);
            rb[1].AddForce(Vector3.up * force, ForceMode.Impulse);
            Debug.Log("Player");
        }
        
    }
}
