using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Criminal_RM_Using_MVC.Models
{
    public class UserModel
    {
        [DisplayName("User Name"), Required, StringLength(15, MinimumLength = 5, ErrorMessage = "Length should be 5 to 15 chars")]
        public string User_Name { get; set; }
        [DisplayName("Password"), Required, DataType(DataType.Password), StringLength(15, MinimumLength = 8, ErrorMessage = "Length should be between 8 to 15")]
        public string User_Password { get; set; }
    }
}