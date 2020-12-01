# AsciiExtensions

ASCII extensions for .NET

## Instalation

Add *AsciiExtensions.cs* or *AsciiExtensions.dll* to your Visual Studio project.
Add *using System.Text.Ascii;* to your source file.

## Usage

```csharp
var a = "München ist eine “übergröße” Stadt".ToAscii();
//  a : "Munchen ist eine "ubergrose" Stadt"    

var b = "abc123".IsAlphaNumeric();
//  b : true

var c = "123".IsNaturalNumber();
//  c : true

var d = "docs.microsoft.com".IsValidDomain();
//  d : true

var e = "qusho@gmail.com".IsValidEmail();
//  d : true
```

## Credits


Some code was derived from [Lucene.Net library](https://github.com/apache/lucenenet/blob/master/src/Lucene.Net.Analysis.Common/Analysis/Miscellaneous/ASCIIFoldingFilter.cs).
Full license of Lucene.Net library is here:  
https://github.com/apache/lucenenet/blob/master/LICENSE.txt

Some code was derived from [Alexander on StackOverflow](https://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net#comment86833005_34272324).

A lot of code was derived from [andyraddatz](https://gist.github.com/andyraddatz/e6a396fb91856174d4e3f1bf2e10951c).
