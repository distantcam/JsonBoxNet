<a name='assembly'></a>
# JsonBoxNet

## Contents

- [IJsonBoxClient](#T-JsonBoxNet-IJsonBoxClient 'JsonBoxNet.IJsonBoxClient')
  - [CreateAsync(json)](#M-JsonBoxNet-IJsonBoxClient-CreateAsync-System-String- 'JsonBoxNet.IJsonBoxClient.CreateAsync(System.String)')
  - [CreateAsync(collection,json)](#M-JsonBoxNet-IJsonBoxClient-CreateAsync-System-String,System-String- 'JsonBoxNet.IJsonBoxClient.CreateAsync(System.String,System.String)')

<a name='T-JsonBoxNet-IJsonBoxClient'></a>
## IJsonBoxClient `type`

##### Namespace

JsonBoxNet

##### Summary

The base interface for working with jsonbox.io with strings representing the json objects.

<a name='M-JsonBoxNet-IJsonBoxClient-CreateAsync-System-String-'></a>
### CreateAsync(json) `method`

##### Summary

Create a new record.

##### Returns

The JSON record.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| json | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The JSON object to store. |

<a name='M-JsonBoxNet-IJsonBoxClient-CreateAsync-System-String,System-String-'></a>
### CreateAsync(collection,json) `method`

##### Summary

Create a new record.

##### Returns

The JSON record.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| collection | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The collection to store the object in. |
| json | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The JSON object to store. |
