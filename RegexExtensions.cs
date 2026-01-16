using System.Text.RegularExpressions;

namespace YamatoDaiwa.CSharpExtensions;


public static class RegexExtensions
{

  public static string ReplaceMatchesWithRegularExpressionToDynamicValue(
    ReplacingOfMatchesWithRegularExpressionToDynamicValue.CompoundParameter compoundParameter
  )
  {

    Regex regularExpression = new(
      compoundParameter.regularExpressionWithCapturingGroups.ToString(), 
      RegexOptions.Multiline | RegexOptions.CultureInvariant
      );

    return regularExpression.Replace(
      compoundParameter.targetString, 
      (Match match) =>
        {

          ReplacingOfMatchesWithRegularExpressionToDynamicValue.Matching matching = new()
          {
            fullMatching = match.Value,
            positionIndex = match.Index,
            namedCapturingGroups = [],
            numeratedCapturingGroups = []
          };

          foreach (string groupName in regularExpression.GetGroupNames())
          {
            if (int.TryParse(groupName, out int groupNumber))
            {
              if (groupNumber > 0)
              {
                matching.numeratedCapturingGroups[groupNumber] = match.Groups[groupNumber].Value;
              }
            }
            else
            {
              matching.namedCapturingGroups[groupName] = match.Groups[groupName].Value;
            }
          }

          return compoundParameter.replacer(matching) ?? match.Value;

        }
    );

  }


  public static class ReplacingOfMatchesWithRegularExpressionToDynamicValue
  {

    public record CompoundParameter
    {
      public required string targetString { get; init; }
      public required Regex regularExpressionWithCapturingGroups { get; init; }
      public required Replacer replacer { get; init; }
    }

    public delegate string? Replacer(Matching matching);

    public record Matching
    {
      public required string fullMatching { get; init; }
      public required int positionIndex { get; init; }
      public required Dictionary<string, string?> namedCapturingGroups { get; init; }
      public required Dictionary<int, string?> numeratedCapturingGroups { get; init; }
    }

  }

}