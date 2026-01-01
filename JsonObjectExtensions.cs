using System.Text.Json.Nodes;

namespace YamatoDaiwa.CSharpExtensions;


public static class JsonObjectExtensions
{
    
  extension(JsonObject targetObject)
  {
    
    public JsonObject SetProperty(string dotSeparatedPath, object value)
    {
    
      string[] segments = dotSeparatedPath.Split('.');
      string firstSegment = segments[0];
      string remainingPath = string.Join('.', segments.Skip(1));

      if (!targetObject.ContainsKey(firstSegment))
      {
        targetObject[firstSegment] = new JsonObject();
      }

      if (segments.Length == 1)
      {
        targetObject[firstSegment] = JsonValue.Create(value);
        return targetObject;
      }

    
      if (targetObject[firstSegment] is JsonObject nestedObject)
      {
        nestedObject.SetProperty(remainingPath, value);
        return targetObject;
      }

    
      throw new ArgumentNullException(
        nameof(targetObject), $"Expected a JsonObject at path \"{ firstSegment }\", found null."
      );

    }

    public JsonObject SetProperties(Dictionary<string, object> dotSeparatedPathsAndValues)
    {
    
      foreach ((string dotSeparatedPath, object value) in dotSeparatedPathsAndValues)
      {
        targetObject.SetProperty(dotSeparatedPath, value);
      }    
    
      return targetObject;
    
    }
    
  }
}