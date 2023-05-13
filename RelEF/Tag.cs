using System;
using Microsoft.EntityFrameworkCore;

namespace RelEF
{
    [Index(nameof(Name), IsUnique = true)]

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}