using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeManager : IManager, ILoadable
{

}

public class UpgradeManager : MonoBehaviour,IUpgradeManager
{

    private UpgradeView m_view;
    private UpgradeModel m_model;

    private static UpgradeManager m_inst;
    public static UpgradeManager Inst
    {
        get { return m_inst; }
    }
    public UpgradeManager()
    {
        m_inst = this;
    }

    public void InitManager()
    {
        m_model = Utils.MakeGameObjectWithComponent<UpgradeModel>(this.gameObject);
        m_model.InitModel();

        m_view = Utils.MakeGameObjectWithComponent<UpgradeView>(this.gameObject);
        m_view.InitView(m_model);
        m_view.OnItemSelectButtonClicked += M_view_OnItemSelectButtonClicked;
        m_view.OnItemSelectSlotClicked += M_view_OnItemSelectSlotClicked;
        m_view.OnRuneButtonClicked += M_view_OnRuneButtonClicked;
    }
    public void MenuButtonClicked(MenuName menuName)
    {
        if (menuName == MenuName.Upgrade)
            m_view.Show();
        else
            m_view.Hide();
    }
    public bool Load()
    {
        return m_view.Load();
    }

    private bool IsPossibleToBeCrafted(ItemData _data, RuneName _name)
    {
        if (_data.IsCurrupted)
            return false;

        switch (_name)
        {
            case RuneName.Reinforcement:

                if (_data.GetItemRarity != ItemRarity.Magic)
                    return false;

                if (_data.GetNumOfPrefix == 1 && _data.GetNumOfSuffix == 1)
                    return false;

                return true;

            case RuneName.MagicPower:
            case RuneName.Alteration:

                if (_data.GetItemRarity != ItemRarity.Magic)
                    return false;
                else
                    return true;

            case RuneName.Unholy:

                if (_data.GetItemRarity != ItemRarity.Rare)
                    return false;

                if (_data.GetNumOfPrefix == 3 && _data.GetNumOfSuffix == 3)
                    return false;

                return true;

            case RuneName.Chaos:

                if (_data.GetItemRarity != ItemRarity.Rare)
                    return false;
                else
                    return true;

            case RuneName.BlackSmith:
            case RuneName.Luck:
            case RuneName.Wizard:

                if (_data.GetItemRarity != ItemRarity.Normal)
                    return false;
                else
                    return true;

            case RuneName.Purification:
            case RuneName.Divine:

                if (_data.GetItemRarity == ItemRarity.Normal)
                    return false;
                else
                    return true;

            case RuneName.Void:

                if (_data.GetItemRarity == ItemRarity.Normal || _data.GetItemRarity == ItemRarity.Unique)
                    return false;

                if (_data.GetNumOfPrefix == 0 && _data.GetNumOfSuffix == 0)
                    return false;

                return true;

            case RuneName.Curruption:
                    return true;
                
            default:
                return false;
        }
    }


    // 이벤트 핸들러
    private void M_view_OnItemSelectSlotClicked(object sender, ItemSelectSlotArgs e)
    {
        SlotData slotData = StoreManager.Inst.GetSlotData(e.m_slotID);
        ItemData itemData = slotData.ItemData;

        m_model.SelectedItemData = itemData;
        m_view.ShowSelectedItem(itemData);
        m_view.HideItemSelectInventoryPanel();
    }
    private void M_view_OnItemSelectButtonClicked(object sender, EventArgs e)
    {
        Debug.Log("Item select btn clicked");      
        List<SlotData> slotDataList = StoreManager.Inst.GetSlotDataList;
        m_view.ShowItemSelectInventoryPanel(slotDataList);
    }
    private void M_view_OnRuneButtonClicked(object sender, RuneButtonClickArgs e)
    {
        Debug.Log(e.m_clickRune.ToString() + " 입력 됨 ");

        if (IsPossibleToBeCrafted(m_model.SelectedItemData, e.m_clickRune))
        {
            m_view.ShowItemInfoAtDescPanel(m_model.SelectedItemData);
            ItemManager.Inst.CraftItem(m_model.SelectedItemData, e.m_clickRune);
            m_view.ShowSelectedItem(m_model.SelectedItemData);
            m_view.HideItemSelectInventoryPanel();
        }
        else
        {
            Debug.Log("아이템과 룬의 사용이 맞지 않음 인풋 룬  = " + e.m_clickRune.ToString());
        }
    }
}
