using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MK.Toon;

public class BuildingMeshController : MonoBehaviour
{
    [SerializeField] private List<Renderer> renderers;

    private void Awake()
    {
        ChangeBuildingSaturation(0);
    }

    public void ChangeBuildingSaturation(float saturation)
    {
        foreach (var rend in renderers)
        {
            rend.material.DOFloat(saturation, "_Saturation", .5f);
        }
    }
}
