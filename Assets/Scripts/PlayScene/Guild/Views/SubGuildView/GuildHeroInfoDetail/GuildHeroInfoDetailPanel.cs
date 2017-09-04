using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IGuildHeroInfoDetailPanel
{
    event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;
}
public class GuildHeroInfoDetailPanel : MonoBehaviour, IGuildHeroInfoDetailPanel {

    [SerializeField] private bool m_isActive;
    [SerializeField] private Button m_btn;
    [SerializeField] private Text m_testText;

    [SerializeField] private EquipPanel m_equipPanel;

    public event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;

    internal void Init()
    {
        m_equipPanel = this.GetComponentInChildren<EquipPanel>();
        m_equipPanel.Init();
        m_equipPanel.OnEquipItemPanelClicked += M_equipPanel_OnEquipItemPanelClicked;
        m_btn.onClick.AddListener(() => Hide());
        Hide();
    }

    private void M_equipPanel_OnEquipItemPanelClicked(object sender, EquipItemPanelClickedArgs e)
    {
        OnEquipItemPanelClicked(this, e);
    }

    public void Show(HeroData _data)
    {
        m_equipPanel.Show(_data);
        m_testText.text = _data.GetHeroClass.ToString();
        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
}
