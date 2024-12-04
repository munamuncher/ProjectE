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

        if(Input.GetKeyDown(KeyCode.T))
        {
            Inventory.InvenInst.AddItem(0, 10);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Inventory.InvenInst.AddItem(1, 10);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Inventory.InvenInst.UseItem(0, 1);
        }

    }

    private MonsterSpawner monSpawner;
    [Header("Game Stage Management")]
    [SerializeField]
    private int StageCounter;
    [SerializeField]
    private int MonsterCounter;
    [SerializeField]
    private int currentMonsterCount;
    [SerializeField]
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
                currentMonsterCount = MonsterCounter;
                break;
            case GameState.StageEnd:
                StageCounter++;
                MonsterCounter++;
                if(MonsterCounter >= 10)
                {
                    MonsterCounter = 3;
                }
                StartCoroutine(StageTimeController(GameState.StageStart));
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void DeathNotice()
    {
        currentMonsterCount--;
        if(currentMonsterCount <= 0)
        {
            GameStateController(GameState.StageEnd);
            Debug.Log("Stage has been cleard Stage end has been sumited and makeing a new stage");
        }
    }

    IEnumerator StageTimeController(GameState gamestate)
    {
        while (counter >= 0)
        {
            counter -= Time.deltaTime;
            yield return null;
        }

        GameStateController(gamestate);
        counter = 5;
    }
}
