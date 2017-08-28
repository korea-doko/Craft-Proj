using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroView<T> : IView<T>
{
    event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;
}
public class HeroView : MonoBehaviour , IHeroView<HeroModel>,ILoadable{

    private HeroViewPanel m_heroViewPanel;

    public event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;

    public void InitView(HeroModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Hero/HeroViewPanel") as GameObject;
        m_heroViewPanel = ((GameObject)Instantiate(prefab)).GetComponent<HeroViewPanel>();
        m_heroViewPanel.Init();
        m_heroViewPanel.OnHeroPanelClicked += M_heroViewPanel_OnHeroPanelClicked;
    }

    private void M_heroViewPanel_OnHeroPanelClicked(object sender, HeroPanelClickedArgs e)
    {
        OnHeroPanelClicked(this, e);
    }

    public bool Load()
    {
        MenuPanel parent = MenuManager.Inst.GetMenuPanel(MenuName.Traveller);
        
        if (parent == null)
            return false;
        m_heroViewPanel.Load(parent.Width, parent.Height);
        parent.SetGameObjectAsChild(m_heroViewPanel.gameObject);        
        return true;
    }


    internal void Hide()
    {
        m_heroViewPanel.Hide();
    }

    internal void Show(HeroModel _model)
    {
        m_heroViewPanel.Show(_model);
    }
}
