using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ISpriteManager : IManager
{
    Sprite GetRarityPanelSprite(ItemRarity _rarity);
}
public class SpriteManager : MonoBehaviour ,ISpriteManager{

    [SerializeField] private List<Sprite> m_raritySpriteList;

    private static SpriteManager m_inst;
    public static SpriteManager Inst
    {
        get
        {
            return m_inst;
        }
    }
    public SpriteManager()
    {
        m_inst = this;
    }

    public void InitManager()
    {
        m_raritySpriteList = new List<Sprite>();

        Sprite[] sps = Resources.LoadAll<Sprite>("Image/ItemRarityPanel");
        foreach (Sprite s in sps)
            m_raritySpriteList.Add(s);
    }

    public Sprite GetRarityPanelSprite(ItemRarity _rarity)
    {
        return m_raritySpriteList[(int)_rarity];
    }
}
