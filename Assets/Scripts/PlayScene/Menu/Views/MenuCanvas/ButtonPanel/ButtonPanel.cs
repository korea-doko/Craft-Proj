using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ButtonPanel : MonoBehaviour {

    public EventHandler OnMenuButtonClicked;
   
    private MenuButton[] m_menuButtonAry;

    private RectTransform m_rect;

    private float m_width;
    private float m_height;

    public void Init()
    {
        m_rect = this.GetComponent<RectTransform>();

        m_width = m_rect.rect.width;
        m_height = m_rect.rect.height;

        float buttonWidth = m_width / (float)(1 + System.Enum.GetNames(typeof(MenuName)).Length);

        int numOfMenu = System.Enum.GetNames(typeof(MenuName)).Length;
        m_menuButtonAry = new MenuButton[numOfMenu];

        MenuButton[] cic = this.GetComponentsInChildren<MenuButton>();

        for (int i = 0; i < numOfMenu; i++)
        {
            m_menuButtonAry[i] = cic[i];
            m_menuButtonAry[i].Init((MenuName)i, buttonWidth, m_height);
            m_menuButtonAry[i].OnButtonClicked += HandleMenuButtonClicked;

            if (i != 2)
                m_menuButtonAry[i].NormaledButton();
            else
                m_menuButtonAry[i].HighLightedButton();

        }       
    }

    protected void HandleMenuButtonClicked(object _sender, EventArgs _args)
    {
        MenuButton btn = (MenuButton)_sender;
        int menuButtonName = (int)btn.MenuName;

        for (int i = 0; i < 5; i++)
        {
            if (menuButtonName != i)
                m_menuButtonAry[i].NormaledButton();
            else
                m_menuButtonAry[i].HighLightedButton();
        }

        OnMenuButtonClicked(_sender, _args);
    }
}
