using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTmp : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        }
    }

}
