
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;


namespace CarShow_console_app

{

    internal class Program
    {
        static void RunMyLogic()  // دي المفروض داله main بس انا غيرت اسمها عشان اقدر اعمل تجريب عليها قبل ما اشغل البرنامج
        {
            Store s = new Store();   // I create a new instance of the Store class to manage the car inventory and shopping cart (I will reuse it in swich case 1 )
            Console.WriteLine("Welcome to the car store.Frist you must create som car inventory.Then you may add some cars to the shopping cart.finally you may checkout which willl give you a total value of the shopping cart. ");
            Console.WriteLine();

            int action = chooseAction();
            while(action != 0 )
            {
                Console.WriteLine("You choose "+action);
                switch (action)
                {
                    // case 1: add a new car to inventory
                    case 1:                                                            
                        Console.WriteLine("You chose to add a new car to inventory");
                        string carmake, carmodel,carcolor;
                        int car_size_of_engine;
                        Console.WriteLine("What is the car make? ford, gm, nissan etc.");
                        carmake = Console.ReadLine();

                        Console.WriteLine("What is the car model? corvetta, focus, ranger etc.");
                        carmodel = Console.ReadLine();

                        int carprice; // مسؤلة عن التأكيد اذا كان اللي داخل دا رقم ولا نص 
                        Console.WriteLine("What is the car price?");

                        while (!int.TryParse(Console.ReadLine(), out carprice))
                        {
                            Console.WriteLine("Invalid input! Please enter a number, not text:");
                        }
                        Console.WriteLine("Great! The price is saved as: " + carprice);



                        Console.WriteLine("What is the car color? Red, Blue, Black etc.");
                        carcolor = Console.ReadLine();

                        Console.WriteLine("What is the size of the car's engine?(e.g., 2.0L, 3.5L)");
                        while(!int.TryParse(Console.ReadLine(), out car_size_of_engine))
                        {
                            Console.WriteLine("Invalid input! Please enter an integer number:");
                        }
                        Console.WriteLine("Great! The engine size is saved as: " + car_size_of_engine);


                        Car newcar = new Car(carmake, carmodel, carprice,carcolor,car_size_of_engine);
                       
                        s.CarList.Add(newcar);

                        Printinventory(s);
                        break;

                    // case 2: add to cart
                    case 2:
                        Console.WriteLine("You chose to add a car to the shopping cart");
                        Printinventory(s);
                        Console.WriteLine("Which car would you like to buy ? (number)");
                        int carchosen = int.Parse(Console.ReadLine());
                        while (carchosen >= s.CarList.Count || carchosen < 0) { 
                            Console.WriteLine("Invalid input! Please enter a number between 0 and " + (s.CarList.Count - 1) + ":");
                            carchosen = int.Parse(Console.ReadLine());
                        }

                        s.ShoppingList.Add(s.CarList[carchosen]);

                        Print_ShoppingCart(s);


                        break;
                    // case 3: checkout
                    case 3:
                        Print_ShoppingCart(s);
                        Console.WriteLine("The total cost of your items is : " +s.Checkout());
                        break;

                    default:
                        break;
                }
                action = chooseAction();
            }
        }


        private static void Print_ShoppingCart(Store s) 
        {
            Console.WriteLine("Cars you have choosen to buy: ");
            for(int i = 0; i<s.ShoppingList.Count;i++)
            {
                Console.WriteLine("Car # " + i + " " + s.ShoppingList[i]); 
            }
        }

        private static void Printinventory(Store s)
        {
            for(int i = 0; i<s.CarList.Count;i++)
            {
                Console.WriteLine("Car # " + i + " " + s.CarList[i]); // print the details of each car in the inventory using the overridden ToString method of the Car class in 114 line.
            }
                
        }

        static public int chooseAction() // method to prompt the user to choose an action and return the chosen action as an integer
        {
            int choice = 0;
            Console.WriteLine("Choose an action (0) to quit ,(1) to add a new car to inventory ,(2) add car to cart ,(3) cheakout");
            choice = int.Parse(Console.ReadLine());
            return choice;
        }

        
    }


    public class Car // class to represent a car with properties for make, model and price
    {
        public string Make { get; set; }         // Property for the car's make (e.g., Toyota, Ford)
        public string Model { get; set; }       // Property for the car's model (e.g., Camry, Mustang)
        public decimal Price { get; set; }          // Property for the car's price
        public string Color { get; set; }          // Property for the car's color (e.g., Red, Blue)
        public int size_of_engine { get; set; }          // Property for the size of the car's engine (e.g., 2.0L, 3.5L)
        public Car() // default constructor
        {
            Make = "Nothing yet";
            Model = "Nothing yet";
            Price = 0.00m;
        }
        public Car(string Make, string Model, decimal Price) // parameterized constructor
        {
            this.Make = Make;
            this.Model = Model;
            this.Price = Price;
        }
        public Car(string Make, string Model, decimal Price, string Color, int size_of_engine) // parameterized constructor with color and engine size
        {
            this.Make = Make;
            this.Model = Model;
            this.Price = Price;
            this.Color = Color;
            this.size_of_engine = size_of_engine;
        }
        public override string ToString() // override the ToString method to provide a string representation of the car
        {
            return " "+Make+" - "+Model+" - "+Price+" - "+Color+" - "+size_of_engine+"L";
        }
    }


    public class Store // class to represent a car store with properties for the list of cars in the store and the list of cars in the q
    {
        public List<Car> CarList { get; set; }  // Property for the list of cars in the store
        public List<Car> ShoppingList { get; set; } // Property for the list of cars in the shopping cart


        public Store() // default constructor
        {
            CarList = new List<Car>();
            ShoppingList = new List<Car>();
        }
        public decimal Checkout()  // method to calculate the total price of the cars in the shopping cart
        {
            decimal totalPrice = 0.00m;
            foreach (var car in ShoppingList)
            {
                totalPrice += car.Price; // add the price of each car in the shopping cart to the total price
            }
            ShoppingList.Clear(); // clear the shopping cart after checkout
            return totalPrice; // return the total price of the cars in the shopping cart
        }





    }

}
