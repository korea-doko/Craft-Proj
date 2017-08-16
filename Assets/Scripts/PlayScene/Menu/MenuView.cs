using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenuView<T> :  IView<T>
{
    void UpdateView();
}
public class MenuView : MonoBehaviour, IMenuView<MenuModel>{

    private MenuCanvas m_menuCanvas;
    
    public void InitView(MenuModel _model)
    {
        GameObject menuCanvasPrefab = Resources.Load("PlayScene/Menu/MenuCanvas") as GameObject;

        m_menuCanvas = ((GameObject)Instantiate(menuCanvasPrefab)).GetComponent<MenuCanvas>();
        m_menuCanvas.Init();
    }

    public void UpdateView()
    {
        m_menuCanvas.UpdateThis();
    }
}
