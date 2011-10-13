﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaTest;
using Codaxy.CodeReports.CodeModel;
using Codaxy.CodeReports.Data;
using Codaxy.CodeReports.Controls;
using Codaxy.CodeReports.Tests.Helpers;

namespace Codaxy.CodeReports.Tests
{
    [TestFixture]
    class CellDisplayModeTests
    {
        [GroupingLevel(0, ShowHeader = false, ShowFooter = true, ShowCaption = false)]
        class Item
        {
            [TableColumn(CellDisplayMode=CellDisplayMode.RowNumber)]
            public string No { get; set; }

            [TableColumn()]
            public String Name { get; set; }

            [TableColumn(CellDisplayMode = CellDisplayMode.AccumulatorValue, AggregateFunction=AggregateFunction.Sum)]
            public int Value { get; set; }
        }

        [Test]
        public void Test1()
        {
            var dc = new DataContext();
            dc.AddTable("data", new[] { 
                new Item { Name="1", Value = 2 },
                new Item { Name="2", Value = 3 },
                new Item { Name="3", Value = 4 }
            });

            var flow = new Flow { Orientation = FlowOrientation.Vertical };
            flow.AddTable<Item>("data");

            var rep = Report.CreateReport(flow, dc);
            var cells = ReportUtil.GetCellMatrix(rep);

            //RowNumber
            Assert.AreEqual(1, cells[0][0].Value);
            Assert.AreEqual(2, cells[1][0].Value);
            Assert.AreEqual(3, cells[2][0].Value);

            //RowNumber
            Assert.AreEqual(2, cells[0][2].Value);
            Assert.AreEqual(5, cells[1][2].Value);
            Assert.AreEqual(9, cells[2][2].Value);
        }
    }
}
