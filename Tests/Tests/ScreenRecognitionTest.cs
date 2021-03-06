﻿using System.Drawing;
using DominationsBot.DI;
using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.ImageProcessing.TextReading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests.Tests
{
    [TestClass]
    public class ScreenRecognitionTests
    {
        private readonly ITextRecognition _textRecognition;

        public ScreenRecognitionTests()
        {
            IContainer container = new Container(new TestRootRegistry());
            _textRecognition = container.GetInstance<TextRecognition>();
        }

        //[TestMethod]
        //public void TestFilter()
        //{
        //    var bitmap = (Bitmap) TestScreens._1790024.Clone();
        //    _colourFilter.ApplyFilter(bitmap);
        //    bitmap.Save("filter.png");
        //}

        //[TestMethod, Ignore]
        //public void GoldAndMoneyRecognition()
        //{

        //    var int1790024 = _textRecognition.GetText<int>(TestScreentemplateMatchExtss._1790024);
        //    var int819444 = _textRecognition.GetText<int>(TestScreens._819444);

        //    var bitmap = (Bitmap)TestScreens._666348.Clone();
        //    _colourFilter.ApplyFilter(bitmap);
        //    var int666348 = _textRecognition.GetText<int>(bitmap);
        //    var int384174 = _textRecognition.GetText<int>(TestScreens._384174);

        //    Assert.AreEqual(1790024, int1790024);
        //    Assert.AreEqual(819444, int819444);
        //    Assert.AreEqual(666348, int666348);
        //    Assert.AreEqual(384174, int384174);
        //}
        
    }
}