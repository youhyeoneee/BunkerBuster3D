using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl_test : MonoBehaviour
{
   [SerializeField] GameObject Player;
   [SerializeField] GameObject Cam;   
    void Start()
    {
        Player.SetActive(false);
        Cam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {            
            Player.SetActive(true);
            Cam.SetActive(true);
        }
    }
}
