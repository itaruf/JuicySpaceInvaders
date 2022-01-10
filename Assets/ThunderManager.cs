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

    void Update()
    {
        
        if(thunderValue >= 0)
        {
            thunderValue -= Time.deltaTime;
        }
        else
        {
            anim.SetTrigger("Trigger");
            thunderValue = Random.Range(thunderCooldownMinxMax.x, thunderCooldownMinxMax.y);
        }
    }
}
