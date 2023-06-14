using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concrit : MonoBehaviour
{
    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    private Collider collider;
    private ConcritController concritController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        concritController = transform.parent.GetComponent<ConcritController>();
    }
    
    public void Shatter()
    {
        rb.isKinematic = false;
        collider.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float parentXpos = transform.parent.position.x;
        float xPos = meshRenderer.bounds.center.x;
         
        Vector3 subdir = (parentXpos - xPos < 0) ? Vector3.right : Vector3.left;

        Vector3 dir = (Vector3.up * 1.5f + subdir).normalized;

        float force = Random.Range(50,100);
        float torque = Random.Range(110,180);

        rb.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        rb.AddTorque(Vector3.left * torque);
        rb.velocity = Vector3.down;
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {            
            Debug.Log("Player pass the Wall");
        }
        Debug.Log($"Not Player but {other.gameObject.name}");
    }

    
}
