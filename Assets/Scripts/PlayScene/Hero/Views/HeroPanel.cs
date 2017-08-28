using System.Collections;
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
    [SerializeField] private Button m_btn;
    [SerializeField] private int m_id;

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
        m_btn = this.GetComponent<Button>();
        m_btn.onClick.AddListener(() => OnHeroPanelClicked(this, new HeroPanelClickedArgs(m_id)));
        Hide();
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
    public void Show(HeroData data)
    {
        m_isHidden = false;
        this.gameObject.SetActive(true);
    }

}
