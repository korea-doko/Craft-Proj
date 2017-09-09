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
        m_view.OnItemChangedButtonClicked += M_view_OnItemChangedButtonClicked;
        m_view.OnEquipItemInventorySlotClicked += M_view_OnEquipItemInventorySlotClicked;
        m_view.OnItemRemoveButtonClicked += M_view_OnItemRemoveButtonClicked;
    }

   

    public bool Load()
    {
        return m_view.Load();
    }

    public void MenuButtonClicked(MenuName menuName)
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


    // 이벤트 핸들러

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
                        equippedData = hero.GetEquipDataAry[(int)EEquipParts.Head];
                        break;
                    case ArmorLowerClass.BodyArmor:
                        equippedData = hero.GetEquipDataAry[(int)EEquipParts.Body];
                        break;
                    case ArmorLowerClass.Boots:
                        equippedData = hero.GetEquipDataAry[(int)EEquipParts.Foot];
                        break;
                    case ArmorLowerClass.Gloves:
                        equippedData = hero.GetEquipDataAry[(int)EEquipParts.GloveHand];
                        break;
                    default:
                        break;
                }
                break;
            case ItemUpperClass.Weapon:

                WeaponBaseData wbd = (WeaponBaseData)selectedSlot.ItemData.GetItemBaseData;

                equippedData = hero.GetEquipDataAry[(int)EEquipParts.WeaponHand];

                break;
            case ItemUpperClass.Misc:

                MiscBaseData mbd = (MiscBaseData)selectedSlot.ItemData.GetItemBaseData;

                switch (mbd.LowerClassName)
                {
                    case MiscLowerClass.Ring:
                        equippedData = hero.GetEquipDataAry[(int)EEquipParts.Finger];
                        break;
                    case MiscLowerClass.Amulet:
                        equippedData = hero.GetEquipDataAry[(int)EEquipParts.Neck];
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
        // 장착한 모드 적용시키기

        if (equippedData != null)
            hero.RemoveModTypeValueInItemData(equippedData);
        // 장착했던 것 모드 다 제거, 없으면 제거 안함

        selectedSlot.ItemData = equippedData;
        // 이미 착용되어 있던 장비를 다시 창고에 넣기

        m_view.ShowHeroInfoDetailPanel(m_model.SelectedHeroData);
        // 바뀐 정보 보여주기

    }
    private void M_view_OnGuildHeroInfoPanelClicked(object sender, GuildHeroInfoPanelClickedArgs e)
    {
        HeroData data = PlayerManager.Inst.GetHeroData(e.m_id);
        m_model.SelectedHeroData = data;
        m_view.ShowHeroInfoDetailPanel(data);
    }
    private void M_view_OnItemChangedButtonClicked(object sender, OnItemChangedButtonClickedArgs e)
    {
        Debug.Log("해당 부위의 아이템을 가져와서 보여주고 싶긴하다. " + e.m_changedParts.ToString());
        // 그러나 그냥 전체 아이템창 띄우자

        List<SlotData> inventory = StoreManager.Inst.GetSlotDataList;
        m_view.ShowEquipItemInventory(inventory);
    }
    private void M_view_OnItemRemoveButtonClicked(object sender, OnItemRemoveButtonClickedArgs e)
    {
        HeroData heroData = e.m_hero;
        EEquipParts parts = e.m_removeParts;

        ItemData itemData = heroData.GetEquipDataAry[(int)parts];

        if( itemData != null)
        {
            // 아이템이 존재해야 뺀다.

            heroData.RemoveItemParts(parts);
            StoreManager.Inst.AddItemData(itemData);
        }
        m_view.ShowHeroInfoDetailPanel(m_model.SelectedHeroData);
        // 화면 갱신
    }
}
