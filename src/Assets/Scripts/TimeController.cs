using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    public float slowDuration;
    public float isTime;

    private bool completeTime = false;
    private float slowRunningTime;

	// Use this for initialization
	void Start () {
        slowRunningTime = slowDuration;
	}
	
	// Update is called once per frame
	void Update () {
        if (!PauseMenuController.GameIsPaused) {
            if (slowRunningTime < slowDuration)
            {
                Time.timeScale = isTime;

                slowRunningTime += Time.deltaTime;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
	}

    public void SlowTrigger()
    {
        slowRunningTime = 0;
        completeTime = false;
    }
}
