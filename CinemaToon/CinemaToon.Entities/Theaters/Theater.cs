using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CinemaToon.Entities.Theaters
{
    public class Theater
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Full Address")]
        public string FullAddress { get; set; }

    }
}
