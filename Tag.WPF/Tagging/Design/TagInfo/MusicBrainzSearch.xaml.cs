﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tag.Core.Cue;
using Tag.Core.Tagging;

namespace Tag.WPF
{
    /// <summary>
    /// MusicBrainzSearch.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MusicBrainzSearch : UserControl
    {
        MusicBrainzSearchViewModel viewModel;

        public MusicBrainzSearch(ObservableCollection<TaggingModel> user)
        {
            InitializeComponent();
            DataContext = viewModel = new MusicBrainzSearchViewModel(user);
            
        }

        public void Search(TagInfo info)
        {
            viewModel.SearchAlbum(info, this);
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (sender is ListView) ? sender as ListView : null;
            if (listView != null && listView?.SelectedIndex != -1)
            {
                viewModel.SelectItem(listView.SelectedIndex, this);
            }
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            int itemindex = ListViews.SelectedIndex != -1 ? ListViews.SelectedIndex : 0;
            viewModel.Yes_Click(itemindex);
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(false, null);
            
        }
    }
}
