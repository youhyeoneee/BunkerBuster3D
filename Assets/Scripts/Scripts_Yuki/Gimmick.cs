using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class Gimmick : MonoBehaviour
{

    [SerializeField] private GimmickType    gimmickType;

    [Header("Rotate Settings")]
    [SerializeField] private bool           rotate;
	[SerializeField] private float          rotationSpeed;


    [Header("Control Settings")]
    [SerializeField] public     float       moveYSpeed          = 18f;

    [Header("Size Settings")]
    [SerializeField] private float          sizeScale = 3f;
    private float _growthDuration = 0.1f;
    Vector3 targetScale;

    [Header("Materials")]
    [SerializeField] private Material redMat;
    [SerializeField] private Material greenMat;

    

    private Missile missile;
    private GimmickManager gimr;
    private GameManager gmr;

    private void Start()
    {
        missile = Missile.instance;
        gimr = GimmickManager.instance;
        gmr = GameManager.instance;

        targetScale = transform.localScale;
        Debug.Log(targetScale);
    }

    private void OnEnable() 
    {
        // StartCoroutine(ChangeSize());
    }
    private void Update () 
    {

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);


        if (gmr.gameState == GameStateType.Playing)
        {
            // 자동으로 올라가게
            float moveY = moveYSpeed * Time.fixedDeltaTime;
            transform.parent.Translate(Vector3.up * moveY);
        }
           

	}


	private void OnTriggerEnter(Collider other)
	{
        GameObject triggerObject = other.gameObject;

		if (triggerObject.tag == TagType.Player.ToString()) {
			ActivateGimmick();
            gimr.isnextGimmikActivated = true;
		}
	}

    private void ActivateGimmick()
	{
		// if(collectSound)
		// 	AudioSource.PlayClipAtPoint(collectSound, transform.position);

        switch (gimmickType)
        {
            case GimmickType.HorizontalSizeUp:
                StartCoroutine(missile.ChangeSize(sizeScale, false));
                StartCoroutine(missile.ChangerColor(greenMat));
                break;
            case GimmickType.HorizontalSizeDown:
                StartCoroutine(missile.ChangeSize(-sizeScale, false));
                StartCoroutine(missile.ChangerColor(redMat));
                break;            
            case GimmickType.VerticalSizeUp:
                StartCoroutine(missile.ChangeSize(sizeScale, true));
                StartCoroutine(missile.ChangerColor(greenMat));
                break;    
            case GimmickType.VerticalSizeDown:
                StartCoroutine(missile.ChangeSize(-sizeScale, true));
                StartCoroutine(missile.ChangerColor(redMat));
                break;   
            case GimmickType.Evolve:
                StartCoroutine(missile.ChangerColor(greenMat));
                break;
            case GimmickType.Devolve:
                StartCoroutine(missile.ChangerColor(redMat));
                break;
        }
    }

    public IEnumerator ChangeSize()
    {   

        Debug.Log("Change size" + targetScale);
        float elapsedTime = 0f;
        Vector3 startScale = Vector3.zero;

        transform.localScale = startScale;

        while (elapsedTime < _growthDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / _growthDuration);
            yield return null;
        }

    }

}


