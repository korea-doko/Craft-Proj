﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemManager  : IManager, IUpdatable
{

}
public class ItemManager : MonoBehaviour,IItemManager
{
    private ItemModel m_model;
    private ItemView m_view;
    
    private static ItemManager m_inst;
    public static ItemManager Inst
    {
        get
        {
            return m_inst;
        }        
    }
    public ItemManager()
    {
        m_inst = this;
    }
     
    public void InitManager()
    {
        m_inst = this;

        m_model = Utils.MakeGameObjectWithComponent<ItemModel>(this.gameObject);
        m_model.InitModel();

        m_view = Utils.MakeGameObjectWithComponent<ItemView>(this.gameObject);
        m_view.InitView(m_model);

    }
    public void UpdateThis()
    {


        if (Input.GetKeyDown(KeyCode.Space))
            TestMakeWeapon();

        if( Input.GetKeyDown(KeyCode.B))
        {
            ItemData data = GenerateArmor();
            StoreManager.Inst.AddItemData(data);
        }

        if( Input.GetKeyDown(KeyCode.R))
        {
            ItemData data = GenerateMisc();
            StoreManager.Inst.AddItemData(data);
        }
    }

    public void TestMakeWeapon()
    {
        ItemData data = GenerateWeapon();
        StoreManager.Inst.AddItemData(data);
    }
    

    public ModData GetPrefix(ItemData _data)
    {
        ModData prefix = m_model.GetPrefixData();
        ModData copiedPrefix = new ModData(prefix);
        return copiedPrefix;
    }
    public ModData GetSuffix(ItemData _data)
    {
        ModData suffix = m_model.GetSuffixData();
        ModData copiedSuffix = new ModData(suffix);
        return copiedSuffix;
    }

    public void CraftItem(ItemData _item, RuneType _name)
    {
        switch (_name)
        {
            case RuneType.Reinforcement:
                CraftReinforcement(_item);
                break;
            case RuneType.MagicPower:
                CraftMagicPower(_item);
                break;
            case RuneType.Unholy:
                CraftUnholy(_item);
                break;
            case RuneType.BlackSmith:
                CraftBlackSmith(_item);
                break;
            case RuneType.Luck:
                CraftLuck(_item);
                break;
            case RuneType.Wizard:
                CraftWizard(_item);
                break;
            case RuneType.Alteration:
                CraftAlteration(_item);
                break;
            case RuneType.Chaos:
                CraftChaos(_item);
                break;
            case RuneType.Purification:
                CraftPurification(_item);
                break;
            case RuneType.Void:
                CraftVoid(_item);
                break;
            case RuneType.Divine:
                CraftDivine(_item);
                break;
            case RuneType.Curruption:
                CraftCurruption(_item);
                break;
            default:
                break;
        }
    }

    public ItemData GenerateArmor()
    {
        ArmorBaseData abd = m_model.GetRandomArmorBaseData();
        return UpgradeToRandomRarity(abd);
    }
    public ItemData GenerateWeapon()
    {
        WeaponBaseData wbd = m_model.GetRandomWeaponBaseData();
        return UpgradeToRandomRarity(wbd);
    }
    public ItemData GenerateMisc()
    {
        MiscBaseData mbd = m_model.GetRandomMiscBaseData();
        return UpgradeToRandomRarity(mbd);
    }
    public RuneData GetRuneData(RuneType _type)
    {
        return m_model.GetRuneData(_type);
    }

    private ItemData UpgradeToRandomRarity(ItemBaseData _base)
    {
        ItemRarity rarity = (ItemRarity)UnityEngine.Random.Range(0, 4);

        ItemData data = new ItemData(_base, rarity);

        int numOfAddedSuffix = 0;
        int numOfAddedPrefix = 0;

        switch (rarity)
        {
            case ItemRarity.Normal:
                break;
            case ItemRarity.Magic:
                numOfAddedPrefix = UnityEngine.Random.Range(0, 2); // 0 ~ 1
                numOfAddedSuffix = UnityEngine.Random.Range(0, 2); // 0 ~ 1

                if (numOfAddedPrefix == 0 && numOfAddedSuffix == 0)
                {
                    AffixType affix = (AffixType)UnityEngine.Random.Range(0, 2);

                    if (affix == AffixType.Prefix)
                        numOfAddedPrefix = 1;
                    else
                        numOfAddedSuffix = 1;
                }
                break;
            case ItemRarity.Rare:
                numOfAddedPrefix = UnityEngine.Random.Range(1, 3); // 1 ~ 2
                numOfAddedSuffix = UnityEngine.Random.Range(1, 3); // 1 ~ 2
                break;
            case ItemRarity.Unique:
                numOfAddedPrefix = UnityEngine.Random.Range(2, 4); // 2 ~ 3
                numOfAddedSuffix = UnityEngine.Random.Range(2, 4); // 2 ~ 3
                break;
            default:
                break;
        }

        for (int i = 0; i < numOfAddedPrefix; i++)
        {
            ModData prefix = GetPrefix(data);
            data.AddPrefix(prefix);
        }

        for (int i = 0; i < numOfAddedSuffix; i++)
        {
            ModData suffix = GetSuffix(data);
            data.AddSuffix(suffix);
        }

        return data;
    }

