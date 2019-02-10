using ATL.CatalogDataReaders;
using NAudio.Wave;
using System;
using System.Collections.Generic;

namespace Tag.Core.Cue
{
    public class TrackInfo
    {
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Composer { get; set; } = string.Empty;

        public string Album { get; set; } = string.Empty;
        public double StartPosition { get; set; } = 0.0;
        public double DurationMS { get; set; } = 0.0;

        public double TimeOffSet { get; set; } = 0;
        public int Track { get; set; } = 0;
    }
    public class REM
    {

        public string Genre { get; set; } = String.Empty;
        public string Date { get; set; } = string.Empty;
        public string DiscId { get; set; } = string.Empty;
    
    }

    public class CueInfo
    {
        public string Path { get; set; } = string.Empty;
        public string SavePath { get; set; } = string.Empty;
        public string WavPath { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = String.Empty;
        public string Barcode { get; set; } = String.Empty;

        public AudioType AudioType { get; set; } = AudioType.NONE;
        public WaveFormat WaveFormat { get; set; } = new WaveFormat();
        public REM REM { get; set; } = new REM();
        public List<TrackInfo> Track { get; set; } = new List<TrackInfo>();
    }
}
