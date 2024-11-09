using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CALCULATOR.Model;
using Prism.Mvvm;

namespace CALCULATOR.VievModel
{
    public class MainViewModel : BindableBase
    {
        private OperandsAndOperatorsHelper _operationsModel;
        private string _display = "";
        private Dictionary<char, OperationType> SymbolTypeOperationsDictionary;
        private DelegateCommand _addZero;
        private DelegateCommand _addOne;
        private DelegateCommand _addTwo;
        private DelegateCommand _addThree;
        private DelegateCommand _addFour;
        private DelegateCommand _addFive;
        private DelegateCommand _addSix;
        private DelegateCommand _addSeven;
        private DelegateCommand _addEight;
        private DelegateCommand _addNine;
        private DelegateCommand _addPoint;
        private DelegateCommand _selectPlus;
        private DelegateCommand _selectMinus;
        private DelegateCommand _selectMultiply;
        private DelegateCommand _selectDiv;
        private DelegateCommand _calculate;
        private DelegateCommand _clearError;

        public DelegateCommand AddZero => _addZero ??= DelegateCommandBuilder(0);
        public DelegateCommand AddOne => _addOne ??= DelegateCommandBuilder(1);
        public DelegateCommand AddTwo => _addTwo ??= DelegateCommandBuilder(2);
        public DelegateCommand AddThree => _addThree ??= DelegateCommandBuilder(3);
        public DelegateCommand AddFour => _addFour ??= DelegateCommandBuilder(4);
        public DelegateCommand AddFive => _addFive ??= DelegateCommandBuilder(5);
        public DelegateCommand AddSix => _addSix ??= DelegateCommandBuilder(6);
        public DelegateCommand AddSeven => _addSeven ??= DelegateCommandBuilder(7);
        public DelegateCommand AddEight => _addEight ??= DelegateCommandBuilder(8);
        public DelegateCommand AddNine => _addNine ??= DelegateCommandBuilder(9);
        public DelegateCommand AddPoint => _addPoint ??= new DelegateCommand(AddPointVoid);
        public DelegateCommand SelectPlus => _selectPlus ??= DelegateCommandBuilder('+');
        public DelegateCommand SelectMinus => _selectMinus ??= DelegateCommandBuilder('-');
        public DelegateCommand SelectMultiply => _selectMultiply ??= DelegateCommandBuilder('*');
        public DelegateCommand SelectDiv => _selectDiv ??= DelegateCommandBuilder('/');
        public DelegateCommand Calculate => _calculate ??= new DelegateCommand(CalculateVoid);
        public DelegateCommand ClearError => _clearError ??= new DelegateCommand(ClearErrorVoid);

        public MainViewModel()
        {
            _operationsModel = new OperandsAndOperatorsHelper();
            SymbolTypeOperationsDictionary = new Dictionary<char, OperationType>()
            {
                ['+'] = OperationType.addition,
                ['-'] = OperationType.subtraction,
                ['*'] = OperationType.multiplication,
                ['/'] = OperationType.division,
            };
            //КОНСТРУКТОР СОЗДАЁТ ОБЪЕКТ МОДЕЛИ _operationsModel
            //И ОПРЕДЕЛЯЕТ СЛОВАРЬ С СИМВОЛАМИ ОПЕРАЦИЙ В КАЧЕСТВЕ КЛЮЧЕЙ И ТИПАМИ ПЕРЕЧИСЛЕНИЯ OperationType В КАЧЕСТВЕ ЗНАЧЕНИЙ
        }

        public string Display
        {
            get
            {
                return _display;
            }

            set
            {
                _display = value;
                RaisePropertyChanged(nameof(Display));
            }
        }

        public DelegateCommand DelegateCommandBuilder(object input)
        {
             switch(input.GetType().ToString())
             {
                 case "System.Int32" :
                 void AddDigitCommandExecute() => AddDigit(byte.Parse(input.ToString()));
                 return new DelegateCommand(AddDigitCommandExecute);

                 case "System.Char":
                 void SelectOperationCommandExecute() => SelectOperation((char)input);
                 return new DelegateCommand(SelectOperationCommandExecute);

                 default:
                 return null;
             }
        }
        #region PRIVATE_REGION
        private void AddDigit(byte digit)
        {
            if (digit == 0 && _operationsModel.inputIntNumber == 0) 
                return;
            
            _display += digit.ToString();
            _operationsModel.AddDigit(digit);
            RaisePropertyChanged(nameof(Display));
        }

        private void AddPointVoid()
        {
            _display += _operationsModel.AddDecimalPoint();
            RaisePropertyChanged(nameof(Display));
        }

        private void SelectOperation(char operationTypeKey)
        {
            _display += $" {operationTypeKey} ";
            _operationsModel.SelectOperation(SymbolTypeOperationsDictionary[operationTypeKey]);
            RaisePropertyChanged(nameof(Display));
        }

