#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Rules;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Plugins.Framework.Extensions.Editor.Config
{
    /// <summary>
    /// 数据库基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SObjDatabase<T> : OdinEditorWindow
        where T : SerializedScriptableObject, IHaveID
    {
        [InlineEditor]
        [HideReferenceObjectPicker] [LabelText("数据库")] [Searchable]
        public List<T> datas = new List<T>();


      
        protected virtual uint StartIndex => 0;

        private void Save(T data, int index, uint id)
        {            
            OnSave(data, index, id);
            Debug.LogError(data.ID+"保存了");
        }

        protected virtual void OnSave(T data, int index, uint id)
        {
        
        }

        
        
        [GUIColor(0, 1, 0.5f)]
        [PropertyOrder(-1)]
        [Button("保存所有数据", ButtonSizes.Gigantic)]
        public virtual void SaveAll()
        {
            SaveDatabase(datas);
        }

        protected void SaveDatabase(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var data = list[i];
                if (data == null)
                {
                    continue;
                }

                var id = (uint)(i + StartIndex);
                data.ID = id;

                Save(data, i, id);
                EditorUtility.SetDirty(data);
            }


            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            //Unity弹窗显示保存情况
            EditorUtility.DisplayDialog($"{GetType()}保存成功", $"一共保存了{list.Count}份数据", "OK");
        }

        [GUIColor(0, 1, 1)]
        [PropertyOrder(-1)]
        [Button("创建新配置", ButtonSizes.Gigantic)]
        public void CreateNew()
        {
            //创建T的实例
            var data = CreateInstance<T>();
            //将data放置到指定路径
            var path = string.Format(CREATE_PATH, CREATE_NAME);
            AssetDatabase.CreateAsset(data, path);
            var id = (uint)(StartIndex + datas.Count);
            //AssetDatabase.RenameAsset(path, id.ToString());
            data.ID = id;

            if (!datas.Contains(data))
            {
                //创建后的回调
                OnCreateNew(data);
            
                EditorUtility.SetDirty(data);

                AssetDatabase.Refresh();
                Debug.LogError("刷新卡牌");
                datas.Add(data);
            }
        }


        protected virtual string CREATE_PATH => "Assets/AAResources/{0}.asset";
        protected abstract string CREATE_NAME { get; }
        
        protected virtual void OnCreateNew(T data)
        {
            
        }

        [LabelText("更多选项")]
        public bool loadToggle = false;
        

        // [ShowIf(nameof(loadToggle))]
        // [GUIColor(1, 0, 0)]    
        // [Button("重载并覆盖所有数据", ButtonSizes.Gigantic)]
        // public void Load()
        // {
        //     //Unity弹窗确认是否要覆盖读取
        //     if (EditorUtility.DisplayDialog("警告", "读取数据会覆盖当前数据，是否继续？", "确认", "取消"))
        //     {
        //         loadToggle = false;
        //         datas.Clear();
        //
        //         LoadCore();
        //     }
        // }
        //
        // protected void LoadCore()
        // {
        //     var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
        //     foreach (var guid in guids)
        //     {
        //         var path = AssetDatabase.GUIDToAssetPath(guid);
        //         var data = AssetDatabase.LoadAssetAtPath<T>(path);
        //         //能否加载
        //         var canLoad = OnLoad(data);
        //         if (!canLoad)
        //         {
        //             continue;
        //         }
        //
        //         //创建scriptableObject
        //         datas.Add(data);
        //     }
        // }
        //
        //
        // protected virtual bool OnLoad<T>(T data) where T : SerializedScriptableObject, IHaveID
        // {
        //     return true;
        // }   
    }
}
#endif