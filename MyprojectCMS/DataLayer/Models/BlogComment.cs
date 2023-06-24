using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class BlogComment
    {
        [Key]
        public int CommentId { get; set; }
        [Display(Name = "وبلاگ")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید")]
        public int BlogId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "وب سایت")]
        public string Website { get; set; }
        [Display(Name = "نظرات")]
        [Required(ErrorMessage = "لطفا{0}را وارد کنید")]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
