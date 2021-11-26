using System.Windows;
using System.Diagnostics;
using System.IO;
using System;
using System.Linq;

namespace CleanAppFlo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DirectoryInfo winTemp;
        public DirectoryInfo appTemp;
        public MainWindow()
        {
            InitializeComponent();
            winTemp = new DirectoryInfo(@"C:\Windows\Temp");
            appTemp = new DirectoryInfo(System.IO.Path.GetTempPath());

        }

        // Fonction qui calcule la taille d'un dossier
        public long DirSize(DirectoryInfo dir)
        {
            return dir.GetFiles().Sum(file => file.Length) + dir.GetDirectories().Sum(directorie => DirSize(directorie));
        }

        // Fonction qui supprime le contenue d'un dossier

        public void DeleteDir(DirectoryInfo dir)
        {
            foreach(FileInfo file in dir.GetFiles())
            {
                try
                {

                }
                catch (Exception ex)
                {
                   
                }
            }
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logiciel à jour", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_histo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("TODO: creer page historique", "Historique", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Web_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("https://www.florianbeme.com")
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
            }
           
        }

        private void Button_Analyser_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
