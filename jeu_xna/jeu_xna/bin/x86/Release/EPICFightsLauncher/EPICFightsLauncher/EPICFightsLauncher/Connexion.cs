using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace EPICFightsLauncher
{
    public class Connexion
    {

        private string login;
        private string password;

        public Connexion(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public string Connect()
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect("epic-fights.sebb-dev.org", 80);
                Stream stream = new NetworkStream(s);
                StreamWriter streamwriter = new StreamWriter(stream);
                streamwriter.WriteLine("POST /launcher/login.php HTTP/1.1\nHost : epic-fights.sebb-dev.org \nConnection : close\nContent-type: application/x-www-form-urlencoded\nContent-Length: " + (password.Length + login.Length) + "\n\nlogin=" + login + "&password=" + password);
                streamwriter.Flush();

                StreamReader streamreader = new StreamReader(stream);
                string result = streamreader.ReadToEnd();

                streamwriter.Close();
                streamreader.Close();
                stream.Close();
                s.Close();
                
                return HttpFormating.Convert(result);
            }
            catch
            {
                return "erreur_connexion";
            }
        }
    }
}
