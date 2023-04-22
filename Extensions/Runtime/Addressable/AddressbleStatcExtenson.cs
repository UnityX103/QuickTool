using UnityEngine;
using UnityEngine.AddressableAssets;

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
    }
}