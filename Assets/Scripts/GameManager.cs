using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string savePath;

    public string playerName;
    public List<ScoreRecord> scores { get; private set; }

    public ScoreRecord BestRecord
    {
        get
        {
            if (scores.Count == 0) { return new ScoreRecord(); }
            return scores[0];
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath + "/save.json";
        LoadScores();
    }

    public void newScore(int score)
    {
        ScoreRecord record = new ScoreRecord();
        record.name = playerName;
        record.score = score;

        if (record.name == "")
        {
            record.name = "Unknown";
        }

        scores.Add(record);
        scores.Sort((r1, r2) => r2.score - r1.score);

        if (scores.Count > 10) { scores = scores.GetRange(0, 10); }
    }

    public struct ScoreRecord
    {
        public string name;
        public int score;
    }

    [System.Serializable]
    class SaveData
    {
        public ScoreRecord[] highScores;
    }

    public void SaveScores()
    {
        SaveData data = new SaveData();
        data.highScores = scores.ToArray();

        File.WriteAllText(savePath, JsonUtility.ToJson(data));
    }

    public void LoadScores()
    {
        if (!File.Exists(savePath))
        {
            scores = new List<ScoreRecord>();
            return;
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        scores = new List<ScoreRecord>(data.highScores);
    }

    public void ClearScores()
    {
        scores.Clear();
        SaveScores();
    }
}
