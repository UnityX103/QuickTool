using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
    [Serializable]
    [TrackClipType(typeof(AnimationPlayableAsset), false)]
    [TrackBindingType(typeof(Animator))]
    [ExcludeFromPreset]
    [TrackColor(100 / 255f, 120 / 255f, 0)]
   public  class DynamicBindingTrack : AnimationTrack
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
             base.GatherProperties(director, driver);
            foreach (var item in outputs)
            {
                var source = director.GetGenericBinding(item.sourceObject);
                if (source != null)
                {
                    ObjKey = source.name;
                    NeedDynamicSelection = true;
                }
            }
        }
    }
}