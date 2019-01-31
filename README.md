# Audio2Tag
this program can Cue Split and Audio Tagging

## How do work?

Here you can see the test and auto-run one by one.
<hr/>

## How to build?


### Windows & Debian Linux:
```
You need Clone this repo.
1. git clone https://github.com/Piorosen/Audio2Tag

Move Clone folder.
2. cd Audio2Tag

Dependency project clone.
3. git clone https://github.com/Piorosen/atldotnet.git

connect solution file.
4. load atldotnet csproj.

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
