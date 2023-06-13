using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class Missile : MonoBehaviour
{


    [Header("Control Settings")]
    [SerializeField] private    float       moveXSpeed          = 50f;
    [SerializeField] public     float       moveYSpeed          = 18f;
    private float dragDirection;

    [Header("Rotate Settings")]
    [SerializeField] private bool           rotate;
	[SerializeField] private float          rotationSpeed;

    [Header("Particles")]
	[SerializeField] private ParticleSystem sizeUpParticle;
	[SerializeField] private ParticleSystem sizeDownParticle;

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
        _mr = GetComponent<MeshRenderer>();
        _mat = _mr.material;
        gmr = GameManager.instance;

        // 파티클 꺼주기
        if (sizeDownParticle != null)
            sizeUpParticle.Stop();
        
        if (sizeDownParticle != null)
            sizeDownParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (gmr.gameState == GameStateType.Playing)
        {
        
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
        _mr.material = newMat;
        yield return new WaitForSeconds(0.2f);

        _mr.material = _mat;
    }
    
}
