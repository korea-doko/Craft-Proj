using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour,IManager
{
    private ItemModel m_model;
    private ItemView m_view;


    public WeaponData m_testWeapon;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            m_testWeapon = GenerateWeapon();
    }
    WeaponData GenerateWeapon()
    {

        WeaponBaseData baseData = m_model.GetWeaponBaseData();
        ItemRarity rarity = (ItemRarity)UnityEngine.Random.Range(0, 4);

        WeaponData data = new WeaponData(baseData, rarity);

        int rarityChangedforLoop = (int)rarity;

        for (int i = 0; i < rarityChangedforLoop; i++)
        {
            WeaponModData suffix = m_model.GetWeaponMod();
            WeaponModData prefix = m_model.GetWeaponMod();

            data.AddPrefix(prefix);
            data.AddSuffix(suffix);
        }
        

        return data;
    }
}
