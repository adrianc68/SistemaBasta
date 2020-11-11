using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain {
    public class Room {
        private string code;
        private RoomConfiguration roomConfiguration;
        private Game game;

        public string Code { get => code; set => code = value; }
        public RoomConfiguration RoomConfiguration { get => roomConfiguration; set => roomConfiguration = value; }
        public Game Game { get => game; set => game = value; }

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
