using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flores_API.Models
{
	public partial class Mediciones
	{


		public int IdMedicion { get; set; }
		public int IdFlor { get; set; }
		public int IdDatoMedicion { get; set; }
		public string Valor { get; set; }
		public string Comentario { get; set; }
		public string Fecha { get; set; }

        public virtual Flores? IdFlorNavigation { get; set; }
        public virtual DatosMedicion? IdDatoMedicionNavigation { get; set; }
    }
}

