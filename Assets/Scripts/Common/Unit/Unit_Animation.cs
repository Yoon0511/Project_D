using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    public Animator Animator;
    protected void PlayAnimation(string _state, int _animation)
    {
        if (Animator != null)
        {
            Animator.SetInteger(_state, _animation);
        }
    }

    // 애니메이션 이벤트
    void OnAttackAnimationEnd()
    {
        IsAttackFinished = true;
    }
}
