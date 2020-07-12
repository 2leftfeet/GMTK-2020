using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Animator StartButton;
    [SerializeField]
    Animator ExitButton;
    [SerializeField]
    Animator Title;

    [SerializeField] Animator Controls;
    [SerializeField] ToggleMeniuCamera ToggleMeniuCamera;

    bool isStillIntro = false;
    bool isInMeniu = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isStillIntro)
        {
            if (!isInMeniu)
                EnterMeniu();
            else
                ExitMeniu();
        }



    }

    public void ExitIntro()
    {
        isStillIntro = false;
        StartButton.SetTrigger("hide");
        ExitButton.SetTrigger("hide");
        Title.SetTrigger("hide");
        Controls.SetTrigger("show");
    }


    void ExitMeniu()
    {
        isInMeniu = false;
        ExitButton.SetTrigger("hide");
        Title.SetTrigger("hide");
        ToggleMeniuCamera.ToggleCamera();
    }

    void EnterMeniu()
    {
        isInMeniu = true;
        ExitButton.SetTrigger("show");
        Title.SetTrigger("show");
        ToggleMeniuCamera.ToggleCamera();
    }

    public void ExitAplication()
    {
        Application.Quit();
    }
}
