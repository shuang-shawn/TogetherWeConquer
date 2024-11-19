using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using Unity.UI;

public class GameStateManager : MonoBehaviour
{
    public PlayerManager playerManager1;
    public PlayerManager playerManager2;
    private EnemyManager bossManager;
    public GameObject canvas;
    public MobSpawner mobSpawner;

    public int currXP;
    public int nextLevel = 100;
    public int level = 1;
    private bool hasEnded = false;
    public bool levelUp = false;
    public bool isPlayer1Level = true;
    public bool duoLevel = false;
    private GameObject lastBoss = null;
    private bool lastBossSpawned = false;


    // Start is called before the first frame update
    void Start()
    {
        playerManager1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerManager>();
        playerManager2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerManager>();
        // bossManager = GameObject.FindGameObjectWithTag("boss").GetComponent<EnemyManager>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        StartCoroutine(HandleStart());
    }

    private IEnumerator HandleStart()
    {
        StartCoroutine(HandleLevelUp());
        while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
        {
            yield return null;
        }
        UnityEngine.Debug.Log("Duo level");
        duoLevel = true;
        StartCoroutine(HandleLevelUp());
        mobSpawner.SpawnMobs();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasEnded) 
        {
            if (playerManager1.IsDead() && playerManager2.IsDead())
            {
                canvas.transform.Find("LoseWindow").gameObject.SetActive(true);
                canvas.transform.Find("ComboWindow").gameObject.SetActive(false);
                hasEnded = true;
            }
            // bossManager = GameObject.FindGameObjectWithTag("boss")?.GetComponent<EnemyManager>();
            if (lastBossSpawned && lastBoss == null)
            {
                canvas.transform.Find("WinWindow").gameObject.SetActive(true);
                canvas.transform.Find("ComboWindow").gameObject.SetActive(false);
                canvas.transform.Find("LevelUpWindow").gameObject.SetActive(false);
                hasEnded = true;
                
            }
        }

        if (currXP >= nextLevel)
        {
            level += 1;
            currXP %= nextLevel;
            nextLevel += 25;

            StartCoroutine(HandleLevelUp());
            if (level % 2 == 0) {

                GameObject boss = mobSpawner.SpawnBoss();
                if (mobSpawner.isLastBoss() && boss != null) {
                    lastBossSpawned = true;
                    lastBoss = boss;

                }
            } else {
                mobSpawner.SpawnMobs();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddXP(nextLevel + 5);
        }
    }

    public void LevelUp()
    {

        levelUp = !levelUp;
    }

    public void AddXP(int xp)
    {
        currXP += xp;
    }
    public void HandleBossDeath() {
        StartCoroutine(HandleLevelUp());
        mobSpawner.SpawnMobs();
    }

    private IEnumerator HandleLevelUp()
    {
        UnityEngine.Debug.Log("Level: " + level + "\nCurrXP: " + currXP + "\nNextLevel: " + nextLevel);
        UnityEngine.Debug.Log(duoLevel);

        LevelUp();

        if (!duoLevel)
        {
            UnityEngine.Debug.Log("Level up player 1");
            canvas.transform.Find("LevelUpWindow").gameObject.SetActive(true);
            isPlayer1Level = false;
            while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
            {
                yield return null; // Wait for the next frame
            }
            UnityEngine.Debug.Log("Level up player 2");
            canvas.transform.Find("LevelUpWindow").gameObject.SetActive(true);
            isPlayer1Level = true;
        }
        else
        {
            canvas.transform.Find("LevelUpWindow").gameObject.SetActive(true);
        }

        while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
        {
            yield return null;
        }

        duoLevel = false;
     
        
    }


}
