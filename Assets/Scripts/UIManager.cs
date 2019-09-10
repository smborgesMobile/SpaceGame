using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] livesImage;
    public Image livesImageDisplay;
    public GameObject mainMenuImage;
    public Text scoreText;
    public int score;

    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = livesImage[currentLives];
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void ShowMainMenu()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        //Show main menu.
        mainMenuImage.SetActive(true);
    }

    public void HideMainMenu()
    {
        // Hide main menu.
        mainMenuImage.SetActive(false);
    }
}