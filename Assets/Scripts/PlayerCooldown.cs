using System.Collections;
using UnityEngine;

public class PlayerCooldown : MonoBehaviour
{
    private bool isInCooldown = false;

    public bool IsInCooldown()
    {
        return isInCooldown;
    }

    public void StartCooldown(float duration)
    {
        if (!isInCooldown)
        {
            StartCoroutine(CooldownCoroutine(duration));
        }
    }

    private IEnumerator CooldownCoroutine(float duration)
    {
        isInCooldown = true;
        yield return new WaitForSeconds(duration);
        isInCooldown = false;
    }
}
