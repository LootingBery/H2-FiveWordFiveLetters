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
using System.Collections.ObjectModel;
using SolutionFinderLibary;
using System.Diagnostics;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using System.IO;

namespace SecondAttemptWPFapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            WordsFound = "0";
            Stopwatch = "00:00:00.0000000";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private void FindSolutions_Click(object sender, RoutedEventArgs e)
        {
            SolutionFinder finder = new SolutionFinder(5, 5);
            finder.ProgressBar += Finder_WordFound;
            finder.FoundWords += Finder_Solutions;
            finder.SolutionsFound += Finder_SolutionsFound;
            //SelectFile.IsEnabled = false;
            _solutions = new List<string>(); // TODO: Pak _solutions ind i en liste FØR den ankommer her
            NotifyPropertyChanged("Solutions");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            finder.LoadWords(FileTextBox.Text);
            string[] FoundSolutions = finder.FindSolutions();
            watch.Stop();
            Stopwatch = watch.Elapsed.ToString();
            //SelectFile.IsEnabled = true;
            DownloadResults.IsEnabled = true;
        }
        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            var fileSelect = new OpenFileDialog();
            fileSelect.DefaultExt = ".txt";
            fileSelect.Filter = "Text documents (.txt)|*.txt";
            var results = fileSelect.ShowDialog();
            if (results == true)
            {
                FileTextBox.Text = fileSelect.FileName;
                Start.IsEnabled = true;
            }
        }
        private void DownloadResults_Click(object sender, RoutedEventArgs e)
        {
            var saveResults = new SaveFileDialog();
            saveResults.FileName = "Results";
            saveResults.DefaultExt = ".txt";
            saveResults.Filter = "Text documents (.txt)|*.txt";

            if (saveResults.ShowDialog() == true)
                File.WriteAllLines(saveResults.FileName, Solutions);
        }

        private string _stopWatch;
        public string Stopwatch
        {
            get { return _stopWatch; }
            set { _stopWatch = value; NotifyPropertyChanged(nameof(Stopwatch));}
        }
        private int _solutionsFound;
        public int SolutionsFound
        {
            get { return _solutionsFound; }
            set { _solutionsFound = value; NotifyPropertyChanged("SolutionsFound"); }
        }
        private int _wordsFound;

        public string WordsFound
        {
            get { return _wordsFound.ToString(); }
            set { _wordsFound = int.Parse(value); NotifyPropertyChanged("WordsFound"); }
        }

        private List<string> _solutions = new List<string>();

        public List<string> Solutions
        {
            get { return _solutions; }
            set { _solutions = value; NotifyPropertyChanged("Solutions"); }
        }

        private void Finder_SolutionsFound(object? sender, int e)
        {
            _solutionsFound = e;
            NotifyPropertyChanged("SolutionsFound");
        }

        private void Finder_Solutions(object? sender, string e)
        {
            _solutions.Add(e);
            NotifyPropertyChanged("Solutions");
        }

        private void Finder_WordFound(object? sender, int e)
        {
            _wordsFound = e;
            NotifyPropertyChanged("WordsFound");
        }

    }
}