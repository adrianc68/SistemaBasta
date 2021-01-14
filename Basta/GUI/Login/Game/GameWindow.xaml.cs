using Domain.Domain;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Threading;

namespace Basta.GUI.Login.Game {
    /// <summary>
    /// Lógica de interacción para GameWindow.xaml
    /// </summary>
    public partial class GameWindow: Window {
        private Dictionary<string, PlayerLateralUserControl> players = new Dictionary<string, PlayerLateralUserControl>();
        private DispatcherTimer timer;
        private DispatcherTimer timerLetter;
        private TimeSpan time;
        private Dictionary<GameCategory, Word> answersToCategories;
        private GameCategory? actualCategory;
        private Queue<GameCategory> categoriesCopy;
        private Player winner;

        public GameConfiguration GameConfiguration { get; set; }
        public Room Room { get; set; }

        public GameWindow( Room room ) {
            InitializeComponent();
            HideBorderAndGrids();
            Room = room;
            Autentication.GetInstance().GameServiceCallBack = new GameServiceCallBack();
            InstanceContext context = new InstanceContext( Autentication.GetInstance().GameServiceCallBack );
            Autentication.GetInstance().GameServer = new Proxy.GameServiceClient( context );
            Autentication.GetInstance().GameServiceCallBack.GameWindow = this;
            InitializeChannelInServer();
        }

        public void StopTimer() {
            if ( timer != null && timer.IsEnabled ) {
                timer.Stop();
            }

            if ( timerLetter != null && timerLetter.IsEnabled ) {
                timerLetter.Stop();
            }

        }

        public void SendAnswers() {
            CallServiceToSendResults();
        }

        public void AddPlayerLateralUserControl( string username ) {
            PlayerLateralUserControl playerLateralUserControl = new PlayerLateralUserControl();
            playerLateralUserControl.usernameLabel.Text = username;
            players.Add( username, playerLateralUserControl );
            playerUserControlWrapPanel.Children.Add( playerLateralUserControl );
        }

        public void SomeoneDisconnectedFromGame( Player player ) {
            RemovePlayerLateralUserControl( player.AccessAccount.Username );
        }

        public void RemovePlayerLateralUserControl( string username ) {
            playerUserControlWrapPanel.Children.Remove( players[username] );
            players.Remove( username );

        }

        public void SomeonePressedStopButton( Player player ) {
            players[player.AccessAccount.Username].bastaButtonPressed.Visibility = Visibility.Visible;
            if ( timer != null && timer.IsEnabled ) {
                if ( time.Seconds >= GameConfiguration.TimeToStopGameOnButtonStopPressed ) {
                    timer.Stop();
                    TimerToShow( CallServiceToShowResults, UpdateCounterGridGameLabel, (int) GameConfiguration.TimeToStopGameOnButtonStopPressed );
                }
            }
        }

        public void StartAnimationLetter( GameConfiguration gameConfiguration ) {
            GameConfiguration = gameConfiguration;
            ShowCategoriesToLabel();
            InitializeAnswersToCategoriesDictionary();
            AssignLetterBorder.Visibility = Visibility.Visible;
            gridGame.Effect = null;
            StartAssignLetterAnimationTimer( GameConfiguration.ActualLetter );
        }

        public void StartMainScreenBorder( double pointsWon ) {
            AssignLetterBorder.Visibility = Visibility.Hidden;
            mainScreenBorder.Visibility = Visibility.Visible;
            actualRoundLabel.Text = GameConfiguration.ActualRound.ToString();
            scoreWonLabel.Text = pointsWon.ToString();
            counterLabel.Text = GameConfiguration.TimeToStart.ToString();
            TimerToShow( CallServiceToShowGridGame, UpdateCounterLabel, (int) GameConfiguration.TimeToStart );
        }

        public void StartGameInGridGame() {
            mainScreenBorder.Visibility = Visibility.Hidden;
            gridGame.Visibility = Visibility.Visible;
            roundsActualLabel.Text = GameConfiguration.ActualRound.ToString();
            roundsLimitLabel.Text = GameConfiguration.RoundsLimit.ToString();
            ClearButtonPressedUserControl();
            letterAssignedToRoundLabel.Text = GameConfiguration.ActualLetter.ToString();
            categoriesCopy = new Queue<GameCategory>( GameConfiguration.GameCategories );
            ChangeCategoryInGridGame();
            TimerToShow( CallServiceToShowResults, UpdateCounterGridGameLabel, (int) GameConfiguration.TimeToEndRound );
        }

        public void StartShowResults( Dictionary<string, Dictionary<GameCategory, Word>> results, Player winner ) {
            resultsBorder.Visibility = Visibility.Visible;
            ShowResultsFromOtherPlayers( results );
            this.winner = winner;
            TimerToShow( ShowWinnerToLabels, null, (int) GameConfiguration.TimeToStart );
        }

