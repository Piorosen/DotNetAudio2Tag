using MaterialDesignThemes.Wpf;

namespace Tag.WPF
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow
    {
        MainWindowsViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            
            new PaletteHelper().ReplacePrimaryColor("grey");
            
            DataContext = viewModel = new MainWindowsViewModel();

            global::Tag.Core.Conv.AudioConverter conv = new Core.Conv.AudioConverter();
            conv.AddFile(new Core.Conv.ConvInfo
            {
                FilePath = @"C:\Users\aoika\Desktop\임시\data\Coe\1. 1. MASAYUME CHASING.flac",
                ResultPath = @"C:\Users\aoika\Desktop\임시\data\Coe\test.mp3",
                Type = ATL.CatalogDataReaders.AudioType.FLAC

            });
            foreach (var i in conv.Execute())
            {

            }
        }

    }
}
