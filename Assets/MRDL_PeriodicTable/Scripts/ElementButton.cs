//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using HUX.Buttons;
using HUX.Interaction;

public class ElementButton : Button {    
    
    public override void Pressed(InteractionManager.InteractionEventArgs args)
    {
        Element element = gameObject.GetComponent<Element>();
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
    }

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

            default:
                element.Dim();
                break;
        }

        base.OnStateChange(newState);
    }
}
