﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBO.ViewModel;

namespace DBO.View
{
    /// <summary>
    /// Логика взаимодействия для ViewGoodsPage.xaml
    /// </summary>
    public partial class Options : Page
    {
        public Options()
        {
            InitializeComponent();
            //var optionsViewModel = DataContext as OptionsViewModel;
            //if (optionsViewModel == null) return;
            //if(optionsViewModel.OptionsFrameSource == null)
            //    optionsViewModel.OptionsFrameSource = "ViewsOptions/ViewInterfesPage.xaml";
        }
    }
}
