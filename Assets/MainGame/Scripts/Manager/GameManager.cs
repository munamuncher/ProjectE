using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Start,
    StageStart,
    StageEnd,
    Pause,
    GameOver
}


public class GameManager : MonoBehaviour
{
    #region _SingleTon_

    private static GameManager instance;
    public static GameManager GMInst => instance;

    private void Awake()
    {
        if(instance != this && instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion


    private void Start()
    {
        GameStateController(GameState.Start);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            monSpawner.SpawnMonster(2, 2);
            monSpawner.ReciveMonsterGameObject(2);
        }
    }

    private MonsterSpawner monSpawner;
    [SerializeField]
    private int StageCounter = 0;
    private int MonsterCounter = 5;
    private float counter = 5;
    private GameState currentGameState = GameState.Start;

    public void GameStateController(GameState gameState)
    {
        monSpawner = MonsterSpawner.mInst;
        currentGameState = gameState;
        switch (gameState)
        {
            case GameState.Start:
                StartCoroutine(StageTimeController(GameState.StageStart));
                break;
            case GameState.StageStart:
                monSpawner.ReciveMonsterGameObject(StageCounter);
                monSpawner.SpawnMonster(StageCounter, MonsterCounter);
                break;
            case GameState.StageEnd:
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                break;
        }
    }

    IEnumerator StageTimeController(GameState gamestate)
    {
        while (counter < 0)
        {
            counter -= Time.deltaTime;
            yield return null;
        }

        GameStateController(gamestate);
        counter = 5;
    }
}
