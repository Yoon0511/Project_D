using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Unit : MonoBehaviour
{
    public delegate void OnMoveComplte();
    public event OnMoveComplte onMoveComplte;

    public bool IsMine = false;
    private void Start()
    {
        MaterialInit();
    }
    private void Update()
    {
        if(IsMine == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(IAttack(transform.position,Target.transform.position));
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(IShake(0.5f,0.5f));
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            DoSelected();
        }
    }
}
