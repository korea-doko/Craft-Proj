using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipPanel
{
    event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;
}
public class EquipPanel : MonoBehaviour , IEquipPanel{

    public event EventHandler<EquipItemPanelClickedArgs> OnEquipItemPanelClicked;

    private EquipItemPanel[] m_equipItemPanelAry;

    internal void Init()
    {
        m_equipItemPanelAry = this.GetComponentsInChildren<EquipItemPanel>();

        for(int i = 0; i < m_equipItemPanelAry.Length;i++)
        {
            EquipItemPanel p = m_equipItemPanelAry[i];
            p.Init((EEquipParts)i);
            p.OnEquipItemPanelClicked += P_OnEquipItemPanelClicked;
            
        }
    }

    private void P_OnEquipItemPanelClicked(object sender, EquipItemPanelClickedArgs e)
    {
        OnEquipItemPanelClicked(this, e);
    }

    internal void Show(HeroData data)
    {
        for(int i = 0; i < data.EquipDataAry.Length;i++)
        {
            EquipItemPanel p = m_equipItemPanelAry[i];
            ItemData itemData = data.EquipDataAry[i];
            p.Show(itemData);
        }
    }
}
