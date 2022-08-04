using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace WriteSerialNumber.Controllers
{
    class WriteSerialController
    {
        private const string afunwin = @"Tools\AMIDEWIN\AMIDEWINx64.EXE";
        private const string insyde = @"Tools\H2OSDE\H2OSDE-Wx64.exe";

        public static bool WriteSerialNumber(string value, string type)
        {

            Process process = new Process();
            LogController log = new LogController("SYSTEM.LOG");

            try
            {                

                if (DMIController.GetBiosManufacturer() == "INSYDE Corp.")
                {
                    process.StartInfo.FileName = insyde;
                    process.StartInfo.Arguments = $"-BS \"{value}\"";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.Start();

                    log.WriteMenssage($"INSYDE SERIALNUMBER: {value}");

                }
                else
                {
                    process.StartInfo.FileName = afunwin;
                    process.StartInfo.Arguments = $"/BS {value}";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.Start();

                    log.WriteMenssage($"AMIDEWIN SERIALNUMBER: {value}");

                }

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"WriteSerialNumber: {process.ExitCode}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"WriteSerialNumber: {ex.Message}");
            }

            finally
            {
                if (process != null)
                {
                    process.Close();
                }
            }

            return true;
        }
    }
}
