using System;
using System.Collections.Generic;
using System.Globalization;

namespace MyChallenges.Objects
{
    class MathExpression
    {
        string baseExpression;
        List<string> listElements = new List<string>();
        double expressionResult;

        List<char> operators = new List<char> { '+', '-', '*', '/' };
        List<char> firstOperators = new List<char> { '*', '/' };
        List<char> secondOperators = new List<char> { '+', '-' };

        public MathExpression(string expression)
        {
            baseExpression = expression.Replace(".", ",");
            listElements.Add(baseExpression);
            this.MenageDivideElements();
        }

        public void MenageDivideElements()
        {
            bool resultReady = false;
            do
            {
                DivideElements();
            }
            while (!this.AreElementsReady());

            do
            {
                resultReady = MultipleStep();
            }
            while (!resultReady);

            this.SaveExpressionResult();
        }

        public bool MultipleStep()
        {
            for (int index = 0; index < listElements.Count - 1; index++)
            {
                if (firstOperators.Contains(listElements[index][0]))
                {
                    SimpleStep(index);
                    return false;
                }
            }

            for (int index = 0; index < listElements.Count - 1; index++)
            {
                if (
                    (secondOperators.Contains(listElements[index][0]))
                    &&
                    !(listElements[index][0] == '-' && listElements[index].Length > 1 && (listElements[index][1] == '(' || char.IsNumber(listElements[index][1])))
                )
                {
                    SimpleStep(index);
                    return false;
                }
            }

            return true;
        }

        public void SimpleStep(int operatorIndex)
        {
            double firstNumber = double.Parse(this.listElements[operatorIndex - 1]);
            double secondNumber = double.Parse(this.listElements[operatorIndex + 1]);
            char op = this.listElements[operatorIndex][0];

            string resultStep = Helper.Calc(firstNumber, secondNumber, op).ToString();

            listElements.RemoveRange(operatorIndex - 1, 3);
            listElements.Insert(operatorIndex - 1, resultStep);
        }

        public bool AreElementsReady()
        {
            foreach (string element in listElements)
            {
                foreach (char c in element)
                {
                    if (c == ' ' || c == '(' || c == ')')
                        return false;
                }
            }

            return true;
        }

        public void DivideElements()
        {
            for (int indexElement = 0; indexElement < listElements.Count; indexElement++)
            {
                for (int index = 0; index < listElements[indexElement].Length; index++)
                {
                    if (listElements[indexElement][index] == '(')
                    {
                        int posParentheseseOpen = index;
                        int verifyParenthesesOpen = index;
                        while ((verifyParenthesesOpen = listElements[indexElement].IndexOf("(", posParentheseseOpen + 1)) > 0)
                        {
                            posParentheseseOpen = verifyParenthesesOpen;
                        }

                        int posParenthesesClose = listElements[indexElement].IndexOf(")", posParentheseseOpen);
                        MathExpression parenthesesExpression = new MathExpression(listElements[indexElement].Substring(posParentheseseOpen + 1, posParenthesesClose - posParentheseseOpen - 1));
                        listElements[indexElement] = listElements[indexElement].Remove(posParentheseseOpen, posParenthesesClose - posParentheseseOpen + 1).Insert(posParentheseseOpen, parenthesesExpression.expressionResult.ToString());

                        return;
                    }

                    if (
                        (operators.Contains(listElements[indexElement][index]) && listElements[indexElement].Length > 1)
                        &&
                        !(listElements[indexElement][index] == '-' && (listElements[indexElement][index+1] == '(' || char.IsNumber(listElements[indexElement][index + 1])))
                    )
                    {
                        string element = listElements[indexElement];
                        listElements.RemoveAt(indexElement);

                        listElements.Insert(indexElement, element.Substring(0, index).Trim());
                        listElements.Insert(indexElement + 1, element.Substring(index, 1).Trim());
                        listElements.Insert(indexElement + 2, element.Substring(index + 1).Trim());

                        return;
                    }
                }
            }
        }

        public void SaveExpressionResult()
        {
            this.expressionResult = double.Parse(listElements[0]);
            Console.WriteLine(this.ToString());
        }



        public override string ToString()
        {
            return $"{this.baseExpression} = {this.expressionResult}";
        }
    }

    class Helper
    {
        public static double Calc(double firstNumber, double secondNumber, char op)
        {
            double r = 0.00;

            switch (op)
            {
                case '+':
                    r = firstNumber + secondNumber;
                    break;
                case '-':
                    r = firstNumber - secondNumber;
                    break;
                case '*':
                    r = firstNumber * secondNumber;
                    break;
                case '/':
                    r = firstNumber / secondNumber;
                    break;
            }

            return r;
        }
    }
}