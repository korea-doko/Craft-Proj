using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquipItemPanelClickedArgs :EventArgs
{
    public EEquipParts m_clickedParts;

    public EquipItemPanelClickedArgs(EEquipParts _clickedPart)
    {
        m_clickedParts = _clickedPart;
    }
}
public interface IEquipItemPanel
{
    event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;
}
public class EquipItemPanel : MonoBehaviour, IEquipItemPanel
{
    [SerializeField] private EEquipParts m_equipPartsName;
    [SerializeField] private Text m_text;
    [SerializeField] private Button m_btn;

    public event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;

    

    internal void Init(EEquipParts _partsName)
    {
        m_equipPartsName = _partsName;
        m_text = this.GetComponentInChildren<Text>();
        m_btn = this.GetComponent<Button>();
        m_btn.onClick.AddListener(() => OnEquipItemPanelClicked(this, new EquipItemPanelClickedArgs(m_equipPartsName)));

        m_text.text = "Init";
    }

    internal void Show(ItemData itemData)
    {
        if (itemData != null)
            m_text.text = itemData.GetItemBaseData.GetItemName;
        else
            m_text.text = "Item is not equipped";
    }

    internal void Clear()
    {
        m_text.text = "";
    }
}
