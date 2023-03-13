using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;

    public static PlayerAnimation instance;
    private void Awake()
    {
        instance = this;
    }

    public void RunAnim(bool isRun)
    {
        playerAnim.SetBool("isBoost", isRun);
    }

    public void JumpAnim(bool isJump,bool isSpeed)
    {
        if(!isSpeed)
        {
            StartCoroutine(Jump(isJump));
        }
        else
        {
            StartCoroutine(SpeedJump(isJump));
        }
        
    }
    public void DieAnim()
    {
        playerAnim.SetTrigger("Die");
    }
    public void CheckFall(float jumpVelocity)
    {
        playerAnim.SetFloat("yVelocity", jumpVelocity);
    }

    IEnumerator Jump(bool isJump)
    {
        playerAnim.SetBool("isJump", isJump);
        yield return new WaitForSeconds(0.1f);
        playerAnim.SetBool("loopJump", isJump);
    }
    IEnumerator SpeedJump(bool isJump)
    {
        playerAnim.SetBool("isSpeedJump", isJump);
        yield return new WaitForSeconds(0.1f);
        playerAnim.SetBool("loopSpeedJump", isJump);
    }
}
