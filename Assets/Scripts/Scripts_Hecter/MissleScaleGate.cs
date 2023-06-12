using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScaleGate : MonoBehaviour
{
    [SerializeField] Vector3 Scaleup;
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
         if(other.CompareTag("Player"))    
        {
            Debug.Log("Player");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))    
        {
            other.transform.localScale += Scaleup;
            gameObject.SetActive(false);
        }
    }


  
}
