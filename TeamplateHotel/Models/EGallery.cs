using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamplateHotel.Models
{
    public class EGallery
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string SmallImg { get; set; }
        public string LargeImg { get; set; }
    }
}