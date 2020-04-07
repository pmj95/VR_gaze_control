using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CylinderCollider))]
public class CylinderColliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Bake Colliders"))
        {
            var cylinder = target as CylinderCollider;
            cylinder.CreateColliders(cylinder.colliderParent == null ? cylinder.transform : cylinder.colliderParent);
            cylinder.enabled = false;
        }
    }
}