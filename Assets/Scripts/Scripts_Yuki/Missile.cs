using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{


    [Header("Control Settings")]
    [SerializeField] private    float       moveXSpeed          = 50f;
    [SerializeField] public     float       moveYSpeed          = 18f;
    private float dragDirection;

    [Header("Rotate Settings")]
    [SerializeField] private bool           rotate;
	[SerializeField] private float          rotationSpeed;

    private float _growthDuration = 0.1f;

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
    }

    // Update is called once per frame
    void Update()
    {
        
        // 자동으로 떨어지게
        float moveY = moveYSpeed * Time.fixedDeltaTime;
        transform.Translate(Vector3.up * moveY);

        if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        if (Input.GetMouseButton(0))
        {
            // 마우스 드래그 입력 받기
            dragDirection = Input.GetAxis("Mouse X");

            // 좌우로 이동
            float moveX = dragDirection * moveXSpeed * Time.fixedDeltaTime;
            transform.Translate(Vector3.right * moveX);
        }
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
