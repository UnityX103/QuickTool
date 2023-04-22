using System.Reflection;
using UnityEngine;

namespace FrameworkExtensions.Reflection
{
    public class AssemblyExtensions
    {
        private static Assembly _runtime;

        public static Assembly Runtime
        {
            get
            {
                if (_runtime == null)
                {
                    _runtime = Assembly.Load("Assembly-CSharp");
                }

                return _runtime;
            }
        }

        private static Assembly _editor;

        public static Assembly Editor
        {
            get
            {
                if (_editor == null)
                {
                    _editor = Assembly.Load("Assembly-CSharp-Editor");
                }

                return _editor;
            }
        }

        public static Assembly Main => Application.isPlaying ? Runtime : Editor;
    }
}