using System;
using System.Collections.Generic;
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
using LibraryCountries;

namespace PW9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {/// <summary>
    /// Практическая работа №9. Лопаткин Сергей ИСП-31
    /// 8. Заполнить таблицу со списком 5 стран мира с полями: страна, народонаселение,                
    /// столица, денежная единица.Вывести результат на экран.Вывести общее население всех стран
    /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Table.ItemsSource = VisualTable.CreateTable().DefaultView;
        }
        private Country[] countries = new Country[0];                //Создаем пустой массив структуры Country
        public void MessageForUser()
        {
            MessageBox.Show("Некорректно введены значения, прочитайте справку для понимания особенностей программы!",
                "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AddCountry_Click(object sender, RoutedEventArgs e)
        {            
            if ((CountryName.Text != "") && (People.Text != "") && (MainTown.Text != "") && (Money.Text != ""))
            {
                bool ProvePeople = long.TryParse(People.Text, out long people);
                if (ProvePeople)
                {
                    Country newcountry = new Country();//Задаем новый объект структуры Country
                    newcountry.CreateCountryInfo(CountryName.Text, people, MainTown.Text, Money.Text);//Ввод данных посредством обращения к методу
                    countries += newcountry;//Выполнение действий при помощи бинарного оператора
                    Table.ItemsSource = VisualTable.AddCountry(countries[countries.Length - 1]).DefaultView;//Занесение данных нового объекта в таблицу
                    Calculate.IsEnabled = true;
                    AllPeople.Clear();
                }
                else MessageForUser();
            }
            else MessageForUser();
        }

        private void DeleteCountry_Click(object sender, RoutedEventArgs e)
        {
            if (CountryName.Text != "")
            {
                AllPeople.Clear();
                Table.ItemsSource = VisualTable.DeleteCountry(CountryName.Text).DefaultView;
                Country name = new Country(CountryName.Text);//Создание нового объекта для поиска имени(что требуется для выполнения) бинарным оператором
                countries -= name;
                if (countries.Length == 0) Calculate.IsEnabled = false;
            }
            else MessageForUser();            
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {                        
            AllPeople.Text = Country.Counter(in countries).ToString();//Обращение к статическому методу структуры с данными объекта для подсчета населения
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Практическая работа №9. Лопаткин Сергей ИСП-31" +
                "8. Заполнить таблицу со списком 5 стран мира с полями: страна, народонаселение," +
                "столица, денежная единица.Вывести результат на экран.Вывести общее население всех стран", 
                "О программе",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Support_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1) Нельзя вводить больше двенадцатизначного числа в строку народонаселения\n" +
                "2) Допустимо вводить наименование страны, столицы, денежной единицы размером в 19 символов(включая пробел)\n" +
                "3) Произвести подсчет населения можно только при наличии " +
                "хотя бы одной страны\n" +
                "4) Удаление страны производится по наименовании", 
                "Справка", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CountryName_GotFocus(object sender, RoutedEventArgs e)
        {
            AddCountry.IsDefault = true;
        }
    }
}
