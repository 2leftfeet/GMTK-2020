using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    //public List<GameObject> levels;
    
    Transition transitionManager;

    void Awake()
    {
        transitionManager = GetComponent<Transition>();
    }

    

    void RestartLevel()
    {

    }

    void LoadNextLevel()
    {

    }
}
