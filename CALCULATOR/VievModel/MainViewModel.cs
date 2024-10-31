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
        private DelegateCommand _equal;
        private DelegateCommand _clearError;

        public DelegateCommand AddZero => _addZero ??= DelegateCommandBuilder(0);//new DelegateCommandHelper(0, this)._delegateCommand;
        public DelegateCommand AddOne => _addOne ??= DelegateCommandBuilder(1);//new DelegateCommandHelper(1, this)._delegateCommand;
        public DelegateCommand AddTwo => _addTwo ??= DelegateCommandBuilder(2);// new DelegateCommandHelper(2, this)._delegateCommand;
        public DelegateCommand AddThree => _addThree ??= DelegateCommandBuilder(3);// new DelegateCommandHelper(3, this)._delegateCommand;
        public DelegateCommand AddFour => _addFour ??= DelegateCommandBuilder(4);// new DelegateCommandHelper(4, this)._delegateCommand;
        public DelegateCommand AddFive => _addFive ??= DelegateCommandBuilder(5);// new DelegateCommandHelper(5, this)._delegateCommand;
        public DelegateCommand AddSix => _addSix ??= DelegateCommandBuilder(6);// new DelegateCommandHelper(6, this)._delegateCommand;
        public DelegateCommand AddSeven => _addSeven ??= DelegateCommandBuilder(7);// new DelegateCommandHelper(7, this)._delegateCommand;
        public DelegateCommand AddEight => _addEight ??= DelegateCommandBuilder(8);// new DelegateCommandHelper(8, this)._delegateCommand;
        public DelegateCommand AddNine => _addNine ??= DelegateCommandBuilder(9);// new DelegateCommandHelper(9, this)._delegateCommand;
        public DelegateCommand AddPoint => _addPoint ??= new DelegateCommand(AddPointVoid);
        public DelegateCommand SelectPlus => _selectPlus ??= DelegateCommandBuilder('+');//new DelegateCommandHelper('+', this)._delegateCommand;
        public DelegateCommand SelectMinus => _selectMinus ??= DelegateCommandBuilder('-');//new DelegateCommandHelper('-', this)._delegateCommand;
        public DelegateCommand SelectMultiply => _selectMultiply ??= DelegateCommandBuilder('*');//new DelegateCommandHelper('*', this)._delegateCommand;
        public DelegateCommand SelectDiv => _selectDiv ??= DelegateCommandBuilder('/');//new DelegateCommandHelper('/', this)._delegateCommand;
        public DelegateCommand Equal => _equal ??= new DelegateCommand(EqualVoid);
        public DelegateCommand ClearError => _clearError ??= new DelegateCommand(ClearErrorVoid);

        private DelegateCommand DelegateCommandBuilder(byte digit)
        {
            void AddDigit(byte digit)
            {
                _display += digit.ToString();
                _operationsModel.AddDigit(digit);
                RaisePropertyChanged(nameof(Display));
            }

            void AddDigitCommandExecute()
            {
                AddDigit(digit);
            }
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

            void SelectOperationCommandExecute()
            {
                SelectOperation(operationTypeKey);
            }
            return new DelegateCommand(SelectOperationCommandExecute);

        }

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

        /*internal void AddDigit(byte digit)
        {
            _display += digit.ToString();
            _operationsModel.AddDigit(digit);
            RaisePropertyChanged(nameof(Display));
        }

        internal void SelectOperation(char operationTypeKey)
        {
            _display += $" {operationTypeKey} ";
            _operationsModel.SelectOperation(SymbolTypeOperationsDictionary[operationTypeKey]);
            RaisePropertyChanged(nameof(Display));
        }*/

        private void AddPointVoid()
        {
            _display += _operationsModel.AddDecimalPoint();
            RaisePropertyChanged(nameof(Display));
        }


        private void EqualVoid()
        {
            _operationsModel.Equal();
            _display = _operationsModel.Result;
            RaisePropertyChanged(nameof(Display));
        }

        private void ClearErrorVoid()
        {
            _display = "";
            _operationsModel.ClearError();
            RaisePropertyChanged(nameof(Display));
        }
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
       */
#endregion
