using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuView<T> :  IView<T>
{
    void UpdateView();
}
public class MenuView : MonoBehaviour, IMenuView<MenuModel>{

    public EventHandler OnMenuButtonClicked;

    private MenuCanvas m_menuCanvas;

    public MenuCanvas MenuCanvas
    {
        get
        {
            return m_menuCanvas;
        }
    }

    public void InitView(MenuModel _model)
    {
        GameObject menuCanvasPrefab = Resources.Load("PlayScene/Menu/MenuCanvas") as GameObject;

        m_menuCanvas = ((GameObject)Instantiate(menuCanvasPrefab)).GetComponent<MenuCanvas>();
        m_menuCanvas.Init();
        m_menuCanvas.OnMenuButtonClicked += HandleMenuButtonClicked;
    }

    private void HandleMenuButtonClicked(object _sender, EventArgs _args)
    {
        OnMenuButtonClicked(this, _args);
    }

    public void UpdateView()
    {
        m_menuCanvas.UpdateThis();
    }

    public MenuPanel GetMenuPanel(MenuName _name)
    {
        return m_menuCanvas.GetMenuPanel(_name);
    }
}
