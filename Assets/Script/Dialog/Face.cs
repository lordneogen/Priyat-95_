using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using UnityEngine;

public class Face : MonoBehaviour
{ 
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public string happy;
    public string angry;
    public string sad;
    public string surprise;
    public List<string> Faces = new List<string>(); // Initialize Faces list

    private void Start()
    {
        InitializeFaces();
    }

    private void InitializeFaces()
    {
        for (int i = 0; i < SkinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            string blendShapeName = SkinnedMeshRenderer.sharedMesh.GetBlendShapeName(i);
            Faces.Add(blendShapeName);
        }
    }

    public void SetFace(Dialog dialog)
    {
        if (dialog.happy) SetFaceTransition(this.happy);
        if (dialog.sad) SetFaceTransition(this.sad);
        if (dialog.surprise) SetFaceTransition(this.surprise);
        if (dialog.angry) SetFaceTransition(this.angry);
    }

    public void ResetBlendShapes() // Renamed to avoid conflict with reserved keyword
    {
        foreach (var i in Faces)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(i), 0);
        }
    }

    private void SetFaceTransition(string type)
    {
        ResetBlendShapes();
        foreach (var i in Faces)
        {
            // string pattern = $@"\b{type}\b";
            // Regex regex = new Regex(pattern);
            if (i.IndexOf(type, StringComparison.OrdinalIgnoreCase) >= 0 && i.IndexOf("Fung", StringComparison.OrdinalIgnoreCase) < 0 && i.IndexOf("Skin", StringComparison.OrdinalIgnoreCase) < 0)
            {
                DOTween.To(() => 0, x => {
                    SkinnedMeshRenderer.SetBlendShapeWeight(SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(i), x);
                }, 100, 0.3f);
            }
        }
    }
}
