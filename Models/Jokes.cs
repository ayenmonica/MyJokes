
using AspNetCoreGeneratedDocument;
namespace MyJokesWeb.Models
{
    public class Jokes
    {
        public int Id { get; set; }
        public string JokesQuestion { get; set; }
        public string JokesAnswer { get; set; } 
        public string? Profile { get; set; } = null;

        public int Likes { get; set; } = 0;

        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
