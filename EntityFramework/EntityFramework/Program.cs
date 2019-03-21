using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Entities;
using ORM;

namespace EntityFramework
{



    class Program
    {
        static string[] Airports =

        {

           "Berlin", "Frankfurt", "Munich", "Hamburg", "Chicago", "Rome", "London", "Paris", "Milan", "Prague", "Moscow",

           "New York/JFC", "Seattle", "Berlin", "Kapstadt", "Madrid", "Oslo", "Dallas", "Vienna"

        };

        static string[] Surnames =

        {

           "Müller", "Schmidt", "Schneider", "Fischer", "Weber", "Meyer", "Wagner", "Becker", "Schulz", "Hoffmann", "Schäfer",

           "Koch", "Bauer", "Richter", "Klein", "Wolf", "Schröder", "Neumann", "Schwarz", "Zimmermann"

        };


        static string[] Firstnames =

         {"Leon", "Hannah", "Lukas", "Anna", "Leonie", "Marie", "Niklas", "Sarah", "Jan", "Laura", "Julia", "Lisa", "Kevin"};



        static string[] PilotsNachnamen =

        {

           "Gysi", "Seehofer", "Wagenknecht", "Steinmeyer", "Schulz", "Merkel", "Lindner", "Gabriel", "Lafontaine", "Özdemir",

           "Roth"

        };



        static string[] PilotsVornamen =

        {

           "Horst", "Sahra", "Martin", "Angela", "Joschka", "Christian", "Gregor", "Sigmar", "Frank-Walter", "Oskar", "Cem",

           "Claudia"

        };



        const int NO_OF_FLIGHTS = 100;

        const int NO_OF_PASSENGERS = 20;

        const int NO_OF_PILOTS = 5;



        static Random rnd = new Random(DateTime.Now.Millisecond);



        public static void Run(bool delete = false)
        {


            ExampleContext ctx = new ExampleContext();



            Console.WriteLine("Number of Flights: " + ctx.Flight.Count());
            Console.WriteLine("Number of Pilots: " + ctx.Pilot.Count());
            Console.WriteLine("Number of Passengers: " + ctx.Passenger.Count());
            Console.WriteLine("Number of FlightPassengers: " + ctx.FlightPassenger.Count());


            if (ctx.Flight.Any() || ctx.Pilot.Any() || ctx.Passenger.Any() || ctx.FlightPassenger.Any())
            {

                if (!delete) return;


                var key = Console.ReadKey();
                if (key.Key != ConsoleKey.Y)
                {
                    Environment.Exit(1);
                }
            }
            
            if (delete)
            {
                ctx.Database.ExecuteSqlCommand("Delete from Operation.Flight_Passenger");
                ctx.Database.ExecuteSqlCommand("Delete from Operation.Flight");
                ctx.Database.ExecuteSqlCommand("Delete from People.Passenger");
                ctx.Database.ExecuteSqlCommand("Delete from People.Pilot");
                ctx.Database.ExecuteSqlCommand("Delete from People.Employee");

            }

            Console.WriteLine("Number of Flights: " + ctx.Flight.Count());
            Console.WriteLine("Number of Pilots: " + ctx.Pilot.Count());
            Console.WriteLine("Number of Passengers: " + ctx.Passenger.Count());
            Console.WriteLine("Number of FlightPassengers: " + ctx.FlightPassenger.Count());

            Init_Passenger(ctx);
            Init_Pilot(ctx);
            Init_Flight(ctx);
            Init_FlightPassenger();

        }


        private static Random Init_Flight(ExampleContext ctx)

        {

            Console.WriteLine("Create Flights...");



            var Flight = from f in ctx.Flight select f;
            int Start = 100;

            Pilot[] PilotArray = ctx.Pilot.ToArray();



            for (int i = Start; i < (Start + NO_OF_FLIGHTS); i++)

            {

                if (i % 100 == 0)

                {

                    Console.WriteLine("Flight #" + i);

                    try

                    {

                        ctx.SaveChanges();

                    }

                    catch (Exception ex)

                    {

                        Console.WriteLine("ERROR: " + ex.Message.ToString());

                    }

                }

                Flight flightNew = new Flight();

                flightNew.FlightNo = i;

                flightNew.Departure = Airports[rnd.Next(0, Airports.Length - 1)];

                flightNew.Destination = Airports[rnd.Next(0, Airports.Length - 1)];

                ;

                if (flightNew.Departure == flightNew.Destination)

                {

                    i--;

                    continue;

                } // Keine Rundflüge!

                flightNew.FreeSeats = Convert.ToInt16(new Random(i).Next(250));

                flightNew.Seats = 250;



                flightNew.FlightDate = DateTime.Now.AddDays((double)flightNew.FreeSeats).AddMinutes((double)flightNew.FreeSeats * 7);

                flightNew.PilotPersonId = PilotArray[rnd.Next(PilotArray.Count() - 1)].PersonId;



                ctx.Flight.Add(flightNew);

            }

            ctx.SaveChanges();





            var f101 = ctx.Flight.Find(101);

            f101.Departure = "Berlin";

            f101.Destination = "Seattle";

            ctx.SaveChanges();





            return rnd;

        }



