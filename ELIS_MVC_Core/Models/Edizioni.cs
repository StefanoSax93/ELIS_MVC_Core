﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ELIS_MVC_Core.Models
{
    public partial class Edizioni
    {
        public Edizioni()
        {
            Partecipazionis = new HashSet<Partecipazioni>();
        }

        public int Idedizione { get; set; }
        public int Idcorso { get; set; }
        public DateTime? DataInizio { get; set; }
        public string Luogo { get; set; }

        public virtual Corsi IdcorsoNavigation { get; set; }
        public virtual ICollection<Partecipazioni> Partecipazionis { get; set; }
    }
}