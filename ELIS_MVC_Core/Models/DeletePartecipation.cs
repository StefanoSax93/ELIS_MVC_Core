namespace ELIS_MVC_Core.Models
{
    public class DeletePartecipation
    {
        public int? Id { get; set; }
        public int IdEdizione { get; set; }
        public string? NomePartecipante { get; set; }
        public string? CognomePartecipante { get; set; }
        public string? NomeCorso { get; set; }

        public string? Luogo { get; set; }
        public int? Durata { get; set; }
    }
}
