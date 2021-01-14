using Domain.Domain;
using System.Collections.Generic;

namespace Basta.Service {
    public class PlayerInGameObjects {
        public Dictionary<GameCategory, Word> Answers { get; set; }
        public double PointsWon { get; set; }
    }
}
