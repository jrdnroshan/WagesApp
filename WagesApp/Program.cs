namespace WagesApp;

public class Program
{
    // Global Variables
    // Constants
    public const decimal PAYRATE = 22.0m;
    public static readonly IReadOnlyCollection<decimal> TAXRATES = new List<decimal> { 0.075m, 0.08m }.AsReadOnly();
    public static readonly int BONUS = 5, BONUSCONDITION = 30;






    static void Main(string[] args)
    {
        //Display App title screen
        Console.WriteLine("  _      __                    ___           \n | | /| / /__ ____ ____ ___   / _ | ___  ___ \n | |/ |/ / _ `/ _ `/ -_|_-<  / __ |/ _ \\/ _ \\\n |__/|__/\\_,_/\\_, /\\__/___/ /_/ |_/ .__/ .__/\n             /___/               /_/  /_/    ");

        //Repeat until all OneEmployee() pay slips have been generated

    }
    //colloect employee data and generate payslip
    public static void OneEmployee()
    {

    }


}

