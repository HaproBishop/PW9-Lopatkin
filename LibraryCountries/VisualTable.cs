using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LibraryCountries
{
    public static class VisualTable
    {
        private static DataTable res = new DataTable();
        private static int j = 0;
        private static int i = 0;
        /// <summary>
        /// Задание шаблона таблицы
        /// </summary>
        /// <returns>DataTable объект с данными для DataGrid</returns>
        public static DataTable CreateTable()
        {
            res.Columns.Add("№", typeof(string));
            res.Columns.Add("Страна", typeof(string));
            res.Columns.Add("Народонаселение", typeof(long));
            res.Columns.Add("Столица", typeof(string));
            res.Columns.Add("Денежная единица", typeof(string));
            return res;
        }
        /// <summary>
        /// Добавление новой страны
        /// </summary>
        /// <param name="country"></param>
        /// <returns>DataTable объект с данными для DataGrid</returns>
        public static DataTable AddCountry(Country country)
        {
            if (ProveName(country.Name) == false)
            {
                var row = res.NewRow();
                j++;
                row[0] = j;
                row[1] = country.Name;
                row[2] = country.People;
                row[3] = country.MainTown;
                row[4] = country.Money;
                res.Rows.Add(row);
            }
            return res;
        }/// <summary>
         /// Добавление данных в таблицу без объекта
         /// </summary>
         /// <param name="name"></param>
         /// <param name="people"></param>
         /// <param name="maintown"></param>
         /// <param name="money"></param>
         /// <returns>DataTable объект с данными для DataGrid</returns>
        public static DataTable AddCountry(string name, long people, string maintown, string money)
        {            
            if (ProveName(name) == false)
            {
                var row = res.NewRow();
                j++;
                row[0] = j;
                row[1] = name;
                row[2] = people;
                row[3] = maintown;
                row[4] = money;
                res.Rows.Add(row);
            }
            return res;
        }/// <summary>
         /// Удаление страны из таблицы
         /// </summary>
         /// <param name="name"></param>
         /// <returns>DataTable объект с данными для DataGrid</returns>
        public static DataTable DeleteCountry(string name)
        {
            if (ProveName(name))
            {
                j--;
                res.Rows[i].Delete();
            }
            return res;
        }
        /// <summary>
        /// Локальная проверка на наличие наименования страны в таблице
        /// </summary>
        /// <param name="name"></param>
        /// <returns>DataTable объект с данными для DataGrid</returns>
        private static bool ProveName(string name)
        {
            DataRow row;
            for (i = 0; i < res.Rows.Count; i++)
            {
                row = res.Rows[i];                                
                if ((string)row[1] == name) return true;
            }
            return false;
        }
    }
}
