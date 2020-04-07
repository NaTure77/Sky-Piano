using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class DataManager
{


    //public static List<Word> words = new List<Word>();
    public static string fileName = "Untitle";
    public static int rythmType = 16;
    //public static string fileName;
    //public static int rythmType = 16;
    //public static string instrumentType = "C";
    //public static int bpm = 240;
    //public static float delay = 0.25f;
    //public static int toneLevel = 0;
    //public static int metronomeBpm = 240;
    public static PlayerData userData = null;
    public static void LoadPlayerData()
    {
        
        if (File.Exists(Application.persistentDataPath + "/" + "playerData.dat"))
        {
            Debug.Log(Application.persistentDataPath);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/" + "playerData.dat", FileMode.Open);
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            userData = data;
            stream.Close();
        }
        else userData = new PlayerData();
        
    }
    public static void SavePlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/" + "playerData.dat", FileMode.Create);
        formatter.Serialize(stream, userData);
        stream.Close();
    }
    [System.Serializable]
    public class PlayerData // 연습모드 데이터
    {
        public string instrumentType = "Piano";
        public int padSize = 300;
        public int tone = 0;
        public int bpm = 60;
        public PlayerData()
        {

        }

    }

}
