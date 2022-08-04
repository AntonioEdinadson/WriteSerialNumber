using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;

namespace WriteSerialNumber.Controllers
{
    class DMIController
    {        
        public static String GetBiosManufacturer()
        {
            String biosManufacturer = "";
            ManagementObjectSearcher s2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (ManagementObject mo in s2.Get())
            {
                biosManufacturer = mo["Manufacturer"].ToString().Trim();
            }

            return biosManufacturer;
        }                        

        public static void ShutdownCabra()
        {
            ManagementBaseObject mboShutdown = null;
            ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();

            mcWin32.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject mboShutdownParams = mcWin32.GetMethodParameters("Win32Shutdown");

            mboShutdownParams["Flags"] = "1";
            mboShutdownParams["Reserved"] = "0";

            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                mboShutdown = manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
            }
        }
    }
}
