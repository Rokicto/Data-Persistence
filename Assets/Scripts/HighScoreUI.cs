using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreUI : MonoBehaviour
{
    public RectTransform scorePanel;
    public Text[] scoreTexts;

    void Start()
    {
        UpdateScoreList();
    }

    void UpdateScoreList() {
        List<GameManager.ScoreRecord> scores = GameManager.Instance.scores;
        int disableLines = 0;

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            if (i >= scores.Count)
            {
                scoreTexts[i].gameObject.SetActive(false);
                disableLines++;
                continue;
            }

            GameManager.ScoreRecord record = scores[i];

            scoreTexts[i].gameObject.SetActive(true);
            scoreTexts[i].text = $"{record.name}: {record.score}";
        }

        scorePanel.sizeDelta = new Vector2(400, 450 - disableLines * 30);
    }

    public void ClearAll()
    {
        GameManager.Instance.ClearScores();
        UpdateScoreList();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("startmenu");
    }
}
