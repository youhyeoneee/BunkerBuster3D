using System.Collections;
using System.Collections.Generic;
using EnumTypes;
using UnityEngine;

public class DrillController : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> drills;

    [Header("Rotate Settings")] [SerializeField]
    private float rotationSpeed = 1000;
    
    private float           _growthDuration = 0.1f;

    #region singleton
    public static DrillController instance = null;
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
    
    [SerializeField] private int childDriil = 1; // 드릴로 물체를 이어붙일 위치

    public Vector3 offset = new Vector3(0f, 0f, -0.14f);
    private void Start()
    {

        // GetChild() 메서드를 사용하여 자식 오브젝트 얻기
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            drills.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    public void AttachObject(Transform target)
    {
        target.parent = transform;
        target.localPosition = Vector3.zero + (offset * childDriil);
        target.localRotation = Quaternion.identity;
        target.localScale = Vector3.one;

        drills.Add(target.gameObject);
        childDriil++;
    }
}
