//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace belife.DALC
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contrato_Vivienda
    {
        public string Numero { get; set; }
        public int CodigoPostal { get; set; }
    
        public virtual Vivienda Vivienda { get; set; }
    }
}
