using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHeroViewPanel
{
    event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;
}
public class HeroViewPanel : MonoBehaviour ,IHeroViewPanel{

    [SerializeField] private RectTransform m_rect;
    [SerializeField] private List<HeroPanel> m_heroPanelList;

    public event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;

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
            heroPanel.transform.SetParent(this.transform);
            m_heroPanelList.Add(heroPanel);
        }
    }

    private void HeroPanel_OnHeroPanelClicked(object sender, HeroPanelClickedArgs e)
    {
        OnHeroPanelClicked(this, e);
    }

    public void Load(float _width, float _height)
    {
        float preferredWidth = _width;
        float preferredHeight = _height / 5;

        for(int i = 0; i < 20;i++)
        {
            m_heroPanelList[i].Load(preferredWidth, preferredHeight);
        }
    }

    internal void Hide()
    {
        HidePanelAll();
    }

    internal void Show(HeroModel model)
    {
        HidePanelAll();

        List<HeroData> list = model.AvailableHeroDataList;

        for (int i = 0; i < list.Count; i++)
        {
            HeroData data = list[i];

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
    void HidePanelAll()
    {
        foreach (HeroPanel panel in m_heroPanelList)
            panel.Hide();
    }
}
