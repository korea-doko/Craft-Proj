using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;



public interface IItemModel : IModel
{

}

[System.Serializable]
public class ItemModel : MonoBehaviour, IItemModel
{
    [SerializeField] private List<ModTypeSt> m_modTypeList;

    [SerializeField] private List<List<WeaponBaseData>> m_weaponDataList;
    [SerializeField] private List<List<ArmorBaseData>> m_armorDataList;
    [SerializeField] private List<List<MiscBaseData>> m_miscDataList;

    [SerializeField] private List<ModData> m_commonPrefixModDataList;
    [SerializeField] private List<ModData> m_commonSuffixModDataList;

    [SerializeField] private List<ModData> m_ringImplicitModDataList;
    [SerializeField] private List<ModData> m_amuletImplicitModDataList;
    
    [SerializeField] List<Dictionary<string, string>> m_fullDic;

   
    public void InitModel()
    {
        m_fullDic = new List<Dictionary<string, string>>();

        InitWeaponList();
        InitArmorList();
        InitMiscList();
        // 리스트 만들기


        //ReadModTypeFromXML();
        //MakeModTypeList();
        //// 모드 타입 초기화


        ReadCommonPrefixDataFromXML();
        MakeCommonPrefixDataList();
        // 공용 접두 모드 데이터 초기화

        ReadCommonSuffixDataFromXML();
        MakeCommonSuffixDataList();
        // 공용 접미 모드 데이터 초기화

        ReadRingImplicitModDataFromXML();
        MakeRingImplicitModDataList();
        // 링에 있는 내부 모드

        ReadAmuletImplicitModDataFromXML();
        MakeAmuletImplicitModDataList();
        // 아뮬렛에 있는 내부 모드


        ////
        //// 무기 데이터 초기화
        ////

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
        
       
        ////
        //// 방어구 데이터 초기화
        ////

        ReadBootsBaseDataFromXML();
        MakeBootsDataList();
        // 부츠
        ReadArmorBaseDataFromXML();
        MakeArmorDataList();
        // 아머
        ReadHelmetBaseDataFromXML();
        MakeHelmetDataList();
        // 헬멧
        ReadGlovesBaseDataFromXML();
        MakeGlovesDataList();
        // 장갑

        ////
        //// 악세서리 데이터 초기화
        ////

        ReadRingBaseDataFromXML();
        MakeRingDataList();
        // 링

        ReadAmuletBaseDataFromXML();
        MakeAmuletDataList();
        // 목걸이
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

    public MiscBaseData GetRandomMiscBaseData()
    {
        int numOfMiscClass = System.Enum.GetNames(typeof(MiscLowerClass)).Length;
        MiscLowerClass randomClass = (MiscLowerClass)UnityEngine.Random.Range(0, numOfMiscClass);
        return GetMiscBaseData(randomClass);
    }
    public MiscBaseData GetMiscBaseData(MiscLowerClass _class)
    {
        List<MiscBaseData> dataList = GetMiscDataList(_class);
        int rand = UnityEngine.Random.Range(0, dataList.Count);
        return dataList[rand];
    }

    public ModData GetPrefixData()
    {
        int randIndex = UnityEngine.Random.Range(0, m_commonPrefixModDataList.Count);
        return m_commonPrefixModDataList[randIndex];
    }
    public ModData GetSuffixData()
    {
        int randIndex = UnityEngine.Random.Range(0, m_commonSuffixModDataList.Count);
        return m_commonSuffixModDataList[randIndex];
    }

    public ModData GetImplicitMod(ItemData _data)
    {
        Debug.Log(" 임플리싯 모드는 아이템 클래스에 따라서 추가적으로 달라질 수 있음 " +
            "그러나 일단은 Ring에 대해서 하나 밖에 없어서 그것만 한다.");

        switch (_data.GetItemBaseData.GetItemUpperClass)
        {
            case ItemUpperClass.Armor:
                break;
            case ItemUpperClass.Weapon:
                break;
            case ItemUpperClass.Misc:
                break;
            default:
                break;
        }

        int count = m_ringImplicitModDataList.Count;
        int rand = UnityEngine.Random.Range(0, count);
        return m_ringImplicitModDataList[rand];
    }


    List<WeaponBaseData> GetWeaponDataList(WeaponLowerClass _class)
    {
        return m_weaponDataList[(int)_class];
    }
    List<ArmorBaseData> GetArmorDataList(ArmorLowerClass _class)
    {
        return m_armorDataList[(int)_class];
    }
    List<MiscBaseData> GetMiscDataList(MiscLowerClass _class)
    {
        return m_miscDataList[(int)_class];
    }

    /*
     *      모든 리스트 초기화
     */

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
    void InitMiscList()
    {
        m_miscDataList = new List<List<MiscBaseData>>();

        int numOfMiscType = System.Enum.GetNames(typeof(MiscLowerClass)).Length;

        for (int i = 0; i < numOfMiscType; i++)
            m_miscDataList.Add(new List<MiscBaseData>());
    }

    /*
     *      모든 모드 초기화 
     */
    void ReadModTypeFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/ModType");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("ModType");
        
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
                    case "Type":
                        partialDic.Add("Type", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeModTypeList()
    {
        m_modTypeList = new List<ModTypeSt>();

        for(int i = 0; i < m_fullDic.Count;i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];
            int id = int.Parse(dic["ID"]);
            int givenID = int.Parse(dic["GivenID"]);
            
            ModTypeSt st = new ModTypeSt(id, givenID);
            m_modTypeList.Add(st);
        }

        m_fullDic.Clear();
    }

    void ReadCommonPrefixDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/CommonPrefix");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("CommonPrefix");


        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "ID":
                        partialDic.Add("ID", content.InnerText);
                        break;
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
                    case "ModType":
                        partialDic.Add("ModType", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeCommonPrefixDataList()
    {
        m_commonPrefixModDataList = new List<ModData>();

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int id = int.Parse(dic["ID"]);
            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ModName"];
            int level = int.Parse(dic["Level"]);
            int minValue = int.Parse(dic["MinValue"]);
            int maxValue = int.Parse(dic["MaxValue"]);

            int givenStatusParameterID = int.Parse(dic["ModType"]);
            ModType spn = ModManager.Inst.GetModTypeUsingID(givenStatusParameterID);

            //for (int j = 0; j < m_modTypeList.Count; j++)
            //{
            //    ModTypeSt st = m_modTypeList[j];
            //    int getId = st.GetGivenID(givenStatusParameterID);

            //    if (getId == -1)
            //        continue;

            //    spn = (ModType)getId;
            //    break;
            //}

            ModData mod = new ModData(id,givenID,level, AffixType.Prefix, name, minValue, maxValue, spn);
            m_commonPrefixModDataList.Add(mod);
        }
        m_fullDic.Clear();
    }

    void ReadCommonSuffixDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/CommonSuffix");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("CommonSuffix");


        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "ID":
                        partialDic.Add("ID", content.InnerText);
                        break;
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
                    case "ModType":
                        partialDic.Add("ModType", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeCommonSuffixDataList()
    {
        m_commonSuffixModDataList = new List<ModData>();

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int id = int.Parse(dic["ID"]);
            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ModName"];
            int level = int.Parse(dic["Level"]);
            int minValue = int.Parse(dic["MinValue"]);
            int maxValue = int.Parse(dic["MaxValue"]);

            int givenStatusParameterID = int.Parse(dic["ModType"]);
            ModType spn = ModManager.Inst.GetModTypeUsingID(givenStatusParameterID);

            //for (int j = 0; j < m_modTypeList.Count; j++)
            //{
            //    ModTypeSt st = m_modTypeList[j];
            //    int getId = st.GetGivenID(givenStatusParameterID);

            //    if (getId == -1)
            //        continue;

            //    spn = (ModType)getId;
            //    break;
            //}

            ModData mod = new ModData(id,givenID,level, AffixType.Suffix, name, minValue, maxValue, spn);
            m_commonSuffixModDataList.Add(mod);
        }
        m_fullDic.Clear();
    }

    void ReadRingImplicitModDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/RingImplicitMod");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("RingImplicitMod");


        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "ID":
                        partialDic.Add("ID", content.InnerText);
                        break;
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
                    case "ModType":
                        partialDic.Add("ModType", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeRingImplicitModDataList()
    {
        m_ringImplicitModDataList = new List<ModData>();

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            
            int id = int.Parse(dic["ID"]);
            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ModName"];
            int level = int.Parse(dic["Level"]);
            int minValue = int.Parse(dic["MinValue"]);
            int maxValue = int.Parse(dic["MaxValue"]);

            int givenStatusParameterID = int.Parse(dic["ModType"]);
            ModType spn = ModManager.Inst.GetModTypeUsingID(givenStatusParameterID);

            //for (int j = 0; j < m_modTypeList.Count; j++)
            //{
            //    ModTypeSt st = m_modTypeList[j];
            //    int getId = st.GetGivenID(givenStatusParameterID);

            //    if (getId == -1)
            //        continue;

            //    spn = (ModType)getId;
            //    break;
            //}

            ModData mod = new ModData(id,givenID,level, AffixType.Implicit, name, minValue, maxValue, spn);
            m_ringImplicitModDataList.Add(mod);
        }
        m_fullDic.Clear();
    }

    void ReadAmuletImplicitModDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/AmuletImplicitMod");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("AmuletImplicitMod");


        foreach (XmlNode itemInfo in itemList)
        {
            XmlNodeList itemContent = itemInfo.ChildNodes;
            Dictionary<string, string> partialDic = new Dictionary<string, string>(); // ItemName is TestItem;

            foreach (XmlNode content in itemContent)
            {
                switch (content.Name)
                {
                    case "ID":
                        partialDic.Add("ID", content.InnerText);
                        break;
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
                    case "ModType":
                        partialDic.Add("ModType", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeAmuletImplicitModDataList()
    {
        m_amuletImplicitModDataList = new List<ModData>();

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];


            int id = int.Parse(dic["ID"]);
            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ModName"];
            int level = int.Parse(dic["Level"]);
            int minValue = int.Parse(dic["MinValue"]);
            int maxValue = int.Parse(dic["MaxValue"]);

            int givenStatusParameterID = int.Parse(dic["ModType"]);
            ModType spn = ModManager.Inst.GetModTypeUsingID(givenStatusParameterID);

            //for (int j = 0; j < m_modTypeList.Count; j++)
            //{
            //    ModTypeSt st = m_modTypeList[j];
            //    int getId = st.GetGivenID(givenStatusParameterID);

            //    if (getId == -1)
            //        continue;

            //    spn = (ModType)getId;
            //    break;
            //}

            ModData mod = new ModData(id, givenID, level, AffixType.Implicit, name, minValue, maxValue, spn);
            m_amuletImplicitModDataList.Add(mod);
        }
        m_fullDic.Clear();
    }

    /*
     *  아이템 초기화 
     */

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

    void ReadGlovesBaseDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Gloves");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Gloves");


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
    void MakeGlovesDataList()
    {
        List<ArmorBaseData> list = GetArmorDataList(ArmorLowerClass.Gloves);

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

            ArmorBaseData glovesBase = new ArmorBaseData(ArmorLowerClass.Gloves, givenID, name, level,
                new Attribute(requiredStr, requiredDex, requiredInt), armor, evasionRating,
                energyShield);

            list.Add(glovesBase);
        }

        m_fullDic.Clear();
    }

    void ReadRingBaseDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Ring");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Ring");

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
                    case "ImplicitMod1":
                        partialDic.Add("ImplicitMod1", content.InnerText);
                        break;
                    case "ImplicitMod2":
                        partialDic.Add("ImplicitMod2", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeRingDataList()
    {
        List<MiscBaseData> list = GetMiscDataList(MiscLowerClass.Ring);

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ItemName"];
            int level = int.Parse(dic["Level"]);

            int implicitModID1 = int.Parse(dic["ImplicitMod1"]);
            int implicitModID2 = int.Parse(dic["ImplicitMod2"]);

            ModData implicitMod1 = GetRingImplicitModDataUsingID(implicitModID1);
            ModData implicitMod2 = GetRingImplicitModDataUsingID(implicitModID2);

            list.Add(new MiscBaseData(MiscLowerClass.Ring, givenID, name, level, implicitMod1,
                implicitMod2));
        }

        m_fullDic.Clear();
    }
    ModData GetRingImplicitModDataUsingID(int _id)
    {        
        int count = m_ringImplicitModDataList.Count;

        for (int i = 0; i < count; i++)
        {
            ModData data = m_ringImplicitModDataList[i];

            if (data.GetGivenID == _id)
                return data;
        }

        Debug.Log("에러");
        return null;
    }

