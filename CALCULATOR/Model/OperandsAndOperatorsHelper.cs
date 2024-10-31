using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALCULATOR.Model
{
    public class OperandsAndOperatorsHelper
    {
        private float inputIntNumber = 0f;
        private float inputDecimalNumber = 0f;
        private float firstOperand = 0f;
        private float secondOperand = 0f;
        private int numberDecimalPlaces = 1;
        private bool afterDecimalPoint = false;
        private bool firstEqualClick = true;
        private bool firstOperation = true;
        private OperationType CurrentOperation;
        internal string Result = "";

        internal void AddDigit(float keyFigure)
        {
            if (!afterDecimalPoint)
            {
                inputIntNumber = inputIntNumber * 10 + keyFigure;
                return;
            }
            inputDecimalNumber += keyFigure / (float)Math.Pow(10, numberDecimalPlaces);
            numberDecimalPlaces++;
        }

        internal string? AddDecimalPoint()
        {
            if (afterDecimalPoint)
            {
                return null;
            }
            afterDecimalPoint = true;
            if (inputIntNumber == 0)
            {
                return "0.";

            }
            return ".";
        }

        internal void SelectOperation(OperationType operationType)
        {
            if (!firstOperation)
            {
                if (firstEqualClick)
                {
                    Equal();
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

        internal void Equal()
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
