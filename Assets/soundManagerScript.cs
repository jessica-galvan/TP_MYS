using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagerScript : MonoBehaviour
{
    public static AudioClip Die, PlayerLooseLife, EnemyDie, CoinSound, Shoot, Steps, GetKeySound, PlayerDie, HitEnemy;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        Shoot = Resources.Load<AudioClip>("Shoot");
        PlayerLooseLife = Resources.Load<AudioClip>("PlayerLooseLife");
        EnemyDie = Resources.Load<AudioClip>("EnemyDie");
        CoinSound = Resources.Load<AudioClip>("CoinSound");
        Steps = Resources.Load<AudioClip>("Steps");
        GetKeySound = Resources.Load<AudioClip>("GetKeySound");
        PlayerDie = Resources.Load<AudioClip>("PlayerDie");
        HitEnemy = Resources.Load<AudioClip>("HitEnemy");
        audioSrc = GetComponent<AudioSource>();
    }

   public static void PlaySound (string clip)
    {
        switch(clip)
        {
            case "PlayerLooseLife":
                audioSrc.PlayOneShot(PlayerLooseLife);
                break;
            case "EnemyDie":
                audioSrc.PlayOneShot(EnemyDie);
                break;
            case "CoinSound":
                audioSrc.PlayOneShot(CoinSound);
                break;
            case "Shoot":
                audioSrc.PlayOneShot(Shoot);
                break;
            case "Steps":
                audioSrc.PlayOneShot(Steps);
                break;
            case "GetKeySound":
                audioSrc.PlayOneShot(GetKeySound);
                break;
            case "PlayerDie":
                audioSrc.PlayOneShot(PlayerDie);
                break;
            case "HitEnemy":
                audioSrc.PlayOneShot(HitEnemy);
                break;
        }
    }
}
