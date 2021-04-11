using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Model
{
    public class Profile
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
