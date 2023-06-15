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
    // [SerializeField] private int _amount;

    [SerializeField] ParticleSystem particleSystemPrefab;
    DrillController dc;

    void Start()
    {
        _startRotation = transform.rotation;
        dc = DrillController.instance;

    }

    void Update()
    {
        if (_isRotate)
            Rotate();
    }

    void Rotate()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        // GameObject hitObject = other.collider.gameObject;

        // // 플레이어와 충돌했을 경우
        // if (hitObject.CompareTag(TagType.Player.ToString()))
        // {
        //     ActivateGimmick(hitObject);
        // }
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
        // GameObject hitObject = other.gameObject;

        switch (gimmickType)
        {
            case GimmickType.Drill:
                if (gameObject.tag == TagType.Drill.ToString())
                {
                    dc.AttachObject(transform);
                    gameObject.tag = TagType.Player.ToString();
                }
                break;
        
            case GimmickType.Evolve:
                break;
            
            case GimmickType.Undrilled:         

                // 충돌 파티클 
                Vector3 triggerPoint = other.transform.position;
                Quaternion triggerRotation = other.transform.rotation;       

                // 파티클 시스템 생성 또는 활성화
                ParticleSystem particleSystem = Instantiate(particleSystemPrefab, triggerPoint, triggerRotation, transform);

                // 파티클 시스템 재생
                particleSystem.Play();

                // 파티클 시스템 재생이 끝난 후 삭제
                Destroy(particleSystem.gameObject, particleSystem.main.duration);

                // 드릴 튀어오르는 동작 시작
                StartCoroutine(Player.PlayerController.instance.ReflectAndBounce());        

                dc.RemoveDrill(transform);    
            
    
                break;

        }
    }
}
