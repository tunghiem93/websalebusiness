using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Shared.Model.Product
{
    public class ProductModels
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Làm ơn chọn thể loại!")]
        public string CategoryID { get; set; }
        [Required(ErrorMessage = "Làm ơn chọn khu vực!")]
        public string LocationID { get; set; }
        public string Category { get; set; }
        public int Type { get; set; }
        public int Segment { get; set; }
        [Required(ErrorMessage = "Làm ơn nhập địa chỉ!")]
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Acreage { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public string Right { get; set; }
        public int BedRoom { get; set; }
        public int LivingRoom { get; set; }
        public int BathRoom { get; set; }
        public int Floor { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }
        public string Phone1 { get; set; }
        public string Address1 { get; set; }
        public string Phone2 { get; set; }
        public string Address2 { get; set; }
        public string ImageURL1 { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload1 { get; set; }
        public byte[] PictureByte1 { get; set; }
        public string ImageURL2 { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload2 { get; set; }
        public byte[] PictureByte2 { get; set; }
        public string ImageURL3 { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload3 { get; set; }
        public byte[] PictureByte3 { get; set; }
        public string ImageURL4 { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload4 { get; set; }
        public byte[] PictureByte4 { get; set; }
        public string ImageURL5 { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload5 { get; set; }
        public byte[] PictureByte5 { get; set; }
        public string ImageURL6 { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload6 { get; set; }
        public byte[] PictureByte6 { get; set; }
        public string ImageURL7 { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload7 { get; set; }
        public byte[] PictureByte7 { get; set; }
        public string ImageURL8 { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PictureUpload8 { get; set; }
        public byte[] PictureByte8 { get; set; }
        public List<SelectListItem> ListSegment { get; set; }
        public ProductModels()
        {
            IsActive = true;
            ListSegment = new List<SelectListItem>()
            {
                new SelectListItem(){ Value = Commons.ESegment.Intermediate.ToString("d"), Text = "Trung cấp", },
                new SelectListItem(){ Value = Commons.ESegment.Appellative.ToString("d"), Text = "Phổ thông", },
                new SelectListItem(){ Value = Commons.ESegment.HighUp.ToString("d"), Text = "Cao cấp", },
            };
        }
    }
}
