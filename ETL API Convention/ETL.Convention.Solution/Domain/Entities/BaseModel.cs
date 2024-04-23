using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities
{
    public class BaseModel
    {
        /// <summary>
        /// Reference of the facility where the row is created.
        /// </summary>
        [Display(Name = "Created in")]
        public int? CreatedIn { get; set; }

        /// <summary>
        /// Creation date of the row.
        /// </summary>
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date created")]
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Reference of the user who has created the row.
        /// </summary>
        [Display(Name = "Created by")]
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// Reference of the facility where the row is modified.
        /// </summary>
        [Display(Name = "Modified in")]
        public int? ModifiedIn { get; set; }

        /// <summary>
        /// Last modification date of the row.
        /// </summary>
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Date modified")]
        public DateTime? DateModified { get; set; }

        /// <summary>
        /// Reference of the user who has last modified the row.
        /// </summary>
        [Display(Name = "Modified by")]
        public Guid? ModifiedBy { get; set; }

        /// <summary>
        /// Status of the row. It indicates whether the row is deleted or not.
        /// </summary>
        [Display(Name = "Is row deleted?")]
        public bool? IsDeleted { get; set; }
    }
}