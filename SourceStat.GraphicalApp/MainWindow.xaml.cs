using Microsoft.Win32;
using SourceStat.Core.Models;
using SourceStat.GraphicalApp.Models;
using System.Diagnostics;
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
        private bool _isAdded = false;
        private bool _isAnalyzing = false;
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
            _options.RemoveIgnoreDirectories(dir);  
            _allIgnoreDirectory.Remove(dir);
            IgnoredDirectoriesListBox.ItemsSource = null;
            IgnoredDirectoriesListBox.ItemsSource = _allIgnoreDirectory;
            NewDirectoryTextBox.Text = "Добавить папку...";
        }

        private void AddDirectoryButton_Click(object sender, RoutedEventArgs e)
        {
            string dir = NewDirectoryTextBox.Text.Trim();
            bool isFind = false;
            if (dir == "Добавить папку...") return;
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
                _options.AddIgnoreDirectories(dir);
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
            _options.SelectLanguages.Clear();
        }

        private void SelectAllLanguages_Click(object sender, RoutedEventArgs e)
        {
            foreach (LanguageWithCheckBox lang in _allLanguages) 
            {
                if(!lang.IsSelected)
                {
                    lang.IsSelected = true;
                }
            }
            LanguagesListBox.ItemsSource = null;
            LanguagesListBox.ItemsSource = _allLanguages;
        }

        private void DefaultDirectories_Click(object sender, RoutedEventArgs e)
        {
            if(_isAdded)
            {
                _isAdded = false;
                DefaultDirButton.Content = "Добавить стандартные";
                foreach (string defDir in _options.DefaultIgnore)
                {
                    _options.RemoveIgnoreDirectories(defDir);
                    _allIgnoreDirectory.Remove(defDir);
                }
                IgnoredDirectoriesListBox.ItemsSource = null;
                IgnoredDirectoriesListBox.ItemsSource = _allIgnoreDirectory;
            }
            else
            {
                _isAdded = true;
                DefaultDirButton.Content = "Удалить стандартные";
                bool isFind;
                foreach(string defDir in _options.DefaultIgnore)
                {
                    isFind = false;
                    foreach (string ignoreDir in _allIgnoreDirectory)
                    {
                        if (ignoreDir == defDir)
                        {
                            isFind = true;
                            break;
                        }
                    }
                    if (!isFind)
                    {
                        _options.AddIgnoreDirectories(defDir);
                        _allIgnoreDirectory.Add(defDir);
                    }
                }
                IgnoredDirectoriesListBox.ItemsSource = null;
                IgnoredDirectoriesListBox.ItemsSource = _allIgnoreDirectory;
            }
        }

        private async void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isAnalyzing) return;
            try
            {
                foreach (LanguageWithCheckBox lang in _allLanguages)
                {
                    if (lang.IsSelected)
                    {
                        _options.AddLanguage(Enum.Parse<AvailableLanguage>(lang.Name));
                    }
                }
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
                Stopwatch stopwatch = new Stopwatch();
                await SetAnalyzingState(true);
                stopwatch.Start();
                var languages = await Task.Run(() =>
                {
                    var result = new List<LanguageStat>();
                    foreach (AvailableLanguage lang in _options.SelectLanguages)
                    {
                        _options.SetCurrentLanguage(lang);
                        long countFile = FileChecker.GetCountFiles(folderPath, _options);
                        long countLine = FileChecker.GetCountLineInFiles(folderPath, _options);
                        result.Add(new LanguageStat
                        {
                            Name = lang.ToString(),
                            FilesCount = countFile,
                            LinesCount = countLine,
                            Color = "#9B4F96"
                        });
                    }
                    return result;
                });
                stopwatch.Stop();
                TotalFilesText.Text = languages.Sum(l => l.FilesCount).ToString();
                TotalLinesText.Text = languages.Sum(l => l.LinesCount).ToString();
                LanguagesCountText.Text = languages.Count.ToString();
                ElapsedTimeText.Text = $"{stopwatch.Elapsed.Seconds} с";
                UpdateLanguageBars(languages);
                await SetAnalyzingState(false);
                StatusTextBlock.Text = "Анализ завершен";
                _options.SelectLanguages.Clear();
            }
            catch
            {
                StatusTextBlock.Text = "Ошибка при анализе. Попробуйте еще раз";
                _options.SelectLanguages.Clear();
                await SetAnalyzingState(false);
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
                nameBlock.FontSize = 17;
                nameBlock.FontWeight = FontWeights.SemiBold;
                nameBlock.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetColumn(nameBlock, 0);
                grid.Children.Add(nameBlock);
                TextBlock statsBlock = new TextBlock();
                statsBlock.Text = $"{lang.FilesCount} файлов, {lang.LinesCount} строк";
                statsBlock.Foreground = FindResource("TextSecondaryBrush") as SolidColorBrush;
                statsBlock.FontSize = 17;
                statsBlock.VerticalAlignment = VerticalAlignment.Center;
                statsBlock.HorizontalAlignment = HorizontalAlignment.Right;
                Grid.SetColumn(statsBlock, 1);
                grid.Children.Add(statsBlock);
                langContainer.Child = grid;
                LanguageBarsPanel.Children.Add(langContainer);
            }
        }

        private async Task SetAnalyzingState(bool isAnalyzing)
        {
            _isAnalyzing = isAnalyzing;

            await Dispatcher.InvokeAsync(() =>
            {
                AnalyzeButton.IsEnabled = !isAnalyzing;
                BrowseButton.IsEnabled = !isAnalyzing;
                if (AnalyzeButton.Template.FindName("contentPresenter", AnalyzeButton) is ContentPresenter contentPresenter &&
                    AnalyzeButton.Template.FindName("spinner", AnalyzeButton) is Border spinner)
                {
                    contentPresenter.Visibility = isAnalyzing ? Visibility.Collapsed : Visibility.Visible;
                    spinner.Visibility = isAnalyzing ? Visibility.Visible : Visibility.Collapsed;
                }
                if (isAnalyzing)
                {
                    StatusTextBlock.Text = "Анализ... Пожалуйста, подождите";
                    StatusIndicator.Fill = FindResource("AccentOrangeBrush") as SolidColorBrush;
                }
                else
                {
                    StatusTextBlock.Text = "Готов к анализу";
                    StatusIndicator.Fill = FindResource("AccentGreenBrush") as SolidColorBrush;
                }
            });
        }
    }
}