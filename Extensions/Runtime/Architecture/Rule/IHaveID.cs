using System.Collections;
using UnityEngine;

namespace Rules
{
    /// <summary>
    /// 拥有ID
    /// </summary>
    public interface IHaveID
    {
        /// <summary>
        /// ID
        /// </summary>
        uint ID { get; set; }
    }
}
