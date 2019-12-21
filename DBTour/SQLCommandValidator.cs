using System;

namespace DBTour {
     enum CommandCode {
            EXIT,
            SELECT,
            CREATE,
            DELETE,
            INSERT,
            ERROR,
            TEST_OKAY
            
            
        }
    internal static class SQLCommandValidator {

       

        public static CommandCode validate(string command) {
            int parentCommandIndex = command.IndexOf(" ");
            //Find first occurance of " " to substring the main command
            if(parentCommandIndex == -1) {
                // If " " is not in command, IndexOf() returns -1
                // Single Word Commands
                
                if(command.ToUpper().Equals("EXIT")) {
                    return CommandCode.EXIT;
                }
                else{
                    Console.WriteLine("{0} is the command",command);
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