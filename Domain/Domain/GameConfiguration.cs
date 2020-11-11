using Domain.Domain.gameConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain {
    public class GameConfiguration {
        private List<GameCategory> categories;
        private double timer;
        private double score;
        private List<Round> rounds;

        public List<GameCategory> Categories { get => categories; set => categories = value; }
        public double Timer { get => timer; set => timer = value; }
        public double Score { get => score; set => score = value; }
        public List<Round> Rounds { get => rounds; set => rounds = value; }

        public override String ToString() {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties();
            StringBuilder result = new StringBuilder();
            foreach ( PropertyInfo propertyInfo in propertyInfoList )
                result.AppendFormat( "{0} = {1} ", propertyInfo.Name, propertyInfo.GetValue( this ) );
            return result.ToString();
        }

    }
}
