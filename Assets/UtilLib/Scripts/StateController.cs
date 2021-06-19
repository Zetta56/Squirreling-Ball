using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using Newtonsoft.Json; // Newtonsoft's Json.NET helps serialize and deserialize dictionaries

namespace UtilLib.Scripts
{
  public static class StateController
  {
    static string savePath;
    static Dictionary<string, object> state;

    static StateController() {
      // Create save file if it doesn't exist
      savePath = Application.persistentDataPath + "/save.json";
      if(!File.Exists(savePath)) {
        FileStream fs = File.Create(savePath);
        // Close fs properly to release memory and allow StreamWriter to access save file
        fs.Close();
        fs.Dispose();
      }
      // Writes default state to save file if it's empty
      if(new FileInfo(savePath).Length == 0) {
        using(StreamWriter w = new StreamWriter(savePath)) {
          // currentLevel is in defaultState because it's used throughout
          // scripts and the key doesn't change often
          Dictionary<string, object> defaultState = new Dictionary<string, object> {
            { "currentLevel", "Level 1" }
          };
          w.Write(JsonConvert.SerializeObject(defaultState));
        }
      }
      // Load save file into state
      string rawJson;
      using(StreamReader r = new StreamReader(savePath)) {
        rawJson = r.ReadToEnd();
      }
      state = JsonConvert.DeserializeObject<Dictionary<string, object>>(rawJson);
    }

    public static T Get<T>(string key, T defaultValue = default(T)) {
      if(state.ContainsKey(key)) {
        // Convert class handles casting from object
        return (T)System.Convert.ChangeType(state[key], typeof(T));
      } else {
        return defaultValue;
      }
    }

    public static void Set(string key, object value) {
      state[key] = value;
      using(StreamWriter w = new StreamWriter(savePath)) {
        w.Write(JsonConvert.SerializeObject(state));
      }
    }

    public static void Delete(string key) {
      state.Remove(key);
      using(StreamWriter w = new StreamWriter(savePath)) {
        w.Write(JsonConvert.SerializeObject(state));
      }
    }
  }
}

// Source: https://stackoverflow.com/questions/6416017/json-net-deserializing-nested-dictionaries
// class NestedDictConverter : CustomCreationConverter<IDictionary<string, object>>
// {
//   // IDictionary is basically the superclass of dictionary
//   public override IDictionary<string, object> Create(Type objectType)
//   {
//     return new Dictionary<string, object>();
//   }

//   // Allows deseriallization of dict value of type object
//   public override bool CanConvert(Type objectType)
//   {
//     return objectType == typeof(object) || base.CanConvert(objectType);
//   }

//   public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//   {
//     // Use custom deserializer if next token is an object
//     if (reader.TokenType == JsonToken.StartObject || reader.TokenType == JsonToken.Null)
//       return base.ReadJson(reader, objectType, existingValue, serializer);
//     // Use default deserializer otherwise (ex. strings, numbers, etc.)
//     return serializer.Deserialize(reader);
//   }
// }
