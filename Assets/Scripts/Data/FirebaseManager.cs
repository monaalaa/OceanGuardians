using System;
using Firebase;
using UnityEngine;
using Firebase.Database;
using System.Collections;
using Firebase.Unity.Editor;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Storage;
public class FirebaseManager : MonoBehaviour
{
    public static bool GooglePlayServiceIsUpToDate = false;
    public static FirebaseManager Instance;
    private StorageReference storageReference;
    private static DatabaseReference databaseReference;
    private const string TEXTURES_LOCAL_PATH = "file://Assets/Textures/";
    private const string SKIN_CLOUD_PATH = "Textures/Skin/";
    private FirebaseStorage storage;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        CheckGooglePlayServiceVersion();
        SetupFirebase();
    }

    private void SetupFirebase()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ocean-saver.firebaseio.com/");
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://ocean-saver.appspot.com");
    }

    private void CheckGooglePlayServiceVersion()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                GooglePlayServiceIsUpToDate = true;
            }
            else
            {
                Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    public IEnumerator GetDataList<T>(string dataType, T objectType, Action<List<T>> resultList)
    {
        List<T> fillList = CreateList(objectType);
        var task = FirebaseDatabase.DefaultInstance.GetReference(dataType).GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted || task.IsFaulted);
        if (task.IsFaulted)
        {
            // Handle the error...
            Debug.LogError("Error in retrieving data.. ");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            foreach (DataSnapshot snapshots in snapshot.Children)
            {
                fillList.Add(JsonUtility.FromJson<T>(snapshots.GetRawJsonValue().ToString()));
            }
            resultList(fillList);
        }
    }

    public IEnumerator GetOneElement<T>(string dataType, T objectType, Action<T> resultObject, string childName = "")
    {
        Task<DataSnapshot> task;
        if (!childName.Equals(""))
        {
            task = FirebaseDatabase.DefaultInstance.GetReference(dataType).Child(childName).GetValueAsync();
        }
        else
        {
            task = FirebaseDatabase.DefaultInstance.GetReference(dataType).GetValueAsync();
        }
        yield return new WaitUntil(() => task.IsCompleted || task.IsFaulted);
        if (task.IsFaulted)
        {
            // Handle the error...
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            try
            {
                // return string
                resultObject((T)(Convert.ChangeType(snapshot.GetRawJsonValue(), typeof(T))));
            }
            catch (Exception)
            {
                // return class type
                resultObject(JsonUtility.FromJson<T>(snapshot.GetRawJsonValue()));
            }
        }
    }

    public void SaveSkinData(SkinData skinData)
    {
        StartCoroutine(CreateSkinData(skinData));
    }

    private IEnumerator CreateSkinData(SkinData skinData)
    {
        var imgRef = storageReference.Child(SKIN_CLOUD_PATH + skinData.Name + ".jpg");
        Task task = imgRef.PutFileAsync(TEXTURES_LOCAL_PATH + skinData.Name + ".jpg");
        yield return new WaitUntil(() => task.IsCompleted || task.IsFaulted);
        if (task.IsFaulted)
        {
            Debug.Log(task.Exception.ToString());
            // Uh-oh, an error occurred!
        }
        else if (task.IsCompleted && !task.IsCanceled)
        {
            // Metadata contains file metadata such as size, content-type, and download URL.
            imgRef.GetDownloadUrlAsync().ContinueWith((Task<Uri> uriTask) =>
            {
                if (uriTask.IsFaulted || uriTask.IsCanceled)
                {
                    Debug.Log(uriTask.Exception);
                }
                else
                {
                    skinData.TextureURI = uriTask.Result.ToString();
                    string json = JsonUtility.ToJson(skinData);
                    databaseReference.Child("Skins").Child(skinData.Name).SetRawJsonValueAsync(json);
                }
            });

        }
    }

    public void GetTextureLocalPathByName(string textureName, Action<string> resultObject)
    {
        StartCoroutine(GetTexturePath(textureName, resultObject));
    }

    public IEnumerator GetTexturePath(string textureName, Action<string> resultObject)
    {
        StorageReference imgRef = storageReference.Child(SKIN_CLOUD_PATH + textureName + ".jpg");
        Task task = imgRef.GetFileAsync(TEXTURES_LOCAL_PATH + textureName + ".jpg");
        yield return new WaitUntil(() => task.IsCompleted || task.IsFaulted);
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.Log(task.Exception.ToString());
        }
        else if (task.IsCompleted)
        {
            Debug.Log("download finished");
            resultObject(TEXTURES_LOCAL_PATH + textureName + ".jpg");
        }
    }

    public void GetTextureByUri(string textureUri, Action<Texture2D> resultObject)
    {
        StartCoroutine(GetTexture(textureUri, resultObject));
    }

    public IEnumerator GetTexture(string textureUri, Action<Texture2D> resultObject)
    {
        WWW www = new WWW(textureUri);
        yield return www;
        resultObject(www.texture);
    }

    public void SetData<T>(string childName, T obj, string myname)
    {
        string json = JsonUtility.ToJson(obj);
        databaseReference.Child(childName).Child(myname).SetRawJsonValueAsync(json);
    }

    private static List<T> CreateList<T>(T objectType)
    {
        var list = new List<T>();
        return list;
    }


}
