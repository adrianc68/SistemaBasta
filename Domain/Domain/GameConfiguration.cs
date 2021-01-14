using System.Collections.Generic;

namespace Domain.Domain {
    public class GameConfiguration {
        public double TimeToEndRound { get; set; }
        public double TimeToStart { get; set; }
        public double RoundsLimit { get; set; }
        public double TimeToShowResults { get; set; }
        public double TimeToStopGameOnButtonStopPressed { get; set; }
        public int ActualRound { get; set; }
        public char ActualLetter { get; set; }

        public Queue<GameCategory> GameCategories { get; set; }
    }
}
