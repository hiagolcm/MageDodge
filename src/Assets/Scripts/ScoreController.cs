using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ScoreController : MonoBehaviour {

    public PlayerController blueMage;
    public PlayerController redMage;
    public BallController ball;

    private Vector2 blueInitPostion;
    private Vector2 redInitPostion;
    private Vector2 ballInitPostion;

    public Text blueMageScore;
    public Text redMageScore;

    private void Start()
    {
        blueInitPostion = blueMage.transform.position;
        redInitPostion = redMage.transform.position;
        ballInitPostion = ball.transform.position;
    }

    public void TriggerWin(int playerIndex)
    {
        if (playerIndex == 0) {
            int val = 0;
            Int32.TryParse(blueMageScore.text, out val);
            blueMageScore.text = (val + 1).ToString();
        } else
        {
            int val = 0;
            Int32.TryParse(redMageScore.text, out val);
            redMageScore.text = (val + 1).ToString();
        }

        StartCoroutine(ResetScene());
    }

    IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(1.5f);
        blueMage.Revive();
        redMage.Revive();
        blueMage.transform.position = blueInitPostion;
        redMage.transform.position = redInitPostion;
        ball.transform.position = ballInitPostion;
    }
}
