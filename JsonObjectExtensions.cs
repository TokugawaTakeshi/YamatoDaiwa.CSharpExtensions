using System.Text.Json.Nodes;

namespace YamatoDaiwa.CSharpExtensions;


public static class JsonObjectExtensions
{
    
  public static void SetProperty(this JsonObject targetObject, string dotSeparatedPath, object value)
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
      return;
    }

    
    if (targetObject[firstSegment] is JsonObject nestedObject)
    {
      nestedObject.SetProperty(remainingPath, value);
      return;
    }

    
    throw new ArgumentNullException(
      nameof(targetObject), $"Expected a JsonObject at path \"{ firstSegment }\", but found null."
    );

  }
    
}