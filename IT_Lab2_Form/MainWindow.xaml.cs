using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            }
        }

        private bool IsFieldsFilled()
        {
            if (KeyBox.Text.Length != 33)
            {
                MessageBox.Show("The key length must be 33 bits.", "Incorrect key", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }

            if (InputBitsBox.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = "D:\\Test";
            if (fileDialog.ShowDialog() == true)
            {
                byte[] fileText = System.IO.File.ReadAllBytes(fileDialog.FileName);
                InputBitsBox.Text = new BitArray(fileText).ToBitString();
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = "D:\\Test";
            if (fileDialog.ShowDialog() == true)
            {
                string name = fileDialog.FileName;
                System.IO.File.WriteAllBytes(name, CipherBitsBox.Text.StringToBitArray().ToByteArray());
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
    }
}