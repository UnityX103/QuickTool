using UnityEngine;

namespace FrameworkExtensions.Mono.GameObject
{
    public class Preview : MonoBehaviour {
 
#if UNITY_EDITOR
        public Texture2D PreviewThumbnail;
        public Texture2D PreviewImage;
#endif
    }

}