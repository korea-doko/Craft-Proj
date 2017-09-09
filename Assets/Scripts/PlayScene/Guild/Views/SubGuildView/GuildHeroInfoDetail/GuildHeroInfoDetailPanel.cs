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
public class OnItemRemoveButtonClickedArgs :EventArgs
{
    public HeroData m_hero;
    public EEquipParts m_removeParts;

    public OnItemRemoveButtonClickedArgs(HeroData _data, EEquipParts _parts)
    {
        m_hero = _data;
        m_removeParts = _parts;
    }
}

public interface IGuildHeroInfoDetailPanel
{
    event EventHandler<OnItemChangedButtonClickedArgs> OnItemChangedButtonClicked;
    event EventHandler <OnItemRemoveButtonClickedArgs> OnItemRemoveButtonClicked;
}
public class GuildHeroInfoDetailPanel : MonoBehaviour, IGuildHeroInfoDetailPanel {

    [SerializeField] private bool m_isActive;
    [SerializeField] private Button m_backBtn;
    [SerializeField] private Button m_itemChangeBtn;
    [SerializeField] private Button m_itemRemoveButton;
    

    [SerializeField] private Text m_baseInfoText;
    [SerializeField] private EquipPanel m_equipPanel;
    [SerializeField] private HeroData m_selectedHeroData;
    [SerializeField] private Text m_infoText;

    [SerializeField] private EEquipParts m_itemWantedToChangePart;
    [SerializeField] private bool m_isItemClicked;

    

    public event EventHandler<OnItemChangedButtonClickedArgs> OnItemChangedButtonClicked;
    public event EventHandler<OnItemRemoveButtonClickedArgs> OnItemRemoveButtonClicked;

    internal void Init()
    {
        m_equipPanel = this.GetComponentInChildren<EquipPanel>();
        m_equipPanel.Init();
        m_equipPanel.OnEquipItemPanelClicked += M_equipPanel_OnEquipItemPanelClicked;
        m_backBtn.onClick.AddListener(() => Hide());


        m_itemChangeBtn.onClick.AddListener(() => OnItemChangedButtonClicked(this, new OnItemChangedButtonClickedArgs(m_itemWantedToChangePart)));
        m_itemRemoveButton.onClick.AddListener(() => ItemRemoveButtonClicked());
        
        m_isItemClicked = false;       
        Hide();
    }

   

    public void Show(HeroData _data)
    {
        m_selectedHeroData = _data;
        m_equipPanel.Show(_data);
        m_baseInfoText.text = _data.GetName+ "\t" + _data.GetHeroClass.ToString() + " \t "+ _data.GetLevel.ToString() + "레벨";
        m_infoText.text = _data.GetHeroInfos();
        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }

    private void ItemRemoveButtonClicked()
    {
        if (m_isItemClicked)
            OnItemRemoveButtonClicked(this, new OnItemRemoveButtonClickedArgs(m_selectedHeroData, m_itemWantedToChangePart));        
    }



    // 이벤트 핸들러

    private void M_equipPanel_OnEquipItemPanelClicked(object sender, EquipItemPanelClickedArgs e)
    {
        if( m_isItemClicked )
        {
            // 아이템 클릭이 되었음.
            // 그렇다면 같은 부위 클릭 시, 전체 스테이터스 보여주면서
            // 아이템이 클릭이 안되었다고 해야함.
            // 다른 부위 클릭 시, 해당 아이템을 보여주고
            // 여전히 아이템은 클릭 되어 있음
            if( m_itemWantedToChangePart == e.m_clickedParts)
            {
                m_infoText.text = m_selectedHeroData.GetHeroInfos();
                m_isItemClicked = false;
            }
            else
            {
                m_itemWantedToChangePart = e.m_clickedParts;

                ItemData itemData = m_selectedHeroData.GetEquipDataAry[(int)e.m_clickedParts];

                if (itemData != null)
                    m_infoText.text = itemData.GetItemInfo();
                else
                    m_infoText.text = "아이템이 없습니다.";                
            }
        }
        else
        {
            m_itemWantedToChangePart = e.m_clickedParts;
            m_isItemClicked = true;

            ItemData itemData = m_selectedHeroData.GetEquipDataAry[(int)e.m_clickedParts];

            if (itemData != null)
                m_infoText.text = itemData.GetItemInfo();
            else
                m_infoText.text = "아이템이 없습니다.";

        }      
    }
 

}
