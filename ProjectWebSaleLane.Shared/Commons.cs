using System;
using System.Collections.Generic;
using System.Configuration;
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
        public enum EStatus
        {
            Actived = 1,
            Deleted = 9,
            InActived = 3,
        }
        #endregion

        #region Add category
        public const string Land = "Đất";
        public const string House = "Nhà";
        #endregion

        public const string Image100_100 = "http://placehold.it/100x100";
        public const string Image200_100 = "http://placehold.it/200x100";
        public const string Image350_200 = "http://placehold.it/350x200";
        public const string Image400_250 = "http://placehold.it/400x250";

        public static string HostImage = ConfigurationManager.AppSettings["HostImage"];
        public static string _PublicImages = string.IsNullOrEmpty(ConfigurationManager.AppSettings["PublicImages"]) ? "" : ConfigurationManager.AppSettings["PublicImages"];
    }
}
