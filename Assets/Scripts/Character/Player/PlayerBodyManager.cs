using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyManager : MonoBehaviour
{
    //All of this means nothing right now, but when i have a model, i can disable their head when i put on a helmet, so it doesnt have to be commically big
    //and engulf the actual shape and size of the head to avoid clipping

    [Header("Hair")]
    public GameObject hair;

    [Header("Male")]
    public GameObject maleHead; //head
    public GameObject[] maleBody; //upper body chest shoulders
    public GameObject[] maleArms; //arms elbows hands
    public GameObject[] maleLegs; //legs

    [Header("Female")]
    public GameObject femaleHead;
    public GameObject[] femaleBody;
    public GameObject[] femaleArms;
    public GameObject[] femaleLegs;

    public void EnableHead()
    {
        maleHead.SetActive(true);
        femaleHead.SetActive(true);
    }

    public void DisableHead()
    {
        maleHead.SetActive(false);
        femaleHead.SetActive(false);
    }

    public void EnableHair()
    {
        hair.SetActive(true);
    }

    public void DisableHair()
    {
        hair.SetActive(false);
    }

}
