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

namespace WpfApp18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Position> Data { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            var db = new MySqlTest();
            /*db.UpdatePosition(new Position { ID = 6, Title = "Обновленный админ",
             Description = ""});
            db.DeletePosition(new Position { ID = 7 });
            db.InsertPosition("новая' должность", "какая-то инфа");*/
            Data = db.GetPositions();
            DataContext = this;
        }
    }
}
