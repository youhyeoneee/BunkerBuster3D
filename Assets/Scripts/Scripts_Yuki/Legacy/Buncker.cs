using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class Buncker : MonoBehaviour
{
    [SerializeField] GameObject boomParticle;
    Rigidbody rb;
    
    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }


	private void OnTriggerEnter(Collider other)
	{
        GameObject triggerObject = other.gameObject;

		if (triggerObject.tag == TagType.Player.ToString()) {
            StartCoroutine(Explosion());
		}
	}

    IEnumerator Explosion()
    {
        Debug.Log("Explosion");
        boomParticle.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.BreakingCube();
        // rb.AddExplosionForce(1000.0f, transform.position, 10.0f, 300.0f);
    }
}
