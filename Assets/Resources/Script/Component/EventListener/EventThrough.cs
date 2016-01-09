/* Fuction     ：  UI 事件穿透
 * Authgor     ：  tony
 * time        ：  2015-08-29 
 * copyright   ：  developers  
 */
using UnityEngine;
using System.Collections; 
public class EventThrough : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return false;
    }
}
