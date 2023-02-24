using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomanNumerals;
using System;
using System.Diagnostics;
using System.IO;

namespace UnitTestRoman
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CheckConvert()
        {
            int num = new Random().Next(1, 4999);
            Stopwatch watch = new Stopwatch();

            watch.Start();
            string romanNum = num.ToRoman();
            watch.Stop();
            TimeSpan time = watch.Elapsed;

            watch.Restart();
            int numResult = romanNum.ToInt();
            watch.Stop();
            TimeSpan time1 = watch.Elapsed;

            Assert.AreEqual(numResult, num);
        }

        [TestMethod]
        public void CheckExpression()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            RomanNumbers roman = new RomanNumbers();

            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + @"\input.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] expressions = line.Split('=');
                    string result = roman.Evaluate(expressions[0]);

                    Assert.AreEqual(expressions[1], result, $"Expressions: {expressions[0]}");
                }
            }  
        }
    }
}
