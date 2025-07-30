using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject Target;
    Vector3 OrgPos;

    public delegate void OnMoveComplte();
    public event OnMoveComplte onMoveComplte;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(IMove(transform.position,Target.transform.position));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(IMove(transform.position, OrgPos));
        }
    }

    IEnumerator IMove(Vector3 _from,Vector3 _to)
    {
        float t = 0.0f;
        float durtaion = 1.0f;
        OrgPos = transform.position;

        while (t < 1.0f)
        {
            t += Time.deltaTime / durtaion;
            transform.position = Vector3.Lerp(_from, _to, t);
            yield return null;
        }

        onMoveComplte?.Invoke();
    }

    private void Start()
    {
        onMoveComplte += Atk1;
        onMoveComplte += Atk2;
    }

    void Atk1()
    {
        Debug.Log("Atk1");
    }

    void Atk2()
    {
        Debug.Log("Atk2");
    }
}
