using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveFileDataWriter
{
    public string saveDataDirectoryPath = "";
    public string saveFileName = "";

    //Before we create a new save file, check if one of this character already exists (MAX 3 files)
    public bool CheckToSeeIfFileExists()
    {
        if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //delete save files obvs
    public void DeleteSaveFile()
    {
        File.Delete(Path.Combine(saveDataDirectoryPath,saveFileName));
    }

    //create/overwrite files
    public void CreateNewCharacterSaveFile(CharacterSaveData characterData)
    {
        //make a path to save the file
        string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

        try
        {
            //Create the directory if it does not exist
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            Debug.Log("Creating save file at " + savePath);

            //change the c# game data object into storable JSON
            string dataToStore = JsonUtility.ToJson(characterData, true);

            //write the converted JSON to our device
            //open up a file stream in creation mode in this location
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                //open a writer in the stream
                using (StreamWriter fileWriter = new StreamWriter(stream))
                {
                    //write data
                    fileWriter.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error trying to save" + e.Message);
        }
    }

    //load
    public CharacterSaveData LoadSaveFile()
    {
        CharacterSaveData characterData = null;

        string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

        if(File.Exists(loadPath))
        {
            try
            {
                string dataToLoad = "";

                //find and open the file in open mode if it exists
                using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //change JSON to unity
                characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Something went wrong" + e.Message);
            }

        }

        return characterData;
    }
}
