using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryCountries
{/// <summary>
/// Структура, предназначенная для создания объекта (или массива объектов), который хранит в себе данные о стране:
///Наименование;
///Народонаселение;
///Столица;
///Денежная единица;
/// </summary>
    public struct Country
    {
        long _people;        
        public string Name { get; set; }
        public long People { get => _people; set => _people = ProveValue(value) ? value : throw new Exception("Требуется число больше или равное 0!"); }
        private bool ProveValue(long value)
        {
            return value >= 0;   
        }
        public string MainTown { get; set; }
        public string Money { get; set; }
        public Country(string name)
        {
            Name = name;
            _people = 0;
            MainTown = "";
            Money = "";
        }        /// <summary>
        /// Занесение данных в объект по умолчанию со значениями страны "Россия"
        /// </summary>
        public void CreateCountryInfo()
        {
            Name = "Россия";
            People = 145975300;
            MainTown = "Москва";
            Money = "Рубль";
        }
        /// <summary>
        /// Занесение данных в объект по умолчанию со значениями пользователя
        /// </summary>
        /// <param name="name"></param>
        /// <param name="people"></param>
        /// <param name="maintown"></param>
        /// <param name="money"></param>
        public void CreateCountryInfo(string name, long people, string maintown, string money)
        {
            Name = name;
            People = people;
            MainTown = maintown;
            Money = money;
        }/// <summary>
        /// Подсчет общего количества населения во всех странах
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        public static long Counter(in Country[] countries)
        {
            try
            {
                if (ProveArrayCountry(countries))
                {
                    long counter = 0;
                    for (int i = 0; i < countries.Length; i++)
                    {
                        counter += countries[i].People;
                    }
                    return counter;
                }
                else throw new Exception();
            }
            catch
            {
                MessageBox.Show("Отсутствуют значения для подсчета!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return -1;
        }/// <summary>
        /// Оператор для добавления нового объекта в массив структуры(увеличение размера массива на 1 и добавление объекта)
        /// </summary>
        /// <param name="countries"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public static Country[] operator +(in Country[] countries, Country country)
        {
            try
            {
                if (FindName(country.Name, in countries) == -1)
                {
                    Country[] newcountries = new Country[countries.Length + 1];
                    for (int i = 0; i < countries.Length; i++)
                    {
                        newcountries[i] = countries[i];
                    }
                    newcountries[newcountries.Length - 1] = country;
                    return newcountries;
                }
                else throw new Exception();
            }
            catch
            {
                MessageBox.Show("Такое наименование имеется, введите другое!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return countries;
        }
        /// <summary>
        /// Оператор для удаления объекта из массива(создание нового с размером -1 и недобавление по имени)
        /// </summary>
        /// <param name="countries"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public static Country[] operator -(in Country[] countries, Country country)
        {
            try
            {
                int name = FindName(country.Name, in countries);
                if (name != -1)
                {
                    int j = -1;
                    Country[] newcountries = new Country[countries.Length - 1];
                    for (int i = 0; i < newcountries.Length; i++)
                    {
                        j++;
                        if (countries[i].Name == country.Name)
                        {
                            j++;
                            continue;
                        }
                        newcountries[i] = countries[j];
                    }
                    return newcountries;
                }
                else throw new Exception();                
            }
            catch
            {
                MessageBox.Show("Нет такого наименования, введите другое!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return countries;
        }
        /// <summary>
        /// Нахождение имени и возвращение индекса элемента в массиве
        /// </summary>
        /// <param name="name">Имя объекта, которое ищем</param>
        /// <param name="countries"></param>
        /// <returns></returns>
        private static int FindName(string name, in Country[] countries)
        {
            try
            {
                if (ProveArrayCountry(in countries))
                    for (int i = 0; i < countries.Length; i++)
                    {
                        if (countries[i].Name == name) return i;
                    }
                else throw new Exception();
            }
            catch
            {
                MessageBox.Show("Нет имен для поиска!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return -1;
        }
        /// <summary>
        /// Проверка на пустоту(Используется для построения исключения)
        /// </summary>
        /// <param name="countries"></param>
        /// <returns></returns>
        private static bool ProveArrayCountry(in Country[] countries)
        {
            if (countries == null) return false;
            else return true;
        }
    }
}
