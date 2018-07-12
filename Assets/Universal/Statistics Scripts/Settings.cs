using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Settings : Singleton<Settings>
{
    public bool alwaysNorth;
    public bool soundOn;
    public float volume;

	void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        if (File.Exists(Application.persistentDataPath + "/settings.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/settings.dat", FileMode.Open);
            SettingsData data = (SettingsData) bf.Deserialize(file);
            file.Close();

            alwaysNorth = data.alwaysNorth;
            soundOn = data.soundOn;
            volume = data.volume;
        }
	}
	
	void OnDisable()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/settings.dat");

        SettingsData data = new SettingsData();
        data.alwaysNorth = alwaysNorth;
        data.soundOn = soundOn;
        data.volume = volume;

        bf.Serialize(file, data);
        file.Close();
	}
}

[Serializable]
class SettingsData
{
    public bool soundOn;
    public bool alwaysNorth;
    public float volume;
}
