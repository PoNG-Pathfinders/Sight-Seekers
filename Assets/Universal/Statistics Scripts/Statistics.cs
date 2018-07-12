using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Statistics : Singleton<Statistics> {

    public int lastDaySaved;
    public float[] weeklyDistances = new float[7];

    void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        load();
    }

    void OnDisable()
    {
        save();
    }

    public void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/statistics.dat");

        StatisticsData data = new StatisticsData();
        data.dayOfWeek = (int) DateTime.Now.DayOfWeek;
        data.weeklyDistances = weeklyDistances;


        bf.Serialize(file, data);
        file.Close();
    }

    public void load()
    {
        if (File.Exists(Application.persistentDataPath + "/statistics.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/statistics.dat", FileMode.Open);
            StatisticsData data = (StatisticsData)bf.Deserialize(file);
            file.Close();

            weeklyDistances = data.weeklyDistances;
        }
    }
}

[Serializable]
class StatisticsData
{
    public int dayOfWeek;
    public float[] weeklyDistances;
}