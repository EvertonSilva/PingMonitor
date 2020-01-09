using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace SatMonitor
{
    class Program
    {
        private static Ping pingCmd;
        private static PingOptions opts;

        static void Main(string[] args)
        {
            // ler arquivo de configuração
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
            var config = builder.Build();
            
            pingCmd = new Ping();
            opts = new PingOptions();
            var ipList = config.GetSection("hosts").Get<string[]>();

            foreach (var ip in ipList)
            {
                SendPingRequest(ip);
            }

            Console.WriteLine("Processo finalizado, precione qualquer tecla para sair.");
            Console.ReadLine();
        }

        private static void SendPingRequest(string host)
        {
            opts.DontFragment = true;

            string data = new string('a', 32);
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            try
            {
                PingReply reply = pingCmd.Send(host, timeout, buffer, opts);

                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine("All cool! 8-)");
                }
                else
                {
                    Console.WriteLine($"{host} - {reply.Status.ToString()}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
