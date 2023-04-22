using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Timeline;

namespace UnityEditor.Timeline
{
    [CustomTrackDrawer(typeof(DynamicBindingTrack)),UsedImplicitly]
    class DynamicBindingTrackDrawer : AnimationTrackDrawer
    {
        public override void DrawRecordingBackground(Rect trackRect, TrackAsset trackAsset, Vector2 visibleTime, WindowState state)
        {
            base.DrawRecordingBackground(trackRect, trackAsset, visibleTime, state);
        }
    }
}