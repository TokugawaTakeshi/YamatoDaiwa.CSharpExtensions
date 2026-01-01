# Yamato Daiwa CS(harp) extensions

![Hero image of "Yamato Daiwa CS(sharp) Extensions" library](https://i.imgur.com/RZj1gp3.png)

<!-- ⚠ The NuGet Gallery does not support the definitions list ("dl" tag) -->

## The extensions of the standard classes
### `DateOnly`

* `DateOnly CreateDateOnlyFromISO8601_String(string ISO8601_String)`
* `string ToISO8601_String()`


### `DateTime`

* `DateOnly CreateDateOnlyFromISO8601_String(string ISO8601_String)`
* `string ToISO8601_String()`


### EmailAddress
#### Public static fields


|             |                 |
|-------------|-----------------|
| Name        | `VALID_PATTERN` |
| Type        | Regex           |
| Is readonly | Yes             |

Contains the regular expression of the valid email address according 
  [w3resource.com](https://www.w3resource.com/javascript/form/email-validation.php).

#### Public static methods

* `bool IsValid(string potentialEmail)`


### `Array`

* `TElement[] LogEachElement<TElement>(Action<TElement>? logger = null)`


### `Content`

+ `System.Threading.Tasks.Task<ResponseData> ReadExpectedToBeNonEmptyFromJsonAsync<ResponseData>(this System.Net.Http.HttpContent self)`


### `Dictionary`

* `Dictionary<TKey, TValue> SetPair(TKey key, TValue value)`
* `Dictionary<TKey, TValue> SetPairs(Dictionary<TKey, TValue> pairs)
* `Dictionary<TKey, TValue> SetPairIfValueIsNotNull(TKey key, TValue? value)`
* `SetPairIf`
  * `Dictionary<TKey, TValue> SetPairIf<TKey, TValue>(TKey key, TValue value, bool condition) where TKey : notnull`
  * `Dictionary<TKey, TValue> SetPairIf<TKey, TValue>(TKey key, TValue value, Func<TKey, TValue, bool> condition) where TKey : notnull`


### `List`

* `List<TElement> AddElementsToStart<TElement> (params TElement[] newElements)`
* `List<TElement> AddElementsToEnd<TElement> (params TElement[] newElements) `
* `List<TElement?> AddElementToEndIfNotNull<TElement>(TElement? newElement)`
* `AddElementToEndIf`
  * `List<TElement> AddElementToEndIf<TElement>(TElement newElement, bool condition)`
  * `List<TElement> AddElementToEndIf<TElement>(TElement newElement, Func<TElement?, bool> condition)`
* `StringifyEachElementAndJoin<TElement>(string separator)` 
* `ReplaceArrayElementsByPredicate`
  * `List<TElement> ReplaceArrayElementsByPredicate<TElement>(Func<TElement, bool> predicate, TElement newElement, bool mustReplaceOnlyFirstOne)`
  * `List<TElement> ReplaceArrayElementsByPredicate<TElement>(Func<TElement, bool> predicate, Func<TElement, TElement> replacer, bool mustReplaceOnlyFirstOne)`
* `List<TElement> LogEachElement<TElement>(Action<TElement>? logger = null)` 


### `Number`

* `bool IsValueOfAnyNumericType(object value)`


### `String`

* `bool IsNonEmpty()`
* `string ToUpperCamelCase()`
* `string ToLowerCamelCase()`
* `string RemoveAllSpecifiedCharacters(char[] charactersToRemove)`


### `System.Text.Json.Nodes.JsonObject`

* `void SetProperty(this JsonObject targetObject, string dotSeparatedPath, object value)`


## Random values generators (`RandomValuesGenerator` class)

* `bool GetRandomBoolean()`
* `byte GetRandomByte(byte minimalValue = Byte.MinValue, byte maximalValue = Byte.MaxValue)`
* `ushort GetRandomUShort(ushort minimalValue = ushort.MinValue, ushort maximalValue = ushort.MaxValue)`
* `TArrayElement GetRandomArrayElement<TArrayElement>(TArrayElement[] targetArray)`
* `DateOnly GetRandomDate(DateOnly earliestDate, DateOnly latestDate)`
* `DateTime GetRandomDateTime(DateOnly earliestDate, DateOnly latestDate)`


## Data mocking
### `DataMocking` class
#### `NullablePropertiesDecisionStrategies` enumeration

| Enumeration element                                             | Description                                                                                       |
|-----------------------------------------------------------------|---------------------------------------------------------------------------------------------------|
| `mustGenerateAll`                                               | All nullable (optional) properties must be generated except the cases of incompatible properties. |
| `mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined` | Nullable (optional) properties must be generated with 50% probability.                            |
| `mustSkipIfHasNotBeenPreDefined`                                | Nullable (optional) properties must *not* be randomly generated.                                  |


#### `DecideOptionalValue` static method and `NullablePropertiesDecisionSourceDataAndOptions` structure

```
TValueType? DecideOptionalValue<TValueType>(NullablePropertiesDecisionSourceDataAndOptions<TValueType> sourceDataAndOptions)
```

```csharp
struct NullablePropertiesDecisionSourceDataAndOptions<TPropertyType>
{
  public required NullablePropertiesDecisionStrategies Strategy { get; init; }
  public required Func<TPropertyType> RandomValueGenerator? { get; init; }
  public TPropertyType? PreDefinedValue { get; init; }
}
```

Decides, will nullable (optional) value be available or no, and if yes, returns this value.
Intended to be used when generating the properties for the class / structure.

The decision will depend mainly on specified nullable property decision strategy:

| Enumeration element                                             | Description                                                                                                                                                                                                                                                                                                                                          |
|-----------------------------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `mustGenerateAll`                                               | If target nullable property has *not* been predefined via `NullablePropertiesDecisionStrategies.PreDefinedValue`, it will be generated by specified `RandomValueGenerator`. It means the `RandomValueGenerator` must be specified for this strategy, otherwise `ArgumentException` will be thrown.                                                   |
| `mustGenerateWith50PercentageProbabilityIfHasNotBeenPreDefined` | If target nullable property has been predefined via `NullablePropertiesDecisionStrategies.PreDefinedValue`, this value will be returned otherwise will be generated by specified `RandomValueGenerator` with probability 50%. It means the `RandomValueGenerator` must be specified for this strategy, otherwise `ArgumentException` will be thrown. |
| `mustSkipIfHasNotBeenPreDefined`                                | If target nullable property has *not* been predefined via `NullablePropertiesDecisionStrategies.PreDefinedValue`, no random value will be generated. Only for the strategy `RandomValueGenerator` should be omitted.                                                                                                                                 |



### Errors classes

| Error class                     | Usage                                                                                                           |
|---------------------------------|-----------------------------------------------------------------------------------------------------------------|
| `DataRetrievingFailedException` | Intended to be used when the data retrieving from some external resource (server, database, file, etc.) failed. |
| `DataSubmittingFailedException` | Intended to be used when the data submitting to any external resource (server, database, etc.) failed.          |

#### `DataRetrievingFailedException`

* Constructor
  * `DataRetrievingFailedException(string message)`
  * `DataRetrievingFailedException(string message, Exception innerException)`
  * `DataRetrievingFailedException(string messageSpecificPart, HttpResponseMessage response, object? responseData)`
* Public static Methods
  * `async Task<object?> TryToExtractResponseDataForLogging(HttpResponseMessage response)`


### `MockGatewayHelper` class

When mock the asynchronous data transactions, takes care about such routines as

1. Randomizing of the pending interval
2. Simulation of the error
3. Logging


### Public static methods
#### `SetLogger`

```csharp
void SetLogger(Log log)
    
delegate void Log(string message) 
```

Adds the logging method which will be invoked if the logging will be demanded in other methods.


#### `SimulateDataRetrieving`

```csharp
Task<TResponseData> SimulateDataRetrieving<TRequestParameters, TResponseData>(
  TRequestParameters requestParameters,
  Func<TResponseData> getResponseData,
  SimulationOptions options
)
```

where the `SimulationOptions` is the associated **structure** will be [described below](#associated-structure-simulationoptions).


#### `SimulateDataSubmitting`

```csharp
Task<TResponseData> SimulateDataSubmitting<TRequestData, TResponseData>(
  TRequestData requestData,
  Func<TResponseData> getResponseData,
  SimulationOptions options
)
```

It's behaviour is almost even with `SimulateDataRetrieving`, but there are some differences:

* The logging
* `DataSubmittingFailedException` will be thrown instead of `DataRetrievingFailedException` when error simulation mode
  is enabled.


### Associated structure `SimulationOptions`

```csharp
public struct SimulationOptions
{
  public ushort? MinimalPendingPeriod__Seconds { get; init; }
  public ushort? MaximalPendingPeriod__Seconds { get; init; }
  public bool MustSimulateError { get; init; }
  public bool MustLogResponseData { get; init; }
  public required string GatewayName { get; init; }
  public required string TransactionName { get; init; }
}
```

#### MinimalPendingPeriod__Seconds

|               |                                                                              |
|---------------|------------------------------------------------------------------------------|
| Type          | ushort                                                                       |
| Default value | 1                                                                            |
| Use case      | If you want to see the loading placeholder for a while, increase this value. |

#### MaximalPendingPeriod__Seconds

|               |                                                                              |
|---------------|------------------------------------------------------------------------------|
| Type          | ushort                                                                       |
| Default value | 2                                                                            |
| Use case      | If you want to see the loading placeholder for a while, increase this value. |

#### MustSimulateError

|               |                                                                                                    |
|---------------|----------------------------------------------------------------------------------------------------|
| Type          | bool                                                                                               |
| Default value | false                                                                                              |
| Use case      | If you want to check is the error message displays correctly in your GUI, set this flag to `true`. |

#### MustSimulateError

|             |      |
|-------------|------|
| Type        | bool |
| Is required | Yes  |

#### MustLogResponseData

|             |                                                                 |
|-------------|-----------------------------------------------------------------|
| Type        | bool                                                            |
| Is required | Yes                                                             |
| Note        | `System.Text.Json.JsonSerializer` will be used for serializing. |

#### GatewayName

|             |                                                                                          |
|-------------|------------------------------------------------------------------------------------------|
| Type        | bool                                                                                     |
| Is required | Yes                                                                                      |
| Note        | Used for logging only. "Gateway" could be called "service" or somehow else in your code. |

#### TransactionName

|             |                        |
|-------------|------------------------|
| Type        | bool                   |
| Is required | Yes                    |
| Note        | Used for logging only. |



