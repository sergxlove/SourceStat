using Microsoft.Win32;
using SourceStat.Core.Models;
using SourceStat.GraphicalApp.Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


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
            try
            {
                string folderPath = DirectoryPathTextBox.Text;
                if (string.IsNullOrWhiteSpace(folderPath))
                {
                    StatusTextBlock.Text = "Ошибка при анализе. Необходимо выбрать директорию для анализа";
                    return;
                }
                if (!Directory.Exists(folderPath))
                {
                    StatusTextBlock.Text = "Ошибка при анализе. Указанной директории не существует";
                    return;
                }
                StatusTextBlock.Text = "Анализ...";

                var languages = new List<LanguageStat>();

                languages.Add(new LanguageStat
                {
                    Name = "C#",
                    FilesCount = 42,
                    LinesCount = 3520,
                    Percentage = 45.5,
                    Color = "#9B4F96"
                });

                languages.Add(new LanguageStat
                {
                    Name = "JavaScript",
                    FilesCount = 28,
                    LinesCount = 2150,
                    Percentage = 28.3,
                    Color = "#F1E05A"
                });

                languages.Add(new LanguageStat
                {
                    Name = "HTML",
                    FilesCount = 15,
                    LinesCount = 980,
                    Percentage = 15.2,
                    Color = "#E34C26"
                });

                languages.Add(new LanguageStat
                {
                    Name = "CSS",
                    FilesCount = 12,
                    LinesCount = 850,
                    Percentage = 11.0,
                    Color = "#563D7C"
                });

                TotalFilesText.Text = languages.Sum(l => l.FilesCount).ToString();
                TotalLinesText.Text = languages.Sum(l => l.LinesCount).ToString();
                LanguagesCountText.Text = languages.Count.ToString();
                ElapsedTimeText.Text = "0.5 с";
                UpdateLanguageBars(languages);
                StatusTextBlock.Text = "Анализ завершен";
            }
            catch
            {
                StatusTextBlock.Text = "Ошибка при анализе. Попробуйте еще раз";
            }
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

        private void UpdateLanguageBars(List<LanguageStat> languages)
        {
            LanguageBarsPanel.Children.Clear();
            NoDataMessage.Visibility = Visibility.Collapsed;
            foreach (var lang in languages)
            {
                Border langContainer = new Border();
                langContainer.Background = FindResource("BgLightBrush") as SolidColorBrush;
                langContainer.CornerRadius = new CornerRadius(6);
                langContainer.Margin = new Thickness(0, 0, 0, 10);
                langContainer.Padding = new Thickness(15);
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); 
                TextBlock nameBlock = new TextBlock();
                nameBlock.Text = lang.Name;
                nameBlock.Foreground = FindResource("TextPrimaryBrush") as SolidColorBrush;
                nameBlock.FontSize = 14;
                nameBlock.FontWeight = FontWeights.SemiBold;
                nameBlock.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(nameBlock, 0);
                grid.Children.Add(nameBlock);
                TextBlock statsBlock = new TextBlock();
                statsBlock.Text = $"{lang.FilesCount} файлов, {lang.LinesCount} строк";
                statsBlock.Foreground = FindResource("TextSecondaryBrush") as SolidColorBrush;
                statsBlock.FontSize = 12;
                statsBlock.VerticalAlignment = VerticalAlignment.Center;
                statsBlock.HorizontalAlignment = HorizontalAlignment.Right;
                Grid.SetColumn(statsBlock, 1);
                grid.Children.Add(statsBlock);
                langContainer.Child = grid;
                LanguageBarsPanel.Children.Add(langContainer);
            }
        }
    }
}