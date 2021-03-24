using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IT_Lab2_Encryptor;
using Microsoft.Win32;

namespace IT_Lab2_Form
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyBox.MaxLength = Encryptor.ToXor[0];
        }

        private void StartButt_Click(object sender, RoutedEventArgs e)
        {
            if (IsFieldsFilled())
            {
                Encryptor encryptor = new Encryptor();
                BitArray key = KeyBox.Text.StringToBitArray();
                BitArray toEncrypt = InputBitsBox.Text.StringToBitArray();
                BitArray result = encryptor.Encrypt(key, toEncrypt);
                GenKeyBitsBox.Text = encryptor.GeneratedKey.ToBitString();
                CipherBitsBox.Text = result.ToBitString();
                Console.WriteLine("Completed!");
            }
        }

        private bool IsFieldsFilled()
        {
            if (InputBitsBox.Text.Length == 0 && KeyBox.Text.Length == 0)
            {
                return false;
            }

            if (KeyBox.Text.Length != Encryptor.ToXor[0])
            {
                string msg = "The key length must be " + Encryptor.ToXor[0] + " bits.";
                MessageBox.Show(msg, "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }

            if (InputBitsBox.Text.Length == 0)
            {
                MessageBox.Show("Input field is empty.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            ClearFields();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "D:\\Test";
            if (fileDialog.ShowDialog() == true)
            {
                byte[] fileText = System.IO.File.ReadAllBytes(fileDialog.FileName);
                Array.Reverse(fileText);
                InputBitsBox.Text = new BitArray(fileText).ToBitString();   
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            if (CipherBitsBox.Text.Length == 0)
            {
                MessageBox.Show("Nothing to save.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = "D:\\Test";
            if (fileDialog.ShowDialog() == true)
            {
                string name = fileDialog.FileName;
                byte[] result = CipherBitsBox.Text.StringToBitArray().ToByteArray();
                Array.Reverse(result);
                System.IO.File.WriteAllBytes(name, result);
            }
        }

        private void KeyBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                (
                    textBox
                        .Text
                        .Where
                        (ch =>
                            ch == '1' || ch == '0'
                        )
                        .ToArray()
                );
                textBox.SelectionStart = e.Changes.First().Offset + 1;
                textBox.SelectionLength = 0;
            }
        }


        private void ClearFields()
        {
            InputBitsBox.Clear();
            GenKeyBitsBox.Clear();
            CipherBitsBox.Clear();
        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ClearButtPressed(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
    }
}