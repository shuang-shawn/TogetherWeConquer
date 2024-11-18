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

    private int currXP;
    private int nextLevel = 100;
    private int level = 1;
    private bool hasEnded = false;
    public bool levelUp = false;

    // Start is called before the first frame update
    void Start()
    {
        playerManager1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerManager>();
        playerManager2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerManager>();
        bossManager = GameObject.FindGameObjectWithTag("boss").GetComponent<EnemyManager>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        HandleLevelUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasEnded) {
            if (playerManager1.IsDead() && playerManager2.IsDead()) {
                canvas.transform.Find("LoseWindow").gameObject.SetActive(true);
                canvas.transform.Find("ComboWindow").gameObject.SetActive(false);
                hasEnded = true;
            } else if (bossManager.IsDead()) {
                canvas.transform.Find("WinWindow").gameObject.SetActive(true);
                canvas.transform.Find("ComboWindow").gameObject.SetActive(false);
                hasEnded = true;
            }
        }

        if (currXP > nextLevel)
        {
            level += 1;
            currXP %= nextLevel;
            nextLevel += 25;

            HandleLevelUp();
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

    private void HandleLevelUp()
    {
        UnityEngine.Debug.Log("Level: " + level + "\nCurrXP: " + currXP + "\nNextLevel: " + nextLevel);

        LevelUp();

        canvas.transform.Find("LevelUpWindow").gameObject.SetActive(true);
    }
}
