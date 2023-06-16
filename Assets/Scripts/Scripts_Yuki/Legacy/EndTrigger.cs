using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class EndTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == TagType.Player.ToString()) {
            // GameManager.instance.EndingGame();
		}
	}
}
