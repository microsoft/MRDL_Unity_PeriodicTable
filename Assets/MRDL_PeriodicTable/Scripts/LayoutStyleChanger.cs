using Microsoft.MixedReality.Toolkit.ObjectCollection;
using UnityEngine;

public class LayoutStyleChanger : MonoBehaviour
{
    public GridObjectCollection objectCollection;
    public Transform tableParentTransform;
    public Transform legendTransform;

    public void ChangeLayoutStylePlane()
    {
        if(objectCollection != null)
        {
            objectCollection.SurfaceType = ObjectOrientationSurfaceType.Plane;
            objectCollection.OrientType = OrientationType.FaceParentFoward;
            objectCollection.Radius = 1.6f;
            objectCollection.RadialRange = 180.0f;
            objectCollection.Rows = 4;
            objectCollection.CellWidth = 0.21f;
            objectCollection.CellHeight = 0.21f;
            objectCollection.UpdateCollection();

            tableParentTransform.localPosition = new Vector3(0.0f, -0.5f, 1.2f);
            legendTransform.localPosition = new Vector3(0.0f, 0.15f, 1.8f);
        }
    }

    public void ChangeLayoutStyleCylinder()
    {
        if (objectCollection != null)
        {
            objectCollection.SurfaceType = ObjectOrientationSurfaceType.Cylinder;
            objectCollection.OrientType = OrientationType.FaceOrigin;
            objectCollection.Radius = 1.6f;
            objectCollection.RadialRange = 180.0f;
            objectCollection.Rows = 4;
            objectCollection.CellWidth = 0.21f;
            objectCollection.CellHeight = 0.21f;
            objectCollection.UpdateCollection();

            tableParentTransform.localPosition = new Vector3(0.0f, -0.4f, 1.3f);
            legendTransform.localPosition = new Vector3(0.0f, 0.15f, 1.8f);
        }
    }

    public void ChangeLayoutStyleRadial()
    {
        if (objectCollection != null)
        {
            objectCollection.SurfaceType = ObjectOrientationSurfaceType.Radial;
            objectCollection.OrientType = OrientationType.FaceCenterAxis;
            objectCollection.Radius = 12.0f;
            objectCollection.RadialRange = 120.0f;
            objectCollection.Rows = 10;
            objectCollection.CellWidth = 1.0f;
            objectCollection.CellHeight = 1.0f;
            objectCollection.UpdateCollection();

            tableParentTransform.localPosition = new Vector3(0.0f, -1.7f, 2.0f);
            legendTransform.localPosition = new Vector3(0.0f, 0.15f, 1.8f);
        }
    }

    public void ChangeLayoutStyleSphere()
    {
        if (objectCollection != null)
        {
            objectCollection.SurfaceType = ObjectOrientationSurfaceType.Sphere;
            objectCollection.OrientType = OrientationType.FaceOrigin;
            objectCollection.Radius = 1.2f;
            objectCollection.RadialRange = 180.0f;
            objectCollection.Rows = 8;
            objectCollection.CellWidth = 0.3f;
            objectCollection.CellHeight = 0.3f;
            objectCollection.UpdateCollection();

            tableParentTransform.localPosition = new Vector3(0.0f, -0.35f, 1.3f);
            legendTransform.localPosition = new Vector3(0.24f, 0.6f, 1.8f);
        }
    }
}
