using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Areas.Administrator.EntityModel
{
    public class ERecruitment
    {
        public int ID { get; set; }

        public string LanguageID { get; set; }
        [DisplayName("Tên thành phố")]
        public string City { get; set; }
        [DisplayName("Vị trí tuyển dụng")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập vị trí tuyển dụng")]
        public string Position { get; set; }
        [DisplayName("Alias")]
        [MaxLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        public string Alias { get; set; }
        [DisplayName("Số lượng")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng người cần tuyển dụng")]
        public int Quantity { get; set; }
        [DisplayName("Ngày hết hạn")]
        [Required(ErrorMessage = "Vui lòng nhập ngày hết hạn")]
        public DateTime ExpirationDate { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Vui lòng nhập mô tả chi tiết")]
        public string Content { get; set; }
        public bool Status { get; set; }
        public int Index { get; set; }

    }
}