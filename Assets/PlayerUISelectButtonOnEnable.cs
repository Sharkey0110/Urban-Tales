using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUISelectButtonOnEnable : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    //mostly for console stuff you dont need this yet
    private void OnEnable()
    {
        button.Select();
        //button.Select(null)
    }
}
