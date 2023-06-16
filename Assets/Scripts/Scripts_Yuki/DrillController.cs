using System.Collections;
using System.Collections.Generic;
using EnumTypes;
using UnityEngine;


// 플레이어의 드릴을 관리
public class DrillController : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> drills;

    [Header("Rotate Settings")] [SerializeField]
    private float rotationSpeed = 1000;


    [Header("Reflection Settings")] [SerializeField]
    private float reflectionForce = 10f;
    private float removalForce = 5f;

    [SerializeField] GameObject particleSystemPrefab;

    private MeshRenderer _mr;

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

    public Vector3 offset = new Vector3(0f, 0f, -0.7f);


    private GameManager gmr;
    private void Start()
    {
        // GetChild() 메서드를 사용하여 자식 오브젝트 얻기
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            drills.Add(child.gameObject);
        }

        gmr = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);


        if (gmr.gameState == GameStateType.BreakingCubes && childDriil == 0)
        {
            gmr.FinishGame();
        }
    }

    // 먹는 드릴 기믹 
    public void AttachObject(Transform target)
    {
        target.parent = transform;
        target.localPosition = Vector3.zero + (offset * childDriil);
        target.localRotation = Quaternion.identity;
        target.localScale = Vector3.one / 2;

        drills.Add(target.gameObject);
        childDriil++;
    }

    
    // 못뚫는 기믹 만났을 때 동작 
    public void RemoveDrill(Transform target)
    {
        
        // 마지막엔 아무것도 안남게 
        int remainDrillCnt = (gmr.gameState ==  GameStateType.Playing) ? 2 : 1;
        if (drills.Count > remainDrillCnt)
        {
            // 드릴이 2개 이상인 경우 날아가면서 제거 
            GameObject removedDrill = drills[drills.Count - 1];
            drills.Remove(removedDrill);

            removedDrill.GetComponent<MeshRenderer>().enabled = false;
            
            // 부서진 거 
            Transform brokenDrill = removedDrill.transform.GetChild(0);
            brokenDrill.gameObject.SetActive(true);

            Rigidbody rb = removedDrill.GetComponent<Rigidbody>();

            // 랜덤하게 날아가도록 
            float randomX = Random.Range(-200, 200);       
            float randomY = Random.Range(100, 200);       
            float randomZ = Random.Range(-1000, 1000);
            Vector3 force = new Vector3(randomX, randomY, randomZ);
            rb.AddForce(force, ForceMode.Impulse);


            removedDrill.transform.parent = null;
            childDriil--;

            removedDrill.SetActive(false);
            // Destroy(removedDrill);
        }
        
    }

    void PlayParticle(Transform target)
    {
        Vector3 triggerPoint;
        Quaternion triggerRotation;
        ParticleSystem particleSystem;
         // 충돌 파티클 
        triggerPoint = target.transform.position;
        triggerRotation = target.transform.rotation;       

        // 파티클 시스템 생성 또는 활성화
        // particleSystem = Instantiate(particleSystemPrefab, triggerPoint, triggerRotation, transform);

        // 파티클 시스템 재생
        // particleSystem.Play();

        // // 파티클 시스템 재생이 끝난 후 삭제
        // Destroy(particleSystem.gameObject, particleSystem.main.duration);

    }

}
