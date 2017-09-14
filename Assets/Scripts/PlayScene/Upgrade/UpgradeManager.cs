using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeManager : IManager, ILoadable,IUpdatable
{

}

public class UpgradeManager : MonoBehaviour, IUpgradeManager
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
        m_view.OnRuneButtonLongPressed += M_view_OnRuneButtonLongPressed;
    }

    

    public void MenuButtonClicked(MenuName menuName)
    {
        if (menuName == MenuName.Upgrade)
        {
            int[] playerOwnedRunes = PlayerManager.Inst.GetOwnedRunes();
            m_view.Show(playerOwnedRunes);
        }
        else
            m_view.Hide();
    }
    public bool Load()
    {
        return m_view.Load();
    }
    public void UpdateThis()
    {
        m_view.UpdateThis();
    }

    private bool IsPossibleToBeCrafted(ItemData _data, RuneType _name)
    {
        if (_data.IsCurrupted)
            return false;

        switch (_name)
        {
            case RuneType.Reinforcement:

                if (_data.GetItemRarity != ItemRarity.Magic)
                    return false;

                if (_data.GetNumOfPrefix == 1 && _data.GetNumOfSuffix == 1)
                    return false;

                return true;

            case RuneType.MagicPower:
            case RuneType.Alteration:

                if (_data.GetItemRarity != ItemRarity.Magic)
                    return false;
                else
                    return true;

            case RuneType.Unholy:

                if (_data.GetItemRarity != ItemRarity.Rare)
                    return false;

                if (_data.GetNumOfPrefix == 3 && _data.GetNumOfSuffix == 3)
                    return false;

                return true;

            case RuneType.Chaos:

                if (_data.GetItemRarity != ItemRarity.Rare)
                    return false;
                else
                    return true;

            case RuneType.BlackSmith:
            case RuneType.Luck:
            case RuneType.Wizard:

                if (_data.GetItemRarity != ItemRarity.Normal)
                    return false;
                else
                    return true;

            case RuneType.Purification:
            case RuneType.Divine:

                if (_data.GetItemRarity == ItemRarity.Normal)
                    return false;
                else
                    return true;

            case RuneType.Void:

                if (_data.GetItemRarity == ItemRarity.Normal || _data.GetItemRarity == ItemRarity.Unique)
                    return false;

                if (_data.GetNumOfPrefix == 0 && _data.GetNumOfSuffix == 0)
                    return false;

                return true;

            case RuneType.Curruption:
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
        if (!m_model.IsSelectedItemExist)
            return;

        if (IsPossibleToBeCrafted(m_model.SelectedItemData, e.m_clickRune))
        {
            ItemManager.Inst.CraftItem(m_model.SelectedItemData, e.m_clickRune);
            m_view.ShowSelectedItem(m_model.SelectedItemData);
            m_view.HideItemSelectInventoryPanel();

        }
    }
    private void M_view_OnRuneButtonLongPressed(object sender, RuneButtonLongPressedArgs e)
    {
        IAlarmTrigger runeDataAlarmTrigger = ItemManager.Inst.GetRuneData(e.m_pressedRune);

        AlarmManager.Inst.Alarm(runeDataAlarmTrigger);
    }
}