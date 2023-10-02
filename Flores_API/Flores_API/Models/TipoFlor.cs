using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Flores_API.Models
{
	public partial class TipoFlor
	{
		public TipoFlor()
		{
		}

		public int IdTipoFlor { get; set; }
		public string TipoFlorP { get; set; }
		public string NombreEspecifico { get; set; }

        public virtual Flores? IdFlorNavigation { get; set; }

    }
}

