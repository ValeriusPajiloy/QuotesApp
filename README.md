# QuotesApp
Аннотация к работе "Работа с котировками валют"

В результате выполнения задания было разработано WPF приложение для работы с котировками

Данные использованные для выполнения задания : https://www.cbr-xml-daily.ru/daily_json.js

Функционал приложения:
1. Загрузка и Обновление данных
	Согласно ТЗ "Загрузка" должна загрузить коды валют, а "Обновление" обновить курсы валют.
	Если использовать предоставленные данные, загрузка данных для двух этих функций одинакова.
	Так как данные приходят в едином виде и не делятся, функцонал "Загрузки и обновления" был изменен:
		Загрузка - подгружает и заполняет данные вне зависимости от обстоятельств.
		Обновление - подгружает данные. но перед тем как изменять рабочие данные, сравнивает даты последней загрузки и текущей,
					если даты совпадают - обновление информации не происходит, так как это бессмысленно.

2. Отображение Кодов и Курсов валют
	Отображение кодов валют и курсов разделено на два LIstBox.
	После выбора валюты в одном ListBox, в втором она выбирается автоматически.

3. Конвертер валют
	Для конвертирования требуется выбрать исходную и конечную валюты, и ввести значение я конвертации в первый TextBox
	При изменении любого элемента(ComboBox-ы и TextBox исходной валюты) конвертация автоматически обновляется

4. Поиск валют
	Для поиска требуется ввести текст в соответствующее поле и нажать на кнопку "Поиск"
	Результатом будет все подходящие валюты в виде списка в правом нижнем окне.
	(с указанием текущего курса к рублю и доллару)
	Так же первый элемент из найденных валют выделяется в ListBox-ах п2.

Пояснения:
1. Приложение написано в соответствии с моим пониманием структуры MVVM, без фреймворков.
	До текущего момента мои знания в области WPF и структуры MVVM ограничивались только теоретическими.
	Поэтому я буду очень рад любому фидбеку с указанием на ошибки или недоработки, если такие найдутся.

2. Пункт про подсветку всей строки в TreeView.
	Я не использовал данный элемент при выполнении работы, насколько я понимаю, есть стандартное свойство "FullRowSelect", которое делает именно это

3. Фильтрация
	Для Фильтрации использовал стандартный метод "Where"

4. Получение данных постарался сделать ассинхронным

5. В работу со стилями не вникал, работал только с файлом "xaml" в котором настраивал внешний вид

6. Если я неправильно понял или выполнил задание, прошу мне сообщить

7. Если у вас появятся какие-либо вопросы готов связаться и все прояснить
