using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerController : MonoBehaviour {

    public static AudioClip jump, hitTheGround, dead, bounce, shield;
    static AudioSource audioSrc;

    private void Start()
    {
        jump = Resources.Load<AudioClip>("Jump");
        hitTheGround = Resources.Load<AudioClip>("HitTheground");
        bounce = Resources.Load<AudioClip>("Bounce");
        shield = Resources.Load<AudioClip>("Shield");
        dead = Resources.Load<AudioClip>("Dead");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch(clip){
            case "Jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "HitTheGround":
                audioSrc.PlayOneShot(hitTheGround);
                break;
            case "Bounce":
                audioSrc.PlayOneShot(bounce);
                break;
            case "Shield":
                audioSrc.PlayOneShot(shield);
                break;
            case "Dead":
                audioSrc.PlayOneShot(dead);
                break;
        }
    }
}
