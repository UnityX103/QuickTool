using FrameworkEditorExtensions.Process;

namespace FrameworkEditorExtensions.IO.File
{
   
    public static class FileExtensions
    {

        public static void OpenFile(AllowOpenAPPType type, params  string[] data)
        {
            string param="";
            foreach (var item in data)
            {
                param += item + " ";
            }
            System.Diagnostics.Process.Start(ProcessExtensions. GetFileName(type), param);
        }
    }
}