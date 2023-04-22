using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;

namespace FrameworkExtensions.IO.Directory
{
    using  Directory=System.IO.Directory;
    public static class DirectoryExtensions
    {
     
        /// <summary>
        /// 获取所有子目录的名字
        /// </summary>
        /// <param name="path">当前目录</param>
        /// <returns></returns>
        public static string[] GetAllChildDirectoryName(string path)
        {
            return new DirectoryInfo(path).GetDirectories().Select(d => d.Name).ToArray();
        }
        
        
        /// <summary>
        /// 删除指定目录文件夹
        /// </summary>
        public static void DeleteDir(string path, bool refresh = true)
        {
            if (!Directory.Exists(path)) return;
            Directory.Delete(path, true);
            if (File.Exists($"{path}.meta"))
            {
                File.Delete($"{path}.meta");
            }

            if (refresh)
                AssetDatabase.Refresh();
        }
        
        /// <summary>
        /// 获取所有子目录的名字
        /// </summary>
        /// <param name="info">当前目录</param>
        /// <returns></returns>
        public static string[] GetAllChildDirectoryName(DirectoryInfo info)
        {
           return info.GetDirectories().Select(d => d.Name).ToArray();
        }
        
        
        /// <summary>
        /// 打开指定路径的文件夹
        /// </summary>
        /// <param name="dataPath"></param>
        /// <exception cref="Exception"></exception>
        public static void OpenDirectory(string dataPath)
        {
#if UNITY_STANDALONE_WIN
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo("Explorer.exe");
            psi.Arguments = Path.GetFullPath(dataPath);
            process.StartInfo = psi;
            try
            {
                process.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                process?.Close();
            }
#else
            
            EditorUtility.RevealInFinder(Path.GetFullPath(dataPath));
#endif
        }
        
        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <param name="destFolder"></param>
        /// <returns></returns>
        public static int CopyFolder(string sourceFolder, string destFolder)
        {
            try
            {
                if (!System.IO.Directory.Exists(destFolder))
                {
                    System.IO.Directory.CreateDirectory(destFolder);
                }

                //得到原文件根目录下的所有文件
                string[] files = System.IO.Directory.GetFiles(sourceFolder);
                foreach (string file in files)
                {
                    string name = System.IO.Path.GetFileName(file);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    System.IO.File.Copy(file, dest); //复制文件
                }

                //得到原文件根目录下的所有文件夹
                string[] folders = System.IO.Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders)
                {
                    string name = System.IO.Path.GetFileName(folder);
                    string dest = System.IO.Path.Combine(destFolder, name);
                    CopyFolder(folder, dest); //构建目标路径,递归复制文件
                }

                return 1;
            }
            catch
            {
                return -1;
            }
        }

    }
}