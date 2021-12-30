using System;

namespace TeamplateHotel.Models
{
    public class ShowObject
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string MenuAlias { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int? Index { get; set; }
        public int? Size { get; set; }
        public int MaxPeople { get; set; }
        public decimal Price { get; set; }
        public string SecondMenu { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string MenuName { get; set; }
    }
}