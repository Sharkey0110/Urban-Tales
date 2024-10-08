using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUICharacterMenuManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] GameObject menu;

    public void OpenCharacterMenu()
    {
        PlayerUIManager.Instance.isMenuWindowOpen = true;
        menu.SetActive(true);
    }

    public void CloseCharacterMenu()
    {
        PlayerUIManager.Instance.isMenuWindowOpen = false;
        menu.SetActive(false);
        //close all other menus when they are created too
    }
}
