using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using WMPLib;

namespace Basta {
    public class Sound {
        private static Sound instance;
        static WindowsMediaPlayer introSound;
        static WindowsMediaPlayer loggedSound;
        static WindowsMediaPlayer chatSound;

        public static Sound GetInstance() {
            if ( instance == null ) {
                instance = new Sound();
            }
            return instance;
        }

        public void PlayChatSound() {
            try {
                if ( chatSound != null ) {
                    chatSound.controls.stop();
                }
                ThreadPool.QueueUserWorkItem(
             delegate ( object param ) {
                 chatSound = new WindowsMediaPlayer();
                 chatSound.URL = Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, @"SoundsEFX\ChatSound.mp3" );
             } );
            } catch ( Exception ex ) {
                Console.WriteLine( ex );
            }
        }

        public void VolumenDown( int value ) {
            introSound.settings.volume = introSound.settings.volume - value;
        }

        public void VolumenUp( int value ) {
            introSound.settings.volume = introSound.settings.volume + value;
        }

        public void RandomizePlayListIntroMusic() {
            try {
                if ( introSound != null ) {
                    introSound.controls.stop();
                }
                introSound = new WindowsMediaPlayer();
                introSound.URL = RandomIntroMusic();
            } catch ( Exception ex ) {
                Console.WriteLine( ex );
            }
        }

        public void PlayLoginMusic() {
            if ( loggedSound != null ) {
                loggedSound.controls.stop();
            }
            loggedSound = new WindowsMediaPlayer();
            loggedSound.URL = Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, @"SoundsEFX\LoginEntered.mp3" );
        }

        public static string AssemblyDirectory {
            get {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder( codeBase );
                string path = Uri.UnescapeDataString( uri.Path );
                return Path.GetDirectoryName( path );
            }
        }

        private string RandomIntroMusic() {
            List<string> songs = new List<string>();
            songs.Add( Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, @"SoundsEFX\A.mp3" ) );
            songs.Add( Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, @"SoundsEFX\B.mp3" ) );
            songs.Add( Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, @"SoundsEFX\C.mp3" ) );
            songs.Add( Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, @"SoundsEFX\D.mp3" ) );
            songs.Add( Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, @"SoundsEFX\E.mp3" ) );
            songs.Add( Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, @"SoundsEFX\F.mp3" ) );
            songs.Add( Path.Combine( System.AppDomain.CurrentDomain.BaseDirectory, @"SoundsEFX\G.mp3" ) );
            Random random = new Random();
            int randomInteger = random.Next( 0, songs.Count );
            return songs[randomInteger];
        }

        public void StopMusic() {
            if ( introSound != null ) {
                introSound.controls.stop();
            }
            if ( loggedSound != null ) {
                loggedSound.controls.stop();
            }
        }

        private Sound() { }

    }
}
