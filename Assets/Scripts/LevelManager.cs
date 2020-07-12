using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    public List<GameObject> levels;

    void Awake()
    {
        instance = this;
    }

    void RestartLevel()
    {

    }

    void LoadNextLevel()
    {

    }
}
