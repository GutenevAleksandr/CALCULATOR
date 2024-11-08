using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALCULATOR.Model
{
    public class OperandsAndOperatorsHelper
    {
        internal float inputIntNumber = 0f;
        private float inputDecimalNumber = 0f;
        private float firstOperand = 0f;
        private float secondOperand = 0f;
        private int numberDecimalPlaces = 1;
        internal bool afterDecimalPoint = false;
        private bool firstEqualClick = true;
        private bool firstOperation = true;
        private OperationType CurrentOperation;
        internal string Result = "";

        internal void AddDigit(float digit)
        {
            //МЕТОД ДОБАВЛЕНИЯ ЦИФРЫ В ЧИСЛО
            if (!afterDecimalPoint)
            {
                // bool afterDecimalPoint ПОКАЗЫВАЕТ НАХОДИМСЯ ЛИ МЫ СЕЙЧАС В РЕЖИМЕ ВВОДА ЦИФР В ЦЕЛУЮ ЧАСТЬ ЧИСЛА ИЛИ В ДРОБНУЮ, 

                //В ДАННОМ СЛУЧАЕ МЫ НЕ НАХОДИМСЯ В РЕЖИМЕ ВВОДА ЦИФР В ДРОБНУЮ ЧАСТЬ ЧИСЛА, А ЗНАЧИТ МЫ НАХОДИМСЯ В РЕЖИМЕ ВВОДА ЦИФР В ЦЕЛУЮ ЧАСТЬ ЧИСЛА, 

                inputIntNumber = inputIntNumber * 10 + digit;
                //ПЕРЕМЕННАЯ inputIntNumber ЭТО ОБЩАЯ ПЕРЕМЕННАЯ ДЛЯ ФОРМИРОВАНИЯ ВВОДИМОГО ЧИСЛА, ПРИ ВВЕДЕНИИ НОВОЙ ЦИФРЫ ДЛЯ ЦЕЛОЙ ЧАСТИ ЧИСЛА ПРЕДВДУЩАЯ ВЕРСИЯ ФОРМИРУЕМОГО ВВОДИМОГО ЧИСЛА ДОМНОЖАЕТСЯ НА 10 И К НЕЙ ПРИБАВЛЯЕТСЯ НОВАЯ ЦИФРА

                //ЕСЛИ ВВЕДЁНОЕ ЧИСЛО БЫЛО 100, И МЫ ВВЕЛИ ЦИФРУ 5 ТО ВВОДЁНОЕ ЧИСЛО СТАЛО 1005, ТОГДА НУЖНО 100 * 10 = 1000 1000 + 5 = 1005
                return;
                //ПОСЛЕ ЧЕГО ПРОИСХОДИТ ВЫХОД ИЗ МЕТОДА ОБНОВЛЕНИЕ ВВОДИМОГО ЧИСЛА
            }
            //ЕСЛИ ЖЕ МЫ НАХОДИМСЯ В РЕЖИМЕ ВВОДА ДРОБНОЙ ЧАСТИ ЧИСЛА - ТО ЕСТЬ ЧИСЕЛ ПОСЛЕ ЗАПЯТОЙ МЕНЬШЕ 1
            //ТО ВВОДИМОЕ ЧИСЛО НУЖНО ДЕЛИТЬ НА 10 В СТЕПЕНИ СООТВЕТСТВУЮЩЕЙ БУДУЩЕМУ ПОЛОЖЕНИЮ ЭТОГО ЧИСЛА ЗА ЗАПЯТОЙ

            //К ПРИМЕРУ ЕСЛИ МЫ ВВОДИМ 0,1 ТО 1 НУЖНО РАЗДЕЛИТЬ НА 10 В ПЕРВОЙ СТЕПЕНИ, А ЕСЛИ ВВОДИМ 0,003 ТО 3 НУЖНО РАЗДЕЛИТЬ НА 10 В 3 СТЕПЕНИ ТО ЕСТЬ НА 1000 ТАКИМ ОБРАЗОМ ДОБАВИТЬ К ВВОДИМОМУ ЧИСЛУ ТРИ ТЫСЯЧНЫЕ
            inputDecimalNumber += digit / (float)Math.Pow(10, numberDecimalPlaces);
            numberDecimalPlaces++;
            // ЗА ПОЛОЖЕНИЕ ВВОДИМОЙ ЦИФРЫ ИЗ ДРОБНОЙ ЧАСТИ ЗА ЗАПЯТОЙ ОТВЕЧАЕТ ПЕРЕМЕННАЯ numberDecimalPlaces, ИМЕННО В ЕЁ СТЕПЕНЬ И ВОЗВОДИТСЯ ДЕСЯТКА, ПОСЛЕ ЧЕГО ОНА УЧЕЛИЧИВАЕТСЯ НА 1
        }

        internal string? AddDecimalPoint()
        {
            //МЕТОДА ДЛЯ УТОЧНЕНИЯ И ПЕРЕДАЧИ МОДЕЛИ ОТОБРАЖЕНИЯ ДАННЫХ ДЛЯ ВЫВОДА НА ЭКРАН В СЛУЧАЕ ИСПОЛЬЗОВАНИЯ ДЕСЯТИЧНОЙ ЗАПЯТОЙ

            //ДЕЛО В ТОМ ЧТО ЕСЛИ ЦЕЛАЯ ЧЕСТЬ УЖЕ ВВЕДЕНА ТО НУЖНО ПРОСТО ДОБАВИТЬ РАЗДЕЛИТЕЛЬ, В СЛУЧАЕ ЕСЛИ ВВОДИМОЕ ЧИСЛО МЕНЬШЕ 1 ТО НУЖНО ДОБАВИТЬ 0
            if (afterDecimalPoint)
            {
                return null;
            }
            //ЕСЛИ ИЗНАЧАЛЬНО МЫ ДОБАВЛЯЕМ ЦИФРЫ В ЦЕЛОЕ ЧИСЛО БЕЗ ДРОБНОЙ ЧАСТИ ТО ПОСЛЕ НАЖАТИЯ НА ЗАПЯТУЮ СЛЕДУЕТ СЧИТЕТЬ ЧТО ТЕПЕРЬ ТО МЫ БУДЕМ ВВОДИТЬ ТОЛЬКО ДРОБНУЮ ЧАСТЬ, И ТОГДА НУЖНО ВЕРНУТЬ 0 С ЗАПЯТОЙ,

            //А ЕСЛИ МЫ УЖЕ НАХОДИМСЯ В РЕЖИМЕ ВВОДА ЧИСЕЛ ПОСЛЕ ЗАПЯТОЙ ТО НЕ НУЖНО ВОЗВРАЩАТЬ НИЧЕГО, НУ ПОТОМУ ЧТО ВОЗВРАЩАТЬ НОЛЬ С ЗАПЯТОЙ НУЖНО ТОЛЬКО ОДИН РАЗ
            afterDecimalPoint = true;
            if (inputIntNumber == 0)
            {
                return "0.";

            }
            return ".";

            //ЗДЕСЬ ОТДАЧА ЭТИХ ДАННЫХ ВЕСЬМА ДАЛЕКА ОТ СОВЕРШЕНСТВА ПО ЭТОМУ КАК ЕСТЬ
        }

        internal void SelectOperation(OperationType operationType)
        {
            //ЕСЛИ ЧЕСТНО ЗДЕСЬ И НЕ ПОНЯТНО ЧТО И ДЛЯ ЧЕГО,
            //И ПО ИТОГУ РАБОТАЕТ НЕ ВЕРНО К ПРИМЕРУ ПРИ НАЖАТИИ НЕСКОЛЬКИХ ПЛЮСОВ ОН ИХ ДОБАВЛЯЕТ ЭТО ВМЕСТО ТОГО ЧТО-БЫ ВЫВЕСТИ РЕЗУЛЬТАТ ПРЕДЫДУЩЕЙ ОПЕРАЦИИ
            if (!firstOperation)
            {
                
                if (firstEqualClick)
                {
                    Calculate();
                }
                firstEqualClick = true;
                /*  if(operationType == OperationType.subtraction && CurrentOperation != null)
                  {
                      secondOperand = (0 - secondOperand);
                      return;
                  }*/
                CurrentOperation = operationType;
                return;
            }

            firstOperand = GetOperand();
            firstEqualClick = true;
            firstOperation = false;

            BaseClearForAllMethods();

            /*    if (operationType == OperationType.subtraction && CurrentOperation != null)
                {
                    secondOperand = (0 - secondOperand);
                    return;
                }*/

            CurrentOperation = operationType;
        }

        private void OperationWithNegative()
        {

        }

        internal void Calculate()
        {
            float answer = 0f;
            if (firstEqualClick)
            {
                secondOperand = GetOperand();
            }

            switch (CurrentOperation)
            {
                case OperationType.addition:
                    answer = firstOperand + secondOperand;
                    break;

                case OperationType.subtraction:
                    answer = firstOperand - secondOperand;
                    break;

                case OperationType.multiplication:
                    answer = firstOperand * secondOperand;
                    break;

                case OperationType.division:
                    if (secondOperand == 0)
                    {
                        ClearError();
                    }
                    else
                    {
                        answer = firstOperand / secondOperand;
                    }
                    break;
            }

            Result = answer.ToString();
            firstOperand = answer;
            firstEqualClick = false;
            BaseClearForAllMethods();
        }

        internal void ClearError()
        {
            firstOperand = 0;
            secondOperand = 0;
            firstEqualClick = true;
            firstOperation = true;
            BaseClearForAllMethods();
        }

        private void BaseClearForAllMethods()
        {
            afterDecimalPoint = false;
            inputDecimalNumber = 0;
            inputIntNumber = 0;
            numberDecimalPlaces = 1;
        }

        private float GetOperand()
        {
            return inputIntNumber + inputDecimalNumber;
        }

    }
}
