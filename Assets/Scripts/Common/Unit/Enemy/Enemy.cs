using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MyUnit
{
    public override void Init()
    {
        //base.Init();
        MaterialInit();
        Shared.GameManager.AddEnemyUnit(this);
    }

    public override IEnumerator ITurnEndEvent()
    {
        return base.ITurnEndEvent();
    }

    public override IEnumerator ITurnStartEvent()
    {
        return base.ITurnStartEvent();
    }

    public override IEnumerator IAttackTurn()
    {
        // 공격 대상 자동 선택
        for(int i = 0;i<Shared.GameManager.FriendlyUnits.Count; ++i)
        {
            if (Shared.GameManager.FriendlyUnits[i] != null)
            {
                Target = Shared.GameManager.FriendlyUnits[i];
                Target.DoSelected();
                break;
            }
        }

        yield return IMoveToAttack(transform.position, Target.transform.position);
    }
}
