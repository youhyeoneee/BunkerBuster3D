using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class Drill : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameObject triggerObject = other.gameObject;

        // 자신이 드릴이고 부딪힌 상대가 플레이어일 때만 
        if (gameObject.tag == TagType.Drill.ToString() && triggerObject.tag == TagType.Player.ToString())
        {
            DrillController.instance.AttachObject(transform);
            gameObject.tag = TagType.Player.ToString();
        }
    }
}
