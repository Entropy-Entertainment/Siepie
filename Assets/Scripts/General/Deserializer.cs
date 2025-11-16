using System;
using UnityEngine;

/// <summary>
/// Concrete implementation of a JSON deserializer.
/// </summary>
/// <typeparam name="T">The deserialized type. Constrained to <see cref="IDialogObject"/> here because callers expect dialog objects.</typeparam>
public class Deserializer<T>
{
  public string ResourcesApiPath { get; set; }
  public string SceneFileName { get; set; }
  public TextAsset JsonTextFile { get; set; }
  T deserializedObject;

  public Deserializer(string resourcesApiPath, string sceneFileName)
  {
    this.ResourcesApiPath = resourcesApiPath;
    this.SceneFileName = sceneFileName;
  }

  public static TextAsset ResourcesAPILoader(string resourcesApiPath, string sceneFileName)
  {
    string.Concat(resourcesApiPath, "/", sceneFileName);
    return Resources.Load<TextAsset>(resourcesApiPath);
  }

  public void ResourcesAPILoader()
  {
    string path = string.Concat(ResourcesApiPath, "/", SceneFileName);
    JsonTextFile = Resources.Load<TextAsset>(path);
  }

  public static T GetDeserializedObject(TextAsset jsonTextFile)
  {
    return JsonUtility.FromJson<T>(jsonTextFile.text);
  }

  public T GetDeserializedObject()
  {
    if (JsonTextFile == null)
    {
      throw new Exception($"No JSON file assigned -- or you forgot to call {this}.ResourcesAPILoader()");
    }
    if (deserializedObject == null)
    {
      deserializedObject = JsonUtility.FromJson<T>(JsonTextFile.text);
    }
    return deserializedObject;
  }

  public TextAsset GetCurrentlyAssignedJson()
  {
    return JsonTextFile;
  }
}
