using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain {
    public class RoomConfiguration {
        private RoomState roomState;
        private int playerslimit;

        public RoomState RoomState { get => roomState; set => roomState = value; }
        public int Playerslimit { get => playerslimit; set => playerslimit = value; }

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
