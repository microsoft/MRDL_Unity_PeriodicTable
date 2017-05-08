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

    private void OnEnable()
    {
        propertyBlock = new MaterialPropertyBlock();
        ProtonMat.enableInstancing = true;
        NeutronMat.enableInstancing = true;
        transform.localScale = Vector3.one * 0.01f;

        randomRotations = new Quaternion[10];
        for (int i = 0; i < randomRotations.Length; i++)
        {
            randomRotations[i] = Quaternion.Euler(Random.value * 360, Random.value * 360, Random.value * 360);
        }

        if (nucleusTargetPositions == null || nucleusTargetPositions.Length < NumProtons + NumNeutrons)
        {
            nucleusTargetPositions = new Vector3[NumProtons + NumNeutrons];
            nucleusCurrentPositions = new Vector3[NumProtons + NumNeutrons];
            protonMatrixes = new Matrix4x4[NumProtons];
            neutronMatrixes = new Matrix4x4[NumNeutrons];
        }

        for (int i = 0; i < nucleusTargetPositions.Length; i++)
        {
            nucleusTargetPositions[i] = Random.onUnitSphere;
            nucleusCurrentPositions[i] = nucleusTargetPositions[i] * 5f;
        }

        atomScale = Vector3.one * 0.001f;
    }

    private void Update()
    {
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

        for (int i = 0; i < NumProtons + NumNeutrons; i++)
        {
            nucleusCurrentPositions[i] = Vector3.Lerp(nucleusCurrentPositions[i], nucleusTargetPositions[i], Time.deltaTime * NucleusFlowSpeed);
            
            if (i < NumProtons)
            {
                protonMatrixes[i] = Matrix4x4.TRS(pos + ((nucleusCurrentPositions[i] + (Random.insideUnitSphere * NucleusJitter)) * Radius), randomRotations [i % randomRotations.Length], atomScale);
            } else
            {
                neutronMatrixes[i - NumProtons] = Matrix4x4.TRS(pos + ((nucleusCurrentPositions[i] + (Random.insideUnitSphere * NucleusJitter)) * Radius), randomRotations[i % randomRotations.Length], atomScale);
            }
        }

        Graphics.DrawMeshInstanced(Mesh, 0, ProtonMat, protonMatrixes, protonMatrixes.Length, propertyBlock, UnityEngine.Rendering.ShadowCastingMode.Off, false, AtomLayer);
        Graphics.DrawMeshInstanced(Mesh, 0, NeutronMat, neutronMatrixes, neutronMatrixes.Length, propertyBlock, UnityEngine.Rendering.ShadowCastingMode.Off, false, AtomLayer);
    }
}