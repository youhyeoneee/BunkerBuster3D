using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    [Header("Undrilled")]
    [SerializeField] ParticleSystem particleSystemPrefab;

    [Header("Evolve")]
    [SerializeField] GameObject evolveCanvas;

    [Header("Drilled")]
    [SerializeField] GameObject brokenRockPrefab;
    public float explosionForce = 100f; // 폭발 힘
    public float explosionRadius = 5f; // 폭발 범위
    MeshRenderer _mr;
    bool isPassed = false; // 지나갔는지 

    DrillController dc;
    Player.PlayerController pc;
    GimmickManager gimr;


    void Start()
    {
        _startRotation = transform.rotation;
        dc = DrillController.instance;
        pc = Player.PlayerController.instance;
        gimr = GimmickManager.instance;

        _mr = GetComponent<MeshRenderer>();
    }

    void Update()
    {

        if (_isRotate)
        {
            // 필드에 있는 드릴들
            if (!gameObject.CompareTag(TagType.Player.ToString()) && gimmickType == GimmickType.None)
                Rotate();            
        }

        // if (gmr.gameState == GameStateType.Playing)
        // {
        // 자동으로 올라가게
        // float moveY = moveYSpeed * Time.fixedDeltaTime;
        // transform.parent.Translate(Vector3.up * moveY);
        // }
    }

    void Rotate()
    {
        Quaternion targetRotation = Quaternion.Euler(_startRotation.eulerAngles.x, transform.rotation.eulerAngles.y + (_rotateSpeed * Time.deltaTime), _startRotation.eulerAngles.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    
    }

    void OnCollisionEnter(Collision other)
    {
        // GameObject hitObject = other.collider.gameObject;

        // // 플레이어와 충돌했을 경우
        // if (hitObject.CompareTag(TagType.Player.ToString()))
        // {
        //     ActivateGimmick(hitObject);
        //     
        // }
    }

    void OnTriggerEnter(Collider other) 
    {
        // 플레이어와 충돌했을 경우
        if (other.gameObject.CompareTag(TagType.Player.ToString()))
        {
            ActivateGimmick(other);
            gimr.isnextGimmikActivated = true;
        }
    }

    void ActivateGimmick(Collider other)
    {
        GameObject hitObject = other.gameObject;


        switch (gimmickType)
        {       
            case GimmickType.Evolve:
                if (evolveCanvas.activeSelf)
                    evolveCanvas.SetActive(false);
                Drill drill = hitObject.GetComponent<Drill>();
                StartCoroutine(drill.ChangeSize());
                break;
            
            case GimmickType.Undrilled:         
                PlayParticle(other);
                // 드릴 튀어오르는 동작 시작
                StartCoroutine(pc.ReflectAndBounce());        

                dc.RemoveDrill(transform);    
        
                break;

            case GimmickType.Drilled:       

                if (!isPassed)  
                {
                    PlayParticle(other);
                    isPassed = true;
                    // 드릴 뚫느라 조금 느려지기
                    StartCoroutine(pc.Drilling());        
                    _mr.enabled = false;
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
}
