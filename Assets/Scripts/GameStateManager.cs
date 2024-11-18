using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using Unity.UI;

public class GameStateManager : MonoBehaviour
{
    public PlayerManager playerManager1;
    public PlayerManager playerManager2;
    public EnemyManager bossManager;
    public GameObject canvas;
    public MovementTutorial moveTutorial;
    public ComboTutorial comboTutorial;

    private int currXP;
    private int nextLevel = 100;
    private int level = 1;
    private bool hasEnded = false;
    private bool tutorial = false;
    public bool levelUp = false;
    public bool isPlayer1Level = true;
    public bool duoLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        playerManager1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerManager>();
        playerManager2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerManager>();
        moveTutorial = null;
        if(GameObject.FindGameObjectWithTag("moveTutorial"))
        {
            moveTutorial = GameObject.FindGameObjectWithTag("moveTutorial").GetComponent<MovementTutorial>();
            GameObject.FindGameObjectWithTag("moveTutorial").SetActive(false);
        }
        comboTutorial = null;
        if (GameObject.FindGameObjectWithTag("comboTutorial"))
        {
            comboTutorial = GameObject.FindGameObjectWithTag("comboTutorial").GetComponent<ComboTutorial>();
            GameObject.FindGameObjectWithTag("comboTutorial").SetActive(false);
        }
        bossManager = null;
        if (GameObject.FindGameObjectWithTag("boss"))
        {
            bossManager = GameObject.FindGameObjectWithTag("boss").GetComponent<EnemyManager>();
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
        StartCoroutine(HandleLevelUp());
        while (canvas.transform.Find("LevelUpWindow").gameObject.activeSelf)
        {
            yield return null;
        }
        UnityEngine.Debug.Log("Duo level");
        duoLevel = true;
        StartCoroutine(HandleLevelUp());
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
            if (bossManager != null)
            {
                if (bossManager.IsDead())
                {
                    canvas.transform.Find("WinWindow").gameObject.SetActive(true);
                    canvas.transform.Find("ComboWindow").gameObject.SetActive(false);
                    hasEnded = true;
                }
            }
            else if (GameObject.FindGameObjectWithTag("boss"))
            {
                bossManager = GameObject.FindGameObjectWithTag("boss").GetComponent<EnemyManager>();
            }
        }

        if (currXP > nextLevel)
        {
            level += 1;
            currXP %= nextLevel;
            nextLevel += 25;

            StartCoroutine(HandleLevelUp());
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

    private IEnumerator Tutorial()
    {
        Time.timeScale = 1.0f;

        moveTutorial.Play();

        while (canvas.transform.Find("MoveTutorialWindow").gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }

        UnityEngine.Debug.Log("Done Movement Tutorial");

        comboTutorial.Play();

        while (canvas.transform.Find("Dim").gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }

        tutorial = false;
    }
}
