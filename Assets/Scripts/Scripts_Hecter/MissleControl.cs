using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleControl : MonoBehaviour
{
    [Header("SwipeContrl")]
    [SerializeField] bool click;
    [SerializeField] float runSpeed;
    [SerializeField] float swipeSpeed;
    
    [Space(10)]
    [SerializeField] float MissleSp;

    private Rigidbody rb;
    Vector3 Direction; 
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {      
            click = true;                                                                 
        }
        else
        {
            click = false;                                  
        }

        Direction = new Vector3(Mathf.Lerp(Direction.x, Input.GetAxis("Mouse X"), Time.deltaTime * runSpeed), 0f);

        Direction = Vector3.ClampMagnitude(Direction, 1f);
    }

     void FixedUpdate() 
    {
        if (click)
        {
            Vector3 displacement = new Vector3(Direction.x, 0f, 0f) * Time.fixedDeltaTime;          
            rb.velocity = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSpeed, 0f, 0f) + displacement;            
        }
        else
        {                               
            rb.velocity = Vector3.zero;            
        }

        // MoveDown();
       
    }

    void MoveDown()
    {
        Vector3 move = new Vector3(0.0f, MissleSp * Time.fixedDeltaTime, 0.0f);
        rb.velocity = -move;
    }
}
