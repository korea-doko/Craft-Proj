using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SimpleHeroInfoPanel : MonoBehaviour {

    [SerializeField] private Text m_nameText;
    [SerializeField] private Text m_classText;
    [SerializeField] private List<Text> m_equipIndicatorList;
	
    internal void Init()
    {

    }

    internal void Show(HeroData m_heroData)
    {
        m_nameText.text = m_heroData.GetPersonality.Name + " " + m_heroData.GetName;
        m_classText.text = m_heroData.GetSpeciality.Name + " " + m_heroData.GetHeroClass.ToString();

        ItemData[] itemAry = m_heroData.GetEquipDataAry;
        for(int i = 0; i < 7;i++)
        {
            ItemData item = itemAry[i];
            Text equipIndicator = m_equipIndicatorList[i];

            if (item == null)
            {
                equipIndicator.text = " - ";
                continue;
            }

            switch (item.GetItemRarity)
            {
                case ItemRarity.Normal:
                    equipIndicator.text = "<color=white>?</color>";
                    break;
                case ItemRarity.Magic:
                    equipIndicator.text = "<color=blue>?</color>";
                    break;
                case ItemRarity.Rare:

                    equipIndicator.text = "<color=yellow>?</color>";
                    break;
                case ItemRarity.Unique:

                    equipIndicator.text = "<color=orange>?</color>";
                    break;
                default:
                    break;
            }


        }
    }

}
