using System.ComponentModel.DataAnnotations;
using Utilities.Constants;

namespace Domain.Entities
{
    /// <summary>
    /// Category entity
    /// </summary>
    public class Category : BaseModel
    {
        /// <summary>
        /// Primary Key of the table Province
        /// </summary>
        [Key]
        public int CategoryId { get; set; }

        /// <summary>
        /// Name of the category
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)] 
        [StringLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Avilable of the category
        /// </summary>
        public bool Available { get; set; }
        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