        private void CalculateVoid()
        {
            _operationsModel.Calculate();
            _display = _operationsModel.Result;
            RaisePropertyChanged(nameof(Display));
        }

        private void ClearErrorVoid()
        {
            _display = "";
            _operationsModel.ClearError();
            RaisePropertyChanged(nameof(Display));
        }
        #endregion
    }
}

#region COMMENT
/*
       private void AddZeroVoid() => AddDigit(0);
       private void AddOneVoid() => AddDigit(1);
       private void AddTwoVoid() => AddDigit(2);
       private void AddThreeVoid() => AddDigit(3);
       private void AddFourVoid() => AddDigit(4);
       private void AddFiveVoid() => AddDigit(5);
       private void AddSixVoid() => AddDigit(6);
       private void AddSevenVoid() => AddDigit(7);
       private void AddEightVoid() => AddDigit(8);
       private void AddNineVoid() => AddDigit(9);
        //public DelegateCommand AddOne => _addOne ??= new DelegateCommand(AddOneVoid);

       private void PlusVoid() => SelectOperation('+');
       private void MinusVoid() => SelectOperation('-');
       private void MultiplyVoid() => SelectOperation('*');
       private void DivVoid() => SelectOperation('/');
//new DelegateCommandHelper(1, this)._delegateCommand;

 /*
        private DelegateCommand DelegateCommandBuilder(byte digit)
        {
            
            void AddDigit(byte digit)
            
            {
               

                if(digit == 0 && _operationsModel.inputIntNumber == 0)
                {
                    return;
                }
                _display += digit.ToString();
                
                _operationsModel.AddDigit(digit);
                
                RaisePropertyChanged(nameof(Display));
                
            }

            void AddDigitCommandExecute() => AddDigit(digit);

            return new DelegateCommand(AddDigitCommandExecute);
            
        }

        private DelegateCommand DelegateCommandBuilder(char operationTypeKey)
        {
           
            void SelectOperation(char operationTypeKey)
            {
                _display += $" {operationTypeKey} ";
                
                _operationsModel.SelectOperation(SymbolTypeOperationsDictionary[operationTypeKey]);
                
                RaisePropertyChanged(nameof(Display));
               
            }


            void SelectOperationCommandExecute() => SelectOperation(operationTypeKey);

            return new DelegateCommand(SelectOperationCommandExecute);
           
        }
        */
/// ///////
// 40 ЗНАЧЕНИЯ ДЛЯ СВОЙСТВ ТИПА DelegateCommand ВОЗВРАЩАЮТСЯ СПЕЦИАЛЬНЫМ МЕТОДОМ DelegateCommandBuilder КОТОРЫЙ ИМЕЕТ ПЕРЕГРУЗКУ И В КАЧЕСТВЕ ПАРАМЕТРА ПРИНИМАЕТ ЛИБО ЧИСЛО - ТОГДА РАБОТАЕТ ДЛЯ ВВЕДЕНИЯ ЦИФР В КАЛЬКУЛЯТОР, ЛИБО СИМВОЛ ТОГДА РАБОТАЕТ ДЛЯ ВВЕДЕНИЯ ОПЕРАЦИЙ В КАЛЬКУЛЯТОР

//42 new DelegateCommandHelper(0, this)._delegateCommand;
//111 МЕТОД DelegateCommandBuilder ВОЗВРАЩАЕТ ОБЪЕКТ ТИПА DelegateCommand НЕОБХОДИМ ДЛЯ СВОЙСТВ ТИПА DelegateCommand
//112 ИМЕЕТ ДВЕ ПЕРЕГРУЗКИ - ДЛЯ С ЧИСЛОМ В КАЧЕСТВЕ ПАРАМЕТРА ДЛЯ ВВОДА ЦИФР, И С СИМВОЛОМ ДЛЯ ВВОДА КОМАНДЫ
//114 digit - ЦИФРА ВВОДИМАЯ ПОЛЬЗОВАТЕЛЕМ 
// 123 ВЫВОД КАЖДОЙ НОВОЙ ЦИФРЫ НА ЭКРАН ПРИ ВВОДЕ ЦИФР
//125 ВВОД ЦИФР В МОДЕЛЬ ДЛЯ РАССЧЁТОВ (ВВОД ДАННЫХ ДЛЯ ПОСЛЕДУЮЩЕЙ ОБРАБОТКИ)
//127 ОБНОВЛЕНИЕ ЗНАЧЕНИЯ СВОЙСТВА Display ДЛЯ ОТОБРАЖЕНИЯ КАЖДОЙ НОВОЙ ЦИФРЫ ВВОДИМОЙ ПОЛЬЗОВАТЕЛЕМ
//134 МЕТОД ВНУТРИ КОТОРОГО МЫ НАХОДИМСЯ DelegateCommandBuilder ВОЗВРАЩАЕТ ТИП DelegateCommand, ЭТОТ ТИП ДОЛЖЕН ПРИНИМАТЬ МЕТОД В КАЧЕСТВЕ ПАРАМЕТРА, И ОН ПРИНИМАЕТ ЛОКАЛЬНЫЙ МЕТОД AddDigitCommandExecute, КОТОРЫЙ В СВОЮ ОЧЕРЕДЬ ПОЛНОСТЬЮ ССЫЛАЕТСЯ НА МЕТОД С ЧИСЛОМ В КАЧЕСТВЕ ПАРАМЕТРА КОТОРЫЙ И ЯВЛЯЕТСЯ ТЕМ МЕТОДОМ КОТОРЫЙ ПОЛНОСТЬЮ ИСПОЛНЯЕТ ВСЮ ЛОГИКУ ПО ОБРАБОТКИ ЧИСЛА ВВЕДЁНОГО ПОЛЬЗОВАТЕЛЕМ
// 139 ПЕРЕГРУЗКА МЕТОДА DelegateCommandBuilder С СИМВОЛОМ В КАЧЕСТВЕ ПАРАМЕТРА, ДЛЯ ВВЕДЕНИЯ ПОЛЬЗОВАТЕЛЕМ КОМАНД В КАЛЬКУЛЯТОР

