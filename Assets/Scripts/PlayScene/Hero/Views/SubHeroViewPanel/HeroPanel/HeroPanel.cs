﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HeroPanelClickedArgs :EventArgs
{
    public int m_clickedID;

    public HeroPanelClickedArgs(int _clickedID)
    {
        m_clickedID = _clickedID;
    }
}
public interface IHeroPanel
{
    event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;
}

public class HeroPanel : MonoBehaviour , IHeroPanel{

    [SerializeField] private bool m_isHidden;
    [SerializeField] private LayoutElement m_layoutEle;
    [SerializeField] private Button m_detailInfobtn;
    
    [SerializeField] private int m_id;

    [SerializeField] private SimpleHeroInfoPanel m_simpleHeroInfoPanel;
    [SerializeField] private CountDownPanel m_countDownPanel; 



    public event EventHandler<HeroPanelClickedArgs> OnHeroPanelClicked;

    public bool IsHidden
    {
        get
        {
            return m_isHidden;
        }

        set
        {
            m_isHidden = value;
        }
    }    
    public void Init(int _id)
    {
        m_id = _id;
        m_layoutEle = this.GetComponent<LayoutElement>();

        m_simpleHeroInfoPanel.Init();
        m_countDownPanel.Init();
        Hide();

        m_detailInfobtn.onClick.AddListener(() => OnHeroPanelClicked(this, new HeroPanelClickedArgs(m_id)));
        
    }
    public void Load(float _width, float _height)
    {
        m_layoutEle.preferredWidth = _width;
        m_layoutEle.preferredHeight = _height;
    }
    public void Hide()
    {
        m_isHidden = true;
        this.gameObject.SetActive(false);
    }
    public void Show(HeroDataWithLimitedTime _data)
    {
        m_simpleHeroInfoPanel.Show(_data.m_heroData);
        m_countDownPanel.Show(_data.m_limitedTime);

        m_isHidden = false;
        this.gameObject.SetActive(true);
    }

}
