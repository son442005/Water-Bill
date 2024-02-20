using System.Collections.Generic;

namespace WaterBill;

class Program
{
    const double HOUSEHOLD_FIRST_PRICE = 5973;
    const double MAX_HOUSEHOLD_FIRST_PRICE = 59730;
    const double HOUSEHOLD_SECOND_PRICE = 7052;
    const double MAX_HOUSEHOLD_SECOND_PRICE = 70520;
    const double HOUSEHOLD_THIRD_PRICE = 8699;
    const double MAX_HOUSEHOLD_THIRD_PRICE = 86990;
    const double HOUSEHOLD_FOURTH_PRICE = 15929;
    const double AGENCY_PRICE = 9955;
    const double PRODUCTION_PRICE = 11615;
    const double BUSINESS_PRICE = 22068;

    // Define constants and arrays to store customer data

    static int TypeOfCustomer() 
    {
        Console.WriteLine("Type a customer: \n\t1. Household customer" +
                "                               \n\t2. Administrative agency, public services" +
                "                               \n\t3. Production units" +
                "                               \n\t4. Business services");
        int type;
        while (true)
        {
            Console.Write("\nEnter your type (1-4): ");
            type = int.Parse(Console.ReadLine());
            if (type >= 1 && type <= 4)
                break;
            Console.Write("Invalid customer type. Please enter a number from 1 to 4.");
        }
        return type;
    }

    // Calculate consumption
    static double AmountOfConsumption(ref double lastWaterMeter, ref double thisWaterMeter)
    {
        Console.Write("Enter last month’s water meter readings: ");
        lastWaterMeter = double.Parse(Console.ReadLine());
        //Check if thisWaterMeter < lastWaterMeter
        do
        {
            Console.Write("Enter this month’s water meter readings: ");
            thisWaterMeter = double.Parse(Console.ReadLine());
            if (thisWaterMeter < lastWaterMeter)
                Console.WriteLine("This month's water meter reading should be greater than last month's. Please re-enter.");
        }

        while (thisWaterMeter < lastWaterMeter);

        double consumption = thisWaterMeter - lastWaterMeter;

        return consumption;
    }

    // Calculate prices based on customer type
    static double Price(int type, double consumption)
    {
        double price = default;

        switch (type)
        {
            case 1:
                if (consumption > 0 && consumption <= 10)
                {
                    price = consumption * HOUSEHOLD_FIRST_PRICE;

                }
                else if (consumption > 10 && consumption <= 20)
                {
                    price = (consumption - 10 ) * HOUSEHOLD_SECOND_PRICE + MAX_HOUSEHOLD_FIRST_PRICE;
                }
                else if (consumption > 20 && consumption <= 30)
                    price = ( consumption - 20 ) * HOUSEHOLD_THIRD_PRICE + MAX_HOUSEHOLD_SECOND_PRICE + MAX_HOUSEHOLD_FIRST_PRICE;
                else
                    price = ( consumption - 30 ) * HOUSEHOLD_FOURTH_PRICE + MAX_HOUSEHOLD_THIRD_PRICE + MAX_HOUSEHOLD_SECOND_PRICE + MAX_HOUSEHOLD_FIRST_PRICE;
                break;
            case 2:
                price = consumption * AGENCY_PRICE;
                break;
            case 3:
                price = consumption * PRODUCTION_PRICE;
                break;
            case 4:
                price = consumption * BUSINESS_PRICE;
                break;
            default:
                return 0;
        }
        return price;
    }
    // Calculate totalBill based on price, VAT and ENVIROMENT fee
    static double CalculatWaterBill(double price)
    {
        double environmentFees = price / 10;

        double fee = price / 10;

        double totalBill = price + environmentFees + fee;

        Console.WriteLine($"Total water bill: {totalBill} VND");

        return totalBill;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("================= WATER BILLING PROGRAM =================\n");
        Console.Write("Enter your name: ");
        string customerName = Console.ReadLine();
        Console.WriteLine($"\nWelcome {customerName} to the water billing program!!\n");
        Console.WriteLine("***CALCULATE WATER BILL***");
        int type = TypeOfCustomer();
        double lastWaterMeter = 0;
        double thisWaterMeter = 0;
        double consumption = AmountOfConsumption(ref lastWaterMeter, ref thisWaterMeter);
        double price = Price(type, consumption);
        Console.Write("=======WATER BILLING=======\n");
        Console.Write($"Customer name: {customerName}\n");
        Console.Write($"Last month's water meter readings: {lastWaterMeter}\n");
        Console.Write($"This month's water meter readings: {thisWaterMeter}\n");
        Console.Write($"Amount of consumption: {thisWaterMeter - lastWaterMeter}\n");
        CalculatWaterBill(price);
    }
}
