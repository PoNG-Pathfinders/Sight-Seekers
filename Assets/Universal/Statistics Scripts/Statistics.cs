using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Statistics : Singleton<Statistics> {

    public float[] weeklyDistances = new float[7];
    public DateTime[] invalidSaveDates = new DateTime[7];

    public void addDistance(float dist)
    {
        DateTime date = DateTime.Now;
        int dayOfWeek = (int)date.DayOfWeek;
        if (DateTime.Compare(date, invalidSaveDates[(int) date.DayOfWeek]) > 0)
        {
            weeklyDistances[dayOfWeek] = dist;
            invalidSaveDates[dayOfWeek] = date.AddDays(7);
        }
        else
        {
            weeklyDistances[dayOfWeek] += dist;
        }
    }

    void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < 7; i++)
            invalidSaveDates[i] = DateTime.Now.AddDays(i);
        load();
    }

    void OnDisable()
    {
        save();
        foreach (DateTime date in invalidSaveDates)
        {
            Debug.Log(date);
        }
    }

    public void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/statistics.dat");

        StatisticsData data = new StatisticsData();
        data.weeklyDistances = weeklyDistances;

        data.invalidSaveDates = new long[invalidSaveDates.Length];
        for (int i = 0; i < invalidSaveDates.Length; i++)
            data.invalidSaveDates[i] = invalidSaveDates[i].ToBinary();

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
            for (int i = 0; i < invalidSaveDates.Length; i++)
                invalidSaveDates[i] = new DateTime(data.invalidSaveDates[i]);
        }
    }
}

[Serializable]
class StatisticsData
{
    public float[] weeklyDistances;
    public long[] invalidSaveDates;
}