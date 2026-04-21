using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using System.Security.Cryptography.Xml;

namespace MyJokesWeb.Models
{
    public class Comment
    {
        public int Id { get; set; } 
        public string Content { get; set; }   
        public DateTime CreatedAt{ get; set; } 
        public int JokeId { get; set; }   
        public Jokes Joke{ get; set; }

       
    }
}
