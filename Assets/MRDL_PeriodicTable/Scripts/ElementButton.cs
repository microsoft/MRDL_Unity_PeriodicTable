//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using HoloToolkit.Unity;
using HoloToolkit.Unity.Buttons;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.MRDL.PeriodicTable
{
    public class ElementButton : Button
    {
        public override void OnStateChange(ButtonStateEnum newState)
        {
            Element element = gameObject.GetComponent<Element>();

            switch (newState)
            {
                case ButtonStateEnum.ObservationTargeted:
                case ButtonStateEnum.Targeted:
                    // If we're not the active element, light up
                    if (Element.ActiveElement != this)
                    {
                        element.Highlight();
                    }
                    break;

                case ButtonStateEnum.Pressed:
                    // User has clicked us
                    // If we're the active element button, reset ourselves
                    if (Element.ActiveElement == element)
                    {
                        // If we're the current element, reset ourselves
                        Element.ActiveElement = null;
                    }
                    else
                    {
                        Element.ActiveElement = element;
                        element.Open();
                    }
                    break;

                default:
                    element.Dim();
                    break;
            }

            base.OnStateChange(newState);
        }
    }
}
