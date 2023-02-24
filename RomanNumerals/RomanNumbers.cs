using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanNumerals
{
    public class RomanNumbers
    {

        internal Dictionary<char, int> _romanNumerals = new Dictionary<char, int>(){
                {'M', 1000},
                {'D', 500},
                {'C', 100},
                {'L', 50},
                {'X', 10},
                {'V', 5},
                {'I', 1},
            };

        private string expressionParser(string input)
        {
            char[] operatorArray = new char[] { '*', '/', '+', '-' };
            List<char> operators = new List<char>();
            List<int> nums = new List<int>();

            input = input.Replace(" ", "");
            int romanStartPos = -1;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (operatorArray.Contains(c))
                {
                    operators.Add(input[i]);
                }

                if (_romanNumerals.ContainsKey(c))
                {
                    if (romanStartPos == -1)
                    {
                        romanStartPos = i;
                    }

                    if (i == (input.Length - 1))
                    {
                        if (romanStartPos != -1)
                        {
                            nums.Add((input.Substring(romanStartPos, i - romanStartPos + 1)).ToInt());
                            romanStartPos = -1;
                        }
                    }
                }
                else
                {
                    if (romanStartPos != -1)
                    {
                        nums.Add((input.Substring(romanStartPos, i - romanStartPos)).ToInt());
                        romanStartPos = -1;
                    }
                }


            }

            for (int i = 0; i < operatorArray.Length; i = i + 2)
            {
                int index1 = operators.IndexOf(operatorArray[i]);
                int index2 = operators.IndexOf(operatorArray[i + 1]);
                int indexDst = -1;

                if (index1 > index2)
                    indexDst = index2 != indexDst ? index2 : index1;
                else
                    indexDst = index1 != indexDst ? index1 : index2;


                if (indexDst != -1)
                {
                    int result = calculate(operators[indexDst], nums[indexDst], nums[indexDst + 1]);
                    nums.RemoveRange(indexDst, 2);
                    nums.Insert(indexDst, result);
                    operators.RemoveAt(indexDst);
                    i = -2;
                }
            }

            if (nums.Count != 1)
                throw new Exception("Unknown error");

            return nums[0].ToRoman();
        }

        private int calculate(char action, int left, int right)
        {
            switch (action)
            {
                case '*': return left * right;
                case '/': return left / right;
                case '+': return left + right;
                case '-': return left - right;
                default: throw new ArgumentException("Invalid operator.");
            }
        }

        public string Evaluate(string input)
        {
            int openedPos = -1;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (c == '(')
                {
                    openedPos = i;
                }

                if (c == ')')
                {
                    if (openedPos != -1)
                    {
                        string output = expressionParser(input.Substring(openedPos + 1, i - openedPos - 1));
                        input = input.Remove(openedPos, i - openedPos + 1);
                        input = input.Insert(openedPos, output);
                        i = 0;
                        openedPos = -1;
                    }
                    else
                        throw new ArgumentException("Invalid expression");
                }
            }

            return expressionParser(input);
        }

        public bool IsRomanNumeral(string input)
        {
            foreach (char c in input)
            {
                if (!_romanNumerals.ContainsKey(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
