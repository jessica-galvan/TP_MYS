using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManagerScript : MonoBehaviour
{
    public static AudioClip Die, GameOver, EnemyDie, PickKey;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        GameOver = Resources.Load<AudioClip>("GameOver");
        EnemyDie = Resources.Load<AudioClip>("EnemyDie");
        PickKey = Resources.Load<AudioClip>("PickKey");
        audioSrc = GetComponent<AudioSource>();

    }

   public static void PlaySound (string clip)
    {
        switch(clip)
        {
            case "GameOver":
                audioSrc.PlayOneShot(GameOver);
                break;
            case "EnemyDie":
                audioSrc.PlayOneShot(EnemyDie);
                break;
            case "PickKey":
                audioSrc.PlayOneShot(PickKey);
                break;
            
        }
    }
}
