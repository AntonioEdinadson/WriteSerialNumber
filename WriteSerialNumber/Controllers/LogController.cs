using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WriteSerialNumber.Controllers
{
    class LogController
    {

        private static StreamWriter streamWriter;

        public LogController(string fileName)
        {
            try
            {
                streamWriter = new StreamWriter(fileName, true);
                streamWriter.AutoFlush = true;
                streamWriter.WriteLine(DateTime.Now + ":\tInício de Log");
            }
            catch (Exception e)
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }

                Console.WriteLine($"Erro na inicialização do log: {e.Message}");
                throw new Exception($"Erro na inicialização do log: {e.Message}");
            }
        }

        public void WriteMenssage(string message)
        {
            try
            {
                streamWriter.WriteLine(DateTime.Now + ":\t" + message);
            }
            catch (Exception ex)
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }

                Console.WriteLine($"Erro inserir log: {ex.Message}");
                throw new Exception($"Erro inserir log: {ex.Message}");
            }
        }

        public void CloseMenssage()
        {
            try
            {
                streamWriter.WriteLine($"{DateTime.Now}: Processo Concluído.");
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }

                Console.WriteLine($"Erro fechar log: {ex.Message}");
                throw new Exception($"Erro fechar log: {ex.Message}");
            }
        }
    }
}
