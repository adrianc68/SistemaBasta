using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain {
    public class Round {
        private Player winner;
        private double scoreForWinner;

        public Player Winner { get => winner; set => winner = value; }
        public double ScoreForWinner { get => scoreForWinner; set => scoreForWinner = value; }

    }
}
