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

        private void MetroWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {
                if (Setting.Global.DialogCheck == true)
                {
                    DialogHost.CloseDialogCommand.Execute(false, null);
                }
            }
        }
    }
}
