using Microsoft.Win32;
using SourceStat.Core.Models;
using SourceStat.GraphicalApp.Models;
using System.Windows;
using System.Windows.Controls;


namespace SourceStat.GraphicalApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileCheckerOptions _options;
        private List<LanguageWithCheckBox> _allLanguages;
        private List<string> _allIgnoreDirectory; 
        public MainWindow()
        {
            InitializeComponent();
            _allLanguages = new List<LanguageWithCheckBox>();
            _allIgnoreDirectory = new List<string>();
            InitializeLanguages();
            _options = new FileCheckerOptions();
        }
        
        public void InitializeLanguages()
        {
            LanguagesListBox.Items.Clear();
            foreach (AvailableLanguage lang in Enum.GetValues(typeof(AvailableLanguage)))
            {
                if (lang != AvailableLanguage.None)
                {
                    _allLanguages.Add(new LanguageWithCheckBox() { Name = lang.ToString(), IsSelected = false });
                }
            }
            LanguagesListBox.ItemsSource = _allLanguages;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            dialog.Title = "Выберите папку для анализа";
            if (dialog.ShowDialog() == true)
            {
                DirectoryPathTextBox.Text = dialog.FolderName;
                StatusTextBlock.Text = $"Выбрана папка: {dialog.FolderName}";
            }
        }

        private void RemoveDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            string dir = NewDirectoryTextBox.Text.Trim();
            _allIgnoreDirectory.Remove(dir);
            IgnoredDirectoriesListBox.ItemsSource = null;
            IgnoredDirectoriesListBox.ItemsSource = _allIgnoreDirectory;
            NewDirectoryTextBox.Text = "Добавить папку...";
        }

        private void AddDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            string dir = NewDirectoryTextBox.Text.Trim();
            bool isFind = false;
            foreach (string ignoreDir in _allIgnoreDirectory)
            {
                if(ignoreDir == dir)
                {
                    isFind = true;
                    break;
                }
            }
            if (!isFind)
            {
                _allIgnoreDirectory.Add(dir);
                IgnoredDirectoriesListBox.ItemsSource = null;
                IgnoredDirectoriesListBox.ItemsSource = _allIgnoreDirectory;
                NewDirectoryTextBox.Text = "Добавить папку...";
            }
        }

        private void ClearLanguages_Click(object sender, RoutedEventArgs e)
        {
            foreach (LanguageWithCheckBox lang in _allLanguages)
            {
                if (lang.IsSelected) lang.IsSelected = false;
            }
            LanguagesListBox.ItemsSource = null;
            LanguagesListBox.ItemsSource = _allLanguages;
        }

        private void SelectAllLanguages_Click(object sender, RoutedEventArgs e)
        {
            foreach (LanguageWithCheckBox lang in _allLanguages) 
            {
                if(!lang.IsSelected) lang.IsSelected = true;
            }
            LanguagesListBox.ItemsSource = null;
            LanguagesListBox.ItemsSource = _allLanguages;
        }

        private void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LanguagesTab_Checked(object sender, RoutedEventArgs e)
        {
            if(LanguagesTabContent is not null)
            {
                LanguagesTabContent.Visibility = Visibility.Visible;
                DirectoriesTabContent.Visibility = Visibility.Collapsed;
            }
        }

        private void DirectoriesTab_Checked(object sender, RoutedEventArgs e)
        {
            if(LanguagesTabContent is not null)
            {
                LanguagesTabContent.Visibility = Visibility.Collapsed;
                DirectoriesTabContent.Visibility = Visibility.Visible;
            }
        }

        private void NewDirectoryTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NewDirectoryTextBox.Text = string.Empty;
        }

        private void NewDirectoryTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(NewDirectoryTextBox.Text == string.Empty)
            {
                NewDirectoryTextBox.Text = "Добавить папку...";
            }
        }
    }
}