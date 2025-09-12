using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBtn : MonoBehaviour
{
    public void OnCurrUnitAttack()
    {
        Shared.GameManager.GetCurrUnit().OnAttack();
    }

    public void OnCurrUnitUseUniqueSkill()
    {

    }
}
