using hotel_app.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotel_app.ViewModels
{
    public class FoodViewModel
    {
        public int Id { get; set; }
        public int HotelID { get; set; }
        public string? Hotel { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Price Per Person")]
        public decimal PricePerPerson { get; set; }
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Created Date")]
        public string? Category { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Is Deleted")]
        public bool isDeleted { get; set; }
        public List<FoodCategory>? Categories { get; set; }
     
    }
}
