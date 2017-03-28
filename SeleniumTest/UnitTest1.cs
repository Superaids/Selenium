using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace SeleniumTest
{
    [TestClass]
    public class UnitTest1
    {
        static IWebDriver driverGC;

        [AssemblyInitialize]
        public static void SetUp(TestContext context)
        {
            driverGC = new ChromeDriver(@"C:\Skole\chromedriver_win32");
        }

        [TestMethod]
        public void TestChromeDriver()
        {
            driverGC.Navigate().GoToUrl("http://www.google.com");

            driverGC.FindElement(By.Id("lst-ib")).SendKeys("Goathamster");
            driverGC.FindElement(By.Id("lst-ib")).SendKeys(Keys.Enter);
        }

        [TestMethod]
        public void TestResetData()
        {
            driverGC.Navigate().GoToUrl("http://localhost:3000/reset");
        }

        [TestMethod]
        public void TestFiveRows()
        {
            driverGC.Navigate().GoToUrl("http://localhost:3000");
            Thread.Sleep(2000);
            var hej = driverGC.FindElement(By.Id("tbodycars"));

            var res = hej.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Assert.AreEqual(res.Length, 5);

        }

        [TestMethod]
        public void TestFilterTwoRows()
        {
            driverGC.Navigate().GoToUrl("http://localhost:3000");
            Thread.Sleep(2000);
            driverGC.FindElement(By.Id("filter")).SendKeys("2002");
            driverGC.FindElement(By.Id("filter")).SendKeys(Keys.Enter);

            Thread.Sleep(2000);
            var hej = driverGC.FindElement(By.Id("tbodycars"));
            var res = hej.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Assert.AreEqual(res.Length, 2);
        }

        [TestMethod]
        public void TestClearFilter()
        {
            driverGC.Navigate().GoToUrl("http://localhost:3000");
            Thread.Sleep(2000);
            driverGC.FindElement(By.Id("filter")).Clear();

            Thread.Sleep(2000);
            var hej = driverGC.FindElement(By.Id("tbodycars"));

            var res = hej.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Assert.AreEqual(res.Length, 5);
        }

        [TestMethod]
        public void TestSort()
        {
            bool test = false;
            driverGC.Navigate().GoToUrl("http://localhost:3000");
            Thread.Sleep(2000);
            driverGC.FindElement(By.Id("h_year")).Click();

            Thread.Sleep(2000);
            var hej = driverGC.FindElement(By.Id("tbodycars"));

            var res = hej.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            var topCar = res[0].Split(null);
            var bottomCar = res[res.Length - 1].Split(null);

            if (topCar[0] == "938" && bottomCar[0] == "940")
            {
                test = true;
            }

            Assert.IsTrue(test);
        }

        [TestMethod]
        public void TestEditCoolCar()
        {
            //Couldn't figure this one out
            driverGC.Navigate().GoToUrl("http://localhost:3000");
            Thread.Sleep(2000);
            var hej = driverGC.FindElements(By.TagName("tr"));

            //var res = hej.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            //var topCar = res[0].Split(null);
        }
        

        [TestMethod]
        public void TestFailSave()
        {
            driverGC.Navigate().GoToUrl("http://localhost:3000");
            Thread.Sleep(2000);
           
            driverGC.FindElement(By.Id("new")).SendKeys(Keys.Enter);

            Thread.Sleep(500);

            driverGC.FindElement(By.Id("save")).SendKeys(Keys.Enter);

            Thread.Sleep(2000);

            var test = driverGC.FindElement(By.Id("submiterr"));

            Assert.IsTrue(test != null);
        }

        [TestMethod]
        public void TestSaveCar()
        {
            driverGC.Navigate().GoToUrl("http://localhost:3000");
            Thread.Sleep(2000);

            driverGC.FindElement(By.Id("year")).SendKeys("2008");
            driverGC.FindElement(By.Id("registered")).SendKeys("2002-5-5");
            driverGC.FindElement(By.Id("make")).SendKeys("Kia");
            driverGC.FindElement(By.Id("model")).SendKeys("Rio");
            driverGC.FindElement(By.Id("description")).SendKeys("As new");
            driverGC.FindElement(By.Id("price")).SendKeys("31000");

            driverGC.FindElement(By.Id("save")).SendKeys(Keys.Enter);

            Thread.Sleep(2000);
            var hej = driverGC.FindElement(By.Id("tbodycars"));

            var res = hej.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Assert.AreEqual(res.Length, 6);
        }
    }
}
