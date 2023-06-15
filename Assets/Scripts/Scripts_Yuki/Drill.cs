using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class Drill : MonoBehaviour
{
    [SerializeField] GameObject particle;

    private float _growthDuration = 0.1f;
    private void OnTriggerEnter(Collider other)
    {
        GameObject triggerObject = other.gameObject;

        // 자신이 드릴이고 부딪힌 상대가 플레이어일 때만 
        if (gameObject.tag == TagType.Drill.ToString() && triggerObject.tag == TagType.Player.ToString())
        {
            DrillController.instance.AttachObject(transform);
            gameObject.tag = TagType.Player.ToString();

            if (particle != null)
                particle.SetActive(false);
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
