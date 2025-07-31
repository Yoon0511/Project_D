using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineData
{
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public Color OrgColor;
    public Color ChangeColor;
    public float OrgBold;
    public float ChangeBold;
}

public partial class Unit : MonoBehaviour
{
    public GameObject Mesh;
    public List<OutLineData> ListOutLineData = new();
    public List<GameObject> ListChildTransform = new();

    void MaterialInit()
    {
        if(Mesh == null)
        {
            return;
        }

        int ChildCount = Mesh.transform.childCount;

        for (int i = 0; i < ChildCount; i++)
        {
            GameObject Child = Mesh.transform.GetChild(i).gameObject;
            if (Child.GetComponent<SkinnedMeshRenderer>() != null)
            {
                SkinnedMeshRenderer ChildRenderer = Child.GetComponent<SkinnedMeshRenderer>();
                if (ChildRenderer.material.HasProperty("_OutLine_Color"))
                {
                    OutLineData Data = new OutLineData();
                    Data.SkinnedMeshRenderer = ChildRenderer;
                    ChildRenderer.material.SetFloat("_OutLine_Bold", 1.0f);
                    Data.OrgColor = ChildRenderer.material.GetColor("_OutLine_Color");
                    Data.ChangeColor = Color.red;
                    Data.OrgBold = ChildRenderer.material.GetFloat("_OutLine_Bold");
                    Data.ChangeBold = 2.0f;
                    
                    ListChildTransform.Add(Child);
                    ListOutLineData.Add(Data);
                }
            }
        }
    }

    public void DoSelected()
    {
        ChangeOutLine();
    }

    void ChangeOutLine()
    {
        for (int i = 0; i < ListChildTransform.Count; ++i)
        {
            GameObject Child = ListChildTransform[i];
            SkinnedMeshRenderer ChildRenderer = ListOutLineData[i].SkinnedMeshRenderer;
            if (ChildRenderer != null)
            {
                if (ChildRenderer.material.HasProperty("_OutLine_Color"))
                {
                    if (ChildRenderer.material.GetColor("_OutLine_Color") == ListOutLineData[i].OrgColor)
                    {
                        ChildRenderer.material.SetColor("_OutLine_Color", ListOutLineData[i].ChangeColor);
                        ChildRenderer.material.SetFloat("_OutLine_Bold", ListOutLineData[i].ChangeBold);
                    }
                    else
                    {
                        ChildRenderer.material.SetColor("_OutLine_Color", ListOutLineData[i].OrgColor);
                        ChildRenderer.material.SetFloat("_OutLine_Bold", ListOutLineData[i].OrgBold);
                    }
                }
            }
        }
    }
}
