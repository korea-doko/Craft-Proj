using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGuildHeroPanel
{
    event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
}

public class GuildHeroPanel : MonoBehaviour , IGuildHeroPanel{

    [SerializeField] private GuildHeroScrollRect m_scrollRect;

    public event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;

    internal void Init()
    {
        m_scrollRect = this.GetComponentInChildren<GuildHeroScrollRect>();
        m_scrollRect.Init();
        m_scrollRect.OnGuildHeroInfoPanelClicked += M_scrollRect_OnGuildHeroInfoPanel;
    }

    private void M_scrollRect_OnGuildHeroInfoPanel(object sender, GuildHeroInfoPanelClickedArgs e)
    {
        OnGuildHeroInfoPanelClicked(this, e);
    }

    internal void Show(List<HeroData> heroList)
    {
        m_scrollRect.Show(heroList);
    }
}
