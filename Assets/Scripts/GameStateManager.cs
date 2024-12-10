using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Unity.UI;

public class GameStateManager : MonoBehaviour
{
    public PlayerManager playerManager1;
    public PlayerManager playerManager2;
    private EnemyManager bossManager;
    public GameObject canvas;
    public MobSpawner mobSpawner;
    public MovementTutorial moveTutorial;
    public LevelUpTutorial levelUpTutorial;
    public ComboTutorial comboTutorial;
    public DuoComboTutorial duoComboTutorial;
    public DeathTutorial deathTutorial;
    public ExpBar expBar;
    public AudioSource[] songs;

    public int currXP;
    public int nextLevel = 100;
    public int level = 1;
    private bool hasEnded = false;
    private bool tutorial = false;
    public bool levelUp = false;
    public bool isPlayer1Level = true;
    public bool duoLevel = false;
    private GameObject lastBoss = null;
    private bool lastBossSpawned = false;
    private AudioSource enemyTheme;
    private AudioSource bossTheme;
    private AudioSource pause;
    private AudioSource bossTwoTheme;
    private bool isBoss = false;
    private bool isStarted = false;



    // Start is called before the first frame update
    void Start()
    {
        enemyTheme = songs[0];
        bossTheme = songs[1];
        pause = songs[2];
        bossTwoTheme = songs[3];

        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorial = true;
        }

        playerManager1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerManager>();
        playerManager2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerManager>();

        moveTutorial = null;
        if(GameObject.FindGameObjectWithTag("moveTutorial"))
        {
            moveTutorial = GameObject.FindGameObjectWithTag("moveTutorial").GetComponent<MovementTutorial>();
            GameObject.FindGameObjectWithTag("moveTutorial").SetActive(false);
        }
        levelUpTutorial = null;
        if (GameObject.FindGameObjectWithTag("levelUpTutorial"))
        {
            levelUpTutorial = GameObject.FindGameObjectWithTag("levelUpTutorial").GetComponent<LevelUpTutorial>();
            GameObject.FindGameObjectWithTag("levelUpTutorial").SetActive(false);
        }
        comboTutorial = null;
        if (GameObject.FindGameObjectWithTag("comboTutorial"))
        {
            comboTutorial = GameObject.FindGameObjectWithTag("comboTutorial").GetComponent<ComboTutorial>();
            GameObject.FindGameObjectWithTag("comboTutorial").SetActive(false);
        }
        duoComboTutorial = null;
        if (GameObject.FindGameObjectWithTag("duoComboTutorial"))
        {
            duoComboTutorial = GameObject.FindGameObjectWithTag("duoComboTutorial").GetComponent<DuoComboTutorial>();
            GameObject.FindGameObjectWithTag("duoComboTutorial").SetActive(false);
        }
        deathTutorial = null;
        if (GameObject.FindGameObjectWithTag("deathTutorial"))
        {
            deathTutorial = GameObject.FindGameObjectWithTag("deathTutorial").GetComponent<DeathTutorial>();
            GameObject.FindGameObjectWithTag("deathTutorial").SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("completeTutorial"))
        {
            GameObject.FindGameObjectWithTag("completeTutorial").SetActive(false);
        }

