using DemoScriptShp.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoScriptShp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Config cfg = CargaConfiguracionSettings();
                Dal.SharePoint.AnalizarColeccion(cfg);
            }
            catch(Exception ex)
            {
                Console.WriteLine("\nExcepción en Main: "+ ex.ToString());
            }

            Console.WriteLine("\nProceso terminado. Pulse intro para salir.");
            Console.ReadLine();
        }

        static Config CargaConfiguracionSettings()
        {
            Config cfg = new Config()
            {
                Url = new Uri(Configuraciones.Default.url),
                Admin = Configuraciones.Default.admin,
                Pass = Configuraciones.Default.pass
            };

            return cfg;
        }
    }
}
