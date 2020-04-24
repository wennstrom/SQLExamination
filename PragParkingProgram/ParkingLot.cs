using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace dbtentamenow
{
    class ParkingLot
    {
        public bool AddVehicle(Vehicle v)
        {
            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string add = "dbo.Vehicles_AddVehicleManualSpot";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd1 = new SqlCommand(add, con))
                    {
                        con.Open();
                        cmd1.Parameters.AddWithValue("@Regplate", v.RegPlate);
                        cmd1.Parameters.AddWithValue("@Type", v.VehicleType);
                        cmd1.Parameters.AddWithValue("@Location", v.Location);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }


        }
        public bool MoveVehicle(string RegPlate, int NewLocation)
        {
            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string move = "dbo.Vehicles_Move";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd1 = new SqlCommand(move, con))
                    {
                        con.Open();
                        cmd1.Parameters.AddWithValue("@RegPlate", RegPlate);
                        cmd1.Parameters.AddWithValue("@NewLocation", NewLocation);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool RemoveVehicle(string RegPlate)
        {
            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string remove = "dbo.Vehicles_CheckOut";

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd1 = new SqlCommand(remove, con))
                    {
                        con.Open();
                        cmd1.Parameters.AddWithValue("@RegPlate", RegPlate);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                }
                return true;
            }
            // returns false if transaction fails
            catch
            {
                return false;
            }


        }

        public bool FreeReg(string RegPlate)
        {
            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string cr = "SELECT (dbo.Vehicle_CheckRegPlate (@Regplate))";

            int sqlresult = 1;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd1 = new SqlCommand(cr, con))
                {
                    con.Open();
                    cmd1.Parameters.AddWithValue("@RegPlate", RegPlate);
                    sqlresult = (int)cmd1.ExecuteScalar();
                    con.Close();
                }
            }

            //(sqlresult, 0 = reg is free, 1 = taken)
            if (sqlresult == 0)
                return true;

            return false;



        }
        public bool ParkableSpot(string VehicleType, int Location)
        {
            int sqlresult = 1;

            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string fs = "SELECT(dbo.Vehicles_CheckSpot(@Location, @VehicleType))";

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand(fs, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@Location", Location);
                    cmd.Parameters.AddWithValue("@VehicleType", VehicleType);

                    sqlresult = (int)cmd.ExecuteScalar();
                    con.Close();
                }
            }
            //sqlresult ( 0 = spot is parkable | 1 = occupied )
            if (sqlresult == 0)
                return true;
            else
                return false;
        }

        public int GetSpot(string RegPlate)
        {
            int result = 200;

            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string fr = "SELECT(dbo.Vehicles_GetSpot(@RegPlate))";
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd1 = new SqlCommand(fr, con))
                    {
                        con.Open();
                        cmd1.Parameters.AddWithValue("@RegPlate", RegPlate);
                        result = (int)cmd1.ExecuteScalar();
                        con.Close();
                    }
                }
                return result;
            }
            catch
            {
                return 200;
            }


        }
        public string GetType(string RegPlate)
        {
            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string gt = "SELECT(dbo.Vehicles_GetType(@RegPlate))";

            string result;

            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd1 = new SqlCommand(gt, con))
                {
                    con.Open();
                    cmd1.Parameters.AddWithValue("@RegPlate", RegPlate);
                    result = (string)cmd1.ExecuteScalar();
                    con.Close();
                }
            }

            return result;
        }
        public DateTime GetArrival(string RegPlate)
        {
            DateTime result;

            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string gt = "SELECT(dbo.Vehicle_GetArrival(@RegPlate))";


            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd1 = new SqlCommand(gt, con))
                {
                    con.Open();
                    cmd1.Parameters.AddWithValue("@RegPlate", RegPlate);
                    result = (DateTime)cmd1.ExecuteScalar();
                    con.Close();
                }
            }
            return result;
        }
        public int GetCost(string RegPlate)
        {
            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string gc = "SELECT(dbo.Vehicles_GetCost(@RegPlate))";
            int result;
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand cmd1 = new SqlCommand(gc, con))
                    {
                        con.Open();
                        cmd1.Parameters.AddWithValue("@RegPlate", RegPlate);
                        result = (int)cmd1.ExecuteScalar();
                        con.Close();
                    }
                }
                return result;
            }
            catch
            {
                return -1;
            }

        }
        public Vehicle GetVehicle(string RegPlate)
        {
            int spot = GetSpot(RegPlate);
            string type = GetType(RegPlate);
            DateTime arrival = GetArrival(RegPlate);


            Vehicle result = new Vehicle(RegPlate, spot, type, arrival);

            return result;
        }

        public string VehicleInformation(string RegPlate)
        {
            StringBuilder result = new StringBuilder();
            Vehicle v = GetVehicle(RegPlate);
            int fee = GetCost(RegPlate);

            result.AppendLine($"        -- {RegPlate} --");
            result.AppendLine($"-----------------------------");
            result.AppendLine($"  Location: {v.Location}");
            result.AppendLine($"  Type: {v.VehicleType}");
            result.AppendLine($"  Arrival: {v.Arrival}");
            result.AppendLine($"  Parking fee: {fee}:-");


            return result.ToString();
        }
        public string ParkingLotOverview()
        {
            string cs = "Server=LAPTOP-HHTQ6VEC\\SQLEXPRESS; Database = PragueParking; Integrated security = true";
            string ov = "SELECT(dbo.GetSpotInfo(@Spot))";
            StringBuilder result = new StringBuilder();
            result.AppendLine("--  Prague Parking Overview  --\n-------------------------------");

                for (int i = 1; i <= 100; i++)
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        using (SqlCommand cmd1 = new SqlCommand(ov, con))
                        {
                            con.Open();
                            cmd1.Parameters.AddWithValue("@Spot", i);
                            string value = (string)cmd1.ExecuteScalar();
                            result.AppendLine($"    Spot {i}: {value}");
                            con.Close();
                        }
                    }
                }
                      
            return result.ToString();
        }
    }
}
