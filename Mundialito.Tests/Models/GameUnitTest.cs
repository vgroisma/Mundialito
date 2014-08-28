﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mundialito.Models;
using Mundialito.DAL.Games;

namespace Mundialito.Tests.Models
{
    [TestClass]
    public class GameUnitTest
    {
        [TestMethod]
        public void IsOpenSimpleTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime();

            Assert.IsFalse(game.IsOpen(), "Game should not be open");
        }

        [TestMethod]
        public void IsOngoingSimpleTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime();

            Assert.IsTrue(game.IsOngoing(), "Game should be ongoing");
            Assert.IsFalse(game.IsPendingUpdate(), "Game should not be pending update");
            Assert.IsFalse(game.IsOpen(), "Game should not be open");
        }

        [TestMethod]
        public void IsOngoingTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(80));

            Assert.IsTrue(game.IsOngoing(), "Game should be ongoing");
            Assert.IsFalse(game.IsPendingUpdate(), "Game should not be pending update");
            Assert.IsFalse(game.IsOpen(), "Game should not be open");
        }

        [TestMethod]
        public void IsOngoingTest2()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(92));

            Assert.IsFalse(game.IsOngoing(), "Game should not be ongoing");
            Assert.IsTrue(game.IsPendingUpdate(), "Game should be pending update");
            Assert.IsFalse(game.IsOpen(), "Game should not be open");
        }

        [TestMethod]
        public void IsOpenSameDateTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Add(TimeSpan.FromMinutes(35));
            
            Assert.IsTrue(game.IsOpen(), "Game should be open");
        }

        [TestMethod]
        public void IsOpenNotSameDateTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromDays(1)).Add(TimeSpan.FromMinutes(5));

            Assert.IsFalse(game.IsOpen(), "Game should not be open");
        }

        [TestMethod]
        public void IsOpenNotSameDateTestPastTime()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Add(TimeSpan.FromDays(1)).Subtract(TimeSpan.FromMinutes(5));
            
            Assert.IsTrue(game.IsOpen(), "Game shouldb be open");
        }

        [TestMethod]
        public void MarkNotPlayedTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.Add(TimeSpan.FromMinutes(5));

            Assert.AreEqual("Not Played", game.Mark());
        }
        
        [TestMethod]
        public void MarkDrawTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(5));

            game.HomeScore = 1;
            game.AwayScore = 1;
            game.CardsMark = "X";
            game.CornersMark = "1";

            Assert.AreEqual("X", game.Mark());
        }

        [TestMethod]
        public void MarkHomeWinTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(5));

            game.HomeScore = 2;
            game.AwayScore = 1;
            game.CardsMark = "X";
            game.CornersMark = "1";

            Assert.AreEqual("1", game.Mark());
        }

        [TestMethod]
        public void MarkAwayWinTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(5));

            game.HomeScore = 1;
            game.AwayScore = 2;
            game.CardsMark = "X";
            game.CornersMark = "1";

            Assert.AreEqual("2", game.Mark());
        }

        [TestMethod]
        public void MarkNotScoreTest()
        {
            var game = new Game();
            game.Date = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromMinutes(5));

            Assert.AreEqual("Pending Update", game.Mark());
        }
    }
}
