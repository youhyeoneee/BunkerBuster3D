using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
public class Boss : MonoBehaviour
{

    Animator anim;

    private void Start() 
    {
        // anim = GetComponent<Animator>();   
    }

	private void OnTriggerEnter(Collider other)
	{
        GameObject triggerObject = other.gameObject;

		if (triggerObject.tag == TagType.Player.ToString()) {
            // anim.SetTrigger(Animtype.End.ToString());
            GameManager.instance.FinishGame();
		}
	}

}
