using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [Header("Cam Init")]
    [SerializeField] private Transform playerTr;
    [SerializeField] private float distance;
    [SerializeField] private float height;
    [SerializeField] private float damping;

    private float time = 0.0f;
    private float t;
    [SerializeField] private float destTime = 2.0f;

    [Header("Target Pos")]
     [SerializeField] private Transform targetTr;


    private Transform tr;
    private Vector3 velocity;

    [SerializeField] private bool targetMove = false;

    
    void Start()
    {   
        
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!targetMove)
        {
            Vector3 targetPos = playerTr.position +
                                  -playerTr.forward * distance +
                                  playerTr.up * height;


            tr.position = Vector3.SmoothDamp(tr.position,
                                            targetPos,
                                            ref velocity,
                                            damping);

            


        }
        else
        {
            
            if(time < destTime)
            {
                time += Time.deltaTime;
                t = time / destTime;
            }
            else
            {
                time = destTime;
            }
            tr.position = Vector3.SmoothDamp(tr.position,
                                            targetTr.position,
                                            ref velocity,
                                            damping);

            tr.rotation = Quaternion.Slerp(tr.rotation,
                                            targetTr.rotation,
                                            t);
   
        
        }
    }

}