        private static void Init_FlightPassenger()

        {

            ExampleContext ctx = new ExampleContext();

            int k = 0;

            Random rnd5 = new Random();

            Console.WriteLine("Create FlightPassengers...");

            var Flight2 = from f in ctx.Flight select f;



            Passenger[] passengerArray = ctx.Passenger.ToArray();

            foreach (Flight f in Flight2.ToList())

            {

                k++;

                if (k % 100 == 0) Console.WriteLine(k);



                Console.WriteLine("Creating bookings for flight " + f.FlightNo);

                for (int j = 1; j < 10; j++)

                {

                    Random rnd2 = new Random(DateTime.Now.Millisecond);

                    // Randomly choose a passenger

                    Passenger p = passengerArray[Convert.ToInt16(rnd5.Next(passengerArray.Count()))];



                    if (!ctx.FlightPassenger.Any(b => b.FlightFlightNo == f.FlightNo && b.PassengerPersonId == p.PersonId))

                    {

                        FlightPassenger b = null;

                        try

                        {

                            b = new FlightPassenger();

                            b.FlightFlightNo = f.FlightNo;

                            b.PassengerPersonId = p.PersonId;

                            // Add FlightPassenger 

                            ctx.FlightPassenger.Add(b);

                            ctx.SaveChanges();

                        }

                        catch (Exception ex)

                        {

                            Console.WriteLine(ex.Message);

                            f.FlightPassenger.Remove(b);

                        }

                    }

                }

            }


        }



        private static void Init_Pilot(ExampleContext ctx)

        {

            Console.WriteLine("Create pilots...");



            for (int PNummer = 1; PNummer <= NO_OF_PILOTS; PNummer++)

            {

                string Vorname = PilotsVornamen[rnd.Next(0, PilotsVornamen.Length - 1)];

                ;

                string Nachname = PilotsNachnamen[rnd.Next(0, PilotsNachnamen.Length - 1)];

                ;

                Pilot pi = new Pilot();

                Employee e = new Employee();

                Person p = new Person();

                p.Surname = Nachname;

                p.GivenName = Vorname;

                p.Birthday = new DateTime(1940, 1, 1).AddDays(Convert.ToInt32(new Random(DateTime.Now.Millisecond).Next(20000)));

                ctx.Person.Add(p);

                ctx.SaveChanges();

                pi.PersonId = p.PersonId;

                e.PersonId = p.PersonId;

                ctx.Employee.Add(e);

                ctx.Pilot.Add(pi);

                ctx.SaveChanges();



                Console.WriteLine("Pilot #" + PNummer + ": " + Vorname + " " + Nachname);

            }



        }



        private static void Init_Passenger(ExampleContext ctx)

        {

            Console.WriteLine("Create Passengers...");



            Random rnd2 = new Random();



            for (int PNummer = 1; PNummer <= NO_OF_PASSENGERS; PNummer++)

            {

                string Vorname = Firstnames[rnd2.Next(0, Firstnames.Length - 1)];

                ;

                string Nachname = Surnames[rnd2.Next(0, Surnames.Length - 1)];

                ;

                Passenger pas = new Passenger();



                Person p = new Person();

                //  p.ID = 1;

                p.Surname = Nachname;

                p.GivenName = Vorname;

                p.Birthday =

                 new DateTime(1940, 1, 1).AddDays(Convert.ToInt32(new Random(DateTime.Now.Millisecond).Next(20000)));

                pas.Person = p;



                pas.PassengerStatus = "A";

                ctx.Passenger.Add(pas);

                ctx.SaveChanges();



                if (PNummer % 100 == 0)

                {

                    Console.WriteLine("Passenger #" + PNummer + ": " + Vorname + " " + Nachname);

                }

            }


        }
        static void Main(string[] args)
        {
            Run();

        }
    }
}
