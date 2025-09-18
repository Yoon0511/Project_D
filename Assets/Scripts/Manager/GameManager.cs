using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool OnBattle = false;
    public MyUnit Currunit = null;
    public CinemachineVirtualCamera VCam;

    private List<MyUnit> ListEnemyUnit = new List<MyUnit>();
    private List<MyUnit> ListFriendlyUnit = new List<MyUnit>();
    private void Awake()
    {
        Shared.GameManager = this;
    }

    public void AddEnemyUnit(MyUnit _unit)
    {
        ListEnemyUnit.Add(_unit);
    }
    public void AddFriendlyUnit(MyUnit _unit)
    {
        ListFriendlyUnit.Add(_unit);

        ListFriendlyUnit.Sort((a, b) => a.GetPriority().CompareTo(b.GetPriority())); // 공격 우선 순위로 정렬
    }
    public List<MyUnit> EnemyUnits
    {
        get { return ListEnemyUnit; }
    }
    public List<MyUnit> FriendlyUnits
    {
        get { return ListFriendlyUnit; }
    }
    public MyUnit GetCurrUnit()
    {
        return Currunit;
    }

    public void SetCurrUnit(MyUnit _unit)
    {
        Currunit = _unit;
    }

    public void FocusCameraOnCurrentUnit()
    {
        if (VCam != null && Currunit != null)
        {
            if (Currunit.GetUnitType() == UNIT_TYPE.Friendly)
            {
                VCam.Follow = Currunit.transform;
                VCam.LookAt = Currunit.transform;
            }
        }
    }
}
