using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public static int ferrariScore;
    public static int fordScore;
    int fordCounter;
    int ferrariCounter;
    private Ford ford;
    private Ferrari ferrari;
    private GameManager gameManager;
    private CheckPoint checkPoint;
    private Text ferrariTxtScore;
    private Text fordTxtScore;
    private AudioSource speedUpSoundStop;

    void Start()
    {
        ford = GameObject.FindGameObjectWithTag("Ford").GetComponent<Ford>();
        ferrari = GameObject.FindGameObjectWithTag("Ferrari").GetComponent<Ferrari>();
        gameManager = FindObjectOfType<GameManager>();
        checkPoint = FindObjectOfType<CheckPoint>();
        ferrariTxtScore = GameObject.Find("ScoreFerrari").GetComponent<Text>();
        ferrariTxtScore.text = ferrariScore.ToString();
        fordTxtScore = GameObject.Find("ScoreFord").GetComponent<Text>();
        fordTxtScore.text = fordScore.ToString();
        speedUpSoundStop = GameObject.FindGameObjectWithTag("Speed").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkPoint.FerrariChkp >= 1)
        {
            if (ferrariCounter == 0 && collision.tag == "Ferrari")
            {
                ferrari.MapsWon += 1;
                ferrariScore += 1;
                speedUpSoundStop.Stop();
                ferrariTxtScore.text = ferrariScore.ToString();
                CarsStop();
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    CanvasWinner(ferrari.name, "Ferrari");
                    StartCoroutine(gameManager.LevelTwo());
                }
                else if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    CanvasWinner(ferrari.name, "Ferrari");
                    StartCoroutine(gameManager.LevelThree());
                }
                else
                {
                    Winner();
                    StartCoroutine(gameManager.CreditScene());
                }
            }
            if (collision.tag == "Ferrari")
            {
                ferrariCounter += 1;
            }
            checkPoint.FerrariChkp = 0;
        }

        if (checkPoint.FordChkp >= 1)
        {
            if (fordCounter == 0 && collision.tag == "Ford")
            {
                ford.MapsWon += 1;
                fordScore += 1;
                speedUpSoundStop.Stop();
                fordTxtScore.text = fordScore.ToString();
                CarsStop();
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    CanvasWinner(ford.name, "Ford");
                    StartCoroutine(gameManager.LevelTwo());
                }
                else if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    CanvasWinner(ford.name, "Ford");
                    StartCoroutine(gameManager.LevelThree());
                }
                else
                {
                    Winner();
                    StartCoroutine(gameManager.CreditScene());
                }
            }
            if (collision.tag == "Ford")
            {
                fordCounter += 1;
            }
            checkPoint.FordChkp = 0;
        }
    }

    public void CarsStop()
    {
        ford.gameRunning = false;
        ferrari.GameRunning = false;
    }

    public void CanvasWinner(string name, string car)
    {
        {
            GameObject winUi;
            Canvas canvas;
            Text winner;
            winUi = new GameObject("Canvas");
            winUi.AddComponent<Canvas>();

            canvas = winUi.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            winUi.AddComponent<CanvasScaler>();
            winUi.AddComponent<GraphicRaycaster>();
            GameObject tekst = new GameObject("Tekst");
            tekst.transform.parent = canvas.transform;
            winner = winUi.AddComponent<Text>();
            winner.font = (Font)Resources.Load("Lobster");

            if (car == "Ferrari")
            {
                winner.color = new Color(0.65f, 0f, 0f);
            }
            else
            {
                winner.color = new Color(0.34f, 0.57f, 0.69f);
            }
            winner.fontSize = 100;
            if (fordScore < 2 || ferrariScore < 2)
            {
                winner.text = "The winner is " + name + " (Team " + car + ")";
            }
            Text winnerIs = tekst.AddComponent<Text>();
            winnerIs.text = winner.text;
            RectTransform rectTransform = winner.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.sizeDelta = new Vector2(400, 200);
        }
    }

    public void Winner()
    {
        {
            GameObject winUi;
            Canvas canvas;
            Text winner;
            winUi = new GameObject("Canvas");
            winUi.AddComponent<Canvas>();

            canvas = winUi.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            winUi.AddComponent<CanvasScaler>();
            winUi.AddComponent<GraphicRaycaster>();
            GameObject tekst = new GameObject("Tekst");
            tekst.transform.parent = canvas.transform;
            winner = winUi.AddComponent<Text>();
            winner.font = (Font)Resources.Load("Lobster");
            winner.fontSize = 100;
            if (fordScore >= 2)
            {
                winner.text = "The winner of Ford v Ferrari is " + ford.name + " (Team Ford)";
            }
            else if (ferrariScore >= 2)
            {
                winner.text = "The winner of Ford v Ferrari is " + ferrari.name + " (Team Ferrari)";
            }
            Text winnerIs = tekst.AddComponent<Text>();
            winnerIs.text = winner.text;
            RectTransform rectTransform = winner.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.sizeDelta = new Vector2(400, 200);
        }
    }
}
