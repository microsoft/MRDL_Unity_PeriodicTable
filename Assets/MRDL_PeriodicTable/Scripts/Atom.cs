//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using UnityEngine;

public class Atom : MonoBehaviour
{
    const int AtomLayer = 9;

    public float Radius = 1f;
    [Range(0.1f, 10)]
    public float NucleusFlowSpeed = 5f;
    [Range(0.001f, 1f)]
    public float NucleusJitter = 0.05f;
    [Range(0.1f, 1f)]
    public float NucleusHoldShape = 0.5f;
    [Range(0.001f, 1f)]
    public float NucleusChangeSpeedOdds = 0.25f;

    public Transform ScaleTransform;

    public bool Collapse = false;

    public int NumProtons;
    public int NumNeutrons;
    public int NumElectrons;
    public int Frame = 0;
    public bool Instanced = false;
    public int[] ActiveElectronShells;
    public Mesh Mesh;
    public Mesh[] ElectronShellMeshes;
    public Material ProtonMat;
    public Material NeutronMat;

    Vector3[] nucleusTargetPositions;
    Vector3[] nucleusCurrentPositions;
    Matrix4x4[] protonMatrixes;
    Matrix4x4[] neutronMatrixes;
    Quaternion[] randomRotations;
    Transform transformHelper;
    MaterialPropertyBlock propertyBlock;
    Vector3 atomScale;
    Vector3 finalScale;

    private void OnEnable()
    {
        RefreshProperties();

        atomScale = Vector3.one * 0.001f;
        transform.localScale = Vector3.one * 0.01f;

        if (ScaleTransform == null)
        {
            ScaleTransform = transform.parent;
        }
    }

    private void Update()
    {
        RefreshProperties();

        Vector3 pos = transform.position;

        for (int i = 0; i < nucleusTargetPositions.Length; i++)
        {
            if (Random.value < NucleusChangeSpeedOdds)
            {
                Vector3 newPos = nucleusTargetPositions[i] + Random.insideUnitSphere * (1f - NucleusHoldShape);
                newPos = Vector3.MoveTowards(Vector3.zero, newPos, 1f);
                nucleusTargetPositions[i] = newPos;
            }
        }

        if (Collapse)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.01f, Time.deltaTime * 5);
            atomScale = Vector3.Lerp(atomScale, Vector3.one * 0.001f, Time.deltaTime * 5);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime);
            atomScale = Vector3.Lerp(atomScale, Vector3.one, Time.deltaTime);
        }

        finalScale = Vector3.Scale(atomScale, ScaleTransform.lossyScale);

        for (int i = 0; i < NumProtons + NumNeutrons; i++)
        {
            nucleusCurrentPositions[i] = Vector3.Lerp(nucleusCurrentPositions[i], nucleusTargetPositions[i], Time.deltaTime * NucleusFlowSpeed);

            if (i < NumProtons)
            {
                protonMatrixes[i] = Matrix4x4.TRS(pos + ((nucleusCurrentPositions[i] + (Random.insideUnitSphere * NucleusJitter)) * Radius * finalScale.x), randomRotations[i % randomRotations.Length], finalScale);
            }
            else
            {
                neutronMatrixes[i - NumProtons] = Matrix4x4.TRS(pos + ((nucleusCurrentPositions[i] + (Random.insideUnitSphere * NucleusJitter)) * Radius * finalScale.x), randomRotations[i % randomRotations.Length], finalScale);
            }
        }

        Graphics.DrawMeshInstanced(Mesh, 0, ProtonMat, protonMatrixes, protonMatrixes.Length, propertyBlock, UnityEngine.Rendering.ShadowCastingMode.Off, false, AtomLayer);
        Graphics.DrawMeshInstanced(Mesh, 0, NeutronMat, neutronMatrixes, neutronMatrixes.Length, propertyBlock, UnityEngine.Rendering.ShadowCastingMode.Off, false, AtomLayer);
    }

    private void RefreshProperties()
    {
        if (propertyBlock == null)
        {
            propertyBlock = new MaterialPropertyBlock();
            ProtonMat.enableInstancing = true;
            NeutronMat.enableInstancing = true;
        }

        if (randomRotations == null)
        {
            randomRotations = new Quaternion[10];
            for (int i = 0; i < randomRotations.Length; i++)
            {
                randomRotations[i] = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
            }
        }

        if (nucleusTargetPositions == null || nucleusTargetPositions.Length < NumProtons + NumNeutrons)
        {
            nucleusTargetPositions = new Vector3[NumProtons + NumNeutrons];
            nucleusCurrentPositions = new Vector3[NumProtons + NumNeutrons];
            protonMatrixes = new Matrix4x4[NumProtons];
            neutronMatrixes = new Matrix4x4[NumNeutrons];

            for (int i = 0; i < nucleusTargetPositions.Length; i++)
            {
                nucleusTargetPositions[i] = Random.onUnitSphere;
                nucleusCurrentPositions[i] = nucleusTargetPositions[i] * 5f;
            }
        }
    }
}