//143 ДОБАВЛЯЕТ СИМВОЛ ОПЕРАЦИИ НА ЭКРАН
//145 ВЫЗЫВАЕМ У МОДЕЛИ МЕТОД ВЫБОРА ОПЕРАЦИИ. ПЕРЕДАЁТ В КАЧЕСТВЕ ПАРАМЕРА ТИП ОПЕРАЦИИ ИЗ ПЕРЕЧИСЛЕНИЯ OperationType,ИЗВЛЕКАЯ ЕГО ПО КЛЮЧУ-СИМВОЛУ ИЗ СЛОВАРЯ SymbolTypeOperationsDictionary
//147 ОБНОВЛЯЕТ ОТОБРАЖЕНИЕ СИВОЛА ОПЕРАЦИИ НА ЭКРАНЕ
//154 СХЕМА ВЗАИМОДЕЙСТВИЯ ТИПА DelegateCommand С МЕТОДАМИ АНАЛОГИЧНА ПРЕДЫДУЩЕЙ ПЕРЕГРУЗКЕ

//156 ОЧЕВИДНО ДУБЛИРОВАНИЕ КОДА - ТРЕБУЕТ РЕФАКТОРИНГА - КАК ИМЕННО Я ПОДУМАЮ
//160 МЕТОД ДЛЯ ВВОДА ЧИСЕЛ МЕНЬШЕ ЕДИНИЦЫ - ДРОБНОЙ ЧАСТИ ЧИСЛА

//165 ДЛЯ ОТОБРАЖЕНИЯ ТОЧКИ РАЗДЕЛЯЮЩЕЙ ЦЕЛУЮ И ДРОБНУЮ НА ЭКРАНЕ ПОЛЕ _display ПОЛУЧАЕТ ЭТО СТРОКОВОЕ ЗНАЧЕНИЕ ИЗ КЛАССА МОДЕЛИ В КОТОРОМ ОНО ФОРМИРУЕТСЯ СПЕЦИАЛЬНЫМ ОБРАЗОМ, В ОТЛИЧИИ ОТ ПРОСТОГО ДОБАВЛЕНИЯ ЧИСЛА ИЛИ СИМВОЛА ОПЕРАЦИИ
//167 СТАНДАРТНОЕ ОБНОВЛЕНИЕ ОТОБРАЖЕНИЯ
// 172 ВЫВОД РЕЗУЛЬТАТА ВЫПОЛНЕНИЯ  НА ЭКРАН
//175 ВЫЗОВ МЕТОДА ПОЛУЧЕНИЯ РЕЗУЛЬТАТА У КЛАССА МОДЕЛИ 
//177 ПОЛУЧЕНИЕ РЕЗУЛЬТАТА НА ЭКРАН ИЗ КЛАССА МОДЕЛИ


//179 СТАНДАРТНОЕ ОБНОВЛЕНИЕ ЭКРАНА
//183 СБРОС КАЛЬКУЛЯТОРА, ОЧИСТКА ВСЕХ ДАННЫХ ЭКРАНА И ТЕКУЩЕЙ ОПЕРАЦИИ
//186 ОЧИСТКА ЭКРАНА
//188 ВЫЗОВ МЕТОДА ОЧИСТКИ У КЛАССА МОДЕЛИ
//190 СТАНДАРТНОЕ ОБНОВЛЕНИЕ ЭКРАНА



#endregion
