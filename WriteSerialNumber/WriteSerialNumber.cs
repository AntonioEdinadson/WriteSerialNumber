using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WriteSerialNumber.Controllers;

namespace WriteSerialNumber
{
    public partial class WriteSerialNumber : Form
    {
        public WriteSerialNumber()
        {
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        private void WriteSerialNumber_Load(object sender, EventArgs e)
        {
            lbBIOS.Text = DMIController.GetBiosManufacturer();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));            
        }

        private void txtSerialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            string serialnumber = txtSerialNumber.Text;

            try
            {
                if (e.KeyValue == 13)
                {
                    if(string.IsNullOrEmpty(serialnumber))
                    {
                        throw new Exception("Serial Number is invalid.");
                    }

                    WriteSerialController.WriteSerialNumber(serialnumber, "");
                    lbError.ForeColor = Color.LimeGreen;
                    lbError.Text = "Serial number saved successfully.";
                    lbError.Visible = true;
                    txtSerialNumber.Text = "";
                }
            }
            catch (Exception ex)
            {                
                lbError.Text = ex.Message;
                lbError.Visible = true;
            }
        }
    }
}
