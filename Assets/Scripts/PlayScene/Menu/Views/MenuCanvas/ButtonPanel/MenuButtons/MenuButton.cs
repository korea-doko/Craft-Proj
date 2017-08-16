using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuButtonArgs : EventArgs
{
    [SerializeField] private MenuName m_menuName;

    public MenuButtonArgs(MenuName _name)
    {
        m_menuName = _name;
    }

    public MenuName MenuName
    {
        get
        {
            return m_menuName;
        }

        set
        {
            m_menuName = value;
        }
    }
}

public interface IMenuButton
{
    event EventHandler<MenuButtonArgs> OnButtonClicked;
}

public class MenuButton : MonoBehaviour , IMenuButton{

    
    private Button m_button;
    private LayoutElement m_layoutEle;
    private MenuName m_menuName;

    private float m_width;
    private float m_height;

    public event EventHandler<MenuButtonArgs> OnButtonClicked;

    public MenuName MenuName
    {
        get
        {
            return m_menuName;
        }

    }

    

    public void Init(MenuName _name, float _width, float _height)
    {
        m_layoutEle = this.GetComponent<LayoutElement>();

        m_button = this.GetComponent<Button>();
        this.GetComponentInChildren<Text>().text = _name.ToString();
        m_button.onClick.AddListener(() => OnButtonClicked(this, new MenuButtonArgs(m_menuName)));
        m_menuName = _name;
        m_width = _width;
        m_height = _height;
    }
    

    public void HighLightedButton()
    { 
        m_layoutEle.preferredWidth = m_width * 2.0f;
        m_layoutEle.preferredHeight = m_height;
    }
    public void NormaledButton()
    {
        m_layoutEle.preferredWidth = m_width;
        m_layoutEle.preferredHeight = m_height;
    }

}
