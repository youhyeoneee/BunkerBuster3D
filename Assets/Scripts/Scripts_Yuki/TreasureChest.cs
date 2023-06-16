using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private GameObject[] treasures; // 보물 오브젝트들
    [SerializeField] private float explosionForce = 100f; // 튀어나가는 힘
    [SerializeField] private float explosionRadius = 100f; // 튀어나가는 범위

    private bool isOpened = false; // 보물상자가 열렸는지 여부
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.instance.gameState == GameStateType.Finished && !isOpened)
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (isOpened)
            return;

        isOpened = true;

        anim.SetBool(Animtype.Open.ToString(), true);

        foreach (GameObject treasure in treasures)
        {
            Rigidbody rb = treasure.GetComponent<Rigidbody>();
            if (rb == null)
                rb = treasure.AddComponent<Rigidbody>();

            // 보물에 힘을 가하고 랜덤한 방향으로 튀어나가도록 설정
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }
}
