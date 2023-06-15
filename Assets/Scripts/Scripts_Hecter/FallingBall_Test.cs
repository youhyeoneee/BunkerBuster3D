using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBall_Test : MonoBehaviour
{
    [SerializeField] float moveYSpeed;    
    [SerializeField] GameObject particle;
    [SerializeField] Transform particlePos;
   
    

    // Update is called once per frame
    void Update()
    {
        float moveY = moveYSpeed * Time.fixedDeltaTime;
        transform.Translate(Vector3.forward * moveY);            
    }
   

    void OnCollisionEnter(Collision other)
    {       
        if(other.collider.CompareTag("Finish"))
        {
            Instantiate(particle, particlePos.position, Quaternion.identity);
            moveYSpeed = 0;
        }
    }
    
   
}
