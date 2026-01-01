using System.Net.Http.Json;

namespace YamatoDaiwa.CSharpExtensions.Exceptions;


public class DataRetrievingFailedException : Exception
{
  
  /* ━━━ Constructors ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public DataRetrievingFailedException(string message) : base(message) {}

  public DataRetrievingFailedException(string message, Exception innerException) : base(message, innerException) {}
 
  public DataRetrievingFailedException(string messageSpecificPart, HttpResponseMessage response, object? responseData) : 
      base(DataRetrievingFailedException.BuildMessage(messageSpecificPart, response, responseData)) {}
  
  
  /* ━━━ Public Static Methods ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  public static async Task<object?> TryToExtractResponseDataForLogging(HttpResponseMessage response)
  {
    try
    {
      return await response.Content.ReadFromJsonAsync<object>();
    }
    catch (Exception)
    {
      return null;
    }
  }
  
  
  /* ━━━ Protected Static Methods ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected static readonly System.Text.Json.JsonSerializerOptions JSON_SerializedOptions = new() { WriteIndented = true };
  
  
  /* ━━━ Protected Static Methods ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
  protected static string BuildMessage(string messageSpecificPart, HttpResponseMessage response, object? responseData)
  {
    
    string formattedResponseData = "Response Data: ";

    if (responseData is not null)
    {
    
      try
      {
      
        formattedResponseData += System.Text.Json.JsonSerializer.Serialize(
          responseData,
          options: DataRetrievingFailedException.JSON_SerializedOptions
        );
        
      }
      catch (Exception)
      {
        formattedResponseData += "(not serializable)";
      }
      
    } else
    {
      formattedResponseData += "(not available)";
    }
    

    return $"{ messageSpecificPart }\n${ response }" + 
        (formattedResponseData.Length > 0 ? $"\n{ formattedResponseData }" : "");
    
  }
  
}