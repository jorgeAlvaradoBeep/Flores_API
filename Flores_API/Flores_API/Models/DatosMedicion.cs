using System;
namespace Flores_API.Models
{
	public partial class DatosMedicion
	{

		public int IDDato { get; set; }
		public string NombreDato { get; set; }
		public string Parametros { get; set; } = null!;
        public string Observaciones { get; set; }

        public virtual Mediciones? MedicionNavigation { get; set; } = null!;
    }
}

