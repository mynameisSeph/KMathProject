using System.ComponentModel.DataAnnotations;
namespace KmathProject.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage ="กรุณากรอกบัญชีผู้ใช้งาน")]
        public string Username { get; set; }

        [Required(ErrorMessage = "กรุณากรอกรหัสผ่าน")]
        [MinLength(8, ErrorMessage = "รหัสผ่านต้องไม่น้อยกว่า 8 ตัวอักษร")]
        public string Password { get; set; }

    }
}
