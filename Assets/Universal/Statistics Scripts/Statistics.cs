using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Statistics : Singleton<Statistics> {

    public float[] weeklyDistances = new float[7];
    private DateTime[] invalidDates = new DateTime[7];

    public void addDistance(float dist)
    {
        DateTime date = DateTime.Today;

        int dayOfWeek = (int)date.DayOfWeek;
        if (DateTime.Compare(date, invalidDates[(int) date.DayOfWeek]) > 0)
        {
            weeklyDistances[dayOfWeek] = dist;
            invalidDates[dayOfWeek] = date.AddDays(7);
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
            invalidDates[i] = DateTime.Now.AddDays(i);
        load();
    }

    void OnDisable()
    {
        save();
        foreach (DateTime date in invalidDates)
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

        data.invalidDates = new long[invalidDates.Length];
        for (int i = 0; i < invalidDates.Length; i++)
            data.invalidDates[i] = invalidDates[i].ToBinary();

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
            for (int i = 0; i < invalidDates.Length; i++)
                invalidDates[i] = new DateTime(data.invalidDates[i]);
        }
        else
        {
            weeklyDistances = new float[7];
            invalidDates = new DateTime[7];

            int dayOfWeek = (int) DateTime.Today.DayOfWeek;
            for (int i = 0; i < 7; i++)
            {
                int offset = i - dayOfWeek;
                if (offset > 0)
                    offset -= 7;
                invalidDates[i] = DateTime.Today.AddDays(offset); 
            }
        }
    }
}

[Serializable]
class StatisticsData
{
    public float[] weeklyDistances;
    public long[] invalidDates;
}