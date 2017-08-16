using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class MenuPanel : MonoBehaviour ,IBeginDragHandler, IDragHandler,IEndDragHandler{

    private MenuName m_menuName;
    private LayoutElement m_layoutEle;

    public void Init(MenuName _name)
    {
        m_layoutEle = this.GetComponent<LayoutElement>();
        m_menuName = _name;          
    }
    public void ChangeSize(float _width, float _height)
    {
        m_layoutEle.preferredWidth = _width;
        m_layoutEle.preferredHeight = _height;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log(m_menuName.ToString() + " begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(m_menuName.ToString() + " drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(m_menuName.ToString() + " END");
    }
}
