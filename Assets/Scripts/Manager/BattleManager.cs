using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Unit CurrUnit;

    private void Awake()
    {
        Shared.BattleManager = this;
    }
    private void Update()
    {
        UnitSelectedToRay();
    }

    void UnitSelectedToRay()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0) &&
           Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit != null)
            {
                unit.DoSelected();
                CurrUnit.SetTarget(unit);
            }
        }

        //유닛이 선택되어 있고 빈 공간 클릭시 선택해제
        //공격타입에 따라 다중선택 및 단일선택
        //단일공격일시 유닛이 선택되어 있고 다른 유닛 선택시 기존 유닛 선택 해제
    }
}
