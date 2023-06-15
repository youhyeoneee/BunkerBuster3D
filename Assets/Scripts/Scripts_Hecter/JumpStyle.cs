using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStyle : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float jumpForce;
    [SerializeField] float moveDown;
    [SerializeField] bool jump = false;
    [SerializeField] GameObject particle;
    [SerializeField] Transform particlePos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(jump)
        {
            rb.velocity = new Vector3(0, jumpForce *Time.fixedDeltaTime, 0); 
        }
        else
        {
            rb.velocity = new Vector3(0, moveDown *Time.fixedDeltaTime, 0);
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Walltmp"))
        {
            StartCoroutine(Jump());                       
        }
        
        if(other.collider.CompareTag("Finish"))
        {
            Instantiate(particle, particlePos.position, Quaternion.identity);
            rb.velocity = Vector3.zero;
        }
    }
    IEnumerator Jump()
    {
        jump = true; 
        yield return new WaitForSeconds(0.3f);
        jump = false; 
    }
}
