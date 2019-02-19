using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData
{
    //Placeholder script for saving and loading data
    /*private static readonly string fileName = string.Format("Save.sav");
    private static readonly string filePath = string.Format("{0}/{1}", Application.dataPath, fileName);

    public static void Save(CardClick data)
    {
        using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            using (MemoryStream memory = new MemoryStream())
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
                    formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(memory, new CardClick(data.Placeholder, ()));
                writer.Write(memory.ToArray());
            }
        }
    }

    public static CardClick Load()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("Error. Cannot find save file!");
            return null;
        }
    using (FileStream stream = new FileStream(filePath, FileMode.Open))
    using (BinaryReader reader = new BinaryReader(stream))
    {
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = newSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        return (DataHolder)formatter.Deserialize(stream);
    }
    }*/
}
