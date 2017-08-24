using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour,IUpgradeManager,IUpdatable
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


    ItemData GenerateWeapon()
    {
        // 테스트로 랜덤하게

        WeaponBaseData wbd = m_model.GetWeaponBaseData();
        ItemRarity rarity = (ItemRarity)UnityEngine.Random.Range(0, 4);

        ItemData data = new ItemData(wbd,rarity);

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

        for(int i = 0;i < numOfAddedPrefix;i++)
        {
            ModData prefix = GetPrefix();
            data.AddPrefix(prefix);
        }

        for(int i = 0; i< numOfAddedSuffix;i++)
        {
            ModData suffix = GetSuffix();
            data.AddSuffix(suffix);
        }

        return data;
    }

    public ModData GetPrefix()
    {
        ModData prefix = m_model.GetPrefixData();
        ModData copiedPrefix = new ModData(prefix);
        return copiedPrefix;
    }
    public ModData GetSuffix()
    {
        ModData suffix = m_model.GetSuffixData();
        ModData copiedSuffix = new ModData(suffix);
        return copiedSuffix;
    }

    public void CraftItem(ItemData _item,RuneName _name)
    {
        switch (_name)
        {
            case RuneName.Reinforcement:
                CraftReinforcement(_item);
                break;
            case RuneName.MagicPower:
                CraftMagicPower(_item);
                break;
            case RuneName.Unholy:
                CraftUnholy(_item);
                break;
            case RuneName.BlackSmith:
                CraftBlackSmith(_item);
                break;
            case RuneName.Luck:
                CraftLuck(_item);
                break;
            case RuneName.Wizard:
                CraftWizard(_item);
                break;
            case RuneName.Alteration:
                CraftAlteration(_item);
                break;
            case RuneName.Chaos:
                CraftChaos(_item);
                break;
            case RuneName.Purification:
                CraftPurification(_item);
                break;
            case RuneName.Void:
                CraftVoid(_item);
                break;
            case RuneName.Divine:
                CraftDivine(_item);
                break;
            case RuneName.Curruption:
                CraftCurruption(_item);
                break;
            default:
                break;
        }

    }

    private void CraftCurruption(ItemData item)
    {
        Debug.Log("아직 구현 안됐음");
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
            ModData prefix = GetPrefix();
            item.AddPrefix(prefix);
        }
        for (int i = 0; i < numOfAddedSuffix; i++)
        {
            ModData suffix = GetSuffix();
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

                    ModData prefix = GetPrefix();
                    item.AddPrefix(prefix);
                    isAdded = true;
                    break;
                case AffixType.Suffix:

                    if (item.GetNumOfSuffix == 3)
                        continue;

                    ModData suffix = GetSuffix();
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

            ModData prefix = GetPrefix();
            item.AddPrefix(prefix);
        }

        for (int i = 0; i < numOfAddedSuffix;i++)
        {
            if (item.GetNumOfSuffix >= 3)
                break;
                
            ModData suffix = GetSuffix();
            item.AddSuffix(suffix);
        }             
    }

    public void UpdateThis()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ItemData data = GenerateWeapon();
            StoreManager.Inst.AddItemData(data);               
        }
    }

}
