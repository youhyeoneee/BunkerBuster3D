using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslePlay : MonoBehaviour
{
    [SerializeField] Animator fade;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Cam; 
    Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
        Player.SetActive(false);
        Cam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // ani.SetBool("Play", true);
            StartCoroutine(Fade());
        }
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            ani.SetBool("Play", false);
        }
        
        
    }

    IEnumerator Fade()
    {
        ani.SetBool("Play", true);
        yield return new WaitForSeconds(1.5f);
        fade.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        Player.SetActive(true);
        Cam.SetActive(true);
    }
}
