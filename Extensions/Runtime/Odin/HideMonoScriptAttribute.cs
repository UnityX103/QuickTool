using System;
using System.Diagnostics;

namespace FrameworkExtensions.OdinStateExtensions
{
  
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class HideMonoScriptAttribute : Attribute
    {
        
    }


}