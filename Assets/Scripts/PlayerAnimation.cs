using System.Collections;
using System.Collections.Generic;
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

    public void JumpAnim(bool isJump)
    {
        StartCoroutine(Jump(isJump));
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
}
