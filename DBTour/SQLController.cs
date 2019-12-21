using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.SQLite;  
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DBTour {

    internal class SQLController { 

        private string dbName;
        private SQLiteConnection connection;
        private SQLiteCommand command;
        private bool isNew;
        public bool isDataBaseValid;
        public CommandCode commandStatus;


        public SQLController(string dbName) { 
            this.dbName = dbName;
            validateDB();

        } 

        public void executeCommand(string commandString) {
            this.commandStatus = SQLCommandEvaluator.evaluate(commandString);
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
                connection = new SQLiteConnection(string.Format(commandString,this.dbName,this.isNew.ToString()));
                try{
                    connection.Open();
                }
                catch(Exception e) {
                    Console.WriteLine(e.Message);
                }

        }

    }
}