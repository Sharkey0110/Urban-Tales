using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipment Model")]
public class EquipmentModel : ScriptableObject
{
    //Equipment type and name are both needed,
    //The type lets the game know which parent object to search through, saving time
    //The name lets the game know what equipment in that said model you want, ensuring you only equip one thing

    public EquipmentModelType equipmentModelType;
    public string equipmentName;

    //both set on the scriptable object created for each model

    public void LoadModel(PlayerManager player, bool isMale)
    {
        //Search through a list of all equipment models based on type
        //enable equipment that matches name

        switch (equipmentModelType)
        {
            //if the model is for a helmet run this code only
            case EquipmentModelType.Helmet:
                //for every helmet stored in the helmet parent object on the character model
                foreach (var model in player.playerEquipmentManager.helmetComponents)
                {
                    //find the one that has the same equipment name as this model
                    if (model.gameObject.name == equipmentName)
                    {
                        //and set it active
                        model.gameObject.SetActive(true);
                    }
                }
                break;

            case EquipmentModelType.Torso:
                //for torso, check if character is female or male, as soon chestplates with identical names will have different models
                //the character model will have two torso parent objects in the same place, male and female

                //TODO, maybe make a genral, male and female equipment type enum and only run male and female code if theres no general option, saves repeats
                if (isMale)
                {
                    foreach (var model in player.playerEquipmentManager.maleTorsoComponents)
                    {
                        if (model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                }

                else
                {
                    foreach(var model in player.playerEquipmentManager.femaleTorsoComponents)
                    {
                        if(model.gameObject.name == equipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                }

                break;

            case EquipmentModelType.Back:
                break;

            case EquipmentModelType.RightShoulder:
                foreach (var model in player.playerEquipmentManager.rightShoulderComponents)
                {
                    if (model.gameObject.name == equipmentName)
                    {
                        model.gameObject.SetActive(true);
                    }
                }
                break;

            case EquipmentModelType.RightUpperArm:
                break;

            case EquipmentModelType.RightElbow:
                break;

            case EquipmentModelType.RightLowerArm:
                break;

            case EquipmentModelType.RightHand:
                break;

            case EquipmentModelType.LeftShoulder:
                foreach (var model in player.playerEquipmentManager.leftShoulderComponents)
                {
                    if (model.gameObject.name == equipmentName)
                    {
                        model.gameObject.SetActive(true);
                    }
                }
                break;

            case EquipmentModelType.LeftUpperArm:
                break;

            case EquipmentModelType.LeftElbow:
                break;

            case EquipmentModelType.LeftLowerArm:
                break;

            case EquipmentModelType.LeftHand:
                break;

            case EquipmentModelType.Hips:
                break;

            case EquipmentModelType.HipAttachment:
                break;

            case EquipmentModelType.RightLeg:
                break;

            case EquipmentModelType.RightKnee:
                break;

            case EquipmentModelType.LeftLeg:
                break;

            case EquipmentModelType.LeftKnee:
                break;
        }
    }
}
