using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// Cylinder ColliderEditor; copied from https://github.com/kode80/UnityTools/tree/develop
/// </summary>
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