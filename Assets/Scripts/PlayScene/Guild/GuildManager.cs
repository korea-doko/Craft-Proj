using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGuildManager:IManager,ILoadable
{

}
public class GuildManager : MonoBehaviour ,IGuildManager{

    private GuildModel m_model;
    private GuildView m_view;

    private static GuildManager m_inst;
    public static GuildManager Inst
    {
        get
        {
            return m_inst;
        }
    }
    public GuildManager()
    {
        m_inst = this;
    }

    public void InitManager()
    {
        m_model = Utils.MakeGameObjectWithComponent<GuildModel>(this.gameObject);
        m_model.InitModel();

        m_view = Utils.MakeGameObjectWithComponent<GuildView>(this.gameObject);
        m_view.InitView(m_model);
        m_view.OnGuildHeroInfoPanelClicked += M_view_OnGuildHeroInfoPanelClicked;
        m_view.OnEquipItemPanelClicked += M_view_OnEquipItemPanelClicked;
        m_view.OnEquipItemInventorySlotClicked += M_view_OnEquipItemInventorySlotClicked;
    }

    private void M_view_OnEquipItemInventorySlotClicked(object sender, EquipItemInventorySlotClickedArgs e)
    {
        // 만약에 해당 아이템에 해당되는 아이템이 이미 착용되어 있다면
        // 그것을 다시 창고로 보내주고
        // 아니라면 그냥 착용한다.

        List<SlotData> slotDataList = StoreManager.Inst.GetSlotDataList;
        SlotData selectedSlot = slotDataList[e.m_id];
                
        HeroData hero = m_model.SelectedHeroData;
        // 내가 누른 히어로 정보 가져오기

        ItemData selectedItem = selectedSlot.ItemData;
        // 내가 선택한 아이템, 나중에 이것을 끼워준다.

        ItemData equippedData = null;
        // 착용된 장비가 있는가?
        switch (selectedSlot.ItemData.GetItemBaseData.GetItemUpperClass)
        {
            case ItemUpperClass.Armor:

                ArmorBaseData abd = (ArmorBaseData)selectedSlot.ItemData.GetItemBaseData;
                switch (abd.LowerClassName)
                {
                    case ArmorLowerClass.Helmet:
                        equippedData = hero.EquipDataAry[(int)EEquipParts.Head];
                        break;
                    case ArmorLowerClass.BodyArmor:
                        equippedData = hero.EquipDataAry[(int)EEquipParts.Body];
                        break;
                    case ArmorLowerClass.Boots:
                        equippedData = hero.EquipDataAry[(int)EEquipParts.Foot];
                        break;
                    case ArmorLowerClass.Gloves:
                        equippedData = hero.EquipDataAry[(int)EEquipParts.GloveHand];
                        break;
                    default:
                        break;
                }
                break;
            case ItemUpperClass.Weapon:

                WeaponBaseData wbd = (WeaponBaseData)selectedSlot.ItemData.GetItemBaseData;

                equippedData = hero.EquipDataAry[(int)EEquipParts.WeaponHand];

                break;
            case ItemUpperClass.Misc:

                MiscBaseData mbd = (MiscBaseData)selectedSlot.ItemData.GetItemBaseData;

                switch (mbd.LowerClassName)
                {
                    case MiscLowerClass.Ring:
                        equippedData = hero.EquipDataAry[(int)EEquipParts.Finger];
                        break;
                    case MiscLowerClass.Amulet:
                        equippedData = hero.EquipDataAry[(int)EEquipParts.Neck];
                        break;
                    default:
                        break;
                }

                break;
            default:
                break;
        }
        // 착용된 장비 부위 찾아내서 가져오기


        hero.EquipItemWith(selectedItem);
        // 히어로한테 아이템을 넘겨주면 부위에 맞는 것으로 착용

        selectedSlot.ItemData = equippedData;
        // 이미 착용되어 있던 장비를 다시 창고에 넣기

        m_view.ShowHeroInfoDetailPanel(m_model.SelectedHeroData);
        // 바뀐 정보 보여주기

    }

    private void M_view_OnEquipItemPanelClicked(object sender, EquipItemPanelClickedArgs e)
    {
        List<SlotData> slotDataList = StoreManager.Inst.GetSlotDataList;
       
        m_view.ShowEquipItemInventory(slotDataList);        
    }

    private void M_view_OnGuildHeroInfoPanelClicked(object sender, GuildHeroInfoPanelClickedArgs e)
    {
        HeroData data = PlayerManager.Inst.GetHeroData(e.m_id);
        m_model.SelectedHeroData = data;
        m_view.ShowHeroInfoDetailPanel(data);
    }

    public bool Load()
    {
        return m_view.Load();
    }

    internal void MenuButtonClicked(MenuName menuName)
    {
        if (menuName != MenuName.Guild)
        {
            m_view.HideAll();
        }
        else
        {
            List<HeroData> heroList = PlayerManager.Inst.GetOwnedHeroDataList();
            m_view.ShowAll(heroList);
        }
    }
}
