﻿using InteractWPF.DbConnect;
using InteractWPF.View.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractWPF.Interfaces
{
   public interface IDataService
    {
        MsSQLUserInfo msSQLUserInfo { get; set; }
    }
}
