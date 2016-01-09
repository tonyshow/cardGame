/// <summary>
/// Author:tony
/// 事件触发封装 - 需要什么事件可扩展
/// Event trigger listener.
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public delegate void VoidDelegate(GameObject go);
   
    public VoidDelegate onClick;
    public VoidDelegate onDown;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onUp;

    //key相关
    public delegate void VoidDelegateKey(GameObject go, string key);
    public VoidDelegateKey onClickKey;
    public VoidDelegateKey onDownKey;
    public VoidDelegateKey onEnterKey;
    public VoidDelegateKey onExitKey;
    public VoidDelegateKey onUpKey;


    static public EventListener Get(GameObject go)
    {
        EventListener listener = go.GetComponent<EventListener>();
        if (listener == null) listener = go.AddComponent<EventListener>();
        return listener;
    } 

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
        if (onClickKey != null) onClickKey(gameObject, tag); 
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
        if (onDownKey != null) onDownKey(gameObject, tag);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
        if (onEnterKey != null) onEnterKey(gameObject, tag);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
        if (onExitKey != null) onExitKey(gameObject, tag);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
        if (onUpKey != null) onUpKey(gameObject, tag);
    }
     

}