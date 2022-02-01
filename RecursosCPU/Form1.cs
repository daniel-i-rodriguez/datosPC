using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using System.Management;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;



namespace RecursosCPU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string nombrePC;
        public string memoriaRAM;
        public string memoriaPC;
        public string internet;

        private void button1_Click(object sender, EventArgs e)
        {
            

            Computer myComputer = new Computer();
            //Console.WriteLine("Computer name: ", myComputer.Name);

            ComputerInfo objCI = new ComputerInfo();
            //Console.WriteLine("OS Full Name: ", objCI.OSFullName);

            //MessageBox.Show("nombre del ordenador: " + myComputer.Name + ","
            //    + "\n nombre sistema operativo: " + objCI.OSFullName );

            nombrePC = "nombre del ordenador: " + myComputer.Name + ","
                + "\n nombre sistema operativo: " + objCI.OSFullName;

            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {

                //MessageBox.Show("Total Visible Memory:" + result["TotalVisibleMemorySize"] + "," 
                //    + "\n Free Physical Memory:" + result["FreePhysicalMemory"] + "," 
                //    + "\n Total Virtual Memory:" + result["TotalVirtualMemorySize"] + ","
                //    + "\n Free Virtual Memory:" + result["FreeVirtualMemory"]);

                memoriaRAM = "Total Visible Memory:" + result["TotalVisibleMemorySize"] + ","
                    + "\n Free Physical Memory:" + result["FreePhysicalMemory"] + ","
                    + "\n Total Virtual Memory:" + result["TotalVirtualMemorySize"] + ","
                    + "\n Free Virtual Memory:" + result["FreeVirtualMemory"];

            }

            
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                
                
                if (d.IsReady == true)
                {

                    //MessageBox.Show("Unidad: " + d.Name + "," 
                    //    + "\n Tipo de unidad: " + d.DriveType + ","
                    //    + "\n Etiqueta de volumen: " + d.VolumeLabel + "," 
                    //    + "\n Sistema de archivos: " + d.DriveFormat + ","
                    //    + "\n Espacio disponible en unidad: " + (d.AvailableFreeSpace / 1073741824) + " GB" + ","
                    //    + "\n Espacio total disponible: " + (d.TotalFreeSpace / 1073741824) + " GB" + ","
                    //    + "\n Tamaño total de unidad: " + (d.TotalSize / 1073741824) + " GB");

                    memoriaPC = "Unidad: " + d.Name + ","
                        + "\n Tipo de unidad: " + d.DriveType + ","
                        + "\n Etiqueta de volumen: " + d.VolumeLabel + ","
                        + "\n Sistema de archivos: " + d.DriveFormat + ","
                        + "\n Espacio disponible en unidad: " + (d.AvailableFreeSpace / 1073741824) + " GB" + ","
                        + "\n Espacio total disponible: " + (d.TotalFreeSpace / 1073741824) + " GB" + ","
                        + "\n Tamaño total de unidad: " + (d.TotalSize / 1073741824) + " GB";


                }
            }

            //MessageBox.Show("Nombre PC:" + "\n" + nombrePC + 
            //    "\n\n Memoria RAM:" + "\n" + memoriaRAM + "\n\n Memoria PC:" + "\n" + memoriaPC);

            //label1.Text = "Nombre PC:" + "\n" + nombrePC +
            //    "\n\n Memoria RAM:" + "\n" + memoriaRAM + "\n\n Memoria PC:" + "\n" + memoriaPC;

            const string tempfile = "tempfile.tmp";
            System.Net.WebClient webClient = new System.Net.WebClient();
            Console.WriteLine("Downloading file....");
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            webClient.DownloadFile("http://dl.google.com/googletalk/googletalk-setup.exe", tempfile);
            sw.Stop();
            FileInfo fileInfo = new FileInfo(tempfile);
            long speed = fileInfo.Length / sw.Elapsed.Seconds;

            try
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream("C:/Users/" + myComputer.Name + "/Downloads/nuevo 1.pdf", FileMode.Create));
                //MessageBox.Show("C:/Users/" + myComputer.Name + "/Downloads/nuevo 1.pdf");
                document.Open();
                Paragraph p = new Paragraph("Nombre PC:" + "\n" + nombrePC +
                "\n\n Memoria RAM:" + "\n" + memoriaRAM + "\n\n Memoria PC:" + "\n" + memoriaPC +
                "\n\n Velocidad de internet" + "\n Download duration: " + sw.Elapsed + "," +
                "\n File size: " + fileInfo.Length + "," +"\n Speed: " + speed + " bps");
                document.Add(p);
                document.Close();

                MessageBox.Show("Se descargo archivo correctamente en la ruta" +
                    "\n C:/Users/" + myComputer.Name + "/Downloads/.pdf");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }



    }
}
