using System;
using System.Collections;
using System.IO;

namespace ConsoleApp1
{
    class Client
    {
        public int id { get; set; }
        public string pnumber { get; set; }
        public double payment { get; set; }

        public Client(int id, string pnumber, double payment)
        {
            this.id = id;
            this.pnumber = pnumber;
            this.payment = payment;
        }

        public string ToStreamString()
        {
            return $"{id};{pnumber};{payment}";
        }

        public Client(String text)
        {
            string[] data = text.Split(";");

            this.id = Int32.Parse(data[0]);
            this.pnumber = data[1];
            this.payment = Double.Parse(data[2]);


        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ArrayList clients = new ArrayList();


            //string path = @"D:\Kwin";
            //string subpath = @"music";
            string fullpath = @"D:\Kwin\music\id.txt";
            int menu = -1;
            while (menu != 0)
            {
                Console.WriteLine("0. Exit the program\n" +
               "1. Enter a Person \n");
  
                  menu = Input();
                switch (menu)
                {
                    case 0:
                        break;
                    case 1:
                        int id = InputId();
                        string passNumber = InputPassportNumber();
                        double payment = InputPayment();

                        Client client = new Client(id, passNumber, payment);
                        clients.Add(client);
                        break;
                }
            }

              SaveToFile(clients); 
            
            
            

            try
            {
                using (StreamReader sr = new StreamReader(fullpath,System.Text.Encoding.Default))
                {
                    ArrayList clientsUpdated = new ArrayList();
                    string line;
                    while ((line = sr.ReadLine()) != "")
                    {
                        Client client = new Client(line);
                        client.payment += 3;

                        clientsUpdated.Add(client);
                        Console.WriteLine(client.ToStreamString());
                        SaveToFile(clients);
                        sr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
               
            }
        }



        static void SaveToFile(ArrayList clients)
        {
            string path = @"D:\Kwin";
            string subpath = @"music";

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(subpath);

            string fullpath = @"D:\Kwin\music\id.txt";
            //using (FileStream idnumbers = new FileStream(fullpath, FileMode.OpenOrCreate, FileAccess.ReadWrite)) ;
            FileStream idnumbers = null;
            try
            {
               idnumbers = new FileStream(fullpath, FileMode.OpenOrCreate, FileAccess.ReadWrite) ;
                idnumbers.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error:{ex.Message}");
            }
           
           
            string text = "";

            foreach (Client client in clients)
            {
                text += client.ToStreamString() + "\n";
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(fullpath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                    
                    sw.Close();
                }

               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        static int InputId()
        {
           

            Console.WriteLine($"Enter id number of  client");

            for (; ; )
            {
                int result;
                string message = Console.ReadLine();
                if (Int32.TryParse(message, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Enter correct id number");
                }
            }
        }
        static string InputPassportNumber()
        {
            
            string passportnumber;
            Console.WriteLine($"enter passport number");
           
            
           return   passportnumber = (Console.ReadLine());
             
             
               
        }

        static double InputPayment()
        {
            
            Console.WriteLine($"enter payment quantity");
            for (; ; )
            {
                double result;
                string message = Console.ReadLine();
                if (double.TryParse(message, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Enter correct payment");
                }
            }
        }

        public static int Input()
        {
            for (; ; )
            {
                int result;
                string message = Console.ReadLine();
                if (Int32.TryParse(message, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Enter number");
                }
            }
        }

    }
}
