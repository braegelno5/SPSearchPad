namespace MunirHusseini.SPSearchPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            DataContext = new QuerySession();
            InitializeComponent();
        }
    }
}
