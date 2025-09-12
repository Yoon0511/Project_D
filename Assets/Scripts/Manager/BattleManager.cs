using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    Queue<MyUnit> Qunit = new Queue<MyUnit>();

    private void Awake()
    {
        Shared.BattleManager = this;
    }

    private void Start()
    {
        //BattleStart();
    }

    public void BattleStart()
    {
        Qunit.Clear();
        foreach (var unit in Shared.GameManager.FriendlyUnits)
        {
            Qunit.Enqueue(unit);
        }
        foreach (var unit in Shared.GameManager.EnemyUnits)
        {
            Qunit.Enqueue(unit);
        }

        StartCoroutine(IBattleStart());
    }

    IEnumerator IBattleStart()
    {
        while(Qunit.Count > 0)
        {
            MyUnit unit = Qunit.Dequeue();
            Shared.GameManager.SetCurrUnit(unit);
            // 턴시작
            yield return StartCoroutine(unit.ITurnStartEvent());
            // 유닛 공격
            yield return StartCoroutine(unit.IAttackTurn());
            // 턴종료
            yield return StartCoroutine(unit.ITurnEndEvent());
            DeselectAllUnits();

            Qunit.Enqueue(unit);
        }

        yield return null;
    }

    private void Update()
    {
        UnitSelectedToRay();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            BattleStart();
        }
    }

    void UnitSelectedToRay()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) &&
           Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            MyUnit Myunit = hit.collider.GetComponent<MyUnit>();
            if (Myunit != null)
            {
                Myunit.DoSelected();
                if(Myunit.GetIsSelected() == false)
                {
                    return;
                }
                Shared.GameManager.GetCurrUnit().SetTarget(Myunit);
                //CurrUnit.SetTarget(unit);
            }
        }

        //유닛이 선택되어 있고 빈 공간 클릭시 선택해제
        //공격타입에 따라 다중선택 및 단일선택
        //단일공격일시 유닛이 선택되어 있고 다른 유닛 선택시 기존 유닛 선택 해제
    }

    void DeselectAllUnits()
    {
        foreach (var unit in Shared.GameManager.FriendlyUnits)
        {
            if (unit.GetIsSelected())
            {
                unit.DoSelected();
            }
        }
        foreach (var unit in Shared.GameManager.EnemyUnits)
        {
            if (unit.GetIsSelected())
            {
                unit.DoSelected();
            }
        }
    }
}
