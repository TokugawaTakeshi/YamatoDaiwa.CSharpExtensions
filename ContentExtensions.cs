using System.Net.Http.Json;

namespace YamatoDaiwa.CSharpExtensions;


public static class ContentExtensions
{
  
  /// <summary>
  /// See <see href="https://stackoverflow.com/a/70536133/4818123"/>.
  /// </summary>
  public static async System.Threading.Tasks.Task<ResponseData> ReadExpectedToBeNonEmptyFromJsonAsync<ResponseData>(
    this System.Net.Http.HttpContent self
  ) {
    
    ResponseData? responseData = await self.ReadFromJsonAsync<ResponseData>();

    return responseData ?? 
        throw new Exception();
    
  } 
  
}