namespace YamatoDaiwa.CSharpExtensions;


public static class Dictionary
{
  
  extension<TKey, TValue>(Dictionary<TKey, TValue> self) where TKey : notnull
  {
    
    /* ━━━ Adding / Updating Of Pairs ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
    /* ┅┅┅ Unconditional ┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅ */
    /* ╍╍╍ One Pair ╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍ */
    public Dictionary<TKey, TValue> SetPair(TKey key, TValue value)
    {
      self.Add(key, value);
      return self;
    }
    

    /* ╍╍╍ Multiple Pairs ╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍ */
    public Dictionary<TKey, TValue> SetPairs(Dictionary<TKey, TValue> pairs)
    {
      
      foreach ((TKey key, TValue value) in pairs)
      {
        self.Add(key, value);
      }
      
      return self;
      
    }
    

    /* ┅┅┅ Conditional ┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅┅ */
    /* ╍╍╍ Non-null Condition ╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍ */
    public Dictionary<TKey, TValue> SetPairIfValueIsNotNull(TKey key, TValue? value)
    {
    
      if (value is not null)
      {
        self[key] = value;
      }
    
      return self;
    
    }

    
    /* ╍╍╍ Arbitrary Condition ╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍╍ */
    public Dictionary<TKey, TValue> SetPairIf(TKey key, TValue value, bool condition)
    {

      if (condition)
      {
        self[key] = value;
      }

      return self;

    }

    public Dictionary<TKey, TValue> SetPairIf(TKey key, TValue value, Func<TKey, TValue, bool> condition)
    {

      if (condition(key, value))
      {
        self[key] = value;
      }

      return self;

    }
    
    
    /* ━━━ Deleting ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
    public DeletingOfOnePairByKey.Result<TKey, TValue> DeleteOnePairByKey(TKey key)
    {

      self.Remove(key, out TValue? removedValue);
      
      return new DeletingOfOnePairByKey.Result<TKey, TValue>(
        hasBeenFoundAndDeleted: removedValue is not null,
        removedValue,
        updatedDictionary: self
      );

    }
    
  }
  
  
  public static class DeletingOfOnePairByKey
  {
    public record Result<TKey, TValue>(
      bool hasBeenFoundAndDeleted,
      TValue? removedValue,
      Dictionary<TKey, TValue> updatedDictionary
    ) where TKey : notnull
    {
      public readonly bool hasBeenFoundAndDeleted = hasBeenFoundAndDeleted;
      public readonly TValue? removedValue = removedValue;
      public readonly Dictionary<TKey, TValue> updatedDictionary = updatedDictionary;
    } 
  }
  
}