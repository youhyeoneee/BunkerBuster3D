using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTst : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float radius;
    
    void OnCollisionEnter(Collision other)
    {
        Vector3 explotionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explotionPos, radius);
        foreach(Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, explotionPos, radius, 0.05f, ForceMode.Impulse);
            }
        }
    }
}
