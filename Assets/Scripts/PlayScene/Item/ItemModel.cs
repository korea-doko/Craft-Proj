using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

[System.Serializable]
public struct StatusParameterNameSt
{
    public int m_id;
    public int m_givenID;

    public StatusParameterNameSt(int _id, int _givenId)
    {
        m_id = _id;
        m_givenID = _givenId;
    }

    public int GetID(int _givenId)
    {
        if (_givenId != m_givenID)
            return -1;

        return m_id;
    }
    public int GetGivenID(int _id)
    {
        if (_id != m_id)
            return -1;

        return m_givenID;
    }
}

public interface IItemModel : IModel
{

}

[System.Serializable]
public class ItemModel : MonoBehaviour, IItemModel
{
    [SerializeField] private List<StatusParameterNameSt> m_statusParameterNameList;
    
    [SerializeField] private List<WeaponBaseData> m_weaponBaseDataList;
    [SerializeField] private List<ModData> m_suffixDataList;
    [SerializeField] private List<ModData> m_prefixDataList;
    
    [SerializeField] List<Dictionary<string, string>> m_fullDic;

   
    public void InitModel()
    {
        m_fullDic = new List<Dictionary<string, string>>();

        ReadStatusParameterFromXML();
        MakeStatusParameterList();

        ReadWeaponBaseDataFromXML();        
        MakeWeaponDataList();

        ReadPrefixDataFromXML();
        MakePrefixDataList();


        ReadSuffixDataFromXML();
        MakeSuffixDataList();

    }
 
    void ReadStatusParameterFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/StatusParameterName");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("StatusParameterName");
        
        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "GivenID":
                        partialDic.Add("GivenID", content.InnerText);
                        break;
                    case "ID":
                        partialDic.Add("ID", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeStatusParameterList()
    {
        m_statusParameterNameList = new List<StatusParameterNameSt>();

        for(int i = 0; i < m_fullDic.Count;i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];
            int id = int.Parse(dic["ID"]);
            int givenID = int.Parse(dic["GivenID"]);

            StatusParameterNameSt st = new StatusParameterNameSt(id, givenID);
            m_statusParameterNameList.Add(st);
        }

        m_fullDic.Clear();
    }

    void ReadWeaponBaseDataFromXML()
    {
        
        TextAsset textAsset = (TextAsset)Resources.Load("XML/OneHandedSword");
        
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("OneHandedSword");


        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "GivenID":
                        partialDic.Add("GivenID", content.InnerText);
                        break;
                    case "ItemName":
                        partialDic.Add("ItemName", content.InnerText);
                        break;
                    case "Level":
                        partialDic.Add("Level", content.InnerText);
                        break;
                    case "MinDamage":
                        partialDic.Add("MinDamage", content.InnerText);
                        break;
                    case "MaxDamage":
                        partialDic.Add("MaxDamage", content.InnerText);
                        break;
                    case "AttackSpeed":
                        partialDic.Add("AttackSpeed", content.InnerText);
                        break;
                    case "RequiredStr":
                        partialDic.Add("RequiredStr", content.InnerText);
                        break;
                    case "RequiredDex":
                        partialDic.Add("RequiredDex", content.InnerText);
                        break;
                    case "RequiredInt":
                        partialDic.Add("RequiredInt", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }        
    }
    void MakeWeaponDataList()
    {
        m_weaponBaseDataList = new List<WeaponBaseData>();

        for (int i = 0; i < m_fullDic.Count;i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ItemName"];
            int level = int.Parse(dic["Level"]);
            int minDamage = int.Parse(dic["MinDamage"]);
            int maxDamage = int.Parse(dic["MaxDamage"]);
            int attackSpeed = int.Parse(dic["AttackSpeed"]);
            int requiredStr = int.Parse(dic["RequiredStr"]);
            int requiredDex = int.Parse(dic["RequiredDex"]);
            int requiredInt = int.Parse(dic["RequiredInt"]);
            Attribute attr = new Attribute(requiredStr, requiredDex, requiredInt);

            WeaponBaseData wbd = new WeaponBaseData(WeaponLowerClass.OneHandedSword,
                givenID,name, level, minDamage, maxDamage, attackSpeed, attr);

            m_weaponBaseDataList.Add(wbd);
        }

        m_fullDic.Clear();
    }

