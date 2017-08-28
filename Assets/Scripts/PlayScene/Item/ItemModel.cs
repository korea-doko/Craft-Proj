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

    [SerializeField] private List<List<WeaponBaseData>> m_weaponDataList;
    [SerializeField] private List<List<ArmorBaseData>> m_armorDataList;
    
    [SerializeField] private List<ModData> m_suffixDataList;
    [SerializeField] private List<ModData> m_prefixDataList;
    
    [SerializeField] List<Dictionary<string, string>> m_fullDic;

   
    public void InitModel()
    {
        m_fullDic = new List<Dictionary<string, string>>();

        InitWeaponList();
        InitArmorList();

        

        ReadStatusParameterFromXML();
        MakeStatusParameterList();
        // 파라메터 초기화

        ReadSwordBaseDataFromXML();
        MakeSwordDataList();
        // 소드
        ReadMaceBaseDataFromXML();
        MakeMaceDataList();
        // 메이스
        ReadAxeBaseDataFromXML();
        MakeAxeDataList();
        // 엑스
        ReadClawBaseDataFromXML();
        MakeClawDataList();
        // 클로
        ReadDaggerBaseDataFromXML();
        MakeDaggerDataList();
        // 대거
        ReadBowBaseDataFromXML();
        MakeBowDataList();
        // 보우
        ReadWandBaseDataFromXML();
        MakeWandDataList();
        // 완드
        

        ReadBootsBaseDataFromXML();
        MakeBootsDataList();
        // 부츠
        ReadArmorBaseDataFromXML();
        MakeArmorDataList();
        // 아머
        ReadHelmetBaseDataFromXML();
        MakeHelmetDataList();
        // 헬멧

        ReadPrefixDataFromXML();
        MakePrefixDataList();


        ReadSuffixDataFromXML();
        MakeSuffixDataList();

    }
    

    public ArmorBaseData GetRandomArmorBaseData()
    {
        int numOfArmorClass = System.Enum.GetNames(typeof(ArmorLowerClass)).Length;
        ArmorLowerClass randomClass = (ArmorLowerClass)UnityEngine.Random.Range(0, numOfArmorClass);
        return GetArmorBaseData(randomClass);
    }
    public ArmorBaseData GetArmorBaseData(ArmorLowerClass _class)
    {
        List<ArmorBaseData> list = GetArmorDataList(_class);
        int rand = UnityEngine.Random.Range(0, list.Count);
        return list[rand];
    }

    public WeaponBaseData GetRandomWeaponBaseData()
    {
        int numOfWeaponClass = System.Enum.GetNames(typeof(WeaponLowerClass)).Length;
        WeaponLowerClass randomClass = (WeaponLowerClass)UnityEngine.Random.Range(0, numOfWeaponClass);
        return GetWeaponBaseData(randomClass);
    }
    public WeaponBaseData GetWeaponBaseData(WeaponLowerClass _class)
    {
        List<WeaponBaseData> dataList = GetWeaponDataList(_class);
        int rand = UnityEngine.Random.Range(0, dataList.Count);
        return dataList[rand];
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

    List<WeaponBaseData> GetWeaponDataList(WeaponLowerClass _class)
    {
        return m_weaponDataList[(int)_class];
    }
    List<ArmorBaseData> GetArmorDataList(ArmorLowerClass _class)
    {
        return m_armorDataList[(int)_class];
    }

    void InitWeaponList()
    {
        m_weaponDataList = new List<List<WeaponBaseData>>();

        int numOfWeaponType = System.Enum.GetNames(typeof(WeaponLowerClass)).Length;

        for (int i = 0; i < numOfWeaponType; i++)
            m_weaponDataList.Add(new List<WeaponBaseData>());        
    }
    void InitArmorList()
    {
        m_armorDataList = new List<List<ArmorBaseData>>();

        int numOfArmorType = System.Enum.GetNames(typeof(ArmorLowerClass)).Length;

        for (int i = 0; i < numOfArmorType; i++)
            m_armorDataList.Add(new List<ArmorBaseData>());
        // 아머 데이터 리스트 초기화
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

    void ReadSwordBaseDataFromXML()
    {
        
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Sword");
        
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Sword");


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
    void MakeSwordDataList()
    {
        //m_swordBaseDataList = new List<WeaponBaseData>();
        List<WeaponBaseData> list = GetWeaponDataList(WeaponLowerClass.Sword);
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

            WeaponBaseData wbd = new WeaponBaseData(WeaponLowerClass.Sword,
                givenID,name, level, minDamage, maxDamage, attackSpeed, attr);

            list.Add(wbd);
        }

        m_fullDic.Clear();
    }

    void ReadMaceBaseDataFromXML()
    {

        TextAsset textAsset = (TextAsset)Resources.Load("XML/Mace");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Mace");


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
    void MakeMaceDataList()
    {
        //m_maceBaseDataList = new List<WeaponBaseData>();

        List<WeaponBaseData> list = GetWeaponDataList(WeaponLowerClass.Mace);
        for (int i = 0; i < m_fullDic.Count; i++)
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

            WeaponBaseData wbd = new WeaponBaseData(WeaponLowerClass.Mace,
                givenID, name, level, minDamage, maxDamage, attackSpeed, attr);

            list.Add(wbd);
        }

        m_fullDic.Clear();
    }

    void ReadAxeBaseDataFromXML()
    {

        TextAsset textAsset = (TextAsset)Resources.Load("XML/Axe");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Axe");


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
    void MakeAxeDataList()
    {
        //m_axeBaseDataList = new List<WeaponBaseData>();

        List<WeaponBaseData> list = GetWeaponDataList(WeaponLowerClass.Axe);
        for (int i = 0; i < m_fullDic.Count; i++)
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

            WeaponBaseData wbd = new WeaponBaseData(WeaponLowerClass.Axe,
                givenID, name, level, minDamage, maxDamage, attackSpeed, attr);

            list.Add(wbd);
        }

        m_fullDic.Clear();
    }

    void ReadClawBaseDataFromXML()
    {

        TextAsset textAsset = (TextAsset)Resources.Load("XML/Claw");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Claw");


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
    void MakeClawDataList()
    {
        //m_clawBaseDataList = new List<WeaponBaseData>();

        List<WeaponBaseData> list = GetWeaponDataList(WeaponLowerClass.Claw);
        for (int i = 0; i < m_fullDic.Count; i++)
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

            WeaponBaseData wbd = new WeaponBaseData(WeaponLowerClass.Claw,
                givenID, name, level, minDamage, maxDamage, attackSpeed, attr);

            list.Add(wbd);
        }

        m_fullDic.Clear();
    }

    void ReadDaggerBaseDataFromXML()
    {

        TextAsset textAsset = (TextAsset)Resources.Load("XML/Dagger");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Dagger");


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
    void MakeDaggerDataList()
    {
        //m_daggerBaseDataList = new List<WeaponBaseData>();

        List<WeaponBaseData> list = GetWeaponDataList(WeaponLowerClass.Dagger);
        for (int i = 0; i < m_fullDic.Count; i++)
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

            WeaponBaseData wbd = new WeaponBaseData(WeaponLowerClass.Dagger,
                givenID, name, level, minDamage, maxDamage, attackSpeed, attr);

            list.Add(wbd);
        }

        m_fullDic.Clear();
    }

    void ReadBowBaseDataFromXML()
    {

        TextAsset textAsset = (TextAsset)Resources.Load("XML/Bow");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Bow");


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
    void MakeBowDataList()
    {
        //m_bowBaseDataList= new List<WeaponBaseData>();

        List<WeaponBaseData> list = GetWeaponDataList(WeaponLowerClass.Bow);
        for (int i = 0; i < m_fullDic.Count; i++)
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

            WeaponBaseData wbd = new WeaponBaseData(WeaponLowerClass.Bow,
                givenID, name, level, minDamage, maxDamage, attackSpeed, attr);

            list.Add(wbd);
        }
        m_fullDic.Clear();
    }

    void ReadWandBaseDataFromXML()
    {

        TextAsset textAsset = (TextAsset)Resources.Load("XML/Wand");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Wand");


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
    void MakeWandDataList()
    {
        //m_wandBaseDataList = new List<WeaponBaseData>();

        List<WeaponBaseData> list = GetWeaponDataList(WeaponLowerClass.Wand);
        for (int i = 0; i < m_fullDic.Count; i++)
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

            WeaponBaseData wbd = new WeaponBaseData(WeaponLowerClass.Wand,
                givenID, name, level, minDamage, maxDamage, attackSpeed, attr);

            list.Add(wbd);
        }

        m_fullDic.Clear();
    }



    void ReadBootsBaseDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Boots");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Boots");


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
                    case "Armor":
                        partialDic.Add("Armor", content.InnerText);
                        break;
                    case "EvasionRating":
                        partialDic.Add("EvasionRating", content.InnerText);
                        break;
                    case "EnergyShield":
                        partialDic.Add("EnergyShield", content.InnerText);
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
    void MakeBootsDataList()
    {
        List<ArmorBaseData> list = GetArmorDataList(ArmorLowerClass.Boots);
        
        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ItemName"];
            int level = int.Parse(dic["Level"]);
            int armor = int.Parse(dic["Armor"]);
            int evasionRating = int.Parse(dic["EvasionRating"]);
            int energyShield = int.Parse(dic["EnergyShield"]);
            int requiredStr = int.Parse(dic["RequiredStr"]);
            int requiredDex = int.Parse(dic["RequiredDex"]);
            int requiredInt = int.Parse(dic["RequiredInt"]);
            Attribute attr = new Attribute(requiredStr, requiredDex, requiredInt);
            
            ArmorBaseData armorBase = new ArmorBaseData(ArmorLowerClass.Boots,givenID, name, level,
                new Attribute(requiredStr, requiredDex, requiredInt), armor, evasionRating,
                energyShield);

            list.Add(armorBase);
        }

        m_fullDic.Clear();
    }

    void ReadArmorBaseDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/BodyArmor");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("BodyArmor");


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
                    case "Armor":
                        partialDic.Add("Armor", content.InnerText);
                        break;
                    case "EvasionRating":
                        partialDic.Add("EvasionRating", content.InnerText);
                        break;
                    case "EnergyShield":
                        partialDic.Add("EnergyShield", content.InnerText);
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
    void MakeArmorDataList()
    {
        List<ArmorBaseData> list = GetArmorDataList(ArmorLowerClass.BodyArmor);
       
        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ItemName"];
            int level = int.Parse(dic["Level"]);
            int armor = int.Parse(dic["Armor"]);
            int evasionRating = int.Parse(dic["EvasionRating"]);
            int energyShield = int.Parse(dic["EnergyShield"]);
            int requiredStr = int.Parse(dic["RequiredStr"]);
            int requiredDex = int.Parse(dic["RequiredDex"]);
            int requiredInt = int.Parse(dic["RequiredInt"]);
            Attribute attr = new Attribute(requiredStr, requiredDex, requiredInt);

            ArmorBaseData armorBase = new ArmorBaseData(ArmorLowerClass.BodyArmor,givenID, name, level,
                new Attribute(requiredStr, requiredDex, requiredInt), armor, evasionRating,
                energyShield);

            list.Add(armorBase);
        }

        m_fullDic.Clear();
    }

    void ReadHelmetBaseDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Helmet");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Helmet");


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
                    case "Armor":
                        partialDic.Add("Armor", content.InnerText);
                        break;
                    case "EvasionRating":
                        partialDic.Add("EvasionRating", content.InnerText);
                        break;
                    case "EnergyShield":
                        partialDic.Add("EnergyShield", content.InnerText);
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
    void MakeHelmetDataList()
    {
        List<ArmorBaseData> list = GetArmorDataList(ArmorLowerClass.Helmet);
        
        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ItemName"];
            int level = int.Parse(dic["Level"]);
            int armor = int.Parse(dic["Armor"]);
            int evasionRating = int.Parse(dic["EvasionRating"]);
            int energyShield = int.Parse(dic["EnergyShield"]);
            int requiredStr = int.Parse(dic["RequiredStr"]);
            int requiredDex = int.Parse(dic["RequiredDex"]);
            int requiredInt = int.Parse(dic["RequiredInt"]);
            Attribute attr = new Attribute(requiredStr, requiredDex, requiredInt);

            ArmorBaseData helmetBase = new ArmorBaseData(ArmorLowerClass.Helmet,givenID, name, level,
                new Attribute(requiredStr, requiredDex, requiredInt), armor, evasionRating,
                energyShield);

            list.Add(helmetBase);
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

     
}
