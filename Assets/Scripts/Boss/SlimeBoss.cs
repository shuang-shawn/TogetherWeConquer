using System.Collections;
using UnityEngine;

public class SlimeBoss : MonoBehaviour
{
    private Vector3 startPosition;
    private GameObject closestPlayerObj = null;
    public float speed = 2.0f;
    public float hopHeight = 3.0f;
    public float hopFrequency = 1f;
    public float jumpSpeed = 20f;
    public float dropSpeed = 5f;
    private float hopMotion;
    private bool jumpAttacking = false;
    private float jumpAttackHeight = 10f;
    float jumpAttackLandingTimer = 0f;
    public GameObject shadow;
    private Vector3 landingPoint = new Vector3(0, 0, 0);
    private Coroutine stopwatchCoroutine;
    public int stayInAir = 5;
    public int landingDelay = 2;
    public float speedPercent = 1.0f;
    public float previousSpeedPercent = 1.0f;
    public float timePassed = 0f;
    public float specialAttackInterval = 4f;
    public bool IsDead = false;
    public ParticleSystem shockwavePrefab;

    // For slime splitting

    private static int slimeID = 1;
    public GameObject slimeBossPrefab;
    private int maxSplitSlimes = 3;
    public int currentSlimeMaxHealth;
    public void handleCollision(GameObject gameObject)
    {
        if (gameObject.tag == "TestCombo")
        {
            string hitMessage = "Hit by Combo";
            Debug.Log(hitMessage);
        }
        else if (gameObject.tag == "Player1")
        {
            gameObject.GetComponent<PlayerStats>().playerGotHit(damage);
        }
    }

    private void findClosestPlayer()
    {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
        GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
        float player1Distance = 9999f;
        float player2Distance = 9999f;

        if (player1 != null && player1.activeInHierarchy)
        {
            player1Distance = findDistance(player1);
        }
        if (player2 != null && player2.activeInHierarchy)
        {
            player2Distance = findDistance(player2);
        }

        if (player1 != null && player1.activeInHierarchy && (player2 == null || !player2.activeInHierarchy || player1Distance <= player2Distance))
        {
            closestPlayerObj = player1;
        }
        else if (player2 != null && player2.activeInHierarchy)
        {
            closestPlayerObj = player2;
        }
        else
        {
            closestPlayerObj = null;
        }

        if (closestPlayerObj == null)
        {
            Debug.LogWarning("No player found. closestPlayerObj is null.");
        }
    }

    private float findDistance(GameObject targetObject)
    {
        return Vector3.Distance(transform.position, targetObject.transform.position);
    }

    private void controlHopping()
    {
        hopMotion = Mathf.Sin(Time.time * hopFrequency) * hopHeight;
    }

    private void StartStopwatch()
    {
        if (stopwatchCoroutine == null)
        {
            stopwatchCoroutine = StartCoroutine(StopwatchCoroutine());
        }
    }

    private void StopStopwatch()
    {
        if (stopwatchCoroutine != null)
        {
            StopCoroutine(stopwatchCoroutine);
            stopwatchCoroutine = null;
            jumpAttackLandingTimer = 0;
        }
    }

