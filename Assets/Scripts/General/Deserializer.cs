using System;
using UnityEngine;

public class Deserializer<T>
{
  protected string resourcesApiPath { get; set; }

  /// <summary>
  /// The loaded JSON file from the Resources API. May be null until <see cref="ResourcesAPILoader"/> is called.
  /// </summary>
  protected TextAsset jsonTextFile;

  T deserializedObject;

  /// <summary>
  /// Creates a new <see cref="Deserializer{T}"/> with the specified resources path.
  /// </summary>
  /// <param name="resourcesApiPath">The path passed to <see cref="Resources.Load{TextAsset}(string)"/>,
  /// typically a folder path plus filename without extension.</param>
  public Deserializer(string resourcesApiPath)
  {
    this.resourcesApiPath = resourcesApiPath;
  }

  /// <summary>
  /// Loads the <see cref="TextAsset"/> from the Resources API using the configured <see cref="resourcesApiPath"/>.
  /// After calling this method, <see cref="jsonTextFile"/> will be assigned if a matching resource exists.
  /// </summary>
  public void ResourcesAPILoader()
  {
    jsonTextFile = Resources.Load<TextAsset>(resourcesApiPath);
  }

  /// <summary>
  /// Returns the deserialized instance of <typeparamref name="T"/>. The JSON is parsed on first call
  /// and the result is cached for subsequent calls.
  /// </summary>
  /// <returns>The deserialized object of type <typeparamref name="T"/>.</returns>
  /// <exception cref="Exception">Thrown when no JSON file has been loaded. Call <see cref="ResourcesAPILoader"/> first.</exception>
  public T GetDeserializedObject()
  {
    if (jsonTextFile == null)
    {
      throw new Exception($"No JSON file assigned -- {this}.ResourcesAPILoader() wasn't initialized");
    }
    if (deserializedObject == null)
    {
      deserializedObject = JsonUtility.FromJson<T>(jsonTextFile.text);
    }
    return deserializedObject;
  }

  /// <summary>
  /// Returns the <see cref="TextAsset"/> currently assigned by <see cref="ResourcesAPILoader"/>,
  /// or null if none has been loaded.
  /// </summary>
  /// <returns>The currently assigned <see cref="TextAsset"/>, or null.</returns>
  public TextAsset GetCurrentlyAssignedJson()
  {
    return jsonTextFile;
  }
}
