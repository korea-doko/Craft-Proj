using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroView<T> : IView<T>
{
    event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;
    event EventHandler OnHeroDetailPanelBuyBtnClicked;
}
public class HeroView : MonoBehaviour , IHeroView<HeroModel>,ILoadable{

    private HeroViewPanel m_heroViewPanel;
   

    public event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;
    public event EventHandler OnHeroDetailPanelBuyBtnClicked;

    public void InitView(HeroModel _model)
    {
        GameObject prefab = Resources.Load("PlayScene/Hero/HeroViewPanel") as GameObject;
        m_heroViewPanel = ((GameObject)Instantiate(prefab)).GetComponent<HeroViewPanel>();
        m_heroViewPanel.Init();
        m_heroViewPanel.OnHeroPanelClicked += M_heroViewPanel_OnHeroPanelClicked;
        m_heroViewPanel.OnHeroDetailPanelBuyBtnClicked += M_heroViewPanel_OnHeroDetailPanelBuyBtnClicked;
    }

    
    public bool Load()
    {
        MenuPanel parent = MenuManager.Inst.GetMenuPanel(MenuName.Hero);
        
        if (parent == null)
            return false;

        m_heroViewPanel.Load(parent.Width, parent.Height);
        parent.SetGameObjectAsChild(m_heroViewPanel.gameObject);        
        return true;
    }
    public void Hide()
    {
        m_heroViewPanel.Hide();
    }

    public void ShowDetailPanel(HeroData _data)
    {
        m_heroViewPanel.ShowDetailPanel(_data);
    }
    public void HideDetailPanel()
    {
        m_heroViewPanel.HideDetailPanel();
    }

    public void ShowHeroPanel(List<HeroDataWithLimitedTime> _availableHeroDataList)
    {
        m_heroViewPanel.ShowHeroPanel(_availableHeroDataList);
    }
    public void ShowHeroNumCount(int _cur, int _max)
    {
        m_heroViewPanel.ShowHeroNumCount(_cur, _max);
    }    
    public void UpdateRegenTime(int _currentTime, int _regenTime)
    {
        m_heroViewPanel.UpdateRegenTime(_currentTime, _regenTime);
    }
    /// 이벤트 핸들러

    private void M_heroViewPanel_OnHeroDetailPanelBuyBtnClicked(object sender, EventArgs e)
    {
        OnHeroDetailPanelBuyBtnClicked(this, e);
    }
    private void M_heroViewPanel_OnHeroPanelClicked(object sender, HeroPanelClickedArgs e)
    {
        OnHeroPanelClicked(this, e);
    }

   
}
