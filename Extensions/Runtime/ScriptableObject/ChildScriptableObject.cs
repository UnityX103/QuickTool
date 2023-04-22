using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class ChildScriptableObject : ScriptableObject, IAssetsCanBeHeld
{
    [HideInInspector] public ScriptableObjectGroup Group;
#if UNITY_EDITOR

    private bool _isOnGroup => Group != null;
    [HorizontalGroup	("子物体操作")]
    [ShowIf("_isOnGroup")]
    [HideLabel]
    [PropertyOrder(-1)]
    [SerializeField	]
    private string newName;
    
    [HorizontalGroup	("子物体操作")]
    [Button("修改子物体名字")]
    [PropertyOrder(-1)]
    [ShowIf("_isOnGroup")]
    public void ChangerName()
    {
        name = newName;
        Object obj = UnityEditor.Selection.activeObject;
        UnityEditor.AssetDatabase.RemoveObjectFromAsset(obj);
        UnityEditor.AssetDatabase.AddObjectToAsset(obj, Group);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
    }

    [HorizontalGroup("子物体操作")]
    [Button("移除" ),GUIColor	(1,0.2f,0)]
    [PropertyOrder(-1)]
    [ShowIf("_isOnGroup")]
    public void Delete()
    {
        Object obj = UnityEditor.Selection.activeObject;
        UnityEditor.AssetDatabase.RemoveObjectFromAsset(obj);
        UnityEditor.AssetDatabase.SaveAssets();
        Group.UpdateChildScriptableObjects();
        UnityEditor.Selection.activeObject = null;
        UnityEditor.AssetDatabase.Refresh();
    }
#endif
}