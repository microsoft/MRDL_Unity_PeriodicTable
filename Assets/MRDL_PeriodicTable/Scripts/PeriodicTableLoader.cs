//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.MRDL.PeriodicTable
{
    [System.Serializable]
    public class ElementData
    {
        public string name;
        public string category;
        public string spectral_img;
        public int xpos;
        public int ypos;
        public string named_by;
        public float density;
        public string color;
        public float molar_heat;
        public string symbol;
        public string discovered_by;
        public string appearance;
        public float atomic_mass;
        public float melt;
        public string number;
        public string source;
        public int period;
        public string phase;
        public string summary;
        public int boil;
    }

    [System.Serializable]
    class ElementsData
    {
        public List<ElementData> elements;

        public static ElementsData FromJSON(string json)
        {
            return JsonUtility.FromJson<ElementsData>(json);
        }
    }

    public class PeriodicTableLoader : MonoBehaviour
    {
        // What object to parent the instantiated elements to
        public Transform Parent;

        // Generic element prefab to instantiate at each position in the table
        public GameObject ElementPrefab;

        // How much space to put between each element prefab
        public float ElementSeperationDistance;

        public GridObjectCollection Collection;

        public Material MatAlkaliMetal;
        public Material MatAlkalineEarthMetal;
        public Material MatTransitionMetal;
        public Material MatMetalloid;
        public Material MatDiatomicNonmetal;
        public Material MatPolyatomicNonmetal;
        public Material MatPostTransitionMetal;
        public Material MatNobleGas;
        public Material MatActinide;
        public Material MatLanthanide;

        void OnEnable()
        {
            if (Collection.transform.childCount > 0)
                return;

            Debug.Log("Creating arrangement");

            Dictionary<string, Material> typeMaterials = new Dictionary<string, Material>()
        {
            { "alkali metal", MatAlkaliMetal },
            { "alkaline earth metal", MatAlkalineEarthMetal },
            { "transition metal", MatTransitionMetal },
            { "metalloid", MatMetalloid },
            { "diatomic nonmetal", MatDiatomicNonmetal },
            { "polyatomic nonmetal", MatPolyatomicNonmetal },
            { "post-transition metal", MatPostTransitionMetal },
            { "noble gas", MatNobleGas },
            { "actinide", MatActinide },
            { "lanthanide", MatLanthanide },
        };

            // Parse the elements out of the json file
            TextAsset asset = Resources.Load<TextAsset>("JSON/PeriodicTableJSON");
            List<ElementData> elements = ElementsData.FromJSON(asset.text).elements;

            // Insantiate the element prefabs in their correct locations and with correct text
            foreach (ElementData element in elements)
            {
                GameObject newElement = Instantiate<GameObject>(ElementPrefab, Parent);
                newElement.GetComponentInChildren<Element>().SetFromElementData(element, typeMaterials);
                newElement.transform.localPosition = new Vector3(element.xpos * ElementSeperationDistance - ElementSeperationDistance * 18 / 2, ElementSeperationDistance * 9 - element.ypos * ElementSeperationDistance, Collection.Radius);
                newElement.transform.localRotation = Quaternion.identity;
            }
        }
    }
}