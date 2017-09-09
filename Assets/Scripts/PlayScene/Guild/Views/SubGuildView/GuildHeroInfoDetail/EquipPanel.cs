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

    [SerializeField] private EquipItemPanel[] m_equipItemPanelAry;

    public void Init()
    {
        m_equipItemPanelAry = this.GetComponentsInChildren<EquipItemPanel>();

        for(int i = 0; i < m_equipItemPanelAry.Length;i++)
        {
            EquipItemPanel p = m_equipItemPanelAry[i];
            p.Init((EEquipParts)i);
            p.OnEquipItemPanelClicked += P_OnEquipItemPanelClicked;
            
        }
    }
 
    public void Show(HeroData data)
    {
        ClearAry();

        for(int i = 0; i < data.GetEquipDataAry.Length;i++)
        {
            EquipItemPanel p = m_equipItemPanelAry[i];
            ItemData itemData = data.GetEquipDataAry[i];
            p.Show(itemData);
        }
    }


    private void ClearAry()
    {
        for(int i = 0; i < m_equipItemPanelAry.Length;i++)
        {
            EquipItemPanel p = m_equipItemPanelAry[i];
            p.Clear();
        }
    }

    // 이벤트 핸들러
    private void P_OnEquipItemPanelClicked(object sender, EquipItemPanelClickedArgs e)
    {
        OnEquipItemPanelClicked(this, e);
    }
}
