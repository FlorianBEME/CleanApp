﻿using System.Windows;
using System.Diagnostics;
using System.IO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanAppFlo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DirectoryInfo winTemp;
        public DirectoryInfo appTemp;
        public bool isAnalyse;
        public MainWindow()
        {
            InitializeComponent();
            winTemp = new DirectoryInfo(@"C:\Windows\Temp");
            appTemp = new DirectoryInfo(Path.GetTempPath());
            isAnalyse = false;

        }

        // Fonction qui calcule la taille d'un dossier
        public long DirSize(DirectoryInfo dir)
        {
            return dir.GetFiles().Sum(file => file.Length) + dir.GetDirectories().Sum(directorie => DirSize(directorie));
        }

        // Fonction qui supprime le contenue d'un dossier

        public void ClearTempData(DirectoryInfo dir)
        {
            foreach (FileInfo file in dir.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    continue;
                }
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                try
                {
                    di.Delete();
                    Console.WriteLine(di.FullName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    continue;
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
            AnalyseFolders();
            isAnalyse = true;
        }

        public void AnalyseFolders()
        {
            Console.WriteLine("Début de l'analyse");
            long totalSize = 0;

            try
            {
                totalSize += DirSize(winTemp) / 1000000;
                totalSize += DirSize(appTemp) / 1000000;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
  
            }

            espace.Content = totalSize + " Mb";
            date.Content = DateTime.Today.ToString("dd/MM/yyyy");
            if (totalSize > 0)
            {
                labelCleanButton.Content = "NETTOYER";
            }
        }
        async System.Threading.Tasks.Task WaitMethod(int delay)
        {
            await System.Threading.Tasks.Task.Delay(delay);
        }

       async private void Button_Click_Clean(object sender, RoutedEventArgs e)
        {


            if (isAnalyse)
            {
                Console.WriteLine("Nettoyage en cours...");
                labelCleanButton.Content = "Nettoyage en cours...";
                await WaitMethod(2000);

                try
                {
                    ClearTempData(winTemp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                try
                {
                    ClearTempData(appTemp);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }


                /*AnalyseFolders();*/

                espace.Content = "0 Mb";
                labelCleanButton.Content = "NETTOYAGE TERMINER";
            }
            else
            {
                MessageBox.Show("Veuillez procéder à l'analyse...", "Attention", MessageBoxButton.OK ,MessageBoxImage.Information);
            }
        }
    }
}
