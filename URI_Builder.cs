namespace YamatoDaiwa.CSharpExtensions;


public abstract class URI_Builder
{

  public static string Build(URI_Builder.SourceData sourceData)
  {

    List<string> stringifiedQueryParameters = []; 

    foreach (KeyValuePair<string,object> queryParameter in sourceData.queryParameters ?? [])
    {

      if (
        queryParameter.Value is string || 
        NumberExtensions.IsValueOfAnyNumericType(queryParameter.Value) ||
        queryParameter.Value is Enum
      ) 
      {
        stringifiedQueryParameters.Add($"{ queryParameter.Key }={ queryParameter.Value }");
      } 
      else if (queryParameter.Value is true)
      {
        stringifiedQueryParameters.Add($"{ queryParameter.Key }=true");
      }
      
    }

    return new List<string?>().
        AddElementToEndIfNotNull(sourceData.origin).
        AddElementToEndIfNotNull(sourceData.path).
        AddElementToEndIf(
          $"?{ stringifiedQueryParameters.StringifyEachElementAndJoin("&") }",
  stringifiedQueryParameters.Count > 0
        ).
        StringifyEachElementAndJoin("");

  }
  
  public record SourceData
  {
    public string? origin { get; init; }
    public string? path { get; init; }
    public Dictionary<string, object>? queryParameters { get; init; }
  }
  
}
