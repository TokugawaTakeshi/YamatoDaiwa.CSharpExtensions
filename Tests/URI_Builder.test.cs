namespace YamatoDaiwa.CSharpExtensions.Tests;

using YamatoDaiwa.CSharpExtensions;


public class URI_BuilderTester
{
  
  [Xunit.Fact]
  public void OriginOnly()
  {
    
    string result = URI_Builder.Build(new URI_Builder.SourceData
    {
      origin = "https://example.com"
    });
    
    Xunit.Assert.Equal("https://example.com", result);
    
  }
  
  [Xunit.Fact]
  public void StringTypeQueryParameters()
  {
    
    string result = URI_Builder.Build(new URI_Builder.SourceData
    {
      origin = "https://example.com",
      path = "/search",
      queryParameters = new Dictionary<string, object>
      {
        { "q", "dotnet" },
        { "lang", "en" }
      }
    });
    
    Xunit.Assert.Equal("https://example.com/search?q=dotnet&lang=en", result);
    
  }
  
  [Xunit.Fact]
  public void NumericQueryParameters()
  {
    
    string result = URI_Builder.Build(new URI_Builder.SourceData
    {
      origin = "https://example.com",
      queryParameters = new Dictionary<string, object>
      {
        { "id", 123 },
        { "page", 1 }
      }
    });
    
    Xunit.Assert.Equal("https://example.com?id=123&page=1", result);
    
  }
  
  [Xunit.Fact]
  public void BooleanQueryParameters()
  {
    
    string result = URI_Builder.Build(new URI_Builder.SourceData
    {
      origin = "https://example.com",
      queryParameters = new Dictionary<string, object>
      {
        { "active", true },
        { "deleted", false }
      }
    });
    
    Xunit.Assert.Equal("https://example.com?active=true", result);
    
  }
  
}