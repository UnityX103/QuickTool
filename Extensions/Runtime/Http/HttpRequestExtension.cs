using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Plugins.Framework.Extensions.Runtime.Http
{
    public class HttpRequestExtension
    {
       public  static async UniTask<string> Get(string url)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);

            var asyncOp = www.SendWebRequest();

            while (!asyncOp.isDone)
            {
                await UniTask.Yield(); // 暂停协程，将控制权交回Unity主线程
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                return null;
            }
            Debug.Log("Received: " + www.downloadHandler.text);
            return www.downloadHandler.text;
        }
    }
}