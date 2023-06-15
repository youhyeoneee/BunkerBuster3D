using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
public class Boss : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] GameObject exploseParticle;
    [SerializeField] Transform explosePos;

    private void Start() 
    {
        // anim = GetComponent<Animator>();   
    }

	private void OnTriggerEnter(Collider other)
	{
        GameObject triggerObject = other.gameObject;

		if (triggerObject.tag == TagType.Player.ToString()) 
        {
            Instantiate(exploseParticle, explosePos.transform);
            anim.SetTrigger(Animtype.End.ToString());
            GameManager.instance.FinishGame();
		}
	}

}
