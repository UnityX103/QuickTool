using FrameworkEditorExtensions.IO.File;
using UnityEngine;

namespace FrameworkEditorExtensions.Process
{
    using Process= System.Diagnostics.Process;
    public enum AllowOpenAPPType
    {
        Excel
    }
    
    
    public static class ProcessExtensions
    {
      
        public static string GetFileName(AllowOpenAPPType type)
        {
            string fileName ;
#if UNITY_EDITOR_OSX
            fileName="/Applications/Microsoft Excel.app/Contents/MacOS/Microsoft Excel";
          
#else
              fileName="Excel.exe";
#endif
            return fileName;
        }


        
        public static  void CloseApp( AllowOpenAPPType  type)
        {
#if UNITY_EDITOR_OSX
            Debug.LogError("Mac不支持");
            #else
              string appName = GetFileName(type);
            Process[] processes = Process.GetProcesses();
            foreach (Process p in processes)
            {
                if (p.ProcessName == appName)
                {
                    p.Kill();
                }
            }
#endif
          
        }
    }
}