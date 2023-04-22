using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using FrameworkExtensions.OdinStateExtensions;
using FrameworkExtensions.SystemClass.Type;
using Sirenix.OdinInspector;
using UnityEngine;


public abstract class ScriptableObjectGroup : SerializedScriptableObject
{
#if UNITY_EDITOR

    [ValueDropdown("GetTypeList")] [LabelText("需要添加的类型")] [HorizontalGroup("添加子物体")]
    public Type addTargetList;

    [Button("添加到组")]
    [HorizontalGroup("添加子物体")]
    private void AddToGroup()
    {
        if (addTargetList == null) return;
        ScriptableObject obj = ScriptableObject.CreateInstance(addTargetList);
        obj.name = addTargetList.RemoveNamespaceTypeName();
        if (obj is ChildScriptableObject childScriptableObject)
        {
            childScriptableObject.Group = this;
        }

        // 将ScriptableObject 保存到当前的ScriptableObject中
        UnityEditor.AssetDatabase.AddObjectToAsset(obj, this);
        UnityEditor.AssetDatabase.SaveAssets();
        UpdateChildScriptableObjects();
        UnityEditor.AssetDatabase.Refresh();
    }

    /// <summary>
    ///  更新子物体列表 编辑器模式下使用
    /// </summary>
    public void UpdateChildScriptableObjects()
    {
        _childScriptableObjects.Clear();
        var obj = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(
            UnityEditor.AssetDatabase.GetAssetPath(this));
        foreach (var item in obj)
        {
            if (item is ChildScriptableObject childScriptableObject)
            {
                if (!_childScriptableObjects.TryGetValue(childScriptableObject.GetType(), out var list))
                {
                    list = new List<ChildScriptableObject>();
                    _childScriptableObjects.Add(childScriptableObject.GetType(), list);
                }

                list.Add(childScriptableObject);
            }
        }
    }

    public IEnumerable<Type> GetTypeList() => OdinStaticExtansion.GetTypes(TypeList());
#endif

    [ReadOnly] [LabelText("子物体列表")] [SerializeField]
    [PropertyOrder	(90)]
    private Dictionary<Type, List<ChildScriptableObject>> _childScriptableObjects =
        new Dictionary<Type, List<ChildScriptableObject>>();

    protected abstract Type[] TypeList();

    public T GetChildScriptableObject<T>(string childName) where T : ChildScriptableObject
    {
        if (_childScriptableObjects.TryGetValue(typeof(T), out var list))
        {
            return list.First(e => e.name == childName) as T;
        }

        Debug.LogError("没有找到对应的子物体");
        return null;
    }
}

public interface IAssetsCanBeHeld
{
}