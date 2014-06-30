using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _30Fans.Web.Models
{
    public class TeamPhotoCompareModel
    {
        public string TeamName { get; set; }
        public string TeamPicturePath { get; set; }
        public string NameShouldBe { get; set; }
    }
}