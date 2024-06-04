using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UIParticleSystem : MaskableGraphic
{
    [SerializeField] ParticleSystemRenderer particleSystemRenderer;
    [SerializeField] Camera bakeCamera;

    void Update()
    {
        SetVerticesDirty();
    }

    [System.Obsolete]
    protected override void OnPopulateMesh(Mesh mesh)
    {
        mesh.Clear();
        if (particleSystemRenderer != null && bakeCamera != null)
            particleSystemRenderer.BakeMesh(mesh, bakeCamera);
    }
}
