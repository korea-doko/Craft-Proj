using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour {

    [SerializeField] RectTransform m_rect;
    [SerializeField] private bool m_isActive;
    [SerializeField] private Button m_closeBtn;
    [SerializeField] private Button m_sellBtn;
    [SerializeField] private Text m_infoText;

    internal void Init()
    {
        m_rect = this.GetComponent<RectTransform>();

        m_closeBtn.onClick.AddListener(() => { Hide(); });

        Hide();
    }
    public void Hide()
    {
        m_isActive = false;
        this.gameObject.SetActive(false);
    } 
    internal void ShowItemInfoPanel(ItemData data)
    {


        string infoText = "Name = " + data.GetItemBaseData.GetItemName + " \n" 
        + "Rarity = " + data.GetItemRarity.ToString() + "\n"
        + "ItemUpperClass = " + data.GetItemBaseData.GetItemUpperClass.ToString() + "\n"
        + "ItemLevel = " + data.GetItemBaseData.GetItemLevel.ToString() + "\n"
        + "Str = " + data.GetItemBaseData.GetBaseItemRequiredAttribute.Str.ToString() + "|" 
        + "Dex = " + data.GetItemBaseData.GetBaseItemRequiredAttribute.Dex.ToString() + "|" 
        + "Int = " + data.GetItemBaseData.GetBaseItemRequiredAttribute.Int.ToString() + "\n";

       

        m_infoText.text = infoText;

        m_isActive = true;
        this.gameObject.SetActive(true);
    }




}
