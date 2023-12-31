using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class Drill : MonoBehaviour
{
    [SerializeField] GameObject glowParticle;

    private float _growthDuration = 0.1f;

    Player.PlayerController pc;
    private void Start() 
    {
        pc = Player.PlayerController.instance;

    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject triggerObject = other.gameObject;

        // 자신이 드릴이고 부딪힌 상대가 플레이어일 때만 
        if (gameObject.tag == TagType.Drill.ToString() && triggerObject.tag == TagType.Player.ToString())
        {
            DrillController.instance.AttachObject(transform);
            gameObject.tag = TagType.Player.ToString();

            if (glowParticle != null)
                glowParticle.SetActive(false);
           
        }


        // 벽과 부딪혔을 때 
        if(other.CompareTag(TagType.Walltmp.ToString()))
        {
            other.transform.parent.GetComponent<ConcritController>().ShatterAllConcrits();
            Debug.Log("Wall is tmp");
        }
    }

    public IEnumerator ChangeSize()
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;        
        float newSize = transform.localScale.x * 2;
        Vector3 targetScale = new Vector3(newSize, newSize, startScale.z); 

        while (elapsedTime < _growthDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / _growthDuration);
            yield return null;
        }
    }
}
