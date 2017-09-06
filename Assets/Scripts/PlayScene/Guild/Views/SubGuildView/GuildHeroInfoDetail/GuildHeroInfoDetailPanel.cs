using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnItemChangedButtonClickedArgs : EventArgs
{
    public EEquipParts m_changedParts;

    public OnItemChangedButtonClickedArgs(EEquipParts _parts)
    {
        m_changedParts = _parts;
    }

}

public interface IGuildHeroInfoDetailPanel
{
    event EventHandler<OnItemChangedButtonClickedArgs> OnClickItemChangedButton;
}
public class GuildHeroInfoDetailPanel : MonoBehaviour, IGuildHeroInfoDetailPanel {

    [SerializeField] private bool m_isActive;
    [SerializeField] private Button m_backBtn;
    [SerializeField] private Button m_itemChangeBtn;
    [SerializeField] private Button m_statsBtn;

    [SerializeField] private Text m_baseInfoText;
    [SerializeField] private EquipPanel m_equipPanel;
    [SerializeField] private HeroData m_selectedHeroData;
    [SerializeField] private Text m_infoText;

    [SerializeField] private EEquipParts m_itemWantedToChangePart;

    public event EventHandler<OnItemChangedButtonClickedArgs> OnClickItemChangedButton;

    internal void Init()
    {
        m_equipPanel = this.GetComponentInChildren<EquipPanel>();
        m_equipPanel.Init();
        m_equipPanel.OnEquipItemPanelClicked += M_equipPanel_OnEquipItemPanelClicked;
        m_backBtn.onClick.AddListener(() => Hide());

        m_itemChangeBtn.onClick.AddListener(() => OnClickItemChangedButton(this, new OnItemChangedButtonClickedArgs(m_itemWantedToChangePart)));

        m_statsBtn.onClick.AddListener(() => { Debug.Log("스텟 보여주기 해야함"); });

        Hide();
    }

  

    public void Show(HeroData _data)
    {

        m_selectedHeroData = _data;
        m_equipPanel.Show(_data);
        m_baseInfoText.text = _data.GetName+ "\t" + _data.GetHeroClass.ToString() + " \t "+ _data.GetLevel.ToString() + "레벨";
        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }

    // 이벤트 핸들러

    private void M_equipPanel_OnEquipItemPanelClicked(object sender, EquipItemPanelClickedArgs e)
    {
        ItemData itemData = m_selectedHeroData.GetEquipDataAry[(int)e.m_clickedParts];

        if (itemData != null)
            m_infoText.text = itemData.GetItemInfo();
        else
            m_infoText.text = "아이템이 없습니다.";
    }
}
