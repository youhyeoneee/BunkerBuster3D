using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenRocks : MonoBehaviour
{

    private void Start() 
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // 랜덤하게 날아가도록 
        float randomX = Random.Range(-1000, 1000);       
        float randomY = Random.Range(100, 200);       
        float randomZ = Random.Range(-1000, 1000);
        Vector3 force = new Vector3(randomX, randomY, randomZ);
        rb.AddForce(force, ForceMode.Impulse);    


    }

}
