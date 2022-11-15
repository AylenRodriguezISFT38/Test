using System.IO;
using System;
using SpreadsheetLight;
using DB;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;

namespace WebApplication1.Services
{
    public class StockReport
    {

        public static string path = AppDomain.CurrentDomain.BaseDirectory + "/Reportes/";
        public static string pathNotificacion = AppDomain.CurrentDomain.BaseDirectory + "/Reportes/Notificacion/";


        public static void Report(string mensaje)
        {
            try
            {
                string nameFile = string.Format("Reporte{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                using TextWriter archivo = new StreamWriter(path + nameFile, true);
                archivo.WriteLine(string.Format("{0} - {1}{2}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"),
                    mensaje,
                    Environment.NewLine
                    ));
            }
            catch (Exception ex)
            {
                string nameFile = string.Format("LG{0}-ERROR.txt", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                using TextWriter archivo = new StreamWriter(path + nameFile, true);
                archivo.WriteLine(string.Format("{0} - {1} - {2}{3}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"),
                    ex.ToString() + Environment.NewLine,
                    mensaje,
                    Environment.NewLine
                    ));
            }
        }
    }
}
