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

    public void JumpAnim(bool isJump)
    {
        playerAnim.SetBool("isJump", isJump);
    }
    public void CheckFall(float jumpVelocity)
    {
        playerAnim.SetFloat("yVelocity", jumpVelocity);
    }
}
