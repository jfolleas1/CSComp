using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CompCorpus
{
    static public class LogManager
    {
        static public string logFilePath { get; set; } = "";

        static public void EmptyLogs()
        {
            try
            {
                StreamWriter myStreamWriter = new StreamWriter(File.Open(logFilePath, FileMode.Create), Encoding.UTF8);
                myStreamWriter.Write(String.Empty);
                // close the StreamWriter
                myStreamWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lors de l'initialisation du fichier de log à vide ");
                Console.Write(ex.Message);
            }
        }

        static public void AddLog(string log)
        {
            try
            {
                StreamWriter myStreamWriter = new StreamWriter(File.Open(logFilePath,FileMode.Append), Encoding.UTF8);
                myStreamWriter.Write(log + "\n");
                // close the StreamWriter
                myStreamWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lors de l'écriture dans le fichier de logs");
                Console.Write(ex.Message);
            }
        }

        static public void DisplayLogs()
        {
            try
            {
                StreamReader myStreamReader = new StreamReader(logFilePath);
                Console.WriteLine(myStreamReader.ReadToEnd());
                // close the StreamWriter
                myStreamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lors de l'affichage du fichier de logs");
                Console.Write(ex.Message);
            }
        }

        static public string ReadLogs()
        {
            String allLogs = "";
            try
            {
                StreamReader myStreamReader = new StreamReader(logFilePath);
                allLogs = myStreamReader.ReadToEnd();
                // close the StreamWriter
                myStreamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("When trying read log");
                Console.Write(ex.Message);
            }
            return allLogs;
        }
    }
}
