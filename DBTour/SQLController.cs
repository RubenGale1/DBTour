using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Data.SQLite;  
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DBTour {

    internal class SQLController { 

        private string dbName;
        private SQLiteConnection connection;
        private SQLiteCommand command;
        private SQLCommandGeneratorFactory sQLCommandGeneratorFactory;
        private bool isNew;
        public bool isDataBaseValid;
        public CommandCode commandStatus;
        


        public SQLController(string dbName) { 
            this.dbName = dbName;
            this.sQLCommandGeneratorFactory = new SQLCommandGeneratorFactory(this.connection);
            validateDB();
            if(this.isDataBaseValid){
            this.command = this.connection.CreateCommand();
            }

        } 

        public void executeCommand(string commandString) {
            this.commandStatus = SQLCommandValidator.validate(commandString);

            switch(this.commandStatus) {
                
                case CommandCode.CREATE:
                create();
                break;

                case CommandCode.TABLES:
                tables();
                break;

                case CommandCode.INSERT:
                insert();
                break;

                case CommandCode.SELECT:
                DataTable columns = this.connection.GetSchema("Columns");
                foreach(DataRow row  in columns.Rows) {
                     string columnName = (string)row["DATA_TYPE"];
                    //  row consists of 
                    //     0: sqlite_defalt_schema
                    //     1: main
                    //     2: table name
                    //     3: column_name 
                    // column names are in order
                     Console.WriteLine(columnName);
                }
                select();
                break;
            }
        }

        private void  validateDB() {
            
            var regex = new Regex(@".*\.db$");
            Match match = regex.Match(this.dbName);
            
            if(!match.Success) {
               this.isDataBaseValid = false;
               Console.WriteLine("expected .db file, recieved {0}", this.dbName);
               return;
            }
            this.isDataBaseValid = true;
            if(!File.Exists(this.dbName) ){    
                SQLiteConnection.CreateFile(this.dbName);
                Console.WriteLine("Data Base File Created");
                this.isNew = true;
            }
            else {
                Console.WriteLine("Data Base Already Exists");
                this.isNew = false;
            }
                string commandString  ="Data Source = {0};Version=3;New={1};Compress=True";
                this.connection = new SQLiteConnection(string.Format(commandString,this.dbName,this.isNew.ToString()));
                try{
                    this.connection.Open();
                }
                catch(Exception e) {
                    Console.WriteLine(e.Message);
                }

        }

        private void create() {
            CreateCommandGenerator  createCommandGenerator = this.sQLCommandGeneratorFactory.newCreateSQLCommandGenerator();
            this.command.CommandText = createCommandGenerator.generateCommand();
            this.command.ExecuteNonQuery();
            Console.WriteLine("Finished in correct state");

        }

        private void tables() { 
            List<string> tables = new List<string>();
            DataTable dt = this.connection.GetSchema("Tables");
            foreach(DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                Console.WriteLine(tablename);
                tables.Add(tablename);
            }
        }
        
        private void insert() {
            InsertCommandGenerator insertCommandGenerator = this.sQLCommandGeneratorFactory.newInsertSQLCommandGenerator();
            this.command.CommandText = insertCommandGenerator.generateCommand();
            var returnObject = this.command.ExecuteNonQuery();
            Console.WriteLine(returnObject.ToString());
        }

        private void select() {
            SelectCommandGenerator selectCommandGenerator = this.sQLCommandGeneratorFactory.newSelectSQLCommandGenerator();
            this.command.CommandText = selectCommandGenerator.generateCommand();
            using SQLiteDataReader rdr = this.command.ExecuteReader();

            while(rdr.Read()) { 
                Console.WriteLine($"{rdr.GetInt32(0)},{rdr.GetString(1)},{rdr.GetString(2)}");
            }
        }

    }


}