        bossManager = null;
        if (GameObject.FindGameObjectWithTag("boss"))
        {
            // bossManager = GameObject.FindGameObjectWithTag("boss").GetComponent<EnemyManager>();
        }
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        if (!tutorial)
        {
            StartCoroutine(HandleStart());
        }
        else
        {
            StartCoroutine(Tutorial());
        }
    }

    private IEnumerator HandleStart()
    {
        pause.Play();
        StartCoroutine(HandleLevelUp());
        while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
        {
            yield return null;
        }
        UnityEngine.Debug.Log("Duo level");
        duoLevel = true;
        StartCoroutine(HandleLevelUp());
        mobSpawner.SpawnMobs();
        while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
        {
            yield return null;
        }
        pause.Stop();

        isStarted = true;
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
                Time.timeScale = 1.0f;
            }
            // bossManager = GameObject.FindGameObjectWithTag("boss")?.GetComponent<EnemyManager>();
            if (lastBossSpawned && lastBoss == null)
            {
                canvas.transform.Find("WinWindow").gameObject.SetActive(true);
                canvas.transform.Find("ComboWindow").gameObject.SetActive(false);
                canvas.transform.Find("LevelUpWindow").gameObject.SetActive(false);
                hasEnded = true;
                Time.timeScale = 1.0f;
                
            }
            
        }

        if (currXP >= nextLevel)
        {
            expBar.SetExp(currXP, nextLevel);
            level += 1;
            currXP %= nextLevel;
            nextLevel += 25;

            StartCoroutine(HandleLevelUp());
            if (level % 2 == 0) {
                GameObject boss = mobSpawner.SpawnBoss();
                if (mobSpawner.isLastBoss() && boss != null) {
                    lastBossSpawned = true;
                    lastBoss = boss;

                    bossTheme = bossTwoTheme;
                }

                isBoss = true;
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
        expBar.SetExp(currXP, nextLevel);
    }
    public void HandleBossDeath() {
        StartCoroutine(HandleLevelUp());
        mobSpawner.SpawnMobs();

        isBoss = false;
        bossTheme.Stop();
    }

    private IEnumerator HandleLevelUp()
    {
        UnityEngine.Debug.Log("Level: " + level + "\nCurrXP: " + currXP + "\nNextLevel: " + nextLevel);
        UnityEngine.Debug.Log(duoLevel);

        if (enemyTheme.isPlaying)
        {
            enemyTheme.Pause();
        }
        else
        {
            bossTheme.Pause();
        }

        if (isStarted)
        {
            pause.Play();
        }

        LevelUp();
        UnityEngine.Debug.Log(levelUp);

        if (!duoLevel)
        {
            UnityEngine.Debug.Log("Level up player 1");
            canvas.transform.Find("LevelUpWindow").gameObject.SetActive(true);
            while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
            {
                yield return null; // Wait for the next frame
            }
            isPlayer1Level = false;
            UnityEngine.Debug.Log("Level up player 2");
            canvas.transform.Find("LevelUpWindow").gameObject.SetActive(true);
        }
        else
        {
            canvas.transform.Find("LevelUpWindow").gameObject.SetActive(true);
        }

        while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
        {
            yield return null;
        }

        if(isStarted)
        {
            pause.Stop();
        }

        if (!tutorial)
        {
            if (isBoss)
            {
                bossTheme.Play();
            }
            else
            {
                enemyTheme.Play();
            }
        }

        isPlayer1Level = true;

        LevelUp();
        UnityEngine.Debug.Log(levelUp);

        duoLevel = false;
        expBar.SetExp(currXP, nextLevel);
        
    }

    private IEnumerator Tutorial()
    {
        Time.timeScale = 1.0f;

        moveTutorial.Play();

        while (canvas.transform.Find("MoveTutorialWindow").gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }

        UnityEngine.Debug.Log("Done Movement Tutorial");

        StartCoroutine(HandleLevelUp());

        levelUpTutorial.Play();

        while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }

        comboTutorial.Play();

        while (canvas.transform.Find("ComboTutorialWindow").gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }

        UnityEngine.Debug.Log("Done Combo Tutorial");

        duoLevel = true;

        StartCoroutine(HandleLevelUp());

        levelUpTutorial.Play();

        while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }

        duoComboTutorial.Play();

        while (canvas.transform.Find("DuoComboTutorialWindow").gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }

        UnityEngine.Debug.Log("Done Duo Combo Tutorial");

        deathTutorial.Play();

        while (canvas.transform.Find("DeathTutorialWindow").gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }

        StartCoroutine(CompleteTutorial());

        while (canvas.transform.Find("CompleteTutorialWindow").gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }

        tutorial = false;

        SceneManager.LoadScene("Title Screen");
    }

    private IEnumerator CompleteTutorial()
    {
        canvas.transform.Find("CompleteTutorialWindow").gameObject.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        canvas.transform.Find("CompleteTutorialWindow").gameObject.SetActive(false);
    }
}
