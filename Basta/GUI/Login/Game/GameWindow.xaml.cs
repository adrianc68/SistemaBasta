using Domain.Domain;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Threading;

namespace Basta.GUI.Login.Game {
    /// <summary>
    /// Lógica de interacción para GameWindow.xaml
    /// </summary>
    public partial class GameWindow: Window {
        private Dictionary<string, PlayerLateralUserControl> players = new Dictionary<string, PlayerLateralUserControl>();
        private DispatcherTimer timer;
        private TimeSpan time;
        private DispatcherTimer gameTimer;
        private TimeSpan gameTime;

        public GameConfiguration GameConfiguration { get; set; }
        public Room Room { get; set; }

        public GameWindow( GameConfiguration gameConfiguration, Room room ) {
            InitializeComponent();
            HideBorderAndGrids();
            GameConfiguration = gameConfiguration;
            Room = room;
            Autentication.GetInstance().GameServiceCallBack = new GameServiceCallBack();
            InstanceContext context = new InstanceContext( Autentication.GetInstance().GameServiceCallBack );
            Autentication.GetInstance().GameServer = new Proxy.GameServiceClient( context );
            Autentication.GetInstance().GameServiceCallBack.GameWindow = this;
            InitializeChannelInServer();
        }

        public void AddPlayerLateralUserControl( string username ) {
            PlayerLateralUserControl playerLateralUserControl = new PlayerLateralUserControl();
            playerLateralUserControl.usernameLabel.Text = username;
            players.Add( username, playerLateralUserControl );
            playerUserControlWrapPanel.Children.Add( playerLateralUserControl );
        }

        public void RemovePlayerLateralUserControl( string username ) {
            playerUserControlWrapPanel.Children.Remove( players[username] );

        }

        public void StartRound() {
            StartAnimationLetter();
        }

        public void StartAnimationLetter() {
            AssignLetterBorder.Visibility = Visibility.Visible;
            AssignLetterAnimation( GameConfiguration.ActualLetter );
            GameConfiguration.ActualLetter = GameConfiguration.ActualLetter;
        }


        public void InitializeMainScreenBorder() {
            AssignLetterBorder.Visibility = Visibility.Hidden;
            mainScreenBorder.Visibility = Visibility.Visible;
            actualRoundLabel.Text = GameConfiguration.ActualRound.ToString();
            counterLabel.Text = GameConfiguration.TimeToStart.ToString();
            countDownStartMainScreenBorder( GameConfiguration.TimeToStart );
        }


        // This should be a server callback. dont remove it
        public void ShowWinner( string username ) {
            winnerLabel.Text = username;
            winnerBorder.Visibility = Visibility.Visible;

        }

        public void ShowResultsFromRound() {
            resultsBorder.Visibility = Visibility.Visible;
            countDownResultsBorderView();
        }

        private void countDownResultsBorderView() {
            time = TimeSpan.FromSeconds( GameConfiguration.TimeToShowResults );
            timer = new DispatcherTimer( new TimeSpan( 0, 0, 1 ), DispatcherPriority.Normal, delegate {
                if ( time == TimeSpan.Zero ) {
                    resultsBorder.Visibility = Visibility.Hidden;
                    ShowWinner( "PUT A USERNAME HERE" );
                } else {
                    time = time.Add( TimeSpan.FromSeconds( -1 ) );
                }
            }, Application.Current.Dispatcher );
            timer.Start();
        }


        private char AssignLetterAnimation( char letterSelected ) {
            char letter = 'A';
            char[] lettersArray = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'W', 'X', 'Y', 'Z' };
            int counter = 0;
            DispatcherTimer timerLetter = new DispatcherTimer();

            timerLetter.Interval = new TimeSpan( 0, 0, 0, 0, 20 );
            timerLetter.Tick += ( a, b ) => {
                Random random = new Random();
                int randomIndex = random.Next( 0, 26 );
                letter = lettersArray[randomIndex];
                letterAsigned.Content = lettersArray[randomIndex];
                counter++;
                if ( counter == 100 ) {
                    timerLetter.Stop();
                    letterAsigned.Content = letterSelected;
                    InitializeMainScreenBorder();
                }
            };
            timerLetter.Start();
            return letter;
        }


        private void countDownStartMainScreenBorder( double timeToStart ) {
            time = TimeSpan.FromSeconds( timeToStart );
            timer = new DispatcherTimer( new TimeSpan( 0, 0, 1 ), DispatcherPriority.Normal, delegate {
                counterLabel.Text = time.Seconds.ToString();
                if ( time == TimeSpan.Zero ) {
                    timer.Stop();
                    mainScreenBorder.Visibility = Visibility.Hidden;
                    StartGameInGridGame();
                } else {
                    time = time.Add( TimeSpan.FromSeconds( -1 ) );
                }
            }, Application.Current.Dispatcher );
            timer.Start();
        }

        private void StartGameInGridGame() {
            gridGame.Visibility = Visibility.Visible;
            roundsActualLabel.Text = GameConfiguration.ActualRound.ToString();
            roundsLimitLabel.Text = GameConfiguration.RoundsLimit.ToString();
            StartCountDownGameTimer( GameConfiguration.TimeToEndRound );

        }

        public void StartCountDownGameTimer( double timeToStart ) {
            timeRemainingLabel.Text = timeToStart.ToString();
            gameTime = TimeSpan.FromSeconds( timeToStart );
            gameTimer = new DispatcherTimer( new TimeSpan( 0, 0, 1 ), DispatcherPriority.Normal, delegate {
                if ( gameTime == TimeSpan.Zero ) {
                    gameTimer.Stop();
                    if ( resultsBorder.Visibility == Visibility.Hidden ) {
                        ShowResultsFromRound();
                    }
                } else {
                    timeRemainingLabel.Text = gameTime.Seconds.ToString();
                    gameTime = gameTime.Add( TimeSpan.FromSeconds( -1 ) );
                }
            }, Application.Current.Dispatcher );
            gameTimer.Start();
        }

        public void SomeonePressedStopButton( Player player ) {
            players[player.AccessAccount.Username].bastaButtonPressed.Visibility = Visibility.Visible;
            if ( gameTimer != null && gameTimer.IsEnabled ) {
                if ( gameTime.Seconds >= GameConfiguration.TimeToStopGameOnButtonStopPressed ) {
                    gameTimer.Stop();
                    StartCountDownGameTimer( GameConfiguration.TimeToStopGameOnButtonStopPressed );
                }
            }
        }



        private void ChangeCategoryInGridGame() {

        }

        private void InitializeChannelInServer() {
            try {
                Autentication.GetInstance().GameServer.OpenChannel( Room, Autentication.GetInstance().Player );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    Close();
                }
            }
        }

        private void bastaButtonClicked( object sender, RoutedEventArgs e ) {
            Player player = Autentication.GetInstance().Player;
            Autentication.GetInstance().GameServer.StopButtonPressed( Room, player );
        }

        private void NextCategoryButtonPressed( object sender, RoutedEventArgs e ) {

        }

        private void CloseButtonPressed( object sender, RoutedEventArgs e ) {
            Close();
        }

        private void HideBorderAndGrids() {
            gridGame.Visibility = Visibility.Hidden;
            mainScreenBorder.Visibility = Visibility.Hidden;
            resultsBorder.Visibility = Visibility.Hidden;
            winnerBorder.Visibility = Visibility.Hidden;
            AssignLetterBorder.Visibility = Visibility.Hidden;
        }

        private void GameWindowClosed( object sender, EventArgs e ) {
            Autentication.GetInstance().RoomServiceCallBack.LobbyWindow.Show();
        }
    }
}
