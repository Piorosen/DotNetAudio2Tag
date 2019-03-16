﻿using MaterialDesignThemes.Wpf;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Tag.Core.Cue;
using Tag.Core.Tagging;
using Tag.Setting;
using Tag.WPF.Properties;
using TagLib;
using ToastNotifications.Messages;

namespace Tag.WPF
{
    public class TaggingViewModel : INotifyPropertyChanged
    {
        AudioTagging audioTagging;
        private TaggingModel _selectItem;

        public ObservableCollection<TaggingModel> Items { get => _items; set { _items = value; OnPropertyChanged(); } }
        public TaggingModel SelectItem { get => _selectItem; set { _selectItem = value; OnPropertyChanged(); } }

        public Visibility LabelVisibility { get => _LabelVisibility; set { _LabelVisibility = value; OnPropertyChanged(); } }
        private Visibility _LabelVisibility = Visibility.Visible;

        public bool ButtonEnable { get => _buttonEnable; set { _buttonEnable = value; OnPropertyChanged(); } }

        public bool RemoveFile(int index)
        {
            if (0 <= index && index < Items.Count)
            {
                Items.RemoveAt(index);
                if (Items.Count == 0)
                {
                    LabelVisibility = Visibility.Visible;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ClearFile()
        {
            Items.Clear();
            LabelVisibility = Visibility.Visible;
            

        }

        private void DialogIdentifier_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Global.DialogIdentifier.TaggingEnable))
            {
                ButtonEnable = Global.DialogIdentifier.TaggingEnable;
            }
        }

        public void AddModel(string filePath)
        {
            audioTagging.AddFile(filePath);
            using (AudioFileReader reader = new AudioFileReader(filePath))
            {
                Items.Add(new TaggingModel
                {
                    TagInfo = audioTagging.List()[audioTagging.List().Count - 1],
                    WaveFormat = new WaveFormatModel
                    {
                        Bitrate = reader.WaveFormat.SampleRate,
                        Channel = reader.WaveFormat.Channels,
                        Length = reader.TotalTime.TotalSeconds
                    }
                });
            }
            LabelVisibility = Visibility.Hidden;
        }
        public void ChangeText(string Name, string Value)
        {
            if (SelectItem == null)
            {
                return;
            }

            foreach (var p in SelectItem.TagInfo.GetType().GetProperties())
            {
                if (p.Name == Name)
                {
                    var check = p.GetValue(SelectItem.TagInfo);
                    if (check == null)
                    {
                        return;
                    }

                    if (check is List<string>)
                    {
                        p.SetValue(SelectItem.TagInfo, Value.Split(';').ToList());
                    }
                    else if (check is List<uint>)
                    {
                        try
                        {
                            p.SetValue(SelectItem.TagInfo, Value.Split(',')
                                .ToList()
                                .Select((s) => uint.Parse(s))
                                .ToList());
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        p.SetValue(SelectItem.TagInfo, Value);
                    }

                }
            }
        }

        public bool Select = false;
        private ObservableCollection<TaggingModel> _items;
        private bool _buttonEnable = true;

        public void SelectModel(int index)
        {
            Select = true;
            SelectItem = Items[index];
            Select = false;
        }



        public void SaveOne()
        {

        }

        private void CloseEvent(object sender, DialogClosingEventArgs e)
        {
            audioTagging.tagList.Clear();
            if (e.Parameter == null)
            {
                e.Cancel();
            }

            if ((bool)e.Parameter == true)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    audioTagging.AddFile(Items[i].TagInfo.Path);
                    audioTagging.List()[i] = Items[i].TagInfo;

                }
                foreach (var value in audioTagging.Execute())
                {
                    Console.WriteLine(value);
                }
            }
        }

        public async void AllTagSave()
        {
            var view = new TagAllSave
            {
                Width = 300,
                Height = 100
            };

            Global.DialogIdentifier.TaggingEnable = false;
            // DialogHost.OpenDialogCommand.Execute(view, new System.Windows.Controls.Button().CommandTarget);
            var result = await DialogHost.Show(view, Global.DialogIdentifier.TagSave, CloseEvent);
            if ((result as bool?).HasValue && (result as bool?).Value)
            {
                Application.notifier.ShowInformation(Global.Language.TagSuccess);
            }
            else
            {
                Application.notifier.ShowInformation(Global.Language.TagFail);
            }
        }

        public async void GetTagInfo(int index)
        {
            var user = new List<TrackInfo>();

            foreach (var item in Items)
            {
                user.Add(new TrackInfo
                {
                    Album = item.TagInfo.Album,
                    Artist = string.Join("; ", item.TagInfo.Artist),
                    Composer = string.Join("; ", item.TagInfo.Composer),
                    DurationMS = item.WaveFormat.Length,
                    Title = item.TagInfo.Title,
                    Track = item.TagInfo.Track.Count != 0 ? (int)item.TagInfo.Track[0] : 0,

                });
            }


            var view = new GetTagInfo(Items[index == -1 ? 0 : index].TagInfo, Items)
            {
                Width = 200,
                Height = 100

            };

            Global.DialogIdentifier.TaggingEnable = false;
            var result = await DialogHost.Show(view, Global.DialogIdentifier.GetTagInfo);
        }

        public void RemoveModel(int index)
        {
            Items.RemoveAt(index);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string Name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }


        public TaggingViewModel()
        {
            Items = new ObservableCollection<TaggingModel>();

            audioTagging = new AudioTagging();
            SelectItem = new TaggingModel();
            Global.DialogIdentifier.PropertyChanged += DialogIdentifier_PropertyChanged;
        }
    }
}
