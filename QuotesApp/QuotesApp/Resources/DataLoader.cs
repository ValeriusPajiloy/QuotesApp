using Newtonsoft.Json;
using QuotesApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuotesApp.Resources
{
    /// <summary>
    /// Класс для десериализации внутренних объектов JSON.
    /// Представляет собой одну валюту
    /// </summary>
    public class ValutaJson
    {
        public string ID { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Previous { get; set; }
    }
    /// <summary>
    /// Класс для десериализации всего объекта JSON.
    /// Представляет собой структуру все ответного JSON.
    /// </summary>
    public class JsonObj
    {
        public string Date { get; set; }
        public string PreviousDate { get; set; }
        public string PreviousURL { get; set; }
        public string Timestamp { get; set; }
        public Dictionary<string, ValutaJson> Valute { get;set;}
    }
    /// <summary>
    /// Класс для загрузки данных с сервера
    /// </summary>
    public class DataLoader
    {
        /// <summary>
        /// Ссылка на данные
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// Дата последнего получения
        /// </summary>
        public string currentDate { get; set; }
        /// <summary>
        /// Десериализованный объект JSON
        /// </summary>
        public JsonObj jsonObj { get; set; }
        public DataLoader()
        {
            url = "";
            currentDate = "";
            jsonObj = new JsonObj();
        }
        /// <summary>
        /// Метод загрузки JSON и его десериализации
        /// </summary>
        /// <returns></returns>
        public async Task<JsonObj> LoadJson()
        {
            url = "https://www.cbr-xml-daily.ru/daily_json.js";
            var tempstring = new WebClient().DownloadString(url);
            jsonObj = JsonConvert.DeserializeObject<JsonObj>(tempstring);
            currentDate = jsonObj.Date;
            return jsonObj;
        }
        /// <summary>
        /// Проверка на новизну данных
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsNewLoad()
        {
            string lastDate = currentDate;
            await LoadJson();
            if (lastDate != currentDate) return true;
            return false;
        }
        /// <summary>
        /// Метод возвращающий готовую коллекцию для работы
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<Valuta>> GetValutes()
        {
            ObservableCollection<Valuta> valutas = new ObservableCollection<Valuta>();
            foreach(var valuta in jsonObj.Valute)
            {
                valutas.Add(new Valuta {
                    Id = valuta.Value.ID,
                    CharCode = valuta.Value.CharCode,
                    NumCode = valuta.Value.NumCode,
                    Name = valuta.Value.Name,
                    Nominal = valuta.Value.Nominal,
                    Worth = valuta.Value.Value,
                    Previous = valuta.Value.Previous
                });
            }
            return valutas;
        }

    }
}
