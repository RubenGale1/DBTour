using System;

namespace DBTour
{
    sealed class Controler
    {    
        static void Main(string[] args)
        {
            SQLController sqlController;
            string command;
            string databaseName;

    
            Console.WriteLine("Starting up");
    
            do {
                
                Console.WriteLine("Input Database Name");
                databaseName = Console.ReadLine();
                sqlController = new SQLController(databaseName);

            }
            while(!sqlController.isDataBaseValid); 
            
            do{
                Console.WriteLine("Input Command");
                command = Console.ReadLine();
                sqlController.executeCommand(command);
            }
            while(sqlController.commandStatus!= CommandCode.EXIT);

        }
    }
}
