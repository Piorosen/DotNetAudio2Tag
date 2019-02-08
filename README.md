# Audio2Tag
this program can Cue Split and Audio Tagging

## How do work?

Cue Split    | Wav, Flac to Mp3 Conv | Complete
:----------:|:-----------------------:|:----------:
<img src="https://drive.google.com/uc?authuser=0&id=1kA-_LcyrB0Qma3YiOWkSYI_165Fk5NRQ&export=download" width="100%"> | <img src="https://drive.google.com/uc?authuser=0&id=1skptmJfpJxWADUAYZ_8fIKK0axsuk3oP&export=download" width="100%"> | <img src="https://drive.google.com/uc?authuser=0&id=1RzTA6gx-rcvY0ExbMB9t_f-Q0e-tyqqI&export=download" width="100%">

<hr/>

## How to build?
### Only Windows
```
You need Clone this repo.
1. git clone --recursive https://github.com/Piorosen/Audio2Tag
2. Solution Build.
3. ProjectSetting Run
4. Tag.WPF Run

Build.
6. this project is dotnet framework 4.7.2 base.
```

<hr />

## How do split?

   Flac |   Wav  |   Mp3
:------:|:------:|:-----:
CueTools.Flake | NAudio.Wave | NONE
NAudio.Wave    |  --- | ---

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
