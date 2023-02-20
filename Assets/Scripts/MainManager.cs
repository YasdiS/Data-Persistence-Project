using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject HighScore;
    public TMP_InputField inputField;

    private bool m_Started = false;
    public int m_Points;

    private bool m_GameOver = false;

    public string m_Name;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        SaveManager.Instance.HighdcoreText1.SetActive(true);
        SaveManager.Instance.HighdcoreText2.SetActive(false);
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (m_Points > LoadPoints())
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SavePoints();
                    SceneManager.LoadScene(0);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;

        if (m_Points > LoadPoints())
        {
            HighScore.SetActive(true);
        }
        else
        {
            GameOverText.SetActive(true);
        }
    }

    public void SavePoints()
    {
        if (m_Points > LoadPoints())
        {
            m_Name = inputField.text;

            SaveManager.Instance.m_Points = m_Points;
            SaveManager.Instance.m_Name = m_Name;
            SaveManager.Instance.SaveScore();
        }
    }

    public int LoadPoints()
    {
        SaveManager.Instance.LoadScore();
        int LoadPoints = SaveManager.Instance.m_Points;

        return LoadPoints;
    }

    public void ResetPoints()
    {
        SaveManager.Instance.ResetScore();
    }
}