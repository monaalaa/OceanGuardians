using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class HomeDataController
{
    static string HOME_LOCAL_FILE_PATH = Application.persistentDataPath + "/Home Data.json";

    public static void SavePlayerHomeData()
    {

        if (DataManager.Instance.PlayerHomeItems.Count >0)
        {
            if (DataManager.Instance.PlayerHomeItems[0] == null)
            {
                GetHomeItems();
            }
        }
        using (StreamWriter file = File.CreateText(HOME_LOCAL_FILE_PATH))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            JObject data = new JObject();
            var playerHomeItems = DataManager.Instance.PlayerHomeItems;
            JObject statObject = new JObject(
                                        new JProperty("GoldScore", DataManager.Instance.GoldScore),
                                        new JProperty("FishScore", DataManager.Instance.FishScore),
                                        new JProperty("RecycleScore", DataManager.Instance.RecycleScore),
                                       new JProperty("RecycleBlockScore", DataManager.Instance.RecycleBlockScore),
                                       new JProperty("PearlScore", DataManager.Instance.PearlScore));
            data.Add("Stats", statObject);

            if (playerHomeItems.Count > 0)
            {
                JArray itemsArray = new JArray();

                for (int i = 0; i < playerHomeItems.Count; i++)
                {
                    JObject jObject = null;
                    if (playerHomeItems[i].HomeItemType == HomeItemType.Factory)
                    {
                        jObject = SaveFactoryData(playerHomeItems[i]);
                    }
                    else if (playerHomeItems[i].HomeItemType == HomeItemType.House)
                    {
                        jObject = SaveHouseData(playerHomeItems[i]);
                    }
                    itemsArray.Add(jObject);
                }
                data.Add("Home Items", itemsArray);
            }

            data.WriteTo(writer);
        }
    }

    private static JObject SaveHouseData(HomeItem playerHomeItem)
    {
        return new JObject(
            new JProperty("Type", playerHomeItem.HomeItemType.ToString()),
            new JProperty("Position",
                new JObject(
                        new JProperty("x", playerHomeItem.transform.position.x),
                        new JProperty("y", playerHomeItem.transform.position.y),
                        new JProperty("z", playerHomeItem.transform.position.z))),
                new JProperty("Home Item Name",
               playerHomeItem.gameObject.GetComponent<SpriteRenderer>().sprite.name));
    }

    private static JObject SaveFactoryData(HomeItem playerHomeItem)
    {
        JObject jObject;
        var factory = (FactoryManager)playerHomeItem;
        jObject = new JObject(
        new JProperty("Type", playerHomeItem.HomeItemType.ToString()),
        new JProperty("Factory Name", factory.gameObject.GetComponent<SpriteRenderer>().sprite.name),
        new JProperty("FactoryType", factory.CurrentFactory.FactoryType.ToString()),
        new JProperty("GeneratedItemsCount", factory.GeneratedItemsCount.ToString()),
        new JProperty("Position",
                        new JObject(
                            new JProperty("x", factory.transform.position.x),
                            new JProperty("y", factory.transform.position.y),
                            new JProperty("z", factory.transform.position.z))),
        new JProperty("Factories Upgrades",
                        new JArray(
                            from f in factory.FactoriesUpgrades
                            select new JObject(
                                new JProperty("FactorySprite", "Home/Sprites/Home Items/" + f.FactorySprite.name),
                                new JProperty("CollectableType", f.CollectableType.ToString()),
                                new JProperty("Description", f.CollectableType.ToString()),
                                new JProperty("TimeTakenToGenerateAnItem", f.TimeTakenToGenerateAnItem.ToString()),
                                new JProperty("FactoryCapacity", f.FactoryCapacity.ToString()),
                                new JProperty("Cost", f.Cost.ToString()))
                            )),
        new JProperty("CurrentFactoryLevel", factory.CurrentFactoryLevel),
        new JProperty("Generator",
                        new JObject(
                            new JProperty("DateTimeToFinishGeneration", factory.Generator.DateTimeToFinishGeneration),
                            new JProperty("ClosingGameDateTime", factory.Generator.ClosingGameDateTime))));
        return jObject;
    }

    public static void GetPlayerHomeData()
    {
        try
        {
            FileInfo t = new FileInfo(HOME_LOCAL_FILE_PATH);
            if (t.Exists)
            {
                JObject homeData = JObject.Parse(File.ReadAllText(HOME_LOCAL_FILE_PATH));
                // get stats
                DataManager.Instance.GoldScore = int.Parse(homeData["Stats"]["GoldScore"].ToString());
                DataManager.Instance.FishScore = int.Parse(homeData["Stats"]["FishScore"].ToString());
                DataManager.Instance.RecycleScore = int.Parse(homeData["Stats"]["RecycleScore"].ToString());
                DataManager.Instance.RecycleBlockScore = int.Parse(homeData["Stats"]["RecycleBlockScore"].ToString());
                DataManager.Instance.PearlScore = int.Parse(homeData["Stats"]["PearlScore"].ToString());
                // get all home items (factories, houses, mosque)
                var homeItems = homeData["Home Items"].ToObject<JArray>();
                if (homeItems != null)
                {
                    List<HomeItem> items = new List<HomeItem>();
                    for (int i = 0; i < homeItems.Count; i++)
                    {
                        HomeItem homeItem = null;
                        var type = (HomeItemType)Enum.Parse(typeof(HomeItemType), homeItems[i]["Type"].ToString());
                        if (type == HomeItemType.Factory)
                        {
                            homeItem = RetrieveFactory(homeItems[i], type);
                        }
                        else if (type == HomeItemType.House)
                        {
                            homeItem = RetrieveHouse(homeItems[i], type);
                        }
                        items.Add(homeItem);
                    }
                    //DataManager.Instance.PlayerHomeItems = items;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private static HomeItem RetrieveHouse(JToken jObject, HomeItemType type)
    {
        HomeItem homeItem;
        GameObject homeGo = UnityEngine.Object.Instantiate((GameObject)Resources.Load("Home/" + jObject["Home Item Name"]));
        homeGo.name = jObject["Home Item Name"].ToString();
        homeItem = homeGo.GetComponent<HomeItem>();
        homeGo.transform.position = new Vector3(
            float.Parse(jObject["Position"]["x"].ToString()),
            float.Parse(jObject["Position"]["y"].ToString()),
             float.Parse(jObject["Position"]["z"].ToString()));
        homeItem.HomeItemType = type;
        homeItem.Placed = true;
        return homeItem;
    }

    private static HomeItem RetrieveFactory(JToken jObject, HomeItemType type)
    {
        HomeItem homeItem;
        GameObject factoryGo = UnityEngine.Object.Instantiate((GameObject)Resources.Load("Home/" + jObject["Factory Name"]));
        factoryGo.name = jObject["Factory Name"].ToString();
        homeItem = factoryGo.GetComponent<HomeItem>();
        homeItem.transform.position = new Vector3(
            float.Parse(jObject["Position"]["x"].ToString()),
            float.Parse(jObject["Position"]["y"].ToString()),
            float.Parse(jObject["Position"]["z"].ToString()));
        homeItem.HomeItemType = type;
        ((FactoryManager)homeItem).GeneratedItemsCount = (int)jObject["GeneratedItemsCount"];
        ((FactoryManager)homeItem).CurrentFactoryLevel = Convert.ToInt32(jObject["CurrentFactoryLevel"].ToString());
        ((FactoryManager)homeItem).Generator.DateTimeToFinishGeneration = Convert.ToDateTime(jObject["Generator"]["DateTimeToFinishGeneration"].ToString());
        ((FactoryManager)homeItem).Generator.ClosingGameDateTime = Convert.ToDateTime(jObject["Generator"]["ClosingGameDateTime"].ToString());
        homeItem.Placed = true;
        return homeItem;
    }

    public static void GetHomeItems()
    {
        DataManager.Instance.PlayerHomeItems.Clear();
        FileInfo t = new FileInfo(HOME_LOCAL_FILE_PATH);
        if (t.Exists)
        {
            try
            {
                JObject homeData = JObject.Parse(File.ReadAllText(HOME_LOCAL_FILE_PATH));
                DataManager.Instance.homeItems = homeData["Home Items"].ToObject<JArray>();
                Instantiate(DataManager.Instance.homeItems);
            }
            catch
            {
                Debug.Log("cant find");
            }
        }
    }

    public static void Instantiate(JArray homeItems)
    {
        if (homeItems != null)
        {
            List<HomeItem> items = new List<HomeItem>();
            for (int i = 0; i < homeItems.Count; i++)
            {
                HomeItem homeItem = null;
                var type = (HomeItemType)Enum.Parse(typeof(HomeItemType), homeItems[i]["Type"].ToString());
                if (type == HomeItemType.Factory)
                {
                    homeItem = RetrieveFactory(homeItems[i], type);
                }
                else if (type == HomeItemType.House)
                {
                    homeItem = RetrieveHouse(homeItems[i], type);
                }
                items.Add(homeItem);
            }
            DataManager.Instance.PlayerHomeItems = items;

            //if (HomeDataGeted != null)
            //    HomeDataGeted.Invoke();
        }
    }
}