    void ReadPrefixDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Prefix");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Prefix");


        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "GivenID":
                        partialDic.Add("GivenID", content.InnerText);
                        break;
                    case "ModName":
                        partialDic.Add("ModName", content.InnerText);
                        break;
                    case "Level":
                        partialDic.Add("Level", content.InnerText);
                        break;
                    case "MinValue":
                        partialDic.Add("MinValue", content.InnerText);
                        break;
                    case "MaxValue":
                        partialDic.Add("MaxValue", content.InnerText);
                        break;
                    case "StatusParameterName":
                        partialDic.Add("StatusParameterName", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakePrefixDataList()
    {
        m_prefixDataList = new List<ModData>();

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ModName"];
            int level = int.Parse(dic["Level"]);
            int minValue = int.Parse(dic["MinValue"]);
            int maxValue = int.Parse(dic["MaxValue"]);

            int givenStatusParameterID = int.Parse(dic["StatusParameterName"]);
            StatusParameterName spn = StatusParameterName.None;

            for (int j = 0; j < m_statusParameterNameList.Count; j++)
            {
                StatusParameterNameSt st = m_statusParameterNameList[j];
                int getId = st.GetGivenID(givenStatusParameterID);

                if (getId == -1)
                    continue;

                spn = (StatusParameterName)getId;
                break;
            }

            ModData mod = new ModData(level, AffixType.Prefix, name, minValue, maxValue, spn);
            m_prefixDataList.Add(mod);
        }
        m_fullDic.Clear();
    }

    void ReadSuffixDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Suffix");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Suffix");


        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "GivenID":
                        partialDic.Add("GivenID", content.InnerText);
                        break;
                    case "ModName":
                        partialDic.Add("ModName", content.InnerText);
                        break;
                    case "Level":
                        partialDic.Add("Level", content.InnerText);
                        break;
                    case "MinValue":
                        partialDic.Add("MinValue", content.InnerText);
                        break;
                    case "MaxValue":
                        partialDic.Add("MaxValue", content.InnerText);
                        break;
                    case "StatusParameterName":
                        partialDic.Add("StatusParameterName", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeSuffixDataList()
    {
        m_suffixDataList = new List<ModData>();
        
        for(int i = 0; i < m_fullDic.Count;i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ModName"];
            int level = int.Parse(dic["Level"]);
            int minValue = int.Parse(dic["MinValue"]);
            int maxValue = int.Parse(dic["MaxValue"]);

            int givenStatusParameterID = int.Parse(dic["StatusParameterName"]);
            StatusParameterName spn = StatusParameterName.None;

            for (int j = 0; j < m_statusParameterNameList.Count;j++)
            {
                StatusParameterNameSt st = m_statusParameterNameList[j];
                int getId = st.GetGivenID(givenStatusParameterID);

                if (getId == -1)
                    continue;

                spn = (StatusParameterName)getId;
                break;
            }

            ModData mod = new ModData(level, AffixType.Suffix, name, minValue, maxValue, spn);
            m_suffixDataList.Add(mod);
        }
        m_fullDic.Clear();
    }

    public WeaponBaseData GetWeaponBaseData()
    {
        int rand = UnityEngine.Random.Range(0, m_weaponBaseDataList.Count);
        return m_weaponBaseDataList[rand];
    }

    public ModData GetPrefixData()
    {
        int randIndex = UnityEngine.Random.Range(0, m_prefixDataList.Count);
        return m_prefixDataList[randIndex];
    }
    public ModData GetSuffixData()
    {
        int randIndex = UnityEngine.Random.Range(0, m_suffixDataList.Count);
        return m_suffixDataList[randIndex];
    }
     
}
