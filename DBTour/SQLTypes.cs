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

    internal class InsertCommandGenerator: SQLCommandGenerator {
        CommandCode commandCode;
        string tableName;
        int colNumber;
        int rowNumber;
        string[] colNames;
        string[,] colValues;
        internal InsertCommandGenerator(CommandCode commandCode){
            this.commandCode = commandCode; 
            string buffer;

            Console.WriteLine("Enter Table Name");
            this.tableName = Console.ReadLine();
            
            while(true) {

            Console.WriteLine("How many columns would you like to insert into?");
            buffer = Console.ReadLine();
            if(!Int32.TryParse(buffer, out this.colNumber)){
                Console.WriteLine("{0} : is not a valid integer",buffer);
                    }
            else {
                break;
                // Will stay in loop until integer is provided
                }
            }
            while(true) {
            Console.WriteLine("How many rows would you like to insert?");
            buffer = Console.ReadLine();
            if(!Int32.TryParse(buffer, out this.rowNumber)){
                Console.WriteLine("{0} : is not a valid integer",buffer);
                    }
            else {
                break;
                // Will stay in loop until integer is provided
                }
            }
            

            colNames = new string[this.colNumber];
            colValues = new string[this.rowNumber,this.colNumber];
           for(int i = 0; i < this.colNumber ; i++) {
               Console.WriteLine("Enter Next Column Name");
               this.colNames[i] = Console.ReadLine();
           }
           for(int i =0; i < this.rowNumber; i++) {
                Console.WriteLine("New Row, Please Enter data for the following columns");
               for(int j = 0; j<this.colNumber; j++){
                  Console.Write("{0}: ", this.colNames[j]); 
                  this.colValues[i,j] = Console.ReadLine();
                  Console.WriteLine();
               }
           }
        }
        internal string generateCommand() {
            var stringBuilder = new System.Text.StringBuilder();
            stringBuilder.Append(string.Format("INSERT INTO {0}(",this.tableName));
            for(int i = 0; i < this.colNumber; i++) {
                stringBuilder.Append(string.Format("{0},",this.colNames[i]));
            }
            stringBuilder.Append(") VALUES");
            for(int i = 0 ;i <this.rowNumber ; i++) {
                stringBuilder.Append("(");
                for(int j =0; j< this.colNumber ; j++) {
                    stringBuilder.Append(string.Format("{0},",this.colValues[i,j]));
                }
                stringBuilder.Remove(stringBuilder.Length-1,1);
                stringBuilder.Append("),");
            }
            stringBuilder.Remove(stringBuilder.Length-1,1);
            Console.WriteLine(stringBuilder.ToString());
            return stringBuilder.ToString();
        }
    }
    
 
    internal class SQLCommandGeneratorFactory {

       internal SQLCommandGeneratorFactory() {
       }
       internal InsertCommandGenerator newInsertSQLCommandGenerator() {
           return new InsertCommandGenerator(CommandCode.INSERT);
       }
       internal CreateCommandGenerator newCreateSQLCommandGenerator(){
               return new CreateCommandGenerator(CommandCode.CREATE);
           }
       }
}