using System;
using UnityEngine;

public class Deserializer<T>
{
  protected string resourcesApiPath { get; set; }
  protected TextAsset jsonTextFile;
  T deserializedObject;

  public Deserializer(string resourcesApiPath)
  {
    this.resourcesApiPath = resourcesApiPath;
    ResourcesAPILoader();
    GetDeserializedObject();
  }

  protected void ResourcesAPILoader()
  {
    jsonTextFile = Resources.Load<TextAsset>(resourcesApiPath);
  }

  public T GetDeserializedObject()
  {
    if (jsonTextFile == null)
    {
      throw new Exception("No JSON file assigned");
    }
    if (deserializedObject == null)
    {
      deserializedObject = JsonUtility.FromJson<T>(jsonTextFile.text);
    }
    return deserializedObject;
  }

  public TextAsset GetCurrentlyAssignedJson()
  {
    return jsonTextFile;
  }
}
