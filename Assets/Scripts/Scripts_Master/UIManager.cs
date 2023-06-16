using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnumTypes;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject levelText;
    [SerializeField] private GameObject tapText;
    [SerializeField] private GameObject swipeText;
    [SerializeField] private Text gemText;

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

        levelText.SetActive(false);
        tapText.SetActive(false);
        swipeText.SetActive(false);

        gemText.text = gm.gem.ToString();
    }

    void Update()
    {

        gemText.text = gm.gem.ToString();

        switch(gm.gameState)
        {
            case GameStateType.Ready:
                if (!levelText.activeSelf)
                    levelText.SetActive(true);

                if (!tapText.activeSelf)
                    tapText.SetActive(true);
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


        }
    }

    IEnumerator EnableSwipeText()
    {
        swipeText.SetActive(true);

        yield return new WaitForSeconds(2f);

        swipeText.SetActive(false);

    }
}
