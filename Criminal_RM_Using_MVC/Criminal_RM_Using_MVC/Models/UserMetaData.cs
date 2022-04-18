using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Criminal_RM_Using_MVC.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User { }
    public class UserMetaData
    {
        public int User_ID { get; set; }
        [DisplayName("User Name")]
        [Remote("IsUserNameValid", "Users", HttpMethod = "POST", ErrorMessage = "User Name Exits"), Required]
        public string User_Name { get; set; }

        [DisplayName("Password"), Required, StringLength(15, MinimumLength = 8, ErrorMessage = "should be from 8 to 15 chars")]
        public string User_Password { get; set; }

        [DisplayName("Confirm Password"),StringLength(15, MinimumLength = 8, ErrorMessage = "should be from 8 to 15 chars")]
        [Required(ErrorMessage ="Confirm your Password")]
        [System.ComponentModel.DataAnnotations.Compare("User_Password",ErrorMessage ="Password Mismatch")]
        public string Confirm_Password { get; set; }
        public Nullable<int> Position_id { get; set; }
    }
}