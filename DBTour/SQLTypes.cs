using System;

namespace DBTour {

    internal class SQLCommandGenerator {
        private CommandCode commandCode;
        internal SQLCommandGenerator(CommandCode commandCode) {
            this.commandCode = commandCode;
            Console.WriteLine("Sucess in Other Generator");
        }
        internal SQLCommandGenerator() {}

    }

    internal class CreateCommandGenerator : SQLCommandGenerator {
        private CommandCode commandCode;
        private string tableName;
        private int colNumber;
        private string[] colNames;
        private string[] colTypes;
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
        private CommandCode commandCode;
        private string tableName;
        private int colNumber;
        private int rowNumber;
        private string[] colNames;
        private string[,] colValues;
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
            stringBuilder.Append(string.Format("INSERT INTO {0} (",this.tableName));
            for(int i = 0; i < this.colNumber; i++) {
                stringBuilder.Append(string.Format("{0},",this.colNames[i]));
            }
            stringBuilder.Remove(stringBuilder.Length-1,1);
            stringBuilder.Append(") VALUES");
            for(int i = 0 ;i <this.rowNumber ; i++) {
                stringBuilder.Append("(");
                for(int j =0; j< this.colNumber ; j++) {
                    stringBuilder.Append(string.Format("'{0}',",this.colValues[i,j]));
                }
                stringBuilder.Remove(stringBuilder.Length-1,1);
                stringBuilder.Append("),");
            }
            stringBuilder.Remove(stringBuilder.Length-1,1);
            Console.WriteLine(stringBuilder.ToString());
            return stringBuilder.ToString();
        }
    }

    internal class SelectCommandGenerator : SQLCommandGenerator{
        private CommandCode commandCode;
        private string tableName;
        private bool isSelectAll;
        private bool isConditional;
        private string conditional;
        private int colNumber;
        private string[] colNames;
        
        internal SelectCommandGenerator(CommandCode commandCode) {
            this.commandCode = commandCode;
            var sB = new System.Text.StringBuilder();
            Console.WriteLine("Enter Table Name");
            this.tableName = Console.ReadLine();
            
            Console.WriteLine("Select from specific columns? [y/n]");
            this.isSelectAll = Console.ReadLine().ToUpper() == "Y" ? false : true;

            if(!this.isSelectAll){ 
                while(true) {
                    Console.WriteLine("Enter the number of columns to select");
                    string buffer = Console.ReadLine();
                    if(!Int32.TryParse(buffer, out this.colNumber)){
                        Console.WriteLine("{0} : is not a valid integer",buffer);
                        } 
                    else  break; 
                    
                }
                this.colNames = new string[this.colNumber];
                for(int i = 0; i < this.colNumber; i++) {
                Console.WriteLine("Enter name for next column");
                this.colNames[i] = Console.ReadLine();
                }
            }
            else {
                this.colNumber =1;
                this.colNames = new string[this.colNumber];
                this.colNames[0] = "*";
            }

            Console.WriteLine("Would you like to add a Conditional? [y/n]");
            this.isConditional = Console.ReadLine().ToUpper() == "Y" ? true : false;
            
            if(this.isConditional) {
                Console.WriteLine("Enter Conditional Statement");
                this.conditional = Console.ReadLine();
            }
            else {
                this.conditional = null;
            }

        }

        internal string generateCommand() {
            var sB = new System.Text.StringBuilder();
            sB.Append("SELECT (");

            for(int i =0; i < colNumber ; i++) { 
                sB.Append(string.Format("{0},",this.colNames[i]));
            }
            sB.Remove(sB.Length-1,1);
            sB.Append(string.Format(") FROM {0}",this.tableName));
            if(this.isConditional) {
                sB.Append(string.Format("WHERE {0}",this.conditional));
            }
            Console.WriteLine(sB.ToString());
            return sB.ToString();

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
       internal SelectCommandGenerator newSelectSQLCommandGenerator(){
            return new SelectCommandGenerator(CommandCode.SELECT);
           }
       }
}