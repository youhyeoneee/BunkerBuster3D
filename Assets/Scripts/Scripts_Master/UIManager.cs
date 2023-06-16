using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumTypes;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject tapText;
    [SerializeField] private GameObject swipeText;
    [SerializeField] private Text gemText;
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject end;


    GameManager gm;

    bool swipeFlag = false;

    #region singleton
    public static UIManager instance = null;
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

    void Start()
    {
        gm = GameManager.instance;
        swipeText.SetActive(false);

        gemText.text = gm.gem.ToString();
    }

    void Update()
    {

        gemText.text = gm.gem.ToString();

        switch(gm.gameState)
        {
            case GameStateType.Ready:
                if (Input.GetMouseButtonDown(0))
                {
                    main.SetActive(false);
                    tapText.SetActive(false);

                }
            
                break;
            
            case GameStateType.Playing:
                if (tapText.activeSelf)
                    tapText.SetActive(false);

                if (!swipeFlag)
                {
                    StartCoroutine(EnableSwipeText());
                    swipeFlag = true;
                } 
                
                break;


            case GameStateType.Finished:
                if (!end.activeSelf)
                    end.SetActive(true);
                break;
        }
    }

    IEnumerator EnableSwipeText()
    {
        swipeText.SetActive(true);

        yield return new WaitForSeconds(2f);

        swipeText.SetActive(false);

    }
}
