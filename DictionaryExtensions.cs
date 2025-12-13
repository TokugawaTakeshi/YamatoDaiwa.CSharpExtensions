namespace YamatoDaiwa.CSharpExtensions;


public static class Dictionary
{
  
  extension<TKey, TValue>(Dictionary<TKey, TValue> self) where TKey : notnull
  {
    
    /* ━━━ SetPair ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
    public Dictionary<TKey, TValue> SetPair(TKey key, TValue value)
    {
      self.Add(key, value);
      return self;
    }
    
    
    /* ━━━ SetPairs ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
    public Dictionary<TKey, TValue> SetPairs(Dictionary<TKey, TValue> pairs)
    {
      
      foreach ((TKey key, TValue value) in pairs)
      {
        self.Add(key, value);
      }
      
      return self;
      
    }
    
    
    /* ━━━ SetPairIfValueNotIsNull ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
    public Dictionary<TKey, TValue> SetPairIfValueIsNotNull(TKey key, TValue? value)
    {
    
      if (value is not null)
      {
        self[key] = value;
      }
    
      return self;
    
    }

    
    /* ━━━ SetPairIf ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ */
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

  }
  
}