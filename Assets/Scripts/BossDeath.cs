using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    void Die()
    {
        Destroy(transform.parent.gameObject);
    }
}
