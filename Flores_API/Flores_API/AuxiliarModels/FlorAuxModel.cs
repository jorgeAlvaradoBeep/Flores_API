using System;
namespace Flores_API.AuxiliarModels
{
	public class FlorAuxModel
	{
		public FlorAuxModel()
		{
		}
        public int IdFlor { get; set; }
        public int IdTipoFlor { get; set; }
        public string Nombre { get; set; }
        public string FechaInsercion { get; set; }
    }
}

