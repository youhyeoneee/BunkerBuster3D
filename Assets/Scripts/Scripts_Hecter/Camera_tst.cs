using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_tst : MonoBehaviour
{
    [SerializeField] GameObject Cam1;
    [SerializeField] GameObject Cam2;
    [SerializeField] GameObject Plane;
    // Start is called before the first frame update
    void Start()
    {
        Cam1.SetActive(true);
        Cam2.SetActive(false);
        Plane.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCam();
    }

    void ChangeCam()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Cam1.SetActive(true);
            Plane.SetActive(true);
            Cam2.SetActive(false);
        }
        
        if(Input.GetKeyDown(KeyCode.S))
        {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
            Plane.SetActive(false);
        }
    }
}
