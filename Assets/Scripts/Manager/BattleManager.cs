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

        //������ ���õǾ� �ְ� �� ���� Ŭ���� ��������
        //����Ÿ�Կ� ���� ���߼��� �� ���ϼ���
        //���ϰ����Ͻ� ������ ���õǾ� �ְ� �ٸ� ���� ���ý� ���� ���� ���� ����
    }
}
