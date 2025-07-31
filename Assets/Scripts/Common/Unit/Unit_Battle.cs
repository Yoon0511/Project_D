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
        yield return IMove(_from, _to); // Ÿ�� ��ġ�� �̵�
        yield return IDoAttack();       // �̵� �Ϸ�� ����
        yield return IMove(_to, _from); // ���� ��ġ�� ����
        
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
        // ���ݾִϸ��̼��� ������ ���� ���
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
        float Duration = _duration; // ������ ���� �ð�
        float Power = _power; // ������ ����
        Vector3 originalPosition = transform.position;

        while (t < Duration)
        {
            t += Time.deltaTime;
            float Xoffset = Random.Range(-Power, Power);
            float Yoffset = Random.Range(-Power, Power);
            transform.position = originalPosition + new Vector3(Xoffset, Yoffset, 0);
            yield return null;
        }

        transform.position = originalPosition; // ���� ��ġ�� �ǵ�����
    }

    public void SetTarget(Unit _target)
    {
        Target = _target;
    }
}