    void ReadAmuletBaseDataFromXML()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("XML/Amulet");

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);
        XmlNodeList itemList = xmlDoc.GetElementsByTagName("Amulet");

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
                    case "ImplicitMod1":
                        partialDic.Add("ImplicitMod1", content.InnerText);
                        break;
                    case "ImplicitMod2":
                        partialDic.Add("ImplicitMod2", content.InnerText);
                        break;
                    default:
                        break;
                }
            }
            m_fullDic.Add(partialDic);
        }
    }
    void MakeAmuletDataList()
    {
        List<MiscBaseData> list = GetMiscDataList(MiscLowerClass.Amulet);

        for (int i = 0; i < m_fullDic.Count; i++)
        {
            Dictionary<string, string> dic = m_fullDic[i];

            int givenID = int.Parse(dic["GivenID"]);
            string name = dic["ItemName"];
            int level = int.Parse(dic["Level"]);

            int implicitModID1 = int.Parse(dic["ImplicitMod1"]);
            int implicitModID2 = int.Parse(dic["ImplicitMod2"]);

            ModData implicitMod1 = GetAmuletImplicitModDataUsingID(implicitModID1);
            ModData implicitMod2 = GetAmuletImplicitModDataUsingID(implicitModID2);

            list.Add(new MiscBaseData(MiscLowerClass.Amulet, givenID, name, level, implicitMod1,
                implicitMod2));
        }

        m_fullDic.Clear();   
    }
    ModData GetAmuletImplicitModDataUsingID(int _id)
    {
        int count = m_amuletImplicitModDataList.Count;

        for (int i = 0; i < count; i++)
        {
            ModData data = m_amuletImplicitModDataList[i];

            if (data.GetGivenID == _id)
                return data;
        }

        Debug.Log("에러");
        return null;
    }

}
