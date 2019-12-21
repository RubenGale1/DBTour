using System;

namespace DBTour {

    internal class SQLCommandGenerator {
        CommandCode commandCode;
        internal SQLCommandGenerator(CommandCode commandCode) {
            this.commandCode = commandCode;
            Console.WriteLine("Sucess in Other Generator");
        }
        internal SQLCommandGenerator() {}

    }

    internal class CreateCommandGenerator : SQLCommandGenerator {
        CommandCode commandCode;
        string tableName;
        int colNumber;
        string[] colNames;
        string[] colTypes;
        internal  CreateCommandGenerator(CommandCode commandCode) {
            string buffer;
            this.commandCode = commandCode;
            
            Console.WriteLine("Enter Table Name");
            tableName = Console.ReadLine();
            
            while(true) {

            Console.WriteLine("How many columns will the table have?");
            buffer = Console.ReadLine();
            if(!Int32.TryParse(buffer, out this.colNumber)){
                Console.WriteLine("{0} : is not a valid integer",buffer);
                    }
            else {
                break;
                // Will stay in loop until integer is provided
                }
            }
            buffer = null;
            colNames = new string[this.colNumber];
            colTypes = new string[this.colNumber];
           for(int i = 0; i < this.colNumber ; i++) {
               Console.WriteLine("Enter Next Column Name");
               this.colNames[i] = Console.ReadLine();
               Console.WriteLine("Enter Next Column Type");
               this.colTypes[i] = Console.ReadLine();
           }

        }

        internal string generateCommand() {
            var stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append(string.Format("CREATE TABLE IF NOT EXISTS {0}(",this.tableName));
            for(int i =0; i< this.colNumber ; i++) {
                stringBuilder.Append(string.Format("{0} {1},",this.colNames[i], this.colTypes[i]));
            }
            stringBuilder.Remove(stringBuilder.Length-1,1);
            stringBuilder.Append(")");
        Console.WriteLine(stringBuilder.ToString());
        return stringBuilder.ToString();
        }


    }
    
 
    internal class SQLCommandGeneratorFactory {

       internal SQLCommandGeneratorFactory() {
       }
       internal CreateCommandGenerator newCreateSQLCommandGenerator(){
               return new CreateCommandGenerator(CommandCode.CREATE);
           }
       }
}