using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnvir : MonoBehaviour
{
    private void Awake()
    {
        int gameEnvirCount = FindObjectsOfType<GameEnvir>().Length;

        if (gameEnvirCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
