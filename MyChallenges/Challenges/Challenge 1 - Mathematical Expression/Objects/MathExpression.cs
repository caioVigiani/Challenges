using System;
using System.Collections.Generic;

namespace MyChallenges.Objects
{
    class MathExpression
    {
        string baseExpression;
        List<string> listElements = new List<string>();
        double expressionResult;

        List<char> operators = new List<char> { '+', '-', '*', '/' };

        public MathExpression(string expression)
        {
            baseExpression = expression;
            listElements.Add(baseExpression);
            Console.WriteLine($"Expression start: {baseExpression}");
            this.MenageDivideElements();
        }

        public void MenageDivideElements()
        {
            do
            {
                DivideElements();
            }
            while (!this.AreElementsReady());

            while(listElements.Count > 1)
            {
                this.SimpleStep();
            }

            for (int index = 0; index < listElements.Count; index++)
            {
                Console.WriteLine($"Elemento {index}: {listElements[index]}");
            }

            this.SaveExpressionResult();
            Console.WriteLine("--------------------------");
        }

        public void SimpleStep()
        {
            double firstNumber = double.Parse(this.listElements[0]);
            double secondNumber = double.Parse(this.listElements[2]);
            char op = this.listElements[1][0];

            string resultStep = Helper.Calc(firstNumber, secondNumber, op).ToString();

            listElements.RemoveRange(0, 3);
            listElements.Insert(0, resultStep);
        }

        public bool AreElementsReady()
        {
            foreach (string element in listElements)
            {
                foreach (char c in element)
                {
                    if (c == ' ')
                        return false;
                }
            }

            return true;
        }

        public void DivideElements()
        {
            for (int indexElement = 0; indexElement < listElements.Count; indexElement++)
            {
                if (listElements[indexElement][0] == '(')
                {
                    int posParenthesesClose = listElements[indexElement].IndexOf(")", 0);
                    MathExpression parenthesesExpression = new MathExpression(listElements[indexElement].Substring(1, posParenthesesClose - 1));
                    listElements[indexElement] = listElements[indexElement].Remove(0, posParenthesesClose + 1).Insert(0, parenthesesExpression.expressionResult.ToString());

                    return;
                }

                for (int index = 0; index < listElements[indexElement].Length; index++)
                {
                    if (operators.Contains(listElements[indexElement][index]) && listElements[indexElement].Length > 1)
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