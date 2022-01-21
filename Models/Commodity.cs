﻿using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PrzeplywDokumentowWFirmie.Models
{
    public class Commodity
    {
        IItem commodity;
        int quantity;

        public IItem Commodity { get => commodity; set => commodity = value; }
        public int Quantity { get => quantity; set => quantity = value; }
    }
}