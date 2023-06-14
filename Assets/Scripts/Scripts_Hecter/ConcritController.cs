using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcritController : MonoBehaviour
{
    [SerializeField] Concrit[] concrits = null;

    public void ShatterAllConcrits()
    {
        if(transform.parent != null)
        {
            transform.parent = null;
        }

        foreach (Concrit item in concrits)
        {
            item.Shatter();
        }

        StartCoroutine(RemoveAllConcrit());
    }

    IEnumerator RemoveAllConcrit()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            ShatterAllConcrits();
        }
    }
    
}
