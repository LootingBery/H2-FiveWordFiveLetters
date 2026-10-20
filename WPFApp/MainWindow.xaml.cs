using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Net;
using System.IO;
using _5W5L;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<string> Text { get; } = new();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                NotifyPropertyChanged(nameof(filePath));
            }
        }

        private string clock;
        public string Clock
        {
            get { return clock; }
            set
            {
                clock = value;
                NotifyPropertyChanged(nameof(Clock));
            }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                NotifyPropertyChanged(nameof(Count));
            }
        }


        private int _percent = 0;
        public int Percent
        {
            get { return _percent; }
            set { _percent = value; NotifyPropertyChanged("Percent"); }
        }

        private async void Run_button_Click(object sender, RoutedEventArgs e)
        {
            IProgress<Report.how_long> progress = new Progress<Report.how_long>((Report.how_long progress) =>
            {
                progressBar.Value = progress.How_far / progress.total;
            });
            var Watch = Stopwatch.StartNew();

            List<string> Text = new List<string>();
            await Task.Run(() =>
            {
                Main.bits(filePath, Text, progress);
            });
            for (int i = 0; i < Text.Count(); i++)
            {
                this.Text.Add(Text[i]);
            }
            Watch.Stop();
            Percent = 100;
            Clock = Watch.Elapsed.ToString();
            Count = this.Text.Count();
        }
        private void Fil_button_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == true)
            {
                FilePath = ofd.FileName;
            }
        }
        private void Download_button_Click(object sender, RoutedEventArgs e)
        {
            if (Text.Count == 0) return;
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Result";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                File.WriteAllLines(dialog.FileName, Text);
            }
        }
    }
}
