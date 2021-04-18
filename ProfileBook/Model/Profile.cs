using ProfileBook.ViewModel;
using SQLite;
using System;

namespace ProfileBook.Model
{
    public class Profile : IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string NickName { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }

        public bool IsSelected { get; set; }
        [Ignore]
        public MainListViewModel ListViewModel { get; set; }
    }
}
