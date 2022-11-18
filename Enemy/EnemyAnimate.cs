using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateRunAnimation()
    {
        bool isRunning = animator.GetBool("IsRun");

        if (!isRunning)
            animator.SetBool("IsRun", true);
    }

    public void DeactivateRunAnimation()
    {
        bool isRunning = animator.GetBool("IsRun");

        if (isRunning)
            animator.SetBool("IsRun", false);
    }


    public void ActivateAttackAnimation()
    {
        bool IsAttack = animator.GetBool("IsAttack");

        if (!IsAttack)
            animator.SetBool("IsAttack", true);
    }

    public void DeactivateAttackAnimation()
    {
        bool IsAttack = animator.GetBool("IsAttack");

        if (IsAttack)
            animator.SetBool("IsAttack", false);
    }

    public void ActivateDiedAnimation()
    {
        bool IsDied = animator.GetBool("IsDied");

        if (!IsDied)
            animator.SetBool("IsDied", true);
    }

    public void DeactivateDiedAnimation()
    {
        bool IsDied = animator.GetBool("IsDied");

        if (IsDied)
            animator.SetBool("IsDied", false);
    }




}
