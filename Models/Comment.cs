using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Comment
    {
        public Comment(string author, string content)
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Author = author;
            Content = content;
        }
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter no mínimo 3 caracteres")]
        [MaxLength(30, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3, ErrorMessage = "Este campo deve conter no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}