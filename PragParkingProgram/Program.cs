namespace dbtentamenow
{
    using System;
    using System.Text;

    namespace DBTentamen
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.ForegroundColor
                    = ConsoleColor.Green;


                Console.WriteLine("Welcome to Prague Parking 3.0");
                Console.ReadLine();


                while (Menu());


                Console.ForegroundColor
                    = ConsoleColor.Green;
                Console.WriteLine("Thank you for using Prague Parking 3.0");
                Console.ReadLine();
            }
            static bool Menu()
            {
                Console.ForegroundColor
                    = ConsoleColor.Blue;
                Console.Clear();

                StringBuilder menu = new StringBuilder();
                menu.AppendLine("  What would you like to do?");
                menu.AppendLine("-------------------------------");
                menu.AppendLine("   [1]. Park Vehicle");
                menu.AppendLine("   [2]. Move Vehicle");
                menu.AppendLine("   [3]. Check out Vehicle");
                menu.AppendLine("   [4]. Show Vehicle");
                menu.AppendLine("   [5]. Show ParkingLot");
                menu.AppendLine("   [0]. Exit");

                Console.WriteLine(menu);
                int options = int.Parse(Console.ReadLine());
               
                    switch (options)
                    {
                        case 1:
                            while (AddVehicle()) ;
                            return true;

                        case 2:
                            while (MoveVehicle()) ;
                            return true;
                        case 3:

                            while (RemoveVehicle()) ;
                            return true;
                        case 4:
                            while (VehicleInformation()) ;
                            return true;
                        case 5:
                            ParkingSpotOverview();
                            return true;
                        case 0:

                            return false;
                        default:
                            Console.Clear();
                            Console.WriteLine("Error! You have to enter an integer between 0-5. \nPress any key to try again...");
                            Console.ReadLine();
                            return true;
                    }
                
                


            }
            static bool AddVehicle()
            {
                ParkingLot pl = new ParkingLot();

                Console.WriteLine("Enter the registration plate for the vehicle you would like to park \n[Min 3 Characters - Max 10]\n Enter: ");
                string RegPlate = Console.ReadLine().ToUpper();
                string VehicleType;

                if (RegPlate.Length > 10 || 3 > RegPlate.Length)
                {
                    Console.Clear();
                    Console.WriteLine("The entered Vehicle has the wrong length. \n Would You like to try again? \n [1] Yes \n [2] No \n Enter: ");
                    int option = int.Parse(Console.ReadLine());
                    try
                    {
                        switch (option)
                        {
                            case 1:
                                AddVehicle();

                                return true;
                            case 2:
                                return false;
                            default:
                                return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                if (pl.FreeReg(RegPlate) == false)
                {
                    Console.Clear();
                    Console.WriteLine("The Registration Plate you entered is already parked. \nPress any key to go back to the main menu");
                    Console.ReadLine();
                }
                Console.Clear();
                Console.WriteLine("Enter the VehicleType \n [1] MC \n [2] CAR \n Enter: ");
                int answer = int.Parse(Console.ReadLine());
                try
                {
                    switch (answer)
                    {
                        case 1:
                            VehicleType = "mc";
                            break;
                        case 2:
                            VehicleType = "car";
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("ERROR! You entered the wrong value. \nPress any key to get back to the main menu");
                            Console.ReadLine();
                            return false;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("ERROR! You entered the wrong value. \nPress any key to get back to the main menu. . .");
                    Console.ReadLine();
                    return false;
                }
                bool manual;
                try
                {
                    Console.Clear();
                    Console.WriteLine("Would you like to use a desired location or get the next available? " +
                        "\n [1]. Choose Manualy \n [2]. Next Available \n Enter: ");
                    int opt = int.Parse(Console.ReadLine());

                    switch (opt)
                    {
                        case 1:
                            manual = true;
                            break;
                        case 2:
                            manual = false;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("You entered a wrong value, press any key to get back to the main menu. . . ");
                            Console.ReadLine();
                            return false;

                    }
                }
                catch
                {
                    Console.WriteLine("You entered a wrong value, press any key to get back to the main menu. . . ");
                    Console.ReadLine();
                    return false;
                }
                int Location;

                if (manual)
                {
                    Console.Clear();
                    Console.WriteLine($"Enter the desired location for {RegPlate}. \n [1-100] \n Enter: ");
                    Location = int.Parse(Console.ReadLine());

                    if (pl.ParkableSpot(VehicleType, Location))
                    {
                        Vehicle v = new Vehicle(RegPlate, Location, VehicleType, DateTime.Now);
                        if (pl.AddVehicle(v))
                        {
                            Console.WriteLine($"Your {v.VehicleType} {v.RegPlate} is now parked at spot {v.Location} from {v.Arrival}.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("ERROR, The vheicle could not be parked. \nPress any key to try again. . .");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"The entered location ({Location}) is already occupied. Press any key to get back to the main menu. . .");
                        Console.ReadLine();
                        return false;
                    }
                }
                else
                {
                    Location = pl.GetSpot(RegPlate);

                    if (Location == 200)
                    {
                        Console.WriteLine("There are no available spot left. Please remove a vehicle inorder to park a new one. " +
                            "\nPress any key to continue. . .");
                        Console.ReadLine();
                        return false;
                    }

                    Vehicle v = new Vehicle(RegPlate, Location, VehicleType, DateTime.Now);

                    pl.AddVehicle(v);

                    Console.WriteLine($"The {v.VehicleType} {v.RegPlate} is now parked at spot {v.Location} from {v.Arrival}");
                    Console.ReadLine();
                }
                return false;
            }
            static bool RemoveVehicle()
            {
                ParkingLot pl = new ParkingLot();
                Console.Clear();
                Console.WriteLine("Enter the regplate of the vehicle that you would like to remove (3-10 letters)");
                string RegPlate = Console.ReadLine().ToUpper();

                if (RegPlate.Length < 3 || 10 < RegPlate.Length)
                {
                    Console.Clear();
                    Console.WriteLine("Error! \nThe RegPlate you entered has the wrong length ");
                    Console.ReadLine();
                    return false;
                }

                if (pl.FreeReg(RegPlate))
                {
                    Console.Clear();
                    Console.WriteLine("Error! \nThe RegPlate you entered could not be found");
                    Console.ReadLine();
                    return false;
                }

                Vehicle v = pl.GetVehicle(RegPlate);
                int fee = pl.GetCost(RegPlate);

                if (pl.RemoveVehicle(v.RegPlate))
                {
                    Console.Clear();
                    Console.WriteLine($"The Vehicle {v.RegPlate} has now been checked out of Prague Parking. " +
                        $"\nThe {v.VehicleType} arrived {v.Arrival} for a parking fee of {fee} KR");
                    Console.ReadLine();
                    return false;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Error! \nThe {v.VehicleType} could not be removed. \nPlease try again. ");
                    Console.ReadLine();
                    return false;
                }
            }
            static bool MoveVehicle()

            {
                ParkingLot pl = new ParkingLot();
                Console.Clear();
                Console.WriteLine("Enter the registration plate for the vehicle you would like to park \n[Min 3 Characters - Max 10]\n Enter: ");
                string RegPlate = Console.ReadLine().ToUpper();



                if (RegPlate.Length > 10 || 3 > RegPlate.Length)
                {
                    Console.Clear();
                    Console.WriteLine("The entered Vehicle has the wrong length. \nPress any key to go back to the main menu. . .");
                    Console.ReadLine();
                    return false;

                }
                bool moveable = false;


                if (pl.FreeReg(RegPlate))
                {
                    Console.Clear();
                    Console.WriteLine("ERROR! \nThe entered regplate could not be found.");
                    Console.ReadLine();
                    return false;
                }


                Vehicle v = pl.GetVehicle(RegPlate);
                bool manual;

                try
                {
                    Console.Clear();
                    Console.WriteLine("Would you like to choose a new spot manually or get the first available? " +
                        "\n[1]. Manual \n[2]. First Available \nEnter: ");
                    int selection = int.Parse(Console.ReadLine());

                    switch (selection)
                    {
                        case 1:
                            manual = true;
                            break;
                        case 2:
                            manual = false;
                            break;
                        default:
                            Console.WriteLine("Wrong Input! \nThe selection has to be entered between 1-2 ");
                            return false;

                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("ERROR! \n The selection has to be entered as a integer. . .");
                    Console.ReadLine();
                    return false;
                }

                int newlocation = 200;

                if (manual)
                {
                    Console.Clear();
                    try
                    {
                        Console.WriteLine($"Enter the desired location for {v.RegPlate}.  ");
                        newlocation = int.Parse(Console.ReadLine());

                        if (newlocation < 1 || 100 < newlocation)
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong Input! \nThe Location you entered does not exists please try again");
                            Console.ReadLine();
                            return false;
                        }
                        if (!pl.ParkableSpot(v.VehicleType, newlocation))
                        {
                            Console.Clear();
                            Console.WriteLine($"The desired location {newlocation} is already occupied. . .");
                            Console.ReadLine();
                            return false;
                        }
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Error! \n The input has to be entered as an integer between 1-2");
                        Console.ReadLine();
                        return false;
                    }
                }
                else
                {
                    newlocation = pl.GetSpot(v.RegPlate);

                    if (newlocation == 200)
                    {
                        Console.Clear();
                        Console.WriteLine("Could not find a free spot \nPlease remove a vehicle and try again.");
                        Console.ReadLine();
                        return false;
                    }
                }

                pl.MoveVehicle(v.RegPlate, newlocation);

                Console.Clear();
                Console.WriteLine($"The {v.VehicleType} with regplate {v.RegPlate} has been moved to spot {newlocation}. ");
                Console.ReadLine();

                return false;
            }
            static bool VehicleInformation()
            {
                Console.Clear();
                ParkingLot pl = new ParkingLot();
                Console.WriteLine("Enter the RegPlate to get information");
                string RegPlate = Console.ReadLine().ToUpper();

                if (pl.FreeReg(RegPlate))
                {
                    Console.Clear();
                    Console.WriteLine($"The entered RegPlate ({RegPlate}) could not be found. " +
                        $"\nPress any key to get back to the menu. . .");
                    Console.ReadLine();
                    return false;
                }
                else
                {
                    Console.Clear();
                    string result = pl.VehicleInformation(RegPlate);
                    Console.WriteLine(result);
                    Console.ReadLine();
                    return true;
                }

            }
            static void ParkingSpotOverview()
            {
                
                ParkingLot pl = new ParkingLot();
                string output = pl.ParkingLotOverview();

                Console.Clear();
                Console.WriteLine(output);
                Console.ReadLine();                
            }
        }
    }
}
