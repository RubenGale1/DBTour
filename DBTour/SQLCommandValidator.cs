using System;

namespace DBTour {
     enum CommandCode {
            EXIT,
            SELECT,
            CREATE,
            DELETE,
            INSERT,
            ERROR,
            TEST_OKAY,
            TABLES
            
            
        }
    internal static class SQLCommandValidator {

       

        public static CommandCode validate(string command) {
            int parentCommandIndex = command.IndexOf(" ");
            //Find first occurance of " " to substring the main command
            if(parentCommandIndex == -1) {
                // If " " is not in command, IndexOf() returns -1
                // Single Word Commands
                switch(command.ToUpper()) {
                case  "CREATE" :
                return CommandCode.CREATE;

                case "INSERT" :
                return CommandCode.INSERT;

                case "SELECT" :
                return CommandCode.SELECT;

                case "DELETE" :
                return CommandCode.DELETE;

                case "EXIT" :
                return CommandCode.EXIT;

                case "TABLES" :
                return CommandCode.TABLES;

                default: 
                return CommandCode.TEST_OKAY;
                }  
            }
            string parentCommand = command.Substring(0,parentCommandIndex-1).ToUpper();
            // Split Command into Head command and body
            string commandBody = command.Substring(parentCommandIndex+1,command.Length-1);
            Console.WriteLine("{0} is the command",parentCommand);
            Console.WriteLine("{0} is the command body",commandBody);

            switch(parentCommand) {
                case  "CREATE" :
                return CommandCode.CREATE;

                case "INSERT" :
                return CommandCode.INSERT;

                case "SELECT" :
                return CommandCode.SELECT;

                case "DELETE" :
                return CommandCode.DELETE;

                default: 
                return CommandCode.TEST_OKAY;
            }
        }

    }
}