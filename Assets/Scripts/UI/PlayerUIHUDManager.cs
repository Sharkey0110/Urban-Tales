using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIHUDManager : MonoBehaviour
{
    [SerializeField] UI_StatBar hpBar;
    //   [SerializeField] UI_StatBar mpBar;

    [SerializeField] CanvasGroup[] canvasGroup;


    public void ToggleHUD(bool status)
    {
        if (status)
        {
            foreach (var canvas in canvasGroup)
            {
                canvas.alpha = 1;
            }
        }

        else
        {
            foreach(var canvas in canvasGroup)
            {
                canvas.alpha = 0;
            }
        }


    }

    public void SetNewHPValue(int oldValue, int newValue)
    {
        hpBar.SetStat(newValue);
    }

    public void SetMaxHPValue(int hp)
    {
        hpBar.SetMaxStat(hp);
    }
}
