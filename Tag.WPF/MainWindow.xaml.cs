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
        }
        
    }
}