    private void CraftCurruption(ItemData item)
    {
        ModData newImplicitMod = m_model.GetImplicitMod(item);

        item.Currupt(newImplicitMod);        
    }
    private void CraftDivine(ItemData item)
    {
        item.RollAllMod();
    }
    private void CraftVoid(ItemData item)
    {
        bool isCrafted = false;

        while(!isCrafted)
        {
            AffixType affix = (AffixType)UnityEngine.Random.Range(0, 2);

            if (item.GetNumOfPrefix == 0)
                affix = AffixType.Suffix;

            if (item.GetNumOfSuffix == 0)
                affix = AffixType.Prefix;

            switch (affix)
            {
                case AffixType.Prefix:

                    if (item.GetNumOfPrefix <= 0)
                        continue;

                    item.RemoveOneRandomPrefix();
                    isCrafted = true;

                    break;
                case AffixType.Suffix:

                    if (item.GetNumOfSuffix <= 0)
                        continue;

                    item.RemoveOneRandomSuffix();
                    isCrafted = true;
                    break;
                default:
                    break;
            }
        }
    }
    private void CraftPurification(ItemData item)
    {
        item.GetItemRarity = ItemRarity.Normal;

        int numOfPrefix = item.GetNumOfPrefix;
        int numOfSuffix = item.GetNumOfSuffix;

        for (int i = 0; i < numOfPrefix; i++)
            item.RemoveOneRandomPrefix();

        for (int i = 0; i < numOfSuffix; i++)
            item.RemoveOneRandomSuffix();

    }
    private void CraftChaos(ItemData item)
    {
        CraftPurification(item);
        CraftWizard(item);
    }
    private void CraftAlteration(ItemData item)
    {
        CraftPurification(item);
        CraftBlackSmith(item);
    }
    private void CraftWizard(ItemData item)
    {
        CraftBlackSmith(item);
        CraftMagicPower(item);
    }
    private void CraftLuck(ItemData item)
    {
        ItemRarity rarity = (ItemRarity)UnityEngine.Random.Range(1, 4);
        // 1 = magic
        // 2 = rare
        // 3 = unique

        switch (rarity)
        {
            case ItemRarity.Normal:
                break;
            case ItemRarity.Magic:
                CraftBlackSmith(item);
                break;
            case ItemRarity.Rare:
                CraftWizard(item);
                break;
            case ItemRarity.Unique:
                Debug.Log("유니크 아이템으로 변화하지만.. 아직 유니크" +
                    "아이템 데이터 베이스가 없다");
                break;
            default:
                break;
        }


    }
    private void CraftBlackSmith(ItemData item)
    {
        item.GetItemRarity = ItemRarity.Magic;

        int numOfAddedPrefix = UnityEngine.Random.Range(0, 2); // 0 ~ 1
        int numOfAddedSuffix = UnityEngine.Random.Range(0, 2); // 0 ~ 1

        if (numOfAddedPrefix == 0 && numOfAddedSuffix == 0)
        {
            AffixType affix = (AffixType)UnityEngine.Random.Range(0, 2);

            if (affix == AffixType.Prefix)
                numOfAddedPrefix = 1;
            else
                numOfAddedSuffix = 1;
        }

        for (int i = 0; i < numOfAddedPrefix; i++)
        {
            ModData prefix = GetPrefix(item);
            item.AddPrefix(prefix);
        }
        for (int i = 0; i < numOfAddedSuffix; i++)
        {
            ModData suffix = GetSuffix(item);
            item.AddSuffix(suffix);
        }

    }
    private void CraftUnholy(ItemData item)
    {
        bool isAdded = false;

        while(!isAdded)
        {
            AffixType addedType = (AffixType)UnityEngine.Random.Range(0, 2);

            switch (addedType)
            {
                case AffixType.Prefix:

                    if (item.GetNumOfPrefix == 3)
                        continue;

                    ModData prefix = GetPrefix(item);
                    item.AddPrefix(prefix);
                    isAdded = true;
                    break;
                case AffixType.Suffix:

                    if (item.GetNumOfSuffix == 3)
                        continue;

                    ModData suffix = GetSuffix(item);
                    item.AddSuffix(suffix);
                    isAdded = true;
                    break;
                default:
                    break;
            }
        }

    }
    private void CraftReinforcement(ItemData item)
    {
        if( item.GetNumOfPrefix == 1)
        {
            //서픽스 더하기
            ModData suffix = m_model.GetSuffixData();
            item.AddSuffix(suffix);
            return;
        }
        
        if( item.GetNumOfSuffix == 1)
        {
            //프리픽스 더하기
            ModData prefix = m_model.GetPrefixData();
            item.AddPrefix(prefix);
            return;
        }                 
    }
    private void CraftMagicPower(ItemData item)
    {
        item.GetItemRarity = ItemRarity.Rare;
        
        int numOfAddedPrefix= UnityEngine.Random.Range(1, 4);
        int numOfAddedSuffix = UnityEngine.Random.Range(1, 4);

        for(int i = 0; i < numOfAddedPrefix;i++)
        {
            if (item.GetNumOfPrefix >= 3)
                break;

            ModData prefix = GetPrefix(item);
            item.AddPrefix(prefix);
        }

        for (int i = 0; i < numOfAddedSuffix;i++)
        {
            if (item.GetNumOfSuffix >= 3)
                break;
                
            ModData suffix = GetSuffix(item);
            item.AddSuffix(suffix);
        }             
    }

}
