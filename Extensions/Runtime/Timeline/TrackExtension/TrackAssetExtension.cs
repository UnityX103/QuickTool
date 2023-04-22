using System;
using JetBrains.Annotations;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
    partial class TrackAsset : IDynamicSelectionTargets
    {
        [SerializeField]
        private string _objKey;
        [SerializeField]
        private bool _needDynamicSelection;

        public string ObjKey { get=>_objKey; set=>_objKey=value; }

        public bool NeedDynamicSelection
        {
            get => _needDynamicSelection;
            set => _needDynamicSelection = value;
        }
    }
}