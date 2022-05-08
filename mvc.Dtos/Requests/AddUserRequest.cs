using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookShop.Dtos.Requests
{
    public class AddUserRequest
    {
        [Required(ErrorMessage ="Ad soyad boş geçilemez")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Kullanıcı adı boş geçilemez")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Mail boş geçilemez")]
        public string UserMail { get; set; }
  
        [Required(ErrorMessage ="Telefon numarası boş geçilemez")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(05(\d{9}))$", ErrorMessage = "Geçersiz telefon numarası")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
