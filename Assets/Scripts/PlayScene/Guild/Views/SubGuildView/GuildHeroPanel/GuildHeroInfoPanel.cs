using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildHeroInfoPanelClickedArgs : EventArgs
{
    public int m_id;
    public GuildHeroInfoPanelClickedArgs(int _id)
    { m_id = _id; }
}
public interface IGuildHeroInfoPanel
{
    event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;
}
public class GuildHeroInfoPanel : MonoBehaviour, IGuildHeroInfoPanel {

    [SerializeField] private int m_id;
    [SerializeField] private bool m_isActive;
    [SerializeField] private Button m_btn;

    [SerializeField] private Text m_simpleInfoText;
    [SerializeField] private Text m_coolDownText;


    public event EventHandler<GuildHeroInfoPanelClickedArgs> OnGuildHeroInfoPanelClicked;

    public bool IsActive
    {
        get
        {
            return m_isActive;
        }

        set
        {
            m_isActive = value;
        }
    }

    internal void Init(int _id)
    {
        m_id = _id;
        m_btn.onClick.AddListener(() => OnGuildHeroInfoPanelClicked(this, new GuildHeroInfoPanelClickedArgs(m_id)));
        Hide();
    }

    public void Show(HeroData _data)
    {
        m_simpleInfoText.text = _data.GetName + "\n" + _data.GetHeroClass.ToString();
        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
}
