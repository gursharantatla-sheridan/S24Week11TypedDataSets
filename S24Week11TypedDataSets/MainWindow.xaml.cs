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
        NorthwindDataSetTableAdapters.CategoriesTableAdapter adpCategories = new NorthwindDataSetTableAdapters.CategoriesTableAdapter();
        NorthwindDataSetTableAdapters.ProdsWithCatsTableAdapter adpProdsWithCats = new NorthwindDataSetTableAdapters.ProdsWithCatsTableAdapter();

        // data table
        NorthwindDataSet.ProductsDataTable tblProducts = new NorthwindDataSet.ProductsDataTable();
        NorthwindDataSet.CategoriesDataTable tblCategories = new NorthwindDataSet.CategoriesDataTable();
        NorthwindDataSet.ProdsWithCatsDataTable tblProdsWithCats = new NorthwindDataSet.ProdsWithCatsDataTable();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadProducts()
        {
            // Fill method
            //adpProducts.Fill(tblProducts);

            // Get method
            tblProducts = adpProducts.GetProducts();

            grdProducts.ItemsSource = tblProducts;
        }

        private void btnLoadAllProducts_Click(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var row = tblProducts.FindByProductID(id);

            if (row != null)
            {
                txtName.Text = row.ProductName;
                txtPrice.Text = row.UnitPrice.ToString();
                txtQuantity.Text = row.UnitsInStock.ToString();
            }
            else
            {
                txtName.Text = txtPrice.Text = txtQuantity.Text = "";
                MessageBox.Show("Invalid ID. Please try again.");
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            short quantity = short.Parse(txtQuantity.Text);

            adpProducts.Insert(name, price, quantity);

            LoadProducts();
            MessageBox.Show("New product added");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            short quantity = short.Parse(txtQuantity.Text);

            adpProducts.Update(name, price, quantity, id);

            LoadProducts();
            MessageBox.Show("Product updated");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            
            adpProducts.Delete(id);

            LoadProducts();
            MessageBox.Show("Product deleted");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;

            tblProducts = adpProducts.GetProductsByName(name);
            grdProducts.ItemsSource = tblProducts;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tblCategories = adpCategories.GetCategories();

            cmbCategories.ItemsSource = tblCategories;
            cmbCategories.DisplayMemberPath = "CategoryName";
            cmbCategories.SelectedValuePath = "CategoryID";
        }

        private void cmbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCategories.SelectedItem != null)
            {
                int catId = (int)cmbCategories.SelectedValue;

                tblProdsWithCats = adpProdsWithCats.GetProductsByCatId(catId);
                grdProducts.ItemsSource = tblProdsWithCats;
            }
        }
    }
}