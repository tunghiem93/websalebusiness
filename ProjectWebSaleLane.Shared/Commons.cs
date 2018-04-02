using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLand.Shared
{
    public class Commons
    {        
        #region Enum
        public enum EGender
        {
            Male = 1, //Nam
            Female = 2, //Nữ
        }        
        public enum EProductType
        {
            Land = 1, //Đất
            House = 2, //Nhà
        }
        public enum EArea
        {
        }
        public enum ESegment
        {
            Intermediate = 1, //Trung cấp
            Appellative = 2, //Phổ thông
            HighUp = 3, //Cao cấp
        }
        #endregion

        public const string Image100_100 = "http://placehold.it/100x100";
        public const string Image200_100 = "http://placehold.it/200x100";
    }
}
