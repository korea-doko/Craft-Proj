using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestItemInfoPanel : MonoBehaviour
{
    public Image m_rarityImage;
    public Text m_nameText;
    public Text m_limitaionText;
    public Text m_baseOptionText;
    public Text m_runeOptionText;

    public void Init()
    {
        Hide();
    }

    public void Show(ItemData _data)
    {
        Sprite sp = SpriteManager.Inst.GetRarityPanelSprite(_data.GetItemRarity);
        m_rarityImage.sprite = sp;


        string name = _data.GetItemBaseData.GetItemName;
        string limit = "착용 레벨 : " + _data.GetItemBaseData.GetItemLevel.ToString();

        if (_data.GetItemBaseData.GetBaseItemRequiredAttribute.Str != 0)
            limit += " 힘 : " + _data.GetItemBaseData.GetBaseItemRequiredAttribute.Str.ToString();
        if (_data.GetItemBaseData.GetBaseItemRequiredAttribute.Dex != 0)
            limit += " 민첩 : " + _data.GetItemBaseData.GetBaseItemRequiredAttribute.Dex.ToString();
        if (_data.GetItemBaseData.GetBaseItemRequiredAttribute.Int != 0)
            limit += " 지능 : " + _data.GetItemBaseData.GetBaseItemRequiredAttribute.Int.ToString();

        string baseOption = "";

        if( _data.GetImplicitMod1 != null && _data.GetImplicitMod1.GetSetValue != 0)
                baseOption += _data.GetImplicitMod1.GetName + " " + _data.GetImplicitMod1.GetSetValue.ToString() + "\n";
        
        if( _data.GetImplicitMod2 != null && _data.GetImplicitMod2.GetSetValue != 0)
                baseOption += _data.GetImplicitMod2.GetName + " " + _data.GetImplicitMod2.GetSetValue.ToString() + "\n";

        switch (_data.GetItemBaseData.GetItemUpperClass)
        {
            case ItemUpperClass.Armor:

                ArmorBaseData abd = (ArmorBaseData)_data.GetItemBaseData;
                
                int armor = abd.Armor;
                if (armor != 0)
                    baseOption += "Armor = " + armor + "\n";

                int evasion = abd.EvasionRating;
                if (evasion != 0)
                    baseOption += "Evasion = " + evasion + "\n";

                int energy = abd.EnergyShield;
                if (energy != 0)
                    baseOption += "EnergyShield = " + energy + "\n";

                break;
            case ItemUpperClass.Weapon:

                WeaponBaseData wbd = (WeaponBaseData)_data.GetItemBaseData;

                
                int physicalMin = wbd.PhysicalMinDamage;
                int physicalMax = wbd.PhysicalMaxDamage;

                //int addedPhysicalMinDamage = wbd.GetAllModValueInList(ModType.AddedPhysicalMinDamage);
                //int addedPhysicalMaxDamage = GetAllModValueInList(ModType.AddedPhysicalMaxDamage);
                //int increasedPhysicalMinDamage = GetAllModValueInList(ModType.IncreasedPhysicalMinDamage);
                //int increasedPhysicalMaxDamage = GetAllModValueInList(ModType.IncreasedPhysicalMaxDamage);

                //if (increasedPhysicalMinDamage == 0) increasedPhysicalMinDamage = 1;
                //if (increasedPhysicalMaxDamage == 0) increasedPhysicalMaxDamage = 1;

                //physicalMin = (physicalMin + addedPhysicalMinDamage) * increasedPhysicalMinDamage;
                //physicalMax = (physicalMax + addedPhysicalMaxDamage) * increasedPhysicalMaxDamage;

                baseOption += "PhysicalDamage = " + physicalMin + " ~ " + physicalMax + "\n";

                //int addedfireMinDamage = GetAllModValueInList(ModType.AddedFireMinDamage);
                //int addedfireMaxDamage = GetAllModValueInList(ModType.AddedFireMaxDamage);
                //int increasedfireMinDamage = GetAllModValueInList(ModType.IncreasedFireMinDamge);
                //int increasedfireMaxDamage = GetAllModValueInList(ModType.IncreasedFireMaxDamage);

                //if (increasedfireMinDamage == 0) increasedfireMinDamage = 1;
                ////if (increasedfireMaxDamage == 0) increasedfireMaxDamage = 1;

                //int fireMin = addedfireMinDamage * increasedPhysicalMinDamage;
                //int fireMax = addedfireMaxDamage * increasedfireMaxDamage;

                //baseOption += "<color=red>FireDamage = " + fireMin + " ~ " + fireMax + "</color>\n";

                //int addedAttackSpeed = GetAllModValueInList(ModType.AddedAttackSpeed);
                //int increasedAttackSpeed = GetAllModValueInList(ModType.IncreasedAttackSpeed);

                //if (increasedAttackSpeed == 0) increasedAttackSpeed = 1;

                int attackSpeed = wbd.AttackSpeed;
                //attackSpeed = (attackSpeed + addedAttackSpeed) * increasedAttackSpeed;

                baseOption += "Attack Speed = " + attackSpeed + "\n";


                break;
            case ItemUpperClass.Misc:
                break;
            default:
                break;
        }


        
        string runeOption = "";

        List<ModData> prefixList = _data.GetPrefixes;
        List<ModData> suffixList = _data.GetSuffixes;

        for(int i = 0; i < prefixList.Count;i++)
        {
            ModData data = prefixList[i];

            runeOption += data.GetModType.ToString() + " " + data.GetSetValue + "\n";
        }
        for(int i = 0; i < suffixList.Count;i++)
        {
            ModData data = suffixList[i];

            runeOption += data.GetModType.ToString() + " " + data.GetSetValue + "\n";
        }


        m_nameText.text = name;
        m_limitaionText.text = limit;
        m_baseOptionText.text = baseOption;
        m_runeOptionText.text = runeOption;

        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
