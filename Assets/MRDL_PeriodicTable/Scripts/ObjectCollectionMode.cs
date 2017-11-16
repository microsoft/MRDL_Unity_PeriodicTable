//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using HoloToolkit.Unity;
using HoloToolkit.Unity.Buttons;
using HoloToolkit.Unity.Collections;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.Receivers;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.MRDL.PeriodicTable
{
    /// <summary>
    /// Receives button input and sets object collection display mode
    /// </summary>
    public class ObjectCollectionMode : InteractionReceiver
    {
        public Vector3 HomeOffset = Vector3.zero;
        public Vector3 PlaneOffset = Vector3.zero;
        public Vector3 SphereOffset = Vector3.zero;
        public Vector3 CylinderOffset = Vector3.zero;
        public float HomeRadius = 1f;
        public float PlaneRadius = 1f;
        public float SphereRadius = 1f;
        public float CylinderRadius = 1f;

        public ObjectCollection TargetCollection;

        protected override void InputDown(GameObject obj, InputEventData eventData)
        {
            Debug.Log(obj.name);
            switch (obj.name)
            {
                case "SurfaceTypeButtonDefault":
                    TargetCollection.GetComponent<ObjectCollectionDynamic>().RestoreArrangement();
                    TargetCollection.Radius = HomeRadius;
                    TargetCollection.transform.localPosition = HomeOffset;
                    break;
                case "SurfaceTypeButtonPlane":
                    TargetCollection.SurfaceType = SurfaceTypeEnum.Plane;
                    TargetCollection.transform.localPosition = PlaneOffset;
                    TargetCollection.Radius = PlaneRadius;
                    TargetCollection.UpdateCollection();
                    break;

                case "SurfaceTypeButtonSphere":
                    TargetCollection.SurfaceType = SurfaceTypeEnum.Sphere;
                    TargetCollection.transform.localPosition = SphereOffset;
                    TargetCollection.Radius = SphereRadius;
                    TargetCollection.UpdateCollection();
                    break;

                case "SurfaceTypeButtonCylinder":
                    TargetCollection.SurfaceType = SurfaceTypeEnum.Cylinder;
                    TargetCollection.transform.localPosition = CylinderOffset;
                    TargetCollection.Radius = CylinderRadius;
                    TargetCollection.UpdateCollection();
                    break;
            }
        }
    }
}
