using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IHeroDetailPanel
{
    event EventHandler OnHeroDetailPanelBackBtnClicked;
    event EventHandler OnHeroDetailPanelBuyBtnClicked;
}

public class HeroDetailPanel : MonoBehaviour , IHeroDetailPanel{

    [SerializeField] private bool m_isActive;
    [SerializeField] private Text m_levelText;
    [SerializeField] private Text m_specialityText;
    [SerializeField] private Text m_classText;
    [SerializeField] private Text m_personalityText;
    [SerializeField] private Text m_nameText;
    [SerializeField] private Text m_costText;

    [SerializeField] private Text[] m_baseAttrText;
    [SerializeField] private Text[] m_offsetAttrText;
    [SerializeField] private Text[] m_traitText;

    [SerializeField] private Text[] m_equipPartsTextAry;



    [SerializeField] private Button m_backBtn;
    [SerializeField] private Button m_buyBtn;
    [SerializeField] private Button m_extensionBtn;
    [SerializeField] private Button m_kickBtn;

    public event EventHandler OnHeroDetailPanelBackBtnClicked;
    public event EventHandler OnHeroDetailPanelBuyBtnClicked;

    internal void Init()
    {
        m_backBtn.onClick.AddListener(() => OnHeroDetailPanelBackBtnClicked(this, EventArgs.Empty));
        m_buyBtn.onClick.AddListener(() => OnHeroDetailPanelBuyBtnClicked(this, EventArgs.Empty));

        Hide();
    }

    public void Show(HeroData _data)
    {
        m_levelText.text = _data.GetLevel.ToString();
        m_specialityText.text = _data.GetSpeciality.Name;
        m_classText.text = _data.GetHeroClass.ToString();
        m_personalityText.text = _data.GetPersonality.Name;
        m_nameText.text = _data.GetName;

        Attribute baseAttr = _data.GetBaseAttribute;
        for (int i = 0; i < 3; i++)
            m_baseAttrText[i].text = baseAttr.Stats[i].ToString();

        Attribute offsetAttr = _data.GetOffsetAttribute;
        for (int i = 0; i < 3; i++)
            m_offsetAttrText[i].text = offsetAttr.Stats[i].ToString();

        int countOfTrait = _data.GetTraitList.Count;

        for (int i = 0; i < countOfTrait; i++)
            m_traitText[i].text = _data.GetTraitList[i].Name;

        ItemData[] itemAry = _data.GetEquipDataAry;
      
        for(int i = 0; i < itemAry.Length;i++)
        {
            ItemData data = itemAry[i];
            Text text = m_equipPartsTextAry[i];

            if (data == null)
            {
                text.text = " - ";
            }
            else
            {
                ItemRarity rarirty = data.GetItemRarity;
                switch (rarirty)
                {
                    case ItemRarity.Normal:
                        text.text = "<color=white>";
                        break;
                    case ItemRarity.Magic:
                        text.text = "<color=blue>";
                        break;
                    case ItemRarity.Rare:
                        text.text = "<color=yellow>";
                        break;
                    case ItemRarity.Unique:
                        text.text = "<color=orange>";
                        break;
                    default:
                        break;
                }
                text.text += " ?</color>";
            }
        }
      
       
        m_costText.text = "-1";
        Debug.Log("코스트 나중에 컴포짓으로해서 하자... ");  
       

        
        m_isActive = true;
        this.gameObject.SetActive(m_isActive);
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(m_isActive);
    }
}
