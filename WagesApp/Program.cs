using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WagesApp;

public class Program
{
    // Global Variables
    // Constants
    public const decimal PAYRATE = 22.0m;
    public static readonly IReadOnlyCollection<decimal> TAXRATES = new List<decimal> { 0.075m, 0.08m }.AsReadOnly();
    public static readonly int BONUS = 5, BONUSCONDITION = 30;
    public static string payslips = "";
    public static string topEmployee = "";
    public static int employeeCounter = 0, topHoursWorked = -1;
    public static decimal totalWages = 0.0m;
    static readonly int[] IDMINMAX = {0, 5000};
    static readonly int[] HOURSMINMAX = { 0, 24 };






    static void Main(string[] args)
    {
        //Display App title screen
        Console.WriteLine("  _      __                    ___           \n | | /| / /__ ____ ____ ___   / _ | ___  ___ \n | |/ |/ / _ `/ _ `/ -_|_-<  / __ |/ _ \\/ _ \\\n |__/|__/\\_,_/\\_, /\\__/___/ /_/ |_/ .__/ .__/\n             /___/               /_/  /_/    ");

        Console.WriteLine("\n\nThis program helps the landscaping team manager quickly and accurately calculate each part‑time worker’s weekly pay. \nThe app takes the worker’s name and hours worked, applies the company’s pay rules, and automatically works out \nbonus hours, gross pay, tax, and final net pay. A clear payslip is then displayed, with an optional summary of\nall workers processed.");


        Console.WriteLine("\n\n Press Enter To Continue...");
        Console.ReadLine();

        Console.Clear();

        //Repeat until all OneEmployee() pay slips have been generated

        char continueInput = 'y';
        while(continueInput == 'y' || continueInput.Equals('y'))
        {
            Console.WriteLine(OneEmployee());

            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();

            Console.Clear();

           
            continueInput = CheckContinueInput("\n\n Do you want to process another employee? (y/n)");

            Console.Clear();
        }
        Console.WriteLine(payslips);

        Console.WriteLine("\n\n----------Payroll Summary----------");
        Console.WriteLine($"Total Employees Processed: {employeeCounter}");
        Console.WriteLine($"Total Wages Paid: {totalWages:C}");
        Console.WriteLine($"Top Employee: {topEmployee} with {topHoursWorked} hours worked");
        Console.WriteLine("---------------------------------------");
    }
    //colloect employee data and generate payslip
    public static string OneEmployee()

    {
        //declare local variables
        int id;
        string name, lastName;
        List<int> hoursWorked = new List<int>();
        List<string> DAYSOFWEEK = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        DAYSOFWEEK.AsReadOnly();
        List<string> QUESTIONS = new List<string> { "Enter Employee ID:", "Enter the Employee's firstname", "Enter the Employee's last name" };

        Console.WriteLine("---------- Add Employee ----------");
        

        //Capture employee data

        //recieve employee id
        
        id = CheckInt(QUESTIONS[0], IDMINMAX[0], IDMINMAX[1]);

        //recieve employee name
       
        name = CheckName(QUESTIONS[1]);

        //recieve employee last name
        
        lastName = CheckName(QUESTIONS[2]);

        //recieve hours worked (for each day of the week)
        foreach(string day in DAYSOFWEEK)
        {
            
            hoursWorked.Add(CheckInt($"Enter hours worked on {day}:", HOURSMINMAX[0], HOURSMINMAX[1]));
        }

        payslips += GeneratePaySlip(id, name, lastName, hoursWorked);

        //increase employee counter -> employeeCounter = employeeCounter + 1
        employeeCounter++;

        //add gross wages earned to total wages
        totalWages += CalculateWeeklyPay(hoursWorked) + CalculateBonus(hoursWorked);

        //check if hours worked is greater than current top hous worked if so update top employee and top hours
        if(hoursWorked.Sum() > topHoursWorked)
        {
            topEmployee = $"{name}  {lastName}";
            topHoursWorked = hoursWorked.Sum();

        }




        return "\n\nEmployee processed";
         


        

        

    
        
    }
    //Calculate Weekly pay
    static decimal CalculateWeeklyPay(List<int> hoursWorked)
    {
        decimal weeklyPay = 0.0m;
        int totalHours = hoursWorked.Sum();
        weeklyPay = totalHours * PAYRATE;


        return weeklyPay;
    }
    //Calculate Bonus
    static decimal CalculateBonus(List<int> hoursWorked)
    {
        

        if(hoursWorked.Sum()>= BONUSCONDITION)
        {
            return BONUS * PAYRATE;
        }

        return 0;
    }
    //Calculate Tax
    static decimal CalculateTax(List<int> hoursWorked)
    {
        decimal tax = 0;

        //calculate total wages (including bonus)
        if(CalculateWeeklyPay(hoursWorked) + CalculateBonus(hoursWorked) < 450)
        {
            tax = (CalculateWeeklyPay(hoursWorked) + CalculateBonus(hoursWorked)) * TAXRATES.ElementAt(0);

        }
        else
        {
            tax = (CalculateWeeklyPay(hoursWorked) + CalculateBonus(hoursWorked)) * TAXRATES.ElementAt(1);
        }
            return tax;
    }
    // Generate Payslip
    private static string GeneratePaySlip(int id, string name, string surname, List<int> hoursWorked)
    {
        string paySlip = "";

        paySlip += "----------Payslip----------\n";

        paySlip += $"Employee ID: {id}\n";
        paySlip += $"Employee Name: {name} {surname}\n";
        paySlip += $"Hours Worked: {hoursWorked.Sum()}\n";
        paySlip += $"Gross Income: {CalculateWeeklyPay(hoursWorked) + CalculateBonus(hoursWorked):C}\n";
        paySlip += $"Tax:{CalculateTax(hoursWorked):C}\n";
        paySlip += $"Net Income: {CalculateWeeklyPay(hoursWorked) + CalculateBonus(hoursWorked) - CalculateTax(hoursWorked):C}\n";

        paySlip += "---------------------------\n\n";



        return paySlip;
    }

    // check if a name is lowercase and cinvert to title case if necessary
    static string CheckName(string question)
    {

        while (true)
        {
            // ask user for name input
            Console.WriteLine(question);

            string nameInput = Console.ReadLine();
            // check if name input is alphabetic charecters and '-' only
            if (Regex.IsMatch(nameInput, @"^[A-Za-z-]+$"))
            {
                nameInput = nameInput[0].ToString().ToUpper() + nameInput.Substring(1);

                return nameInput;
            }
            else
            {
                Console.WriteLine("Error: Names can only contain alphabetic characters and '-' ");
            }

           


        }


    }

    //check if user input is a number between a min and max value
    static int CheckInt(string question, int min, int max)
    {
        while (true)
        {

            try
            {
                Console.WriteLine(question);
                int userInput = Convert.ToInt32(Console.ReadLine());

                //check if user input is between minand max value
                if (userInput >= min && userInput <= max)
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine($"Please enter a number between {min} and {max}");
                }


            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: You must enter an number between {min} and {max}.");
                Console.ForegroundColor = ConsoleColor.Black;

            }


        }
    }

    static char CheckContinueInput(string question)
    {
        while (true)
        {
            string userInput;
            Console.WriteLine(question);
            userInput = Console.ReadLine();

            //check if userInput is not empty and is either 'y' or 'n'
            if (!string.IsNullOrEmpty(userInput) && Regex.IsMatch(userInput, "^[yYnN]$"))
            {
                return userInput.ToLower()[0];
            }
            else
            {
                Console.WriteLine("Error: Only 'y' or 'n' is accepted");
            }

        }
        
    }

}

