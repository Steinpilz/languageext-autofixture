# LanguageExt.AutoFixture

## Introduction

Ensures all [LanguageExt](https://github.com/louthy/language-ext)'s data types could 
be instantiated by [AutoFixture](https://github.com/AutoFixture/AutoFixture)

### Install

Thi library is distributed via Nuget Package https://www.nuget.org/packages/LanguageExt.AutoFixture
and could be installed by:

```
Install-Package LanguageExt.AutoFixture
```

Then customize your `IFixture` instance:

```csharp
fixture.Customize(new LanguageExt.AutoFixture.LanguageExtCustomization());

// or use extensions method from LanguageExt.AutoFixture namespace

fixture.LanguageExt();

```

## Questions & Issues

Use github [issue tracker](https://github.com/Steinpilz/languageext-autofixture/issues)

## Maintainers
@ivanbenko

## Contribution

* Setup development environment:

1. Clone the repo
2. ```.paket\paket restore``` 
3. ```build target=build```

* Creat PR and assign to one of the maintainers
