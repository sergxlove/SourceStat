using SourceStat.Core.Models;
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

namespace SourceStat.GraphicalApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeLanguages();
        }
        
        public void InitializeLanguages()
        {
            LanguagesListBox.Items.Clear();

            foreach (AvailableLanguage lang in Enum.GetValues(typeof(AvailableLanguage)))
            {
                if (lang != AvailableLanguage.None)
                {
                    LanguagesListBox.Items.Add(lang);
                }
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveDirectoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddDirectoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearLanguages_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SelectAllLanguages_Click(object sender, RoutedEventArgs e)
        {

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

        private void LanguageSearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LanguageSearchTextBox.Text = string.Empty;
        }

        private void LanguageSearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            LanguageSearchTextBox.Text = "Поиск языков...";
        }

        private void NewDirectoryTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NewDirectoryTextBox.Text = string.Empty;
        }

        private void NewDirectoryTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NewDirectoryTextBox.Text = "Добавить папку...";
        }
    }
}