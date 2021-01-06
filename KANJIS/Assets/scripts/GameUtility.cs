using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class GameUtility
{
    public const float ResolutionDelaytime=1;
    public const string SavePrefKey = "Game_Highscore_Value";

    public const string FileName = "Q";
    public static string FileDir
    {
        get
        {
            return Application.streamingAssetsPath + "/";
            
        }
    }
}
[System.Serializable()]

//Utility data class for questions from serialized xml file
public class Data
{
    public Question[] Questions = new Question[0];

    public Data() { }

    public static void Write(Data data, string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Data));
        //Stream data
        using(Stream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, data);
        }
    }
    //Fetch from file path
    public static Data Fetch(string filePath)
    {
        return Fetch(out bool result, filePath);
    }
    public static Data Fetch(out bool result, string filePath)
    {
        //Check if file already exist, if not create new Data object
        if (!File.Exists(filePath)) { result = false;return new Data(); }

        XmlSerializer deserializer = new XmlSerializer(typeof(Data));
        using (Stream stream = new FileStream(filePath, FileMode.Open))
        {
            var data = (Data)deserializer.Deserialize(stream);
            result = true; 
            return data;
        }
    }
}