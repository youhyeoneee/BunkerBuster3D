using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTst : MonoBehaviour
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
     [SerializeField] private Transform bossTr;
     [SerializeField] private Transform planeTr;
     [SerializeField] private Transform desertTr;

     [Header("CamRotation")]
    public float targetRotation = 30f;
    public float rotationSpeed = 10f;

    private float currentRotation = 0f;


    private Transform tr;
    private Vector3 velocity;

    [SerializeField] private bool GameStart = false;
    [SerializeField] private bool targetMove = false;

    [Header("CamPostion")]
    [SerializeField] private bool StartPos = false;
    [SerializeField] private bool BossPos = false;
    [SerializeField] private bool PlanePos = false;
    [SerializeField] private bool PlayerPos1 = false;

    
    void Start()
    {   
        
        tr = GetComponent<Transform>();
        tr.position = new Vector3(5.9f, 188f, -375f);     
        desertTr.position = new Vector3(5.9f, 188f, -375f);           
        bossTr.position = new Vector3(5.9f, -209f, -392f);
        planeTr.position = new Vector3(5.9f, 2240f, -142f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BossPos = true;
            PlanePos = false;
            StartPos = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            BossPos = false;
            PlanePos = true;
            StartPos = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            BossPos = false;
            PlanePos = false;
            StartPos = true;
        }
    }
    
    void LateUpdate()
    {

        if (!targetMove)
        {
            if(PlayerPos1)
            {
                distance = -1150f;
                Vector3 targetPos = playerTr.position +
                                                 -playerTr.forward * distance +
                                                 playerTr.up * height;


                tr.position = Vector3.SmoothDamp(tr.position,
                                                targetPos,
                                                ref velocity,
                                                damping);
                if (currentRotation < targetRotation)
                {
                    // Calculate the new rotation
                    float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

                    // Clamp the rotation to the target rotation
                    newRotation = Mathf.Clamp(newRotation, 0f, targetRotation);

                    // Apply the new rotation
                    transform.rotation = Quaternion.Euler(newRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

                    // Update the current rotation
                    currentRotation = newRotation;
                }

            }else
            {
                distance = -100f;
                Vector3 targetPos = playerTr.position +
                                                 -playerTr.forward * distance +
                                                 playerTr.up * height;


                tr.position = Vector3.SmoothDamp(tr.position,
                                                targetPos,
                                                ref velocity,
                                                damping);
                tr.Rotate(0f, 0f, 0f);

            }
           
        }                                            

        BossPosition();
        PlanePosition();
        StartPosition();
    }

    void BossPosition()
    {
        if(BossPos)
        {
                if (time < destTime)
                {
                    time += Time.deltaTime;
                    t = time / destTime;
                }
                else
                {
                    time = destTime;
                }
                tr.position = Vector3.SmoothDamp(tr.position,
                                                bossTr.position,
                                                ref velocity,
                                                damping);

                tr.rotation = Quaternion.Slerp(tr.rotation,
                                                bossTr.rotation,
                                                t);
        }
    }

    void PlanePosition()
    {        
        if(PlanePos)
        {
                if (time < destTime)
                {
                    time += Time.deltaTime;
                    t = time / destTime;
                }
                else
                {
                    time = destTime;
                }
                tr.position = Vector3.SmoothDamp(tr.position,
                                                planeTr.position,
                                                ref velocity,
                                                damping);

                tr.rotation = Quaternion.Slerp(tr.rotation,
                                                planeTr.rotation,
                                                t);
        }
    }

    void StartPosition()
    {        
        if(StartPos)
        {
                if (time < destTime)
                {
                    time += Time.deltaTime;
                    t = time / destTime;
                }
                else
                {
                    time = destTime;
                }
                tr.position = Vector3.SmoothDamp(tr.position,
                                                desertTr.position,
                                                ref velocity,
                                                damping);

                tr.rotation = Quaternion.Slerp(tr.rotation,
                                                desertTr.rotation,
                                                t);
        }
    }
}
