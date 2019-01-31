# Audio2Tag
this program can Cue Split and Audio Tagging

## How do work?

Here you can see the test and auto-run one by one.
<hr/>

## How to build?
### Only Windows
```
You need Clone this repo.
1. git clone https://github.com/Piorosen/Audio2Tag

Move Clone folder.
2. cd Audio2Tag

Dependency project clone.
3. git clone https://github.com/Piorosen/atldotnet.git

connect solution file.
4. load atldotnet csproj.
5. Add a reference to Tag.Core.

Build.
5. this project is dotnet framework 4.7.2 base.
```

<hr />

## How do split?

   Flac |   Wav  |   Mp3
:------:|:------:|:-----:
CueTools.Flake | NAudio.Wave | NONE
NAudio.Flac    |  --- | ---

<hr />

### CueFile Read List

CueInfo
 *Path*  
 *SavePath*  
 *AudioPath*  
 *Title*  
 *Barcode*  
 *AudioType*  
 *REM* : Genre, Date, DiscId  
 *TrackList* : Title, Artist, Composer, StartPosition, DurationMS, TrackNumk  

<hr/>

### TagInfo List
*Identifier*   : string , Tag Number  
*Lang*         : string , language  
*Path*         : string , audio file path  
*Title*        : string , Title  
*Artist*       : List<string> , artist list  
*AlbumArtist*  : List<string> , Album artist list  
*Album*        : string , Album Title  
*Year*         : string , Generate Year  
*Track*        : uint , Track Num  
*Genre*        : List<string> , Genre list  
*Comment*      : string , creater comment  
*Composer*     : List<string> , music composer  
*DiscId*       : string , CatNo and DiscNum  
*Picture*      : List<TagLib.IPicture> , Image file  
*Barcode*      : string , Album Barcode  
*Publisher*    : List<string> , Album publisher  
*Format*       : List<string> , DVD or CD, blue-ray  
*Country*      : string , create country  
