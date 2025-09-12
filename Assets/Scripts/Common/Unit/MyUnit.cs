using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Playables;

public partial class MyUnit : MonoBehaviour
{
    public bool IsMine = false;
    [SerializeField]
    protected int Priority; // 공격 우선 순위
    [SerializeField]
    protected UNIT_TYPE UnitType = UNIT_TYPE.Friendly;

    [SerializeField]
    PlayableDirector PD;

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        if(IsMine == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.K)) //기본공격
        {
            StartCoroutine(IMoveToAttack(transform.position,Target.transform.position));
        }

        if(Input.GetKeyDown(KeyCode.L)) //흔들림
        {
            StartCoroutine(IShake(0.5f,0.5f));
        }

        if(Input.GetKeyDown(KeyCode.J)) // 쉐이더 테스트
        {
            DoSelected();
        }

        if(Input.GetKeyDown(KeyCode.M)) // 타임라인 테스트
        {
            PD.Play();
        }
    }

    public virtual void Init()
    {
        MaterialInit();
        Shared.GameManager.AddFriendlyUnit(this);
    }

    public virtual IEnumerator ITurnStartEvent()// 턴 시작시 실행되는 이벤트
    {
        IsOnAttack = false;
        IsTargetSelected = false;
        Shared.GameManager.FocusCameraOnCurrentUnit();
        yield return null;
    } 
    public virtual IEnumerator ITurnEndEvent() // 턴 종료시 실행되는 이벤트
    {
        IsOnAttack = false;
        IsTargetSelected = false;
        yield return null;
    }

    public void SetPriority(int _priority)
    {
        Priority = _priority;
    }

    public int GetPriority()
    {
        return Priority;
    }

    public UNIT_TYPE GetUnitType()
    {
        return UnitType;
    }
}
