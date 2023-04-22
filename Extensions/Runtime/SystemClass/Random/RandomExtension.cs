namespace FrameworkExtensions.SystemClass.Random
{
    public class RandomExtension
    {
        public static bool RandomResult()
        {
            return UnityEngine.Random.Range(0, 100)%2==1;
        }
    }
}