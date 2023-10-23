using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject optionMenu;
    public GameObject mainMenu;


    public void OnOptionOpen()
    {
        mainMenu.LeanScale(Vector2.zero, .3f).setEaseInBack().setOnComplete(OptionEnable);
        optionMenu.LeanScale(Vector2.one, 0.5f);
    }

    public void OnOptionClose()
    {
        optionMenu.LeanScale(Vector2.zero, .3f).setEaseInBack().setOnComplete(OptionDisable);
        mainMenu.LeanScale(Vector2.one, 0.5f);
    }

    public void OptionEnable()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void OptionDisable()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void MainMenuEnable()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void MainMenuDisable()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
