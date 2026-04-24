using CarShow_console_app;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace car_show_room_GUI
{
    public partial class Form1 : Form
    {
        Store myStore = new Store();
    
        BindingSource carInventoryBindingSource = new BindingSource(); // create a binding source for the car inventory
        BindingSource cartBindingSource = new BindingSource();// create a binding source for the shopping cart
      


        public Form1()
        {
            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_create_car_Click(object sender, EventArgs e)
        {
            Car c = new Car(txt_Make.Text, txt_Model.Text,decimal.Parse( txt_Price.Text), txt_Color.Text,int.Parse(txt_Size_of_Engine.Text));
            //MessageBox.Show(c.ToString());
            myStore.CarList.Add(c);
            carInventoryBindingSource.ResetBindings(false); // reset the bindings for the car inventory binding source
            ClearTextBoxes();
        }
        // This method clears the text boxes after a car is added to the inventory
        private void ClearTextBoxes() 
        {

            txt_Make.Text = "";
            txt_Model.Text = "";
            txt_Price.Text = "";
            txt_Color.Text = "";
            txt_Size_of_Engine.Text = "";

            // Set the focus back to the first text box for convenience
            txt_Make.Focus();
        }

        private void btn_add_to_cart_Click(object sender, EventArgs e)
        {
            // Get the selected car from the inventory list box
            Car selected =(Car) lst_inventory.SelectedItem;
            
            // add that item to the Cart
            myStore.ShoppingList.Add(selected);
            cartBindingSource.ResetBindings(false);
           

        }

        private void btn_cheakout_Click(object sender, EventArgs e)
        {
            decimal totalPrice = myStore.Checkout();
            lbl_total.Text ="$" +totalPrice.ToString(); // display the total price in the label with 2 decimal places
            clearShoppingCart();

        }

        private void clearShoppingCart()
        {
            
            myStore.ShoppingList.Clear(); // clear the shopping cart list
            cartBindingSource.ResetBindings(false); // reset the bindings for the shopping cart binding source
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          carInventoryBindingSource.DataSource = myStore.CarList;// set the data source of the car inventory binding source to the car inventory list in the store
            lst_inventory.DataSource = carInventoryBindingSource; // bind the car inventory list box to the car inventory binding source
            lst_inventory.DisplayMember = ToString();
             
          cartBindingSource.DataSource = myStore.ShoppingList;//set the data source of the shopping cart binding source to the shopping cart list in the store
            lst_cart.DataSource = cartBindingSource; // bind the shopping cart list box to the shopping cart binding source
            lst_cart.DisplayMember = ToString();
        }
    }
}
