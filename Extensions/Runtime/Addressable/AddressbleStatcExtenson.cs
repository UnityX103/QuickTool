using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FrameworkExtensions.AddressableExtensions
{
    public static class AddressbleStatcExtenson
    {
        public static void AARelease(this GameObject obj)
        {
            Addressables.ReleaseInstance(obj);
        }

        public static void AARelease(this AAResource resource)
        {
            Addressables.Release(resource);
        }
        
        
        public static async UniTask<Sprite> LoadAssetReference(this AssetReference assetReferenceSprite)
        {
            if (assetReferenceSprite.RuntimeKeyIsValid())
            {
                AsyncOperationHandle<Sprite> handle;
                if (assetReferenceSprite.OperationHandle.IsValid())
                {
                    handle = assetReferenceSprite.OperationHandle.Convert<Sprite>();
                    if (handle.IsDone)
                    {
                        // AssetReference已经被加载
                        return handle.Result;
                        // 在这里使用sprite
                    }
                    // 异步操作还没有完成
                    await handle.ToUniTask();
                    return handle.Result;
                    // 在这里使用sprite
                }
                handle = assetReferenceSprite.LoadAssetAsync<Sprite>();
                if (handle.IsDone)
                {
                    // AssetReference已经被加载
                    return handle.Result;
                    // 在这里使用sprite
                }
                
                // 异步操作还没有完成
                await handle.ToUniTask();
                return handle.Result;
                // 在这里使用sprite
            }

            return null;
        }
    }
}