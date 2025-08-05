using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public partial class Unit : MonoBehaviour
{
    public delegate void OnMoveComplte();
    public event OnMoveComplte onMoveComplte;

    public bool IsMine = false;

    [SerializeField]
    PlayableDirector PD;

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

        if (Input.GetKeyDown(KeyCode.K)) //기본공격
        {
            StartCoroutine(IAttack(transform.position,Target.transform.position));
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
}
