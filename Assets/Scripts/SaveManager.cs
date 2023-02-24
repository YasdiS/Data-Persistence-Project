using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public int m_Points;
    public string m_Name;
    public GameObject HighdcoreText1;
    public GameObject HighdcoreText2;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }

    private void Update()
    {
        HighdcoreText1.GetComponent<Text>().text = "Highscore: " + m_Name + " " + m_Points;
        HighdcoreText2.GetComponent<Text>().text = "Highscore: " + m_Name + " " + m_Points;
    }

    [System.Serializable]
    class SaveData
    {
        public int m_Points;
        public string m_Name;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.m_Points = m_Points;
        data.m_Name = m_Name;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_Points = data.m_Points;
            m_Name = data.m_Name;
        }
    }

    public void ResetScore()
    {
        foreach (var directory in Directory.GetDirectories(Application.persistentDataPath))
        {
            DirectoryInfo data_dir = new DirectoryInfo(directory);
            data_dir.Delete(true);
        }

        foreach (var file in Directory.GetFiles(Application.persistentDataPath))
        {
            FileInfo file_info = new FileInfo(file);
            file_info.Delete();
        }
    }
}
