using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain {
    public class Game {
        private GameConfiguration gameConfiguration;
        private List<Player> players;

        public GameConfiguration GameConfiguration { get => gameConfiguration; set => gameConfiguration = value; }
        public List<Player> Players { get => players; set => players = value; }

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
