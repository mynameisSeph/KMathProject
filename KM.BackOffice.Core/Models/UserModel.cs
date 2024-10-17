using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KM.BackOffice.Core.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณากรอกบัญชื่อผู้ใช้งาน")]
        public string Username { get; set; }

        [Required(ErrorMessage = "กรุณากรอกรหัสผ่าน")]
        [MinLength(8, ErrorMessage = "รหัสผ่านต้องไม่น้อยกว่า 8 ตัวอักษร")]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
