using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Animator camAnim;

    public void CamShake()
    {
        Debug.Log("Shake!");
        camAnim.SetTrigger("shake");
    }
}
