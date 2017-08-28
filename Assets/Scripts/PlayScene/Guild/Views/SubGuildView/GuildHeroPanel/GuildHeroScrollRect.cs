using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuildHeroScrollRect
{
    event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
}

public class GuildHeroScrollRect : MonoBehaviour ,IGuildHeroScrollRect{

    [SerializeField] private List<GuildHeroInfoPanel> m_guildHeroInfoPanelList;

    public event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;

    public void Init()
    {
        m_guildHeroInfoPanelList = new List<GuildHeroInfoPanel>();

        GameObject prefab = Resources.Load("PlayScene/Guild/GuildHeroInfoPanel") as GameObject;

        for (int i = 0; i < 10; i++)
        {
            GuildHeroInfoPanel ghip = ((GameObject)Instantiate(prefab)).GetComponent<GuildHeroInfoPanel>();
            ghip.Init(i);
            ghip.OnGuildHeroInfoPanelClicked += Ghip_OnGuildHeroInfoPanel;
            ghip.gameObject.transform.SetParent(this.transform);
            m_guildHeroInfoPanelList.Add(ghip);
        }
    }

    private void Ghip_OnGuildHeroInfoPanel(object sender, GuildHeroInfoPanelClickedArgs e)
    {
        OnGuildHeroInfoPanelClicked(this, e); 
    }

    internal void Show(List<HeroData> heroList)
    {
        HideAll();

        for(int i = 0; i < heroList.Count;i++)
        {
            HeroData data = heroList[i];

            GuildHeroInfoPanel infoPanel = GetAvailableInfoPanel();

            if (infoPanel == null)
                Debug.Log("부족");

            infoPanel.Show(data);
        }
    }

    GuildHeroInfoPanel GetAvailableInfoPanel()
    {
        for(int i = 0; i < m_guildHeroInfoPanelList.Count;i++)
        {
            GuildHeroInfoPanel infoPanel = m_guildHeroInfoPanelList[i];

            if (!infoPanel.IsActive)
                return infoPanel;
        }
        return null;
    }
    void HideAll()
    {
        for (int i = 0; i < m_guildHeroInfoPanelList.Count; i++)
            m_guildHeroInfoPanelList[i].Hide();
    }
    
    
}
