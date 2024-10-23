using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUICharacterMenuManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject equipmentMenu;

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

    public void OpenEquipmentMenu()
    {
        Debug.Log("Button clicked");
        equipmentMenu.SetActive(true);
    }

    public void CloseEquipmentMenu()
    {
        equipmentMenu.SetActive(false);
    }
}
