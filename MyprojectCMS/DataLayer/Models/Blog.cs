using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataLayer.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        [Display(Name = "گروه بندی وبلاگ")]
        [Required(ErrorMessage = " {0} وارد کنید")]
        public int GroupId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = " {0} وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = " {0} وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Display(Name = "متن اصلی")]
        [Required(ErrorMessage = " {0} وارد کنید")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string BlogText { get; set; }
        [Display(Name = "برچسب ها")]
        public string Tag { get; set; }
        [Display(Name = "بازدید")]
        public int Visite { get; set; }
        [Display(Name = "نام تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [DisplayFormat(DataFormatString = "{0:yyyy/mm/dd}")]
        public DateTime CreateDate { get; set; }
        public virtual BlogGroup BlogGroup { get; set; }
        public virtual ICollection<BlogComment> BlogComments { get; set; }
    }
}
