using System;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using FrameworkExtensions.AddressableExtensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public interface AAResource
{
}


public static class ResFactory
{
    const string AA_PATH = "Assets/AAResources/";

    public static async UniTask<IList<T>> LoadAllAsync<T>(string path)where T   : Object
    {
        var list = await Addressables.LoadAssetsAsync<T>(AA_PATH + path, null).Task;
        if (list == null)
        {
            Debug.LogError($"异步加载失败：{AA_PATH+path}");
        }
        return list;
    }


    /// <summary>
    /// async加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">直接填AAResources下的路径</param>
    /// <returns></returns>
    public static async UniTask<T> LoadAsync<T>(string path) where T : class
    {
        var handle = Addressables.LoadAssetAsync<T>(AA_PATH + path);
        Debug.Log($"异步加载：{AA_PATH + path}");
        var obj = await handle.Task;
        //这一步是否有必要
       // Addressables.Release(handle);
        return obj;
    }

    /// <summary>
    /// async加载 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">完整路径</param>
    /// <returns></returns>
    public static async UniTask<T> LoadAsyncPath<T>(string path) where T : Object
    {
        return await Addressables.LoadAssetAsync<T>(path).Task;
    }

    /// <summary>
    /// 加载预制体-实例化后查找相对应的组件 （包含子物体上的）
    /// </summary>
    /// <param name="path">直接填AAResources下的路径 不用后缀</param>
    /// <param name="container">放置在那个物体下</param>
    /// <typeparam name="T">需要查找的组件</typeparam>
    /// <returns></returns>
    public static async UniTask<T> LoadMonoAsync<T>(string path, Transform container = null) where T : class
    {
        var obj = await LoadAsync<GameObject>(path + ".prefab");

        if (obj == null)
        {
            return null;
        }

        var instance = GameObject.Instantiate(obj, container);

        instance.name = instance.name.Replace("(Clone)", string.Empty);

        obj.AARelease();

        return instance.GetComponentInChildren<T>();
    }

    /// <summary>
    /// 异步生成物体
    /// </summary>
    /// <typeparam name="T">返回的类型</typeparam>
    /// <param name="path">AA下的预制体路径  不用后缀</param>
    /// <param name="parent">实例到那个物体下</param>
    public static async UniTask<T> InstantiateAsync<T>(string path, Transform parent = null)
        where T : UnityEngine.Object
    {
        var obj = await Addressables.InstantiateAsync(AA_PATH + path + ".prefab", parent).Task;
        if (obj == null)
        {
            return null;
        }

        T t = obj.GetComponent<T>();

        return t;
    }

    /// <summary>
    /// 加载游戏物体
    /// </summary>
    /// <param name="path">AA下的预制体路径  不用后缀</param>
    /// <param name="container">放在那个物体下</param>
    /// <returns></returns>
    public static void LoadPrefab(string path, Action<GameObject > asset, Transform container = null   )
    {
        Load<GameObject>("Prefabs/" + path + ".prefab", (e)=>
        {
            asset( GameObject.Instantiate(e,container));
        });
    }


    /// <summary>
    /// 同步方法加载资源
    /// </summary>
    /// <param name="asset">加载完成时的回调</param>
    /// <typeparam name="T">需要加载的资产类型</typeparam>
    public static async void Load<T>(string path, Action<T> asset) where T : class
    {
        var data = await Addressables.LoadAssetAsync<T>(AA_PATH + path).Task;
        asset?.Invoke(data);
    }
    
#if UNITY_EDITOR
    /// <summary>
    /// 同步方法加载资源 仅限UnityEditor使用
    /// </summary>
    /// <param name="asset">加载完成时的回调</param>
    /// <typeparam name="T">需要加载的资产类型</typeparam>
    public static T LoadFullPath<T>(string path) where T : UnityEngine.Object
    {
        var asset = (T)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(T));
        return asset;
    }
#endif

    
#if UNITY_EDITOR
    /// <summary>
    ///  加载指定路径下所有改类型资产 仅限UnityEditor使用
    /// </summary>
    /// <param name="directory">文件夹路径</param>
    /// <typeparam name="T">类型</typeparam>
    /// <returns></returns>
    public static List<T> Loads<T>(string directory)
    {
        List<T> datas = new List<T>();

        DirectoryInfo directoryInfo = new DirectoryInfo(directory);
        foreach (var item in directoryInfo.GetFiles())
        {
            Object asset;
            if (typeof(T).IsInterface)
            {
                asset = LoadFullPath<GameObject>(directory + "/" + item.Name);
            }
            else
            {
                asset = UnityEditor.AssetDatabase.LoadAssetAtPath(directory + "/" + item.FullName, typeof(T));
            }

            if (asset is GameObject obj && typeof(T) != typeof(GameObject))
            {
                var temp = obj.GetComponent<T>();
                if (temp != null)
                {
                    datas.Add(temp);
                }
            }
            else
            {
                if (asset is T data)
                {
                    datas.Add(data);
                }
            }
        }

        return datas;
    }
#endif
}