using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class Missile : MonoBehaviour
{


    [Header("Control Settings")]
    [SerializeField] private    float       moveXSpeed          = 500f;
    [SerializeField] public     float       moveYSpeed          = 130f;
    private float dragDirection;

    [Header("Rotate Settings")]
    [SerializeField] private bool           rotate;
	[SerializeField] private float          rotationSpeed;

    [Header("Particles")]
	[SerializeField] private ParticleSystem sizeUpParticle;
	[SerializeField] private ParticleSystem sizeDownParticle;

    [Header("Missiles")]
    [SerializeField] private GameObject[] missiles;
    [SerializeField] private int missileIdx = 0;

    private float           _growthDuration = 0.1f;
    private MeshRenderer    _mr;
    private Material        _mat;

    GameManager gmr;

    #region singleton
    public static Missile instance = null;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }    
        instance = this;
    }
    #endregion

    void Start()
    {
        
        gmr = GameManager.instance;

        // 파티클 꺼주기
        if (sizeDownParticle != null)
            sizeUpParticle.Stop();
        
        if (sizeDownParticle != null)
            sizeDownParticle.Stop();


         OnOffMissile();
    }

    // Update is called once per frame
    void Update()
    {


        switch(gmr.gameState)
        {
            case GameStateType.Playing:
                // 자동으로 떨어지게
                float moveY = moveYSpeed * Time.fixedDeltaTime;
                transform.parent.Translate(Vector3.down * moveY);

                if (rotate)
                    transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

                if (Input.GetMouseButton(0))
                {
                    // 마우스 드래그 입력 받기
                    dragDirection = Input.GetAxis("Mouse X");

                    // 좌우로 이동
                    float moveX = dragDirection * moveXSpeed * Time.fixedDeltaTime;
                    transform.parent.Translate(Vector3.right * moveX, Space.World);
                }
                break;
            case GameStateType.Ending:
                // 자동으로 떨어지게
                if (transform.parent.position.y >= 200f)
                {
                    moveY = moveYSpeed * Time.fixedDeltaTime;
                    transform.parent.Translate(Vector3.down * moveY);
                }
                else 
                    transform.parent.position = new Vector3(0f, 0f, -50);
                break;

            case GameStateType.BreakingCubes:
                moveY = 5f * Time.fixedDeltaTime;
                transform.parent.Translate(Vector3.down * moveY);
                break;
                
        }
    }

    public IEnumerator ChangeSize(float amount, bool isVertical)
    {

        // 파티클
        ParticleSystem particle;
        if (amount > 0)
            particle = sizeUpParticle;
        else 
            particle = sizeDownParticle;
        particle.Play(); 

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
            yield return null;
        }

        particle.Stop();
    }

    public IEnumerator ChangerColor(Material newMat)
    {

        _mr = missiles[missileIdx].GetComponent<MeshRenderer>();
        _mat = _mr.material;

        _mr.material = newMat;
        yield return new WaitForSeconds(0.2f);

        _mr.material = _mat;
    }
    
    public void ChangeMissile(int increment)
    {

        if (0 <= missileIdx + increment && missileIdx + increment < missiles.Length)
            missileIdx += increment;
        else 
            Debug.Log("MAX(or MIN) LEVEL");
        
        // 파티클
        ParticleSystem particle;
        if (increment > 0)
            particle = sizeUpParticle;
        else 
            particle = sizeDownParticle;
        particle.Play();     

        OnOffMissile();
    }


    private void OnOffMissile()
    {
        for (int i=0; i<missiles.Length; i++)
        {
            if (i == missileIdx)
                missiles[i].SetActive(true);
            else 
                missiles[i].SetActive(false);
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag(TagType.Walltmp.ToString()))
        {
            other.transform.parent.GetComponent<ConcritController>().ShatterAllConcrits();
        }       
    }

    private void OnTriggerEnter(Collider other) 
    {

        if (other.gameObject.CompareTag(TagType.ExplosionObject.ToString()))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 폭발 효과 적용
                Vector3 explosionPosition = transform.position;
                rb.AddExplosionForce(100f, explosionPosition, 100f);
            }
        }   
    }
}
