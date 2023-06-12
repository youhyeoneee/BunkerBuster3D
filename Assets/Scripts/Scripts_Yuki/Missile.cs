using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{


    [Header("SwipeContrl")]
    [SerializeField] private bool   click;
    [SerializeField] private float  runSpeed;
    [SerializeField] private float  swipeSpeed;
    
    [Space(10)]
    [SerializeField] private float  missleSpeed;

    [Header("Size")]
    private float maxScale = 30f;
    private float minScale = 10f;
    [SerializeField] private float _growthDuration = 0.1f;

    private Rigidbody   rb;
    private Vector3     _direction; 


    #region singleton
    public static Missile instance = null;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }    
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

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

        _direction = new Vector3(Mathf.Lerp(_direction.x, Input.GetAxis("Mouse X"), Time.deltaTime * runSpeed), 0f);

        _direction = Vector3.ClampMagnitude(_direction, 1f);
    }

     void FixedUpdate() 
    {
        if (click)
        {
            Vector3 displacement = new Vector3(_direction.x, 0f, 0f) * Time.fixedDeltaTime;          
            rb.velocity = new Vector3(_direction.x * Time.fixedDeltaTime * swipeSpeed, 0f, 0f) + displacement;            
        }
        else
        {                               
            rb.velocity = Vector3.zero;            
        }

        MoveDown();
       
    }

    void MoveDown()
    {
        Vector3 move = new Vector3(0.0f, missleSpeed * Time.fixedDeltaTime, 0.0f);
        rb.velocity = -move;
    }

    public IEnumerator ChangeSize(float amount, bool isVertical)
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;        
        Vector3 targetScale;
        if (isVertical) // 길이 (Y)
            targetScale = startScale + new Vector3(0, amount, 0);
        else // 두께 (X, Z)
            targetScale = startScale + new Vector3(amount, 0, amount);

        while (elapsedTime < _growthDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / _growthDuration);
            Debug.Log(elapsedTime + "ChangeSize " +  transform.localScale);
            Debug.Log(elapsedTime + "/" + _growthDuration);

            yield return null;
        }
    }
}