    private IEnumerator StopwatchCoroutine()
    {
        while (true)
        {
            jumpAttackLandingTimer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPoint, float moveSpeed)
    {
        while (Vector3.Distance(transform.position, targetPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPoint;
    }

    private IEnumerator TrackPlayer()
    {
        StartStopwatch();
        while (jumpAttackLandingTimer < stayInAir)
        {
            if (closestPlayerObj != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(closestPlayerObj.transform.position.x, jumpAttackHeight, closestPlayerObj.transform.position.z), speed * Time.deltaTime * 2);
            }
            else
            {
                Debug.LogWarning("closestPlayerObj is null. Stopping TrackPlayer coroutine.");
                StopStopwatch();
                yield break;
            }
            yield return null;
        }
        StopStopwatch();
    }

    private IEnumerator JumpAttackSequence()
    {
        yield return StartCoroutine(MoveToPosition(new Vector3(transform.position.x, jumpAttackHeight, transform.position.z), jumpSpeed));
        GameObject instantiatedShadow = Instantiate(shadow, new Vector3(transform.position.x, 0.1f, transform.position.z), Quaternion.identity);

        yield return StartCoroutine(TrackPlayer());

        yield return StartCoroutine(MoveToPosition(new Vector3(transform.position.x, startPosition.y, transform.position.z), dropSpeed));
        Destroy(instantiatedShadow);
        //Play shockwave
        playShockwave();

        //Daze effect

        yield return new WaitForSeconds(landingDelay);
        jumpAttacking = false;
        timePassed = 0f;
    }

    private void playShockwave()
    {

        Vector3 slimePosition = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);

        ParticleSystem shockwaveEffect = Instantiate(shockwavePrefab, slimePosition, shockwavePrefab.transform.rotation);
        shockwaveEffect.Play();
        Destroy(shockwaveEffect.gameObject, shockwaveEffect.main.duration);
    }

    private void revisedJumpAttack()
    {
    private void revisedJumpAttack()
    {
        StartCoroutine(JumpAttackSequence());
    }

    private void HopToPlayer()
    {
        if (closestPlayerObj == null)
        {
            return;
        }
        Vector3 playerPosition = new Vector3(closestPlayerObj.transform.position.x, startPosition.y, closestPlayerObj.transform.position.z);

        if (hopMotion > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, startPosition.y + hopMotion, transform.position.z);
        }
    }

    void Start()
    {
        startPosition = transform.position;
        if (slimeID == 1)
        {
            currentSlimeMaxHealth = gameObject.GetComponent<EnemyManager>().getMaxBossHealth();
        }
    }

    private void updateSpeed(float percent)
    {
        speed *= percent;
        hopFrequency *= percent;
        jumpSpeed *= percent;
        dropSpeed *= percent;
    }

    void ResetSpeed()
    {
        speed = 2.0f;
        jumpSpeed = 20f;
        dropSpeed = 20f;
        hopFrequency = 5f;
    }

    // For slime splitting mechanic
    private void splitSlime()
    {
        spawnSlime();
        spawnSlime();

    }

    private void spawnSlime()
    {
        slimeID++;
        GameObject tempSlime = Instantiate(slimeBossPrefab, gameObject.transform.position, Quaternion.identity);
        tempSlime.GetComponent<SlimeBoss>().updateMaxHealth(currentSlimeMaxHealth / 2);

        Debug.Log("What did i set currentSlimeMaxHealth to: " + tempSlime.GetComponent<SlimeBoss>().currentSlimeMaxHealth);
        Debug.Log("What is my current hp: " + tempSlime.GetComponent<EnemyManager>().currentHealth);

        tempSlime.transform.localScale = gameObject.transform.localScale * 0.5f;
        // tempSlime.GetComponent<EnemyManager>().setMaxHealth(maxHealth / 2)
    }

    public void updateMaxHealth(int newMaxHealth)
    {
        currentSlimeMaxHealth = newMaxHealth;
        gameObject.GetComponent<EnemyManager>().setMaxHealth(currentSlimeMaxHealth);
    }

    void FixedUpdate()
    {
        if (!IsDead)
        {
            controlHopping();

            timePassed += Time.deltaTime;
            if (previousSpeedPercent != speedPercent)
            {
                if (speedPercent == 1)
                {
                    ResetSpeed();
                }
                else
                {
                    ResetSpeed();
                    updateSpeed(speedPercent);
                }
                previousSpeedPercent = speedPercent;
            }

            findClosestPlayer();

            if (timePassed >= specialAttackInterval && !jumpAttacking && hopMotion < -0.9f)
            {
                jumpAttacking = true;
                revisedJumpAttack();
            }

            if (!jumpAttacking)
            {
                HopToPlayer();
            }

            // if(jumpAttacking){
            //     jumpAttack();
            // } else {
            //     HopToPlayer();
            // }

        }
        else if (slimeID < maxSplitSlimes)
        {
            splitSlime();
            Destroy(gameObject);
        }


        // if(Input.GetKey(KeyCode.Space)) {

        // }
    }
}

public void Pause()
{
    enabled = false;
}

public void Resume()
{
    enabled = true;
}
}
