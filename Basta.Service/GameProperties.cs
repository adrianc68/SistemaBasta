using Database.DAO;
using Domain.Domain;
using System;
using System.Collections.Generic;

namespace Basta.Service {
    public class GameProperties {
        private HashSet<WordHelper> words;

        public GameConfiguration GameConfiguration { get; set; }
        public Dictionary<Player, PlayerInGameObjects> Players { get; set; }
        public bool AllAnswersReceived { get; set; }
        public Player PlayerStarted { get; set; }

        public void ScoreWordsFromAllPlayers() {
            words = new HashSet<WordHelper>();
            foreach ( var player in Players ) {
                foreach ( var word in player.Value.Answers ) {
                    if ( IsValidLetter( word.Value ) ) {
                        WordHelper wordHelper = new WordHelper();
                        wordHelper.GameCategory = word.Key;
                        wordHelper.word = word.Value.word;
                        if ( !words.Contains( wordHelper ) ) {
                            if ( CheckInDictionary( word.Value.word, word.Key ) ) {
                                words.Add( wordHelper );
                                Players[player.Key].Answers[word.Key].score = 150;
                            } else {
                                Players[player.Key].Answers[word.Key].score = 0;
                            }
                        } else {
                            AssignEqualScoreToOtherPlayer( word.Key );
                            Players[player.Key].Answers[word.Key].score = 100;
                        }

                    } else {
                        Players[player.Key].Answers[word.Key].score = 0;
                    }
                }
            }
            SaveScoreToPlayersInDatabase();
        }

        public Player GetPlayerWithMoreScore() {
            Player playerWithHighScore = null;
            int score = 0;
            foreach ( var player in Players ) {
                int scoreAuxiliar = 0;
                foreach ( var word in player.Value.Answers ) {
                    scoreAuxiliar += (int) word.Value.score;
                }
                if ( scoreAuxiliar > score ) {
                    playerWithHighScore = player.Key;
                } else if ( scoreAuxiliar == score ) {
                    return null;
                }


            }
            return playerWithHighScore;
        }

        private bool IsValidLetter( Word word ) {
            bool isValid = false;
            if ( word != null && word.word != null && word.word.Length > 0 && word.word.ToUpper()[0] == GameConfiguration.ActualLetter ) {
                isValid = true;
            }
            return isValid;
        }

        private void SaveScoreToPlayersInDatabase() {
            PlayerDAO playerDAO = new PlayerDAO();
            foreach ( var player in Players ) {
                double score = 0;
                foreach ( var word in player.Value.Answers ) {
                    score += (int) word.Value.score;
                }
                player.Value.PointsWon = score;
                playerDAO.SetScoreToPlayer( player.Key.Email, score );
            }
        }

        private bool CheckInDictionary( string word, GameCategory gameCategory ) {
            bool isWordFoundInDictionary = false;
            switch ( gameCategory ) {
                case GameCategory.CITY:
                    isWordFoundInDictionary = WordValidator.CityValidator( word );
                    break;
                case GameCategory.ANIMAL:
                    isWordFoundInDictionary = WordValidator.AnimalValidator( word );
                    break;
                case GameCategory.COLOR:
                    isWordFoundInDictionary = WordValidator.ColorValidator( word );
                    break;
                case GameCategory.COUNTRY:
                    isWordFoundInDictionary = WordValidator.CountryValidator( word );
                    break;
                case GameCategory.FRUIT_VEGETABLE:
                    isWordFoundInDictionary = WordValidator.FruitsVegetablesValidator( word );
                    break;
                case GameCategory.LAST_NAME:
                    isWordFoundInDictionary = WordValidator.LastNameValidator( word );
                    break;
                case GameCategory.NAME:
                    isWordFoundInDictionary = WordValidator.NameValidator( word );
                    break;
                case GameCategory.OBJECT:
                    isWordFoundInDictionary = WordValidator.ObjectValidator( word );
                    break;
            }
            return isWordFoundInDictionary;
        }

        private void AssignEqualScoreToOtherPlayer( GameCategory category ) {
            foreach ( var player in Players ) {
                foreach ( var wordInList in player.Value.Answers ) {
                    WordHelper wordHelper = new WordHelper();
                    wordHelper.word = wordInList.Value.word;
                    wordHelper.GameCategory = wordInList.Key;
                    if ( wordInList.Key.Equals( category ) && words.Contains( wordHelper ) ) {
                        Players[player.Key].Answers[wordInList.Key].score = 100;
                        break;
                    }
                }
            }
        }

    }
}
