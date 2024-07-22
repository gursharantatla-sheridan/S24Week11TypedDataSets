﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S24Week11TypedDataSets
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // table adapter
        NorthwindDataSetTableAdapters.ProductsTableAdapter adpProducts = new NorthwindDataSetTableAdapters.ProductsTableAdapter();

        // data table
        NorthwindDataSet.ProductsDataTable tblProducts = new NorthwindDataSet.ProductsDataTable();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadProducts()
        {
            // Fill method
            adpProducts.Fill(tblProducts);

            grdProducts.ItemsSource = tblProducts;
        }

        private void btnLoadAllProducts_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }
    }
}