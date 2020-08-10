using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;

namespace blizzardAccountCleanup
{
    class Heartbeat
    {
        private readonly Timer _timer;

        public Heartbeat()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += timerElapsed;
        }


        /// <summary>
        /// This will break on international version of windows. Best to update later. Check out Syroot.Windows.IO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerElapsed(Object sender, ElapsedEventArgs e)
        {
            //Try to delete Blizzard Info
            try
            {
                BlizzardAccounts();
            }
            catch (IOException ex)
            {
                
            }

            //Try to delete Origin Info
            try
            {
                OriginAccounts();
            }
            catch (IOException ex)
            {
                
            }

            //Try to delete Steam Info
            try
            {
                SteamAccounts();
            }
            catch (IOException ex)
            {
                
            }

            //Try to delete Uplay Info
            try
            {
                UplayAccounts();
            }
            catch (IOException ex)
            {
                
            }

            //Try to delete Epic Info
            try
            {
                EpicAccounts();
            }
            catch (IOException ex)
            {

            }

            //Try to delete Spotify Info
            try
            {
                SpotifyAccounts();
            }
            catch (IOException ex)
            {

            }
            
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start()
        {
            _timer.Start();
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
        }

        //Blizzard Account Info
        public void BlizzardAccounts()
        {
            string username = Environment.UserName;
            var startingDir = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Battle.net\Account");

            //Delete Directories inside of startingDir
            foreach (var ele in startingDir.EnumerateDirectories())
            {
                ele.Delete(true);
            }
        }

        //Origin Login Data
        public void OriginAccounts()
        {
            string username = Environment.UserName;
            var startingDir = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Roaming\Origin");
            foreach (FileInfo file in startingDir.GetFiles())
            {
                file.Delete();
            }
        }

        //Steam User Data
        public void SteamAccounts()
        {
            var startingDir = new DirectoryInfo(@"D:\Steam\userdata");

            foreach (var ele in startingDir.EnumerateDirectories())
            {
                ele.Delete(true);
            }

            startingDir = new DirectoryInfo(@"D:\Steam\config");

            foreach(var file in startingDir.GetFiles())
            {
                file.Delete();
            }
            foreach(var ele in startingDir.EnumerateDirectories())
            {
                ele.Delete();
            }
        }

        //Uplay user data
        public void UplayAccounts()
        {
            string username = Environment.UserName;
            var startingDir = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Ubisoft Game Launcher");
            foreach(FileInfo file in startingDir.GetFiles())
            {
                if(!file.Name.Contains(".yml"))
                {
                    file.Delete();
                }
            }
        }

        //Epic Games User Data
        public void EpicAccounts()
        {
            string username = Environment.UserName;
            var startingDir = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\EpicGamesLauncher\Saved\Config\Windows");
            foreach (FileInfo file in startingDir.GetFiles())
            {
                if (file.Name.Contains("GameUserSettings"))
                {
                    File.SetAttributes(file.FullName, File.GetAttributes(file.FullName) | FileAttributes.ReadOnly);
                }
            }
        }

        //Spotify User Data
        public void SpotifyAccounts()
        {
            string username = Environment.UserName;
            var startingDir = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Roaming\Spotify\Users");
            foreach (var ele in startingDir.GetDirectories())
            {
                ele.Delete(true);
            }
        }

        //Discord User Data
    }
}
