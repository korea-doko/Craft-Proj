using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroView<T> : IView<T>
{

}
public class HeroView : MonoBehaviour , IHeroView<HeroModel>,ILoadable{

    private HeroViewPanel m_heroViewPanel;

    public void InitView(HeroModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Hero/HeroViewPanel") as GameObject;
        m_heroViewPanel = ((GameObject)Instantiate(prefab)).GetComponent<HeroViewPanel>();
        m_heroViewPanel.Init();
    }
    public bool Load()
    {
        MenuPanel parent = MenuManager.Inst.GetMenuPanel(MenuName.Hero);
        
        if (parent == null)
            return false;
        m_heroViewPanel.Load(parent.Width, parent.Height);
        parent.SetGamObjectAsChild(m_heroViewPanel.gameObject);        
        return true;
    }

    public void HeroMenuButtonClicked(HeroModel _model)
    {
        m_heroViewPanel.HeroMenuButtonClicked(_model);
    }
}
