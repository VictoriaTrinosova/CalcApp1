using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace CalcApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Обработчик событий для всех кнопок на гриде
            InitializeComponent();
            foreach (UIElement el in MainRoot.Children)
            {
                if (el is Button)
                {
                    ((Button)el).Click += Button_Click;
                }
            }
            // Добавление выбора темы в ComboBox
            List<string> styles = new List<string>() { "Синяя тема", "Красная тема" };
            styleBox.ItemsSource = styles;
            styleBox.SelectionChanged += ThemeChange;
            styleBox.SelectedIndex = 0;
        }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                string str = (string)((Button)e.OriginalSource).Content; //Создание строковой переменной для вывода названия кнопки в textBlock
            if (str == "AC") //Реализация кнопки "АС"
                    textBlock.Text = ""; 
                else if (str == "=") //Реализация кнопки "="
            {
                    string value = new DataTable().Compute(textBlock.Text, null).ToString(); // Использование метода Compute для высчета математической операции, которая передается в виде строки
                textBlock.Text = value;
                }
                else
                    textBlock.Text += str; //Вывод текста кнопки в textBlock
        }
        
        private void ThemeChange(object sender, SelectionChangedEventArgs e) //Событие ThemeChange, срабатывающее при смене выбора
        {
            int styleIndex = styleBox.SelectedIndex;
            Uri uri = new Uri("Blue.xaml", UriKind.Relative); //По умолчанию синяя тема
            if (styleIndex == 1) //Определение красной темы
            {
                uri = new Uri("Red.xaml", UriKind.Relative);
            }
            ResourceDictionary resource = Application.LoadComponent(uri) as ResourceDictionary; //Получение ресурса
            Application.Current.Resources.Clear(); //Отчистка ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resource); //Добавление словаря
        }
    }
}
