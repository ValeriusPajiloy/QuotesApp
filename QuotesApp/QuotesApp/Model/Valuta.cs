using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace QuotesApp.Model
{
    /// <summary>
    /// Основная модель данных Valuta
    /// Представляет собой объект "Валюта"
    /// </summary>
    public class Valuta : INotifyPropertyChanged
    {
        string id;
        string numCode;
        string charCode;
        int nominal;
        string name;
        double worth;
        double previous;

        /// <summary>
        /// Id валюты
        /// </summary>
        public string Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        /// <summary>
        /// Числовой код валюты
        /// </summary>
        public string NumCode { get { return numCode; } set { numCode = value; OnPropertyChanged("NumCode"); } }
        /// <summary>
        /// Символьный код валюты
        /// </summary>
        public string CharCode { get { return charCode; } set { charCode = value; OnPropertyChanged("CharCode"); } }
        /// <summary>
        /// Номинал для отображения курса
        /// </summary>
        public int Nominal { get { return nominal; } set { nominal = value; OnPropertyChanged("Nominal"); } }
        /// <summary>
        /// Название валюты
        /// </summary>
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        /// <summary>
        /// Текущий курс по отношению к рублю
        /// </summary>
        public double Worth { get { return worth; } set { worth = value; OnPropertyChanged("Worth"); } }
        /// <summary>
        /// Предыдущий курс по отношению к RUB
        /// </summary>
        public double Previous { get { return previous; } set { previous = value; OnPropertyChanged("Previous"); } }
        /// <summary>
        /// Наименование валюты с символьным кодом
        /// </summary>
        public string NameWithCode { get { return charCode + " - " + Name; } }
        /// <summary>
        /// Текущий курс в по отношению к RUB в виде полной строки
        /// </summary>
        public string CourseString { get { return Nominal.ToString() + " " + CharCode + " = " + Worth.ToString("F3") + " RUB"; } }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
