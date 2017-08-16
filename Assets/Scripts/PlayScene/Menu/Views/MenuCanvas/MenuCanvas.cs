using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class MenuCanvas : MonoBehaviour, IUpdatable {

    public EventHandler OnMenuButtonClicked;

    private ButtonPanel m_buttonPanel;
    private ScrollRectMaskPanel m_scrollRectPanel;

    public void Init()
    {
        m_buttonPanel = this.GetComponentInChildren<ButtonPanel>();
        m_buttonPanel.Init();
        m_buttonPanel.OnMenuButtonClicked += HandleMenuButtonClicked;

        m_scrollRectPanel = this.GetComponentInChildren<ScrollRectMaskPanel>();
        m_scrollRectPanel.Init();
    }

    protected void HandleMenuButtonClicked(object _sender, EventArgs _args)
    {
        MenuButton btn = (MenuButton)_sender;
        m_scrollRectPanel.MovePanelToCenter(btn.MenuName);

        OnMenuButtonClicked(this, _args);
    }
     
    public void UpdateThis()
    {
        m_scrollRectPanel.UpdateThis();
    }

    public MenuPanel GetMenuPanel(MenuName _name)
    {
        return m_scrollRectPanel.GetMenuPanel(_name);
    }
}
