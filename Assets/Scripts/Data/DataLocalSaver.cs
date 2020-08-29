using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class DataLocalSaver
{
    private const string SKIN_LOCAL_FILE_PATH = @"E:\OceanSaver\Assets\Local Data\SkinData.json";
    private static JsonTextWriter writer;
    private static StreamWriter file;

    public static void SaveSkinDataLocally(List<SkinData> skinList)
    {
        using (file = File.CreateText(SKIN_LOCAL_FILE_PATH))
        using (writer = new JsonTextWriter(file))
        {
            foreach (var skin in skinList)
            {
                JObject jObject = new JObject(
                    new JProperty("Name", skin.Name),
                    new JProperty("Cost", skin.Cost),
                    new JProperty("TextureLocalPath", skin.TextureLocalPath));

                jObject.WriteTo(writer);
            }
        }
    }
}

