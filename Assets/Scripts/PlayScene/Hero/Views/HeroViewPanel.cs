using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IHeroViewPanel
{
    event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;
    event EventHandler OnHeroDetailPanelBuyBtnClicked;
}
public class HeroViewPanel : MonoBehaviour ,IHeroViewPanel{

    [SerializeField] private RectTransform m_rect;
    [SerializeField] private List<HeroPanel> m_heroPanelList;
    [SerializeField] private HeroDetailPanel m_heroDetailPanel;
    [SerializeField] private HeroTimeCountPanel m_heroTimeCountPanel;
    [SerializeField] private HeroNumCountPanel m_heroNumCountPanel;
    [SerializeField] private GameObject m_heroPanelParent;
    [SerializeField] private Text m_visitedText;

    public event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;
    public event EventHandler OnHeroDetailPanelBuyBtnClicked;

    public RectTransform Rect
    {
        get
        {
            return m_rect;
        }
    }

    public void Init()
    {
        m_rect = this.GetComponent<RectTransform>();

        GameObject obj = Resources.Load("PlayScene/Hero/HeroPanel") as GameObject;
        for(int i = 0; i < 20;i++)
        {
            HeroPanel heroPanel = ((GameObject)Instantiate(obj)).GetComponent<HeroPanel>();
            heroPanel.Init(i);
            heroPanel.OnHeroPanelClicked += HeroPanel_OnHeroPanelClicked;
            heroPanel.transform.SetParent(m_heroPanelParent.transform);
            m_heroPanelList.Add(heroPanel);
        }

        m_heroTimeCountPanel.Init();
        m_heroNumCountPanel.Init();
        m_heroDetailPanel.Init();

        m_heroDetailPanel.OnHeroDetailPanelBackBtnClicked += M_heroDetailPanel_OnHeroDetailPanelBackBtnClicked;
        m_heroDetailPanel.OnHeroDetailPanelBuyBtnClicked += M_heroDetailPanel_OnHeroDetailPanelBuyBtnClicked;
    }

    public void ShowOwnedHeroAndLimit(int numOfOwn, int numOfLimit)
    {
        m_heroNumCountPanel.Show(numOfOwn, numOfLimit);
    }
    public void ShowVisitedHero(int _curVisited,int _limit)
    {
        m_visitedText.text = _curVisited.ToString() + " / " + _limit.ToString();
    }
    public void HideDetailPanel()
    {
        m_heroDetailPanel.Hide();
    }

    public void Load(float _width, float _height)
    {
        float preferredWidth = _width;
        float preferredHeight = _height / 5;

        for (int i = 0; i < 20; i++)
        {
            m_heroPanelList[i].Load(preferredWidth, preferredHeight);
        }
    }

    public void ShowDetailPanel(HeroData _data)
    {
        m_heroDetailPanel.Show(_data);
    }
       
    public void UpdateRegenTime(int _currentTime, int _regenTime)
    {
        m_heroTimeCountPanel.UpdateRegenTime(_currentTime, _regenTime);
    }
   
    public void ShowHeroPanel(List<HeroDataWithLimitedTime> _heroDataWithLimitTimeList)
    {
        foreach (HeroPanel panel in m_heroPanelList)
            panel.Hide();

        int count = _heroDataWithLimitTimeList.Count;
        
        for (int i = 0; i < count; i++)
        {
            HeroDataWithLimitedTime data = _heroDataWithLimitTimeList[i];

            for (int j = 0; j < m_heroPanelList.Count; j++)
            {
                HeroPanel panel = m_heroPanelList[j];

                if (panel.IsHidden)
                {
                    panel.Show(data);
                    break;
                }
                else
                    continue;
            }
        }
    }
    public void Hide()
    {
        m_heroDetailPanel.Hide();     
    }
    

    /// 이벤트 핸들러

    private void M_heroDetailPanel_OnHeroDetailPanelBuyBtnClicked(object sender, EventArgs e)
    {
        OnHeroDetailPanelBuyBtnClicked(this, e);
        m_heroDetailPanel.Hide();
    }
    private void M_heroDetailPanel_OnHeroDetailPanelBackBtnClicked(object sender, EventArgs e)
    {
        m_heroDetailPanel.Hide();
    }
    private void HeroPanel_OnHeroPanelClicked(object sender, HeroPanelClickedArgs e)
    {
        OnHeroPanelClicked(this, e);
    }
}
