using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;





public class MenuPanel : MonoBehaviour {

    [SerializeField] private MenuName m_menuName;
    [SerializeField] private RectTransform m_rect;
    [SerializeField] private LayoutElement m_layoutEle;
    [SerializeField] private ScrollRect m_scrollRect;
    [SerializeField] private float m_width;
    [SerializeField] private float m_height;
    
    public MenuName MenuName
    {
        get
        {
            return m_menuName;
        }
    }

    public float Width
    {
        get
        {
            return m_width;
        }        
    }
    public float Height
    {
        get
        {
            return m_height;
        }
    }

    public void Init(MenuName _name)
    {
        m_rect = this.GetComponent<RectTransform>();
        m_layoutEle = this.GetComponent<LayoutElement>();
        m_scrollRect = this.GetComponent<ScrollRect>();
        m_menuName = _name;

        
    }
    public void ChangeSize(float _width, float _height)
    {
        m_width = _width;
        m_height = _height;

        m_layoutEle.preferredWidth = _width;
        m_layoutEle.preferredHeight = _height;
    }

    public void SetGamObjectAsChild(GameObject _obj)
    {
        _obj.transform.SetParent(this.transform);
        RectTransform rect = _obj.GetComponent<RectTransform>();
        m_scrollRect.content = rect;

        rect.offsetMax = Vector2.zero;
        rect.offsetMin = Vector2.zero;
    }
}