        private void ShowCategoriesToLabel() {
            categoriesStackPanel.Children.Clear();
            categoriesCopy = new Queue<GameCategory>( GameConfiguration.GameCategories );
            while ( categoriesCopy.Count != 0 ) {
                CategoryLabelUserControl categoryUserControl = new CategoryLabelUserControl();
                categoryUserControl.categoryLabel.Text = GetCategoryString( categoriesCopy.Dequeue() );
                categoriesStackPanel.Children.Add( categoryUserControl );
            }
        }

        private void ShowResultsFromOtherPlayers( Dictionary<string, Dictionary<GameCategory, Word>> results ) {
            PlayerResultWrapPanel.Children.Clear();
            foreach ( var player in results ) {
                categoriesCopy = new Queue<GameCategory>( GameConfiguration.GameCategories );
                PlayerResults playerResults = new PlayerResults();
                playerResults.usernameLabel.Text = player.Key;
                foreach ( var answerScored in player.Value ) {
                    if ( categoriesCopy.Count != 0 ) {
                        actualCategory = categoriesCopy.Dequeue();
                        TextBoxQualifiedUserControl textBoxScored = new TextBoxQualifiedUserControl();
                        textBoxScored.enteredWordTextBox.Text = answerScored.Value.word;
                        textBoxScored.pointsLabel.Text = ( (int) answerScored.Value.score ).ToString();
                        playerResults.TextBoxScoredStackPanel.Children.Add( textBoxScored );
                    }
                }
                PlayerResultWrapPanel.Children.Add( playerResults );
            }

        }

        private void ChangeCategoryInGridGame() {
            if ( categoriesCopy.Count != 0 ) {
                actualCategory = categoriesCopy.Dequeue();
                inputTextBox.Clear();
                categoryLabel.Text = GetCategoryString( actualCategory.Value );
            } else {
                actualCategory = null;
                systemLabel.Text = Properties.Resource.Game_Label_Saveanswers;
            }

        }

        private void NextCategoryButtonPressed( object sender, RoutedEventArgs e ) {
            if ( actualCategory != null ) {
                answersToCategories[actualCategory.Value].word = inputTextBox.Text.Trim();
                ChangeCategoryInGridGame();
            }
        }


        private void TimerToShow( Action functionOnStop, Action functionOnDecrease, int timeToEnd ) {
            if ( timer != null && timer.IsEnabled ) {
                timer.Stop();
            }
            time = TimeSpan.FromSeconds( timeToEnd );
            timer = new DispatcherTimer( new TimeSpan( 0, 0, 1 ), DispatcherPriority.Normal, delegate {
                if ( time == TimeSpan.Zero ) {
                    timer.Stop();
                    // Operator (?ternary) 
                    functionOnStop?.Invoke();
                } else {
                    time = time.Add( TimeSpan.FromSeconds( -1 ) );
                    // Operator (?ternary)
                    functionOnDecrease?.Invoke();
                }

            }, Application.Current.Dispatcher );
            timer.Start();
        }

        private void HideBorderAndGrids() {
            gridGame.Visibility = Visibility.Hidden;
            mainScreenBorder.Visibility = Visibility.Hidden;
            resultsBorder.Visibility = Visibility.Hidden;
            winnerBorder.Visibility = Visibility.Hidden;
            AssignLetterBorder.Visibility = Visibility.Hidden;
            gridGame.IsEnabled = true;
            stopButton.IsEnabled = true;
            systemLabel.Text = "";
            PlayerResultWrapPanel.Children.Clear();
        }

        private void UpdateCounterGridGameLabel() {
            timeRemainingLabel.Text = time.Seconds.ToString();
        }

        private void UpdateCounterLabel() {
            counterLabel.Text = time.Seconds.ToString();
        }

        private string GetCategoryString( GameCategory gameCategory ) {
            string category = "";
            switch ( gameCategory ) {
                case GameCategory.CITY:
                    category = Properties.Resource.Game_Category_City;
                    break;
                case GameCategory.ANIMAL:
                    category = Properties.Resource.Game_Category_Animal;
                    break;
                case GameCategory.COLOR:
                    category = Properties.Resource.Game_Category_Color;
                    break;
                case GameCategory.COUNTRY:
                    category = Properties.Resource.Game_Category_Country;
                    break;
                case GameCategory.FRUIT_VEGETABLE:
                    category = Properties.Resource.Game_Category_Fruitvegetable;
                    break;
                case GameCategory.LAST_NAME:
                    category = Properties.Resource.Game_Category_Lastname;
                    break;
                case GameCategory.NAME:
                    category = Properties.Resource.Game_Category_Name;
                    break;
                case GameCategory.OBJECT:
                    category = Properties.Resource.Game_Category_Object;
                    break;
            }
            return category;
        }

