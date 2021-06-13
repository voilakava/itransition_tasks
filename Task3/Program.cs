using System;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;
using System.Text;




namespace Task3
{
    class Program
    {

        static string WhoWon(int lenth, int comp_choice, int person_choice)
        {

            int[] tmp = new int[lenth];
            for (int i = 0; i < lenth; i++)
            {
                tmp[i] = i;
            }
            int KK = 0;

            if (person_choice > lenth / 2)
            {
                KK = lenth - (person_choice - lenth / 2);
            } else if (person_choice < lenth / 2)
            {
                KK =  lenth / 2 - (person_choice);
            } else
            {
                KK = 0;
            }

            int[] arrayNew = tmp.Skip(tmp.Length - KK).Take(KK).Concat(tmp.Take(tmp.Length - KK)).ToArray();


            if (Array.IndexOf(arrayNew, comp_choice) < Array.IndexOf(arrayNew, person_choice))
            {
                return "You win!"; 
            }
            
            if (comp_choice == person_choice)
            {
                return "Dead heat!";
            }
            else
            {
                return "Computer win!";
            }

        }


        static void show_elements(string[] options)
        {
            Console.WriteLine("Available moves:");
            for(int i = 0; i < options.Length; i++)
            {
                Console.WriteLine((i+1) + " - " + options[i]);
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine("Enter your move:");
        }


        static void Main(string[] args)
        {
            string[] options = args;

            if (options.Length < 3 || options.Length%2 ==0)
            {
                Console.WriteLine("The number of game elements must be odd and more than 2");
                Environment.Exit(0);
            }

            byte[] bytes = new byte[16];

            byte[] pc = new byte[4];

            RandomNumberGenerator random = RandomNumberGenerator.Create();

            
            random.GetBytes(bytes);

            var hmac = new HMACSHA256(bytes);

            Random r = new Random();
            int ipc = r.Next(0, options.Length-1);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(options[ipc]));


            Console.WriteLine("HMAC: " + (BitConverter.ToString(hash, 0)).Replace("-", ""));
              

            show_elements(options);

            int person_choice = Convert.ToInt32(Console.ReadLine());
            
                

            while (person_choice > options.Length || person_choice < 0 || person_choice > options.Length)
            {
                Console.WriteLine("You have entered a number that is out of range. Please try again");
                person_choice = Convert.ToInt32(Console.ReadLine());
            }


            if (person_choice == 0)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Your move:" + options[person_choice-1]);
                Console.WriteLine("Computer move:" + options[ipc]);
                Console.WriteLine(WhoWon(options.Length, ipc, person_choice - 1));
                
            }
            
            
            Console.WriteLine("HMAC key: " + BitConverter.ToString(bytes, 0).Replace("-", ""));

            


        }
    }
}
