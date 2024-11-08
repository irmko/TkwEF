using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.Core.Extentions
{
    public static class DateTimeEx
    {
        /// <summary>
        /// Получить возраст
        /// </summary>
        /// <param name="birghday">День рождения</param>
        /// <returns></returns>
        public static int GetAge(this DateTime birghday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birghday.Year;
            if (birghday > now.AddYears(-age)) age--;
            return age;
        }
        /// <summary>
        /// Получить возраст ассинхронно
        /// </summary>
        /// <param name="birghday">День рождения</param>
        /// <returns></returns>
        public async static Task<int> GetAgeAsync(this DateTime birghday)
        {
            return await Task<int>.Factory.StartNew((d) =>
            {
                DateTime dt = (DateTime)d;
                return dt.GetAge();
            }, birghday);
        }
        /// <summary>
        /// Получить возраст на определенную дату
        /// </summary>
        /// <param name="birghday">День рождения</param>
        /// <param name="date">На какую дату определяем возраст</param>
        /// <returns></returns>
        public static int GetAge(this DateTime birghday, DateTime date)
        {
            int age = date.Year - birghday.Year;
            if (birghday > date.AddYears(-age)) age--;
            return age;
        }
        /// <summary>
        /// Получить возраст на определенную дату ассинхронно
        /// </summary>
        /// <param name="birghday">День рождения</param>
        /// <param name="date">На какую дату определяем возраст</param>
        /// <returns></returns>
        public async static Task<int> GetAgeAsync(this DateTime birghday, DateTime date)
        {
            SortedList<int, DateTime> sl = new SortedList<int, DateTime>();
            sl.Add(1, birghday);
            sl.Add(2, date);
            return await Task<int>.Factory.StartNew((p) =>
            {
                SortedList<int, DateTime> prm = (SortedList<int, DateTime>)p;
                return prm[1].GetAge(prm[2]);
            }, sl);

            //Task<int> task = (new Func<DateTime, DateTime, Task<int>>(async (b, d) =>
            //    await Task.Run(() => GetAge(b, d)))
            //    ).Invoke(birghday, date);
            //return await task;
        }
    }
}
