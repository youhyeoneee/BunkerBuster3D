using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : MonoBehaviour
{

    public bool isnextGimmikActivated = false;

    [SerializeField] private int enableGimmickCnt = 3;

    [SerializeField] private List<GameObject> gimmicks;

    int idx;

    #region singleton
    public static GimmickManager instance = null;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }    
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion


    private void Start() 
    {

        // GetChild() 메서드를 사용하여 자식 오브젝트 얻기
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            gimmicks[i] = child.gameObject;
        }


        // for(int i=0; i<gimmicks.Count; i++)
        // {
        //     if(i < enableGimmickCnt) // 1, 2, 3 활성화 
        //         gimmicks[i].SetActive(true);
        //     else // 나머지 비활성화 
        //         gimmicks[i].SetActive(false);    
        // }

        // index = 3;
        // idx = enableGimmickCnt;
    }

    // 보이는 기믹을 특정 개수로 유지
    void Update()
    {
        // if (isnextGimmikActivated)
        //     StartCoroutine(EnableNextGimmick());
    }

    IEnumerator EnableNextGimmick()
    {
        isnextGimmikActivated = false;

        yield return new WaitForSeconds(0.2f);

        if (idx < gimmicks.Count)
        {
            gimmicks[idx].SetActive(true);
            idx++;
        }
    }
}
