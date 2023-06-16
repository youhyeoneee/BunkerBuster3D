using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class Gem : MonoBehaviour
{

    [SerializeField] private Transform gemUI;

    [SerializeField] private float speed = 3f;
    public bool isMove = false;

    GameManager gmr;
    void Start()
    {
        gmr = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
            transform.position = Vector3.Slerp(transform.position, gemUI.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag(TagType.GemUI.ToString()))
        {
            gameObject.SetActive(false);
            gmr.gem ++;

        }    
    }


}
