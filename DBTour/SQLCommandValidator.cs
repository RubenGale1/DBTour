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
    internal static class SQLCommandEvaluator {

       

        public static CommandCode evaluate(string command) {

            int parentCommandIndex = command.IndexOf(" ");
            //Find first occurance of " " to substring the main command
            if(parentCommandIndex == -1) {
                // If " " is not in command, IndexOf() returns -1
                if(command.ToUpper().Equals("EXIT")) {
                    return CommandCode.EXIT;
                }
                else{
                    Console.WriteLine("{0} is the command",command);
                    return CommandCode.TEST_OKAY;
                }
            }
            string parentCommand = command.Substring(0,parentCommandIndex-1);
            string commandBody = command.Substring(parentCommandIndex+1,command.Length-1);
            Console.WriteLine("{0} is the command",parentCommand);
            Console.WriteLine("{0} is the command body",commandBody);
            return CommandCode.TEST_OKAY;



        }
    }
}