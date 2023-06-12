using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class Gimmick : MonoBehaviour
{

    [SerializeField] private GimmickType    gimmickType;

    [Header("Rotate Settings")]
    [SerializeField] private bool           rotate;
	[SerializeField] private float          rotationSpeed;


    [SerializeField] private float          sizeScale = 3f;
    private Missile missile;

    private void Start()
    {
        missile = Missile.instance;
    }

    private void Update () 
    {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}


	private void OnTriggerEnter(Collider other)
	{
        GameObject triggerObject = other.gameObject;

		if (triggerObject.tag == TagType.Player.ToString()) {
			ActivateGimmick();
		}
	}

    private void ActivateGimmick()
	{
		// if(collectSound)
		// 	AudioSource.PlayClipAtPoint(collectSound, transform.position);
		// if(collectEffect)
		// 	Instantiate(collectEffect, transform.position, Quaternion.identity);

        switch (gimmickType)
        {
            case GimmickType.HorizontalSizeUp:
                StartCoroutine(missile.ChangeSize(sizeScale, false));
                break;
            case GimmickType.HorizontalSizeDown:
                StartCoroutine(missile.ChangeSize(-sizeScale, false));
                break;            
            case GimmickType.VerticalSizeUp:
                StartCoroutine(missile.ChangeSize(sizeScale, true));
                break;    
            case GimmickType.VerticalSizeDown:
                StartCoroutine(missile.ChangeSize(-sizeScale, true));
                break;   
            case GimmickType.Evolve:
                break;
            case GimmickType.Devolve:
                break;
        }
    }
}


