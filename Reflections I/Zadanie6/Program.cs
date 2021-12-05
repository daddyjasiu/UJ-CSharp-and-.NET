using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualBasic.CompilerServices;

namespace Zadanie6
{
    public class Customer
    {
        private string _name;
        protected int _age;
        public bool isPreferred;

        public Customer(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Customer name!");
            _name = name;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Address { get; set; }
        public int SomeValue { get; set; }

        public int ImportantCalculation()
        {
            return 1000;
        }

        public void ImportantVoidMethod()
        {
        }

        public enum SomeEnumeration
        {
            ValueOne = 1,
            ValueTwo = 2
        }

        public class SomeNestedClass
        {
            private string _someString;
        }

        public static void Main(string[] args)
        {
            Type myType = typeof(Customer);
            
            Console.WriteLine("Fields: ");
            //lista pól w klasie Pogrupowane względem dostępu
            
            var myFieldInfo = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            
            var fieldQuery = myFieldInfo
                .GroupBy(f => f.IsPublic);

            foreach (var i in fieldQuery)
            {
                foreach (var j in i)
                {
                    Console.Write($"type: {j.FieldType} name: {j.Name}, ");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("-- Public: ");
            //publiczne
            foreach (var i in fieldQuery)
            {
                foreach (var j in i)
                {
                    if (j.IsPublic)
                        Console.Write($"type: {j.FieldType} name: {j.Name}, ");
                }
            }

            Console.WriteLine();
            Console.WriteLine("-- Non Public: ");
            //niepubliczne
            foreach (var i in fieldQuery)
            {
                foreach (var j in i)
                {
                    if(!j.IsPublic)
                        Console.Write($"type: {j.FieldType} name: {j.Name}, ");
                }
            }
            Console.WriteLine();
            Console.WriteLine("\nMethods: ");
            //Lista metod
            var myMethodInfo = myType.GetMethods();
            foreach (var i in myMethodInfo)
            {
                Console.WriteLine(i.Name);
            }
            Console.WriteLine("\nNested types: ");
            //typy zagnieżdżone
            var myNestedInfo = myType.GetNestedTypes();
            foreach (var i in myNestedInfo)
            {
                Console.WriteLine(i.Name);
            }
            Console.WriteLine("\nProperties: ");
            //propercje
            var myPropertiesInfo = myType.GetProperties();
            foreach (var i in myPropertiesInfo)
            {
                Console.WriteLine(i.Name);
            }
            Console.WriteLine("\nMembers: ");
            //Członkowie
            var myMembersInfo = myType.GetMembers();
            foreach (var i in myMembersInfo)
            {
                Console.WriteLine(i.Name);
            }

            Customer customer = new Customer("Michal");
            Console.WriteLine("\nProperties before changes: ");
            Console.Write("{0}, {1}, {2}", customer.Name, customer.Address, customer.SomeValue);

            customer.GetType().GetProperty("Name").SetValue(customer, "Jasiu");
            customer.GetType().GetProperty("Address").SetValue(customer, "Krakow");
            customer.GetType().GetProperty("SomeValue").SetValue(customer, 20);
            
            Console.WriteLine("\nProperties after changes: ");
            Console.Write("{0}, {1}, {2}", customer.Name, customer.Address, customer.SomeValue);
        }
    }
}