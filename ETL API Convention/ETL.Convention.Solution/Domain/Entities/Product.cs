using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Constants;
namespace Domain.Entities
{
    /// <summary>
    /// product entity
    /// </summary>
    public class Product : BaseModel
    {
        /// <summary>
        /// Primary Key of the product table 
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
        [StringLength(60)]
        [DataType(DataType.Text)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        /// <summary>
        /// Price of product
        /// </summary>
        [Required(ErrorMessage =MessageConstants.RequiredFieldError)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Foreign key. Primary key of the table Provinces.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Navigation property.
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category Categorys { get; set; }
    }
}