<!--
GENERATED FILE - DO NOT EDIT
This file was generated by [MarkdownSnippets](https://github.com/SimonCropp/MarkdownSnippets).
Source File: /docs/mdsource/introduction.source.md
To change this file edit the source file and then run MarkdownSnippets.
-->

# JsonBoxNet

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](https://github.com/distantcam/jsonboxnet/blob/master/LICENSE.md)
[![Tweet](https://img.shields.io/twitter/url/http/shields.io?style=social)](https://twitter.com/intent/tweet?url=https%3A%2F%2Fgithub.com%2Fdistantcam%2Fjsonboxnet&hashtags=dotnet,jsonbox)
<a class="github-button" href="https://github.com/distantcam/jsonboxnet" data-icon="octicon-star" data-show-count="true" aria-label="Star distantcam/jsonboxnet on GitHub">Star</a>

A .NET library for using https://jsonbox.io/

<!-- toc -->
## Contents

  * [Nuget Packages](#nuget-packages)
  * [Usage](#usage)
    * [Newtonsoft](#newtonsoft)
    * [Delete](#delete)
    * [SystemTextJson](#systemtextjson)
    * [Delete](#delete-1)
  * [Contact & Support](#contact--support)
  * [License](#license)<!-- endtoc -->

## Nuget Packages

| Package Name | Version | API |
| ------------ | :-----: | :-: |
| JsonBoxNet | ![JsonBoxNet Nuget](https://img.shields.io/nuget/v/JsonBoxNet?style=flat-square) | [API](api/JsonBoxNet)
| JsonBoxNet.Newtonsoft | ![JsonBoxNet.Newtonsoft Nuget](https://img.shields.io/nuget/v/JsonBoxNet.Newtonsoft?style=flat-square) | [API](api/JsonBoxNet.Newtonsoft)
| JsonBoxNet.TextJson | ![JsonBoxNet.TextJson Nuget](https://img.shields.io/nuget/v/JsonBoxNet.TextJson?style=flat-square) | [API](api/JsonBoxNet.TextJson)

## Usage

### Newtonsoft

Uses the [Newtonsoft Json.NET](https://www.newtonsoft.com/json) for serialization.

#### Shared Settings

`JsonSerializer` and `HttpClient` should be instantiated once and shared for all usages:

<!-- snippet: NewtonsoftShared -->
<a id='snippet-newtonsoftshared'/></a>
```cs
static NewtonsoftJsonUsage()
{
	serializer = new JsonSerializer();
	httpClient = new HttpClient();
}
```
<sup><a href='/src/JsonBoxNet.Tests/Snippets/NewtonsoftJsonUsage.cs#L11-L17' title='File snippet `newtonsoftshared` was extracted from'>snippet source</a> | <a href='#snippet-newtonsoftshared' title='Navigate to start of snippet `newtonsoftshared`'>anchor</a></sup>
<!-- endsnippet -->

#### Create

<!-- snippet: NewtonsoftCreate -->
<a id='snippet-newtonsoftcreate'/></a>
```cs
var box = new NewtonsoftJsonBox(
	httpClient,
	theBoxId,
	serializer);
var person = new Person
{
	Age = 10,
	Name = "Lesle"
};
await box.CreateAsync(person);
```
<sup><a href='/src/JsonBoxNet.Tests/Snippets/NewtonsoftJsonUsage.cs#L21-L32' title='File snippet `newtonsoftcreate` was extracted from'>snippet source</a> | <a href='#snippet-newtonsoftcreate' title='Navigate to start of snippet `newtonsoftcreate`'>anchor</a></sup>
<!-- endsnippet -->

### Delete

<!-- snippet: NewtonsoftDelete -->
<a id='snippet-newtonsoftdelete'/></a>
```cs
var box = new NewtonsoftJsonBox(
	httpClient,
	theBoxId,
	serializer);
await box.DeleteAsync(itemId);
```
<sup><a href='/src/JsonBoxNet.Tests/Snippets/NewtonsoftJsonUsage.cs#L36-L42' title='File snippet `newtonsoftdelete` was extracted from'>snippet source</a> | <a href='#snippet-newtonsoftdelete' title='Navigate to start of snippet `newtonsoftdelete`'>anchor</a></sup>
<!-- endsnippet -->

### SystemTextJson

Uses the [System.Text.Json](https://docs.microsoft.com/en-us/dotnet/api/system.text.json) for serialization.

#### Shared Settings

`HttpClient` should be instantiated once and shared for all usages:

<!-- snippet: SystemTextShared -->
<a id='snippet-systemtextshared'/></a>
```cs
static TextJsonUsage()
{
	httpClient = new HttpClient();
}
```
<sup><a href='/src/JsonBoxNet.Tests/Snippets/TextJsonUsage.cs#L9-L14' title='File snippet `systemtextshared` was extracted from'>snippet source</a> | <a href='#snippet-systemtextshared' title='Navigate to start of snippet `systemtextshared`'>anchor</a></sup>
<!-- endsnippet -->

#### Create

<!-- snippet: SystemTextCreate -->
<a id='snippet-systemtextcreate'/></a>
```cs
var box = new SystemTextJsonBox(
	httpClient,
	theBoxId);
var person = new Person
{
	Age = 10,
	Name = "Lesle"
};
await box.CreateAsync(person);
```
<sup><a href='/src/JsonBoxNet.Tests/Snippets/TextJsonUsage.cs#L18-L28' title='File snippet `systemtextcreate` was extracted from'>snippet source</a> | <a href='#snippet-systemtextcreate' title='Navigate to start of snippet `systemtextcreate`'>anchor</a></sup>
<!-- endsnippet -->

### Delete

<!-- snippet: SystemTextDelete -->
<a id='snippet-systemtextdelete'/></a>
```cs
var box = new SystemTextJsonBox(
	httpClient,
	theBoxId);
await box.DeleteAsync(itemId);
```
<sup><a href='/src/JsonBoxNet.Tests/Snippets/TextJsonUsage.cs#L32-L37' title='File snippet `systemtextdelete` was extracted from'>snippet source</a> | <a href='#snippet-systemtextdelete' title='Navigate to start of snippet `systemtextdelete`'>anchor</a></sup>
<!-- endsnippet -->

## Contact & Support

- Create a [GitHub issue](https://github.com/distantcam/jsonboxnet/issues) for bug reports, feature requests, or questions
- Follow [@distantcam](https://twitter.com/distantcam) on Twitter
- Add a ⭐️ [star on GitHub](https://github.com/distantcam/jsonboxnet) or ❤️ [tweet](https://twitter.com/intent/tweet?url=https%3A%2F%2Fgithub.com%2Fdistantcam%2Fjsonboxnet&hashtags=dotnet,jsonbox) to support the project!

## License

This project is licensed under the [MIT license](https://github.com/distantcam/jsonboxnet/blob/master/LICENSE.md).

Copyright (c) Cameron MacFarland ([@distantcam](https://twitter.com/distantcam))

<!-- GitHub Buttons -->
<script async defer src="https://buttons.github.io/buttons.js"></script>
