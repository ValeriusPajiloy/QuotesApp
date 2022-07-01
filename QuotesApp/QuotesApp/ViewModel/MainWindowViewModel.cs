using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.ComponentModel;
using QuotesApp.Model;
using QuotesApp.Resources;
using System.Collections.ObjectModel;

namespace QuotesApp.ViewModel
{
    /// <summary>
    /// Основная модель-представление.
    /// Связывает модель данных и представление
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Valuta> valutes;
        Valuta selectedValuta;
        Valuta convertSelectedFirst;
        Valuta convertSelectedSecond;
        double? convertFirstValue, convertSecondValue;
        string searchText;
        ObservableCollection<string> searchedValutaText;
        DataLoader dataLoader;
        /// <summary>
        /// Список текущих валют
        /// </summary>
        public ObservableCollection<Valuta> Valutes { get { return valutes; } private set { valutes = value; OnPropertyChanged("Valutes"); } }
        /// <summary>
        /// Выбранная валюта из списка
        /// </summary>
        public Valuta SelectedValuta { get { return selectedValuta; } set { selectedValuta = value; OnPropertyChanged("SelectedValuta"); } }
        /// <summary>
        /// Выбранная исходной валюта для конвертирования
        /// </summary>
        public Valuta ConvertSelectedFirst { get { return convertSelectedFirst; } set { convertSelectedFirst = value; ChangeFirstConvert(ConvertFirstValue); OnPropertyChanged("ConvertSelectedFirst"); } }
        /// <summary>
        /// Выбранная целевой валюта для конвертирования
        /// </summary>
        public Valuta ConvertSelectedSecond { get { return convertSelectedSecond; } set { convertSelectedSecond = value; ChangeFirstConvert(ConvertFirstValue); OnPropertyChanged("ConvertSelectedSecond"); } }
        /// <summary>
        /// Значение исходной валюты для конвертирования
        /// </summary>
        public double? ConvertFirstValue { get { return convertFirstValue; } set { convertFirstValue = value; ChangeFirstConvert(value); OnPropertyChanged("ConvertFirstValue"); } }
        /// <summary>
        /// Значение целевой валюты для конвертирования
        /// </summary>
        public double? ConvertSecondValue { get { return convertSecondValue; } set { convertSecondValue = value; OnPropertyChanged("ConvertSecondValue"); } }
        /// <summary>
        /// Строка поиска
        /// </summary>        
        public string SearchText { get { return searchText; } set { searchText = value; OnPropertyChanged("SearchText"); } }
        /// <summary>
        /// Список найденных валют
        /// </summary>
        public ObservableCollection<string> SearchedValutaText { get { return searchedValutaText; } set { searchedValutaText = value; OnPropertyChanged("SearchedValutaText"); } }

        /// <summary>
        /// Команда поиска
        /// </summary>
        public ICommand SearchCommand { get; private set; }
        /// <summary>
        /// Команда Загрузки
        /// </summary>
        public ICommand LoadCommand { get; private set; }
        /// <summary>
        /// Команда Обновления
        /// </summary>
        public ICommand UpdateCommand { get; private set; }
        /// <summary>
        /// Конструктор по умолчанию модели-представления
        /// </summary>
        public MainWindowViewModel()
        {
            Valutes = new ObservableCollection<Valuta>();
            dataLoader = new DataLoader();
            SearchCommand = new DelegateCommand(Search, CanSearch);
            UpdateCommand = new DelegateCommand(Update, CanUpdate);
            LoadCommand = new DelegateCommand(Load);
        }
        /// <summary>
        /// Метод Загрузки
        /// </summary>
        /// <param name="obj"></param>
        private async void Load(object obj)
        {
            await dataLoader.LoadJson();
            Valutes = await dataLoader.GetValutes();
        }
        /// <summary>
        /// Метод определяющий возможность обновления(если список пуст, то невозможно обновить)
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool CanUpdate(object arg)
        {
            if ((arg as ObservableCollection<Valuta>) != null)
                return (arg as ObservableCollection<Valuta>).Any();
            return false;
        }
        /// <summary>
        /// Метод обновления
        /// </summary>
        /// <param name="obj"></param>
        private async void Update(object obj)
        {
            bool isNew = await dataLoader.IsNewLoad();
            if(isNew) Valutes = await dataLoader.GetValutes();
        }
        /// <summary>
        /// Метод поиска
        /// </summary>
        /// <param name="obj"></param>
        private void Search(object obj)
        {
            ObservableCollection<string> searchedValutaText = new ObservableCollection<string>();
            Valuta Usd = Valutes.Where(valuta => valuta.CharCode.Contains("USD")).FirstOrDefault();
            List<Valuta> SearchedValutes = Valutes.Where(valuta => valuta.Name.Contains(SearchText) || valuta.NumCode.Contains(SearchText) || valuta.CharCode.Contains(SearchText)).ToList();
            foreach (Valuta valuta in SearchedValutes)
            {
                string valutaText = valuta.CourseString + " = " + ((valuta.Worth/ valuta.Nominal) / (Usd.Worth / Usd.Nominal) * valuta.Nominal).ToString("F3") + " USD";
                searchedValutaText.Add(valutaText);
            }
            SearchedValutaText = searchedValutaText;
            if (SearchedValutes.Any())
            {
                SelectedValuta = SearchedValutes[0];
            }
        }
        /// <summary>
        /// Метод определяющий возможность поиска (если строка поиска пустая, то невозможно)
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool CanSearch(object arg)
        {
            if ((arg as string) != null)
                return (arg as string).Length != 0;
            return false;
        }
        /// <summary>
        /// Метод обновления данных в конвертере валют
        /// </summary>
        /// <param name="value"></param>
        void ChangeFirstConvert(double? value)
        {
            if (value.HasValue)
            {
                if (ConvertSelectedFirst != null && ConvertSelectedSecond != null)
                {

                    ConvertSecondValue = (ConvertSelectedFirst.Worth / ConvertSelectedFirst.Nominal) / (ConvertSelectedSecond.Worth / ConvertSelectedSecond.Nominal) * value;
                }
            }

        }
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
