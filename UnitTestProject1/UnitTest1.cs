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
            Assert.AreEqual(Rover.Instance.ScanField(new Field(FieldType.DIRT,0,0)),FieldType.DIRT);
        }

    }
}
