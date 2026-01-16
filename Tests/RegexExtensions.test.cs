namespace YamatoDaiwa.CSharpExtensions.Tests;

using System.Text.RegularExpressions;
using YamatoDaiwa.CSharpExtensions;


public class RegexExtensionsTester
{

  [Xunit.Fact]
  public void WorksAsIntended()
  {

    const string sample = "Meta description is [very important](https://www.orange-sha.co.jp/blog/web01_description/) for SEO.";

    string result = RegexExtensions.ReplaceMatchesWithRegularExpressionToDynamicValue(new RegexExtensions.ReplacingOfMatchesWithRegularExpressionToDynamicValue.CompoundParameter
    {
      targetString = sample,
      regularExpressionWithCapturingGroups = new Regex(@"\[(?<anchorText>.+?)\]\((?<URI>.+?)\)"),
      replacer = matching => $"<a href=\"{ matching.namedCapturingGroups["URI"] }\">{ matching.namedCapturingGroups["anchorText"] }</a>"
    });

    Xunit.Assert.Equal(
      "Meta description is <a href=\"https://www.orange-sha.co.jp/blog/web01_description/\">very important</a> for SEO.",
      result
    );

  }


  [Xunit.Fact]
  public void NumeratedCapturingGroups()
  {

    const string sample = "Item 1: 100, Item 2: 200";

    string result = RegexExtensions.ReplaceMatchesWithRegularExpressionToDynamicValue(new RegexExtensions.ReplacingOfMatchesWithRegularExpressionToDynamicValue.CompoundParameter
    {
      targetString = sample,
      regularExpressionWithCapturingGroups = new Regex(@"Item (\d+): (\d+)"),
      replacer = matching => $"({ matching.numeratedCapturingGroups[1] })={ matching.numeratedCapturingGroups[2] }"
    });

    Xunit.Assert.Equal(
      "(1)=100, (2)=200",
      result
    );

  }

}