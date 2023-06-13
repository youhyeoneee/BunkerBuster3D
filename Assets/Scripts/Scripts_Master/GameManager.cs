using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;

public class GameManager : MonoBehaviour
{

    public GameStateType gameState = GameStateType.Intro;
    [SerializeField] private MisslePlay misslePlay;

    #region singleton
    public static GameManager instance = null;
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
        gameState = GameStateType.Intro;
    }

    void Update()
    {
        switch(gameState)
        {
            case GameStateType.Ready:    
                if (Input.GetMouseButtonDown(0))
                {
                    misslePlay.Fall();
                }
                break;
        }

    }


    public void ReadyGame()
    {
        gameState = GameStateType.Ready;
    }

    public void StartGame()
    {
        gameState = GameStateType.Playing;
    }

}
