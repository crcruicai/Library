﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRC.Controls;
using System.Data;
using System.ComponentModel;

namespace UILibrary
{
    public class DataTableWrapper : ListSelectionWrapper<DataRow>
    {
        public DataTableWrapper(DataTable dataTable, string usePropertyAsDisplayName) : base(dataTable.Rows, false, usePropertyAsDisplayName) { }
    }
}
