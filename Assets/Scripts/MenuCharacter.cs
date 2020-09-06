using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacter : MonoBehaviour
{
    public Animator animator;
    float time, timeToAnimation;
    bool animate = false;
    bool[] animationClips;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        timeToAnimation = 7.0f;
        animate = false;
        animationClips = new bool[7];
    }

    // Update is called once per frame
    void Update()
    {
        if(!animate) time += Time.deltaTime;
        if(time >= timeToAnimation && !animate)
        {
            SelectAnimation();
            animate = true;
            Invoke("RestartTime", 5.0f);
        }
    }

    public void SelectAnimation()
    {
        int i = Random.Range(1, 7);
        PlayAnimation(i);
    }

    public void PlayAnimation(int anim)
    {
        switch (anim)
        {
            case 0:
                animator.SetFloat("Animation", 0.0f);
                break;
            case 1:
                animator.SetFloat("Animation", 1.0f);
                break;
            case 2:
                animator.SetFloat("Animation", 1.0f);
                break;
            default:
                break;
        }
    }

    public void RestartTime()
    {
        time = 0.0f;
        animate = false;
    }
}