        private void InitializeAnswersToCategoriesDictionary() {
            Queue<GameCategory> categoriesCopy = new Queue<GameCategory>( GameConfiguration.GameCategories );
            answersToCategories = new Dictionary<GameCategory, Word>();
            while ( categoriesCopy.Count != 0 ) {
                Word word = new Word();
                answersToCategories.Add( categoriesCopy.Dequeue(), word );
            }
        }

        private char StartAssignLetterAnimationTimer( char letterSelected ) {
            char letter = 'A';
            char[] lettersArray = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'W', 'X', 'Y', 'Z' };
            int counter = 0;
            timerLetter = new DispatcherTimer();
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
                    TimerToShow( CallServiceToShowMainScreenBorder, null, (int) GameConfiguration.TimeToStart );
                }
            };
            timerLetter.Start();
            return letter;
        }

        private void BastaButtonClicked( object sender, RoutedEventArgs e ) {
            Player player = Autentication.GetInstance().Player;
            Autentication.GetInstance().GameServer.StopButtonPressed( Room, player );
            stopButton.IsEnabled = false;
            gridGame.IsEnabled = false;
        }

        private void CloseButtonPressed( object sender, RoutedEventArgs e ) {
            Close();
        }

        private void GameWindowClosed( object sender, EventArgs e ) {
            StopTimer();
            CallServiceToDisconnect();
            Autentication.GetInstance().RoomServiceCallBack.LobbyWindow.Show();
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

        private void CallServiceToShowMainScreenBorder() {
            try {
                Autentication.GetInstance().GameServer.ShowMainScreenBorder( Room, Autentication.GetInstance().Player );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    Close();
                }
            }
        }

        private void ClearButtonPressedUserControl() {
            foreach ( var player in players ) {
                player.Value.bastaButtonPressed.Visibility = Visibility.Hidden;
            }
        }

        private void CallServiceToDisconnect() {
            try {
                Autentication.GetInstance().GameServer.RemoveChannel( Room, Autentication.GetInstance().Player );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    Close();
                }
            }
        }

        private void CallServiceToShowGridGame() {
            try {
                Autentication.GetInstance().GameServer.ShowGridGameFromRoom( Room, Autentication.GetInstance().Player );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    Close();
                }
            }
        }

        private void CallServiceToShowResults() {
            try {
                Autentication.GetInstance().GameServer.FinishedTimeGame( Room, Autentication.GetInstance().Player );
                //Autentication.GetInstance().GameServer.SendResults( Room, Autentication.GetInstance().Player, answersToCategories );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    Close();
                }
            }
        }

        private void CallServiceToSendResults() {
            try {
                Autentication.GetInstance().GameServer.SendResults( Room, Autentication.GetInstance().Player, answersToCategories );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    Close();
                }
            }
        }

        private void CallServiceToStartNewRound() {
            try {
                HideBorderAndGrids();
                Autentication.GetInstance().GameServer.StartNewRound( Room, Autentication.GetInstance().Player );
            } catch ( Exception ex ) {
                if ( ex is EndpointNotFoundException || ex is CommunicationException ) {
                    DropConnectionAlert.ShowDropConnectionAlert();
                    Close();
                }
            }
        }

        private void ShowWinnerToLabels() {
            resultsBorder.Visibility = Visibility.Hidden;
            winnerBorder.Visibility = Visibility = Visibility.Visible;
            addGridGameBlurEffect();

            if ( winner != null ) {
                winnerLabel.Text = winner.AccessAccount.Username;
                winnerSubLabel.Text = Properties.Resource.Game_Label_Winnerroundtext;
            } else {
                winnerLabel.Text = Properties.Resource.Game_Label_Nowinner;
                winnerSubLabel.Text = "";
            }
            TimerToShow( CallServiceToStartNewRound, null, (int) GameConfiguration.TimeToStart );
        }

        private void addGridGameBlurEffect() {
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 0;
            gridGame.Effect = blurEffect;
            Animation();
        }

        private void Animation() {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.To = 35;
            doubleAnimation.Duration = TimeSpan.FromSeconds( 3 );
            Storyboard StoryBoard = new Storyboard();
            Storyboard.SetTarget( doubleAnimation, gridGame );
            Storyboard.SetTargetProperty( doubleAnimation, new PropertyPath( "Effect.Radius" ) );
            StoryBoard.Children.Add( doubleAnimation );
            StoryBoard.Begin();
        }

        private void KeyDownPressed( object sender, System.Windows.Input.KeyEventArgs e ) {
            if ( e.Key == System.Windows.Input.Key.Enter ) {
                if ( actualCategory != null ) {
                    answersToCategories[actualCategory.Value].word = inputTextBox.Text.Trim();
                    ChangeCategoryInGridGame();
                }
            }
        }
    }
}
