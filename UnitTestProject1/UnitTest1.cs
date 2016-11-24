using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover.Controller;
using MarsRover.Model;
using MarsRover.Model.DataType;

namespace MarsRoverTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RoverRunsIntoDirt()
        {
            World w = new World(20);
            Assert.AreEqual(Rover.Instance.ScanField(w.GetField(0,0)), w.GetField(0,0));
        }

        public void


    }
}
