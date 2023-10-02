using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flores_API.Models
{
	public partial class Flores
	{
		public int IdFlor { get; set; }
		public int IdTipoFlor { get; set;}
		public string Nombre { get; set; }
		public string FechaInsercion { get; set; }

        public virtual TipoFlor? TipoFlorNavigation { get; set; } = null!;
		public virtual Mediciones? MedicionNavigation { get; set; } = null!;
    }
}

