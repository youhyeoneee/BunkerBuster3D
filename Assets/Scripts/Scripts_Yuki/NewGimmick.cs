using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumTypes;

public class NewGimmick : MonoBehaviour
{

    [SerializeField] private GimmickType gimmickType;

    [Header("Gimmick for Rotate")] 
    [SerializeField] private bool _isRotate;
    [SerializeField] private float _rotateSpeed;
    private Quaternion _startRotation;

    [Header("Control Settings")]
    [SerializeField] public     float       moveYSpeed          = 18f;
    
    [Header("Particle Settings")]
    [SerializeField] ParticleSystem particleSystemPrefab;

    [Header("Evolve")]
    [SerializeField] GameObject evolveCanvas;

    
    [Header("Half")]
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private Text drillCntText;
    [SerializeField] private int drillCnt = 5;
    [SerializeField] private MeshRenderer _blcakCubemr;

    
    [Header("Drilled")]
    [SerializeField] GameObject brokenRockPrefab;
    MeshRenderer _mr;
    bool isPassed = false; // 지나갔는지 
    [SerializeField] private ParticleSystem gemParticle;

    DrillController dc;
    Player.PlayerController pc;
    GameManager gmr;
    

    void Start()
    {
        _startRotation = transform.rotation;
        dc = DrillController.instance;
        pc = Player.PlayerController.instance;
        gmr = GameManager.instance;
        
        _mr = GetComponent<MeshRenderer>();
    
        
        if (drillCntText != null)
            SetDrillCntText();
    }

    void Update()
    {

        if (_isRotate)
        {
            // 필드에 있는 드릴들
            if (!gameObject.CompareTag(TagType.Player.ToString()) && gimmickType == GimmickType.Drill)
                Rotate();            
        }
    }

    void Rotate()
    {
        Quaternion targetRotation = Quaternion.Euler(_startRotation.eulerAngles.x, transform.rotation.eulerAngles.y + (_rotateSpeed * Time.deltaTime), _startRotation.eulerAngles.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    
    }

    void OnTriggerEnter(Collider other) 
    {
        // 플레이어와 충돌했을 경우
        if (other.gameObject.CompareTag(TagType.Player.ToString()))
        {
            ActivateGimmick(other);
        }
    }

    void ActivateGimmick(Collider other)
    {
        GameObject hitObject = other.gameObject;


        switch (gimmickType)
        {       
            case GimmickType.Evolve:
                isPassed = true;

                if (evolveCanvas.activeSelf)
                    evolveCanvas.SetActive(false);
                Drill drill = hitObject.GetComponent<Drill>();
                StartCoroutine(drill.ChangeSize());
                break;
            
            case GimmickType.Undrilled:
                isPassed = true;

                PlayParticle(other);
                // 드릴 튀어오르는 동작 시작
                StartCoroutine(pc.ReflectAndBounce());        

                dc.RemoveDrill(transform);    
        
                break;
            case GimmickType.Half:
          
                
                drillCnt--;
                if (drillCntText != null)
                    SetDrillCntText();

                if (drillCnt > 0)  
                {
                    PlayParticle(other);
                    isPassed = true;
                    // 드릴 퉁기기 
                    StartCoroutine(pc.ReflectAndBounce());    
                    dc.RemoveDrill(transform);
                }
                else
                {
                    _mr.enabled = false;

                    if (_blcakCubemr != null)
                        _blcakCubemr.enabled = false;

                    // 부서진 바위 오브젝트 생성
                    GameObject brokenRock = Instantiate(brokenRockPrefab, transform.position, transform.rotation, transform);
                    
                    if (canvasObject != null)
                        canvasObject.SetActive(false);
                    
                    if (gameObject.activeSelf)
                        StartCoroutine(SetDisable());
                }
                break;
            case GimmickType.Drilled:       

                if (!isPassed)  
                {
                    PlayParticle(other);
                    isPassed = true;
                    // 드릴 뚫느라 조금 느려지기
                    // StartCoroutine(pc.Drilling());        
                    
                    _mr.enabled = false;

                    // 젬 획득 
                    GetGem();

                    // 부서진 바위 오브젝트 생성
                    GameObject brokenRock = Instantiate(brokenRockPrefab, transform.position, transform.rotation, transform);
                    
                    if (gameObject.activeSelf)
                        StartCoroutine(SetDisable());

                    
                }
                break;
        }
    }


    IEnumerator SetDisable()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    void PlayParticle(Collider other)
    {
        Vector3 triggerPoint;
        Quaternion triggerRotation;
        ParticleSystem particleSystem;
         // 충돌 파티클 
        triggerPoint = other.transform.position;
        triggerRotation = other.transform.rotation;       

        // 파티클 시스템 생성 또는 활성화
        particleSystem = Instantiate(particleSystemPrefab, triggerPoint, triggerRotation, transform);

        // 파티클 시스템 재생
        particleSystem.Play();

        // 파티클 시스템 재생이 끝난 후 삭제
        Destroy(particleSystem.gameObject, particleSystem.main.duration);

    }

    void SetDrillCntText()
    {
        drillCntText.text = drillCnt.ToString();
    }
    void GetGem()
    { 


        if (transform.childCount > 0)
        {
            // GetChild() 메서드를 사용하여 자식 오브젝트 얻기
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform gem = transform.GetChild(i);
                Gem gemScirpt = gem.gameObject.GetComponent<Gem>();
                if (gemScirpt != null)
                {
                    Debug.Log("Particle Play");
                    gemParticle.Play();
                    gemScirpt.isMove = true;
                }
                else 
                {
                    Debug.Log("Gem script is null" + gem.gameObject.name);

                }
            }        
        }
        
    }
}
