﻿using System.IO;
using System;

namespace WebApplication1.Services
{
    public class LogService
    {

        public static string path = AppDomain.CurrentDomain.BaseDirectory + "/LOGS/";
        public static string pathNotificacion = AppDomain.CurrentDomain.BaseDirectory + "/LOGS/Notificacion/";


        public static void Log(string mensaje)
        {
            try
            {
                string nameFile = string.Format("LG{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
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
