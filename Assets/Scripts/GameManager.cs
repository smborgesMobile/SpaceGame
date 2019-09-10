using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    private UIManager _uiManager;

    // Control the start of the game
    public bool gameOver = true;
    // Start is called before the first frame update

    // Update is called once per frame

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Create player if space key is touched.
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                _uiManager.HideMainMenu();
            }
        }
    }

    public void ShowTitleScreen()
    {
        _uiManager.ShowMainMenu();
    }
}