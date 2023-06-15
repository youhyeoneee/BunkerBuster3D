using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Walltmp"))
        {
            other.transform.parent.GetComponent<ConcritController>().ShatterAllConcrits();
            Debug.Log("Wall is tmp");
        }
    }
}
