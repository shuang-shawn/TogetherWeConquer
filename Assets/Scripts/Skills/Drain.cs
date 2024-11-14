using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drain : MonoBehaviour
{
    const string P1_TAG = "Player1";
    const string P2_TAG = "Player2";

    private GameObject player1;
    private GameObject player2;

    private DrainCircle player1DrainCircle;
    private DrainCircle player2DrainCircle;

    public GameObject drainCirclePrefab;

    private float duration = 10f;

    private Coroutine currentDrain;

    private void Start()
    {
        player1 = GameObject.Find(P1_TAG);
        player2 = GameObject.Find(P2_TAG);
    }

    public void CastDrain()
    {
        if (currentDrain != null)
        {
            EndDrainEarly();
        }
        else
        {
            player1DrainCircle = Instantiate(drainCirclePrefab, player1.transform.position + new Vector3(0, 0.001f, 0), Quaternion.Euler(-90, 0, 0), player1.transform).GetComponent<DrainCircle>();
            player1DrainCircle.transform.localScale = new Vector3(7.5f, 7.5f, 1);
            player2DrainCircle = Instantiate(drainCirclePrefab, player2.transform.position + new Vector3(0, 0.001f, 0), Quaternion.Euler(-90, 0, 0), player2.transform).GetComponent<DrainCircle>();
            player2DrainCircle.transform.localScale = new Vector3(7.5f, 7.5f, 1);

            currentDrain = StartCoroutine(EndDrain());
        }
    }

    private IEnumerator EndDrain()
    {
        yield return new WaitForSeconds(duration);

        player1DrainCircle.drain = false;
        player2DrainCircle.drain = false;

        Destroy(player1DrainCircle.gameObject, 0.1f);
        Destroy(player2DrainCircle.gameObject, 0.1f);

        currentDrain = null;
    }

    private void EndDrainEarly()
    {
        player1DrainCircle.drain = false;
        player2DrainCircle.drain = false;

        Destroy(player1DrainCircle.gameObject);
        Destroy(player2DrainCircle.gameObject);

        currentDrain = null;
    }
}
