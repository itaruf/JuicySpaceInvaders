using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderManager : MonoBehaviour
{
    public Vector2 thunderCooldownMinxMax;
    public float thunderValue;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        thunderValue = Random.Range(thunderCooldownMinxMax.x, thunderCooldownMinxMax.y);
    }


    public void ForceThunder()
    {
        anim.SetTrigger("Trigger");
        PlaySoundEffect();
        thunderValue = Random.Range(thunderCooldownMinxMax.x, thunderCooldownMinxMax.y);
    }


    void Update()
    {
        
        if(thunderValue >= 0)
        {
            thunderValue -= Time.deltaTime;
        }
        else
        {
            anim.SetTrigger("Trigger");
            PlaySoundEffect();
            thunderValue = Random.Range(thunderCooldownMinxMax.x, thunderCooldownMinxMax.y);
        }
    }

    void PlaySoundEffect()
    {
        switch(UnityEngine.Random.Range(0,2))
        {
            case 0:
                AudioManager.Instance.PlayAudio("Thunder1Reverbe", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
            case 1:
                AudioManager.Instance.PlayAudio("Thunder2Reverbe", Audio.AudioType.SFX, AudioManager.AudioAction.START, false);
                break;
            default:
                break;
        }
    }
}
