using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    [SerializeField]
    protected Unit Target;

    bool IsAttackFinished = false;
    IEnumerator IAttack(Vector3 _from, Vector3 _to)
    {
        yield return IMove(_from, _to); // 타겟 위치로 이동
        yield return IDoAttack();       // 이동 완료시 공격
        yield return IMove(_to, _from); // 원래 위치로 복귀
        
        PlayAnimation("Ani_State", (int)UNIT_ANIMATION.Idle);
    }

    IEnumerator IMove(Vector3 _from, Vector3 _to)
    {
        float t = 0.0f;
        float Durtaion = 0.5f;
        PlayAnimation("Ani_State", (int)UNIT_ANIMATION.Move);
        while (t < 1.0f)
        {
            t += Time.deltaTime / Durtaion;
            transform.position = Vector3.Lerp(_from, _to, t);
            yield return null;
        }
    }

    IEnumerator IDoAttack()
    {
        IsAttackFinished = false;
        BasicAttack();
        // 공격애니메이션이 끝날때 까지 대기
        yield return new WaitUntil(() => IsAttackFinished);
    }

    void BasicAttack()
    {
        PlayAnimation("Ani_State", (int)UNIT_ANIMATION.BasicAttack);
        Target.Hit();
    }

    protected void Hit()
    {
        TakeDamage();
    }

    protected void TakeDamage()
    {
        StartCoroutine(IShake(0.15f, 0.8f));
    }

    IEnumerator IShake(float _duration, float _power)
    {
        float t = 0.0f;
        float Duration = _duration; // 진동의 지속 시간
        float Power = _power; // 진동의 강도
        Vector3 originalPosition = transform.position;

        while (t < Duration)
        {
            t += Time.deltaTime;
            float Xoffset = Random.Range(-Power, Power);
            float Yoffset = Random.Range(-Power, Power);
            transform.position = originalPosition + new Vector3(Xoffset, Yoffset, 0);
            yield return null;
        }

        transform.position = originalPosition; // 원래 위치로 되돌리기
    }

    public void SetTarget(Unit _target)
    {
        Target = _target;
    }
}
