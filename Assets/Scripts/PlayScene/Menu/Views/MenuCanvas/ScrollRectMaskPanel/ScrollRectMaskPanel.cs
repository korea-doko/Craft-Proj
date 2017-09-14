using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollRectMaskPanel : MonoBehaviour, IUpdatable
{
    [SerializeField] private ScrollPanel m_scrollPanel;

    [SerializeField] private float m_width;
    [SerializeField] private float m_height;

    public void Init()
    {
        RectTransform rect = this.GetComponent<RectTransform>();


        m_width = rect.rect.width;
        m_height = rect.rect.height;
       
        m_scrollPanel = this.GetComponentInChildren<ScrollPanel>();
        m_scrollPanel.Init(m_width, m_height);        
    }

    public void MovePanelToCenter(MenuName _name)
    {
        m_scrollPanel.MovePanelToCenter(_name);
    }  
    public void UpdateThis()
    {
        m_scrollPanel.UpdateThis();
    }

    public MenuPanel GetMenuPanel(MenuName _name)
    {
       return  m_scrollPanel.GetMenuPanel(_name);
    }
}
