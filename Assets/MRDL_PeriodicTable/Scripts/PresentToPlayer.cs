//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using System.Collections;
using UnityEngine;
using UnityEngine.XR.WSA;

namespace HoloToolkit.MRDL.PeriodicTable
{
    public class PresentToPlayer : MonoBehaviour
    {
        public bool InPosition
        {
            get
            {
                return inPosition;
            }
        }

        public bool Presenting
        {
            get
            {
                return presenting;
            }
        }

        public float PresentationDistance = 1f;
        public float TravelTime = 1f;
        public bool OrientToCamera = true;
        public bool OrientYAxisOnly = true;
        public Transform TargetTranfsorm;

        Vector3 initialPosition;
        Quaternion initialRotation;
        bool presenting = false;
        bool returning = false;
        bool inPosition = false;

        public void Present()
        {
            if (presenting)
                return;

            presenting = true;
            StartCoroutine(PresentOverTime());
        }

        public void Return()
        {
            if (!presenting)
                return;

            returning = true;
        }

        IEnumerator PresentOverTime()
        {

            if (TargetTranfsorm == null)
                TargetTranfsorm = transform;

            initialPosition = transform.position;
            initialRotation = transform.rotation;
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;
            // Adjust the forward so we're only orienting in the Y axis
            if (OrientYAxisOnly)
            {
                cameraForward.y = 0f;
                cameraForward.Normalize();
            }
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward, Vector3.up);
            Vector3 targetPosition = cameraPosition + (cameraForward * PresentationDistance);// TODO use a HUX tool or something to get the main camera
            inPosition = false;

            float normalizedProgress = 0f;
            float startTime = Time.time;

            while (!inPosition)
            {
                // Move the object directly in front of player
                normalizedProgress = (Time.time - startTime) / TravelTime;
                TargetTranfsorm.position = Vector3.Lerp(initialPosition, targetPosition, normalizedProgress);
                if (OrientToCamera)
                {
                    TargetTranfsorm.rotation = Quaternion.Lerp(TargetTranfsorm.rotation, targetRotation, Time.deltaTime * 10f);
                }
                inPosition = Vector3.Distance(TargetTranfsorm.position, targetPosition) < 0.05f;
                yield return null;
            }

            while (!returning)
            {
                // Wait to be told to return
                yield return null;
            }

            // Move back to our initial position
            inPosition = false;
            normalizedProgress = 0f;
            startTime = Time.time;
            while (normalizedProgress < 1f)
            {
                normalizedProgress = (Time.time - startTime) / TravelTime;
                TargetTranfsorm.position = Vector3.Lerp(targetPosition, initialPosition, normalizedProgress);
                if (OrientToCamera)
                {
                    TargetTranfsorm.rotation = Quaternion.Lerp(TargetTranfsorm.rotation, initialRotation, Time.deltaTime * 10f);
                }
                inPosition = Vector3.Distance(TargetTranfsorm.position, initialPosition) < 0.05f;
                yield return null;
            }

            TargetTranfsorm.position = initialPosition;
            TargetTranfsorm.rotation = initialRotation;
            presenting = false;
            returning = false;

            yield break;
        }
    }